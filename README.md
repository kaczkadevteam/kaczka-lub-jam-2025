# The Art of Blow

## Core Gameplay Mechanics

### The Bubble Spawner
The Bubble Spawner is an entity placed in the centre of the arena that is responsible for spawning bubbles into the arena.

User doesn't necessarily interact with the spawner itself, but he might find loot in form of valuable upgrades upon defeating enemies.

#### Upgrades
##### Spawner
- Spawn rate
- Number of spawners

##### Bubble
- Movement speed
- Dissipation range
- Size

### The Air Blower
The blower is used to blow the bubbles and direct them towards the enemies. It is always facing the bubble spawner and user may interact with it to rotate it. 

#### Features
- Reversing the direction

#### Upgrades
- Rotation speed

### Enemies
The enemies are spawned on the edges of the arena and move slowly towards the bubble spawner. By default enemies have 1 hp and die instantly when they get in contact with a bubble.

#### Features
- Amount of enemies increases with time
- Damage scaling with time
- Defeating an enemy has a chance of dropping an upgrade
- ? The more rarity the enemy has, the less often it spawns
- ? Killing enemies gives you points
- ? Different enemies drop different upgrades


#### Viruses
#### Features
- Moves fast
- ? Might mutate spontaneously to increase its' speed, hp etc.

#### Bacteria
##### Features
- Moves super slowly
- Might replicate itself after some time

#### Dirt
- Moves slowly
- When it reaches the center it slows down the spawn rate of bubbles

### Cleanness (HP)
- Enemies reduce the amount of cleanness the player has, when it reaches 0 you die

