using Unity.Entities;
using UnityEngine;

namespace Despawner
{
    public class DespawnerAuthoring : MonoBehaviour
    {
        private class DespawnerAuthoringBaker : Baker<DespawnerAuthoring>
        {
            public override void Bake(DespawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<DespawnerComponent>(entity);
            }
        }
    }
}