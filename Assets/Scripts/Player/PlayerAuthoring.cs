using Meteor;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float MoveSpeed = 5f;
}

public class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent<PlayerTag>(entity);
        AddComponent<PlayerMoveInput>(entity);
        
        AddComponent(entity, new PlayerMoveSpeed
        {
            Value = authoring.MoveSpeed
        });
        
        AddComponent<FireProjectileTag>(entity);
        SetComponentEnabled<FireProjectileTag>(entity, false);
        
        AddComponent(entity, new ProjectilePrefab
        {
            Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
        });
    }
}
