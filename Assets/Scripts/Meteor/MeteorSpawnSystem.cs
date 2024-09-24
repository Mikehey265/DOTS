using System.Linq;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Meteor
{
    public partial struct MeteorSpawnSystem : ISystem
    {
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Define interval for spawning meteors
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
            }
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}