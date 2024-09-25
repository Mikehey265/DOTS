using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Meteor
{
    public class MeteorAuthoring : MonoBehaviour
    {
        public float3 Position;
        public float3 Velocity;
        public float RotationSpeed;
        public GameObject MeteorPrefab;
        
        private class MeteorAuthoringBaker : Baker<MeteorAuthoring>
        {
            public override void Bake(MeteorAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MeteorComponents
                {
                    Position = authoring.Position,
                    Velocity = authoring.Velocity,
                    RotationSpeed = authoring.RotationSpeed,
                    MeteorPrefab = GetEntity(authoring.MeteorPrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}