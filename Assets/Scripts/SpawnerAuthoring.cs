using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
}

public class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Spawner
        {
            Prefab = GetEntity(TransformUsageFlags.Dynamic),
            SpawnPosition = float2.zero,
            NextSpawnTime = 0,
            SpawnRate = authoring.SpawnRate
        });
    }
}
