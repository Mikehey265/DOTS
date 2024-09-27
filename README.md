# DOTS

## Meteor Spawning System

The system consists of two components and systems that handle the spawning, movement, and management of meteors using Unity's ECS and Burst Compiler.

### Main Components

- **MeteorAuthoring.cs**: This script acts as a MonoBehaviour that defines meteor properties (position, velocity, rotation speed) and converts these into an ECS-friendly format via the MeteorAuthoringBaker class.
- **MeteorComponents.cs**: This defines the ECS MeteorComponents.
- **MeteorMovementSystem.cs**: This ISystem is responsible for updating meteor positions and applying rotation every frame based on their velocity and rotation speed using a Burst-compiled job for parallel execution with MeteorMovementJob's IJobEntity.
- **MeteorSpawnerAuthoring.cs**: Similar to MeteorAuthoring, this MonoBehaviour handles the configuration of the meteor spawner. It defines the prefab, spawn rate, and the height of meteors when they are spawned.
- **MeteorSpawnSystem.cs**: This system manages the spawning of meteors at random positions and scales. It uses IJobEntity to ensure high-performance meteor instantiation at regular intervals. The spawning logic is driven by a random custom number generator job to create varied positions and sizes for the meteors.

### How It Works

#### Authoring Components:
- The MeteorAuthoring and MeteorSpawnerAuthoring scripts define the initial settings for the meteors prefab and the spawner in the Unity editor.
- The Baker classes in these scripts convert the MonoBehaviour data into ECS-compatible components (MeteorComponents), which are then used by the systems.

#### Meteor Spawning:
- The MeteorSpawnSystem handles the spawning of meteors using the ProcessSpawnerJob. This job runs in parallel and checks if it’s time to spawn a meteor based on the elapsed time and the defined spawn interval.
- Each meteor is instantiated at a random position along the x-axis, with a height defined by the `SpawnYPosition`. The meteor's scale is also randomized, creating variation in size.
- The job uses a thread-safe random number generator (`Unity.Mathematics.Random`) to ensure that each thread spawns meteors with unique properties.

#### Meteor Movement:
- The MeteorMovementSystem updates all spawned meteors, moving them based on their velocity and rotating them based on the defined `RotationSpeed`. This is handled by the MeteorMovementJob, which runs in parallel for all meteor entities.

### Key Features

- **Parallel Execution**: Both the movement and spawning jobs are parallelized, leveraging Burst compilation for high performance.
- **Randomized Meteor Properties**: Each meteor's spawn position, size, and velocity are randomized to create a varied and dynamic meteor field.
- **ECS-Based Architecture**: All meteor-related logic is handled through Unity's ECS, ensuring scalability and performance, even with a large number of meteors in the scene.

### Meteor Components Breakdown

- **Position**: The initial position where the meteor spawns.
- **Velocity**: Determines how fast and in which direction the meteor moves.
- **RotationSpeed**: The speed at which the meteor rotates.
- **SpawnYPosition**: The height at which meteors are spawned.
- **SpawnRate**: Defines how often a meteor is spawned.
- **MeteorPrefab**: The prefab used for spawning new meteor entities.
