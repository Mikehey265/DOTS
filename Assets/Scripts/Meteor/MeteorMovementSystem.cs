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
        protected override void OnUpdate()
        {
            float deltaTime = UnityEngine.Time.deltaTime;

            // Create the job for moving and rotating meteors
            var job = new MeteorMovementJob
            {
                deltaTime = deltaTime
            };

            // Schedule the job for parallel execution
            job.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct MeteorMovementJob : IJobEntity
        {
            public float deltaTime;

            // This method defines what happens to each entity with LocalTransform and MeteorComponents
            public void Execute(ref LocalTransform transform, in MeteorComponents movement)
            {
                // Move the meteor
                transform.Position += movement.Velocity * deltaTime;

                // Rotate the meteor
                transform.Rotation = math.mul(transform.Rotation, quaternion.RotateZ(movement.RotationSpeed * deltaTime));
            }
        }
    }
}