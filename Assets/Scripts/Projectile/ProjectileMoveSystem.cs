using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct ProjectileMoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, ProjectileMoveSpeed>())
        {
            transform.ValueRW.Position += transform.ValueRO.Up() * moveSpeed.Value * deltaTime;
        }
    }
}
