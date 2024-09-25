using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Transforms;

namespace Meteor.MeteorSpawner
{
    public partial struct MeteorSpawnSystem : ISystem
    {
        private NativeArray<Unity.Mathematics.Random> RandomArray;
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            // Initialize the random number generator
            RandomArray = new NativeArray<Unity.Mathematics.Random>(JobsUtility.MaxJobThreadCount, Allocator.Persistent);
            uint seed = (uint)System.Environment.TickCount;
            for (int i = 0; i < RandomArray.Length; i++)
            {
                RandomArray[i] = new Unity.Mathematics.Random(seed == 0 ? 1 : seed);
                seed++;
            }
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);
            
            new ProcessSpawnerJob
            {
                ElapsedTime = SystemAPI.Time.ElapsedTime,
                DeltaTime = SystemAPI.Time.DeltaTime,
                SpawnInterval = 3.0f,
                Ecb = ecb,
                RandomArray = RandomArray
            }.ScheduleParallel();
            
        }
        
        private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            return ecb.AsParallelWriter();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            if (RandomArray.IsCreated) 
                RandomArray.Dispose();
        }
    }

    public partial struct ProcessSpawnerJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        public double ElapsedTime;
        public float DeltaTime;
        public float SpawnInterval;
        [NativeDisableParallelForRestriction] public NativeArray<Unity.Mathematics.Random> RandomArray;
        [NativeSetThreadIndex] private int ThreadIndex;
        

        public void Execute([ChunkIndexInQuery] int chunkIndex, ref MeteorComponents meteorComponent)
        {
            SpawnInterval = meteorComponent.SpawnRate;

            if (ElapsedTime % SpawnInterval < DeltaTime)
            {
                // get the random number generator for this thread
                Random random = RandomArray[ThreadIndex];
                
                // Generate a random position for the meteor
                float randomX = random.NextFloat(-7, 7);
                
                // Update the random number generator for this thread
                RandomArray[ThreadIndex] = random;
                
                if (meteorComponent.MeteorPrefab == Entity.Null)
                {
                    UnityEngine.Debug.LogError("Meteor prefab is null");
                    return;
                }
                
                // Spawn a new meteor
                Entity newMeteor = Ecb.Instantiate(chunkIndex, meteorComponent.MeteorPrefab);
                Ecb.SetComponent(chunkIndex, newMeteor, LocalTransform.FromPositionRotation(new float3(randomX, meteorComponent.SpawnYPosition, 0), quaternion.identity));
                
                // reset the next spawn time
                meteorComponent.NextSpawnTime = (float)ElapsedTime + meteorComponent.SpawnRate;
            }
            /*if (meteorComponent.NextSpawnTime < ElapsedTime)
            {
                
                
            }*/
        }
    }
}

/*// Old Implementation
            float spawnInterval = 1.0f;
            double time = SystemAPI.Time.ElapsedTime;
            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
            if (time % spawnInterval < SystemAPI.Time.DeltaTime)
            {
                Debug.Log("Spawning meteor");
                foreach (var (meteorPrefab, meteorComponents) in SystemAPI.Query<MeteorPrefab, MeteorComponents>().WithAll<MeteorTag>())
                {
                    var meteorTransform = LocalTransform.FromPositionRotation(new float3(UnityEngine.Random.Range(-7, 7), meteorComponents.SpawnYPosition, 0), quaternion.identity);
                    // Instantiate meteor prefab
                    Entity newMeteor = ecb.Instantiate(meteorPrefab.Value);
                    ecb.SetComponent(newMeteor, meteorTransform);
                }
                ecb.Playback(state.EntityManager);
                ecb.Dispose();
            }*/