using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        Camera mainCamera = Camera.main;
        float deltaTime = SystemAPI.Time.DeltaTime;

        if (mainCamera != null)
        {
            float3 minBounds = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            float3 maxBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            new PlayerMovementJob()
            {
                DeltaTime = deltaTime,
                MinBounds = minBounds,
                MaxBounds = maxBounds
            }.Schedule();
        }
    }
}

[BurstCompile]
public partial struct PlayerMovementJob : IJobEntity
{
    public float DeltaTime;
    public float3 MinBounds;
    public float3 MaxBounds;

    public void Execute(ref LocalTransform transform, in PlayerMoveInput input, PlayerMoveSpeed speed)
    {
        float2 newPosition = transform.Position.xy + input.Value * speed.Value * DeltaTime;
        newPosition = math.clamp(newPosition, MinBounds.xy, MaxBounds.xy);
        
        transform.Position.xy = newPosition;
    }
}
