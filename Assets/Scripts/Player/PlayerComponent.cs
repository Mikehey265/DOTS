using Unity.Entities;
using Unity.Mathematics;

public struct PlayerComponent : IComponentData
{
    public float Speed;
}

public struct PlayerMoveInput : IComponentData
{
    public float2 Value;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}

public struct PlayerTag : IComponentData { }

public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}

public struct ProjectileMoveSpeed : IComponentData
{
    public float Value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }
