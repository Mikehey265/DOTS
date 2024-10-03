# DOTS

## Player Movement System

- **PlayerAuthoring.cs**: Defines the player authoring component in the Unity Editor and sets basic attributes for the player such as projectile prefab and movement speed. Baker then converts the authoring component into ECS entities.
- **PlayerComponent.cs**: This defines the ECS player components.
- **PlayerInputSystem.cs**: Manages player input through Unity's new input system. It initializes input actions and listens for movement and shooting inputs. This system ensures the player entity responds to both movement and shooting inputs from the player.
- **PlayerMovementSystem.cs**: Controls the player's movement based on input and constraints within the screen bounds. This system runs before the transform system to update the player's position each frame.
- **ResetInpuSystem.cs**: This script is responsible for resetting the FireProjectileTag at the end of each frame.

## Projectile System

- **FireProjectileSystem.cs**: This script is responsible for instantiating projectiles when the FireProjectileTag is present on a player entity.
- **ProjectileAuthoring.cs**: Responsible for converting the authoring component into an ECS component during the baking process.
- **ProjectileMoveSystem.cs**: Handles the movement of projectile entities during runtime.

The projectile firing and movement logic are handled in an efficient and modular manner using Unity’s ECS. The process begins when the player fires a projectile, triggering the FireProjectileSystem, which instantiates a projectile at the player’s location using a prefab. The speed and movement behavior of the projectile are defined by the ProjectileAuthoring component and baked into ECS components via ProjectileAuthoringBaker. Once instantiated, the projectile’s movement is handled by the ProjectileMoveSystem, which updates the projectile's position each frame based on its speed.

## Meteor Spawning System

The system consists of two components and systems that handle the spawning, movement, and management of meteors.

- **MeteorAuthoring.cs**: This defines authoring component in the Unity Editor.
- **MeteorComponents.cs**: This defines meteor properties (position, velocity, rotation speed).
- **MeteorMovementSystem.cs**: This ISystem is responsible for updating meteor positions and applying rotation every frame based on their velocity and rotation speed using a Burst-compiled job for parallel execution with MeteorMovementJob's IJobEntity.
- **MeteorSpawnerAuthoring.cs**: Similar to MeteorAuthoring, handles the configuration of the meteor spawner. It defines the prefab, spawn rate, and the height of meteors when they are spawned.
- **MeteorSpawnSystem.cs**: This manages the spawning of meteors at random positions and scales. It uses IJobEntity to ensure high-performance meteor instantiation at regular intervals. The spawning logic is driven by a random custom number generator job to create varied positions and sizes for the meteors.

### Spawning meteors, how it works?

#### Authoring Components:
- The MeteorAuthoring and MeteorSpawnerAuthoring scripts define the initial settings for the meteors prefab.
- The Baker classes convert the MonoBehaviour data into ECS components (MeteorComponents).

#### Meteor Spawning:
- The MeteorSpawnSystem handles the spawning of meteors using the ProcessSpawnerJob. This job runs in parallel and checks if it’s time to spawn a meteor based on the elapsed time and the defined spawn interval.
- Each meteor is instantiated at a random position along the x-axis, with a height defined by the `SpawnYPosition`. The meteor's scale is also randomized.
- The job uses a thread-safe random number generator (`Unity.Mathematics.Random`).

#### Meteor Movement:
- The MeteorMovementSystem updates all spawned meteors, moving them based on their velocity and rotating them based on the defined `RotationSpeed`. This is handled by the MeteorMovementJob, which runs in parallel for all meteor entities.

### Meteor Components Breakdown

- **Position**: The initial position where the meteor spawns.
- **Velocity**: Determines how fast and in which direction the meteor moves.
- **RotationSpeed**: The speed at which the meteor rotates.
- **SpawnYPosition**: The height at which meteors are spawned.
- **SpawnRate**: Defines how often a meteor is spawned.
- **MeteorPrefab**: The prefab used for spawning new meteor entities.
