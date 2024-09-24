using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Meteor
{
    [BurstCompile]
    public partial class MeteorMovementSystem : SystemBase
    {
        
        [BurstCompile]
        protected override void OnCreate()
        {
            
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            float deltaTime = UnityEngine.Time.deltaTime;

            // Create the job for moving and rotating meteors
            var job = new MeteorMovementJob
            {
                deltaTimeJob = deltaTime
            };

            // Schedule the job for parallel execution
            job.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct MeteorMovementJob : IJobEntity
        {
            public float deltaTimeJob;

            // This method defines what happens to each entity with LocalTransform and MeteorComponents
            public void Execute(ref LocalTransform transform, in MeteorComponents movement)
            {
                // Move the meteor
                transform.Position += movement.Velocity * deltaTimeJob;

                // Rotate the meteor
                transform.Rotation = math.mul(transform.Rotation, quaternion.RotateZ(movement.RotationSpeed * deltaTimeJob));
            }
        }
    }
}