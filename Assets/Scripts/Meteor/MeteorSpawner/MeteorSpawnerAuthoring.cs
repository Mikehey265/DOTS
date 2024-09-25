using Unity.Entities;
using UnityEngine;

namespace Meteor.MeteorSpawner
{
    public class MeteorSpawnerAuthoring : MonoBehaviour
    {
        public GameObject MeteorPrefab;
        public float SpawnRate;
        public float SpawnHeight;

        private class MeteorSpawnerAuthoringBaker : Baker<MeteorSpawnerAuthoring>
        {
            public override void Bake(MeteorSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new MeteorComponents
                {
                    MeteorPrefab = GetEntity(authoring.MeteorPrefab, TransformUsageFlags.Dynamic),
                    SpawnYPosition = authoring.SpawnHeight,
                    SpawnRate = authoring.SpawnRate,
                    NextSpawnTime = 0.0f
                });
                
                // Ensure the prefab is not null
                if (authoring.MeteorPrefab == null)
                {
                    Debug.LogError("Meteor prefab on authoring is null");
                }
            }
        }
    }
}