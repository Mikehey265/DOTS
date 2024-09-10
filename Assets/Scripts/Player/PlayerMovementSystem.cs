using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        // float moveInput = Input.GetAxis("Horizontal");

        new PlayerMovementJob()
        {
            // MoveInput = moveInput,
            DeltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct PlayerMovementJob : IJobEntity
{
    // public float MoveInput;
    public float DeltaTime;

    public void Execute(ref LocalTransform transform, in PlayerMoveInput input, PlayerMoveSpeed speed)
    {
        transform.Position.xy += input.Value * speed.Value * DeltaTime;
        // float3 position = transform.Position;
        // position.x += MoveInput * playerComponent.Speed * DeltaTime;
        // transform.Position = position;
    }
}
