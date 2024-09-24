using Unity.Entities;
using Unity.Mathematics;

namespace Meteor
{
    public struct MeteorComponents : IComponentData
    {
        public float3 Position;
        public float3 Velocity;
        public float RotationSpeed;
        public float SpawnYPosition;
    }
    
    public struct MeteorPrefab : IComponentData
    {
        public Entity Value;
        public bool HasValue => Value != Entity.Null;
    }
    
    public struct MeteorTag : IComponentData, IEnableableComponent
    { }
}