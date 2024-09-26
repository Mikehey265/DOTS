using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Meteor
{
    public struct MeteorComponents : IComponentData
    {
        public float3 Position;
        public float3 Velocity;
        public float RotationSpeed;
        public float SpawnYPosition;
        public float SpawnRate;
        public Entity MeteorPrefab;
    }
    
    public struct MeteorTag : IComponentData, IEnableableComponent
    { }
}