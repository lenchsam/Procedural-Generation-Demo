# Strategy Game Framework

Hi! This project shows **Procedural Generation** and other features needed in a strategy game such as turn based crafting in **C#** using **Unity**. This readme file will explain all of the features implemented.

## Table of Contents
- [Strategy Game Framework](#strategy-game-framework)
  * [Useful Links](#useful-links)
  * [Features](#features)
  * [Getting Started](#getting-started)
    + [Requirements](#requirements)
    + [Setup](#setup)
    + [Changing Variables](#changing-variables)
    + [Common Issues](#common-issues)
  * [Future Features](#potential-future-features)
- [Contact](#contact)

![Unity_nHXMKcJvtb](https://github.com/user-attachments/assets/e1623fff-66db-4fcf-adc5-e17265633e5c)

## Useful Links
 - [Procedural Generation Class](https://github.com/lenchsam/Procedural-Generation-Demo/blob/main/Assets/Scripts/HexGrid/ProceduralGeneration.cs)
 - [Grid Functions Class](https://github.com/lenchsam/Procedural-Generation-Demo/blob/main/Assets/Scripts/HexGrid/HexGrid.cs)
 - [State Machine](https://github.com/lenchsam/Procedural-Generation-Demo/blob/main/Assets/Scripts/Player/PlayerController.cs)
 - [States Folder](https://github.com/lenchsam/Procedural-Generation-Demo/tree/main/Assets/Scripts/Player/StateMachine)

## Features

 - Poisson disc sampling used to get the spawn locations of players
 - Voronoi noise used for biomes
 - Perlin noise to create height layers
 - Fog of War
 - Turn-based combat
 - A* pathfinding
 - Different map sizes
 - Turn based Unit Crafting

## Getting Started
Note that I used paid assets for the models and other paid plugins while creating this project, these are not found in this github repository. 
### Requirements

 - Unity version 6000.0.23f1
 - Visual Studio Code (or your preferred)

### Setup
 1. Clone the repository. 
 2. Open the project using Unity Hub.
 3. Navigate to the `Assets -> Scenes` folder and open the **MainMenu** scene.
 4. Experiment and explore the project!

### Changing Variables
The variables that you'll probably want to have fun with are located in the ----GridManager---- game object. This manages all of the [procedural generation](https://github.com/lenchsam/Procedural-Generation-Demo/blob/main/Assets/Scripts/HexGrid/ProceduralGeneration.cs) and the [grid functions](https://github.com/lenchsam/Procedural-Generation-Demo/blob/main/Assets/Scripts/HexGrid/HexGrid.cs).

### Common Issues
 - make sure to also lower poisson-disc radius when making map size smaller in editor. If this isn't done, it may freeze the editor.

## Potential Future Features
Should this framework be taken further, here are some of the extra mechanics that may be put in.
 - Functioning Districts
 - Resource farming
 - System to buy/make units, districts and potentially more
 - UI
 - SFX
 - Reworked battle system to have more interactive battles when a unit attacks another unit
 - Map saving and loading to a file
# Contact
[Portfolio](https://lenchsam.com)

[LinkedIn](https://www.linkedin.com/in/sam-lench-8586b6279/)

[X](https://x.com/SamLenchGameDev)

Email - samlenchgamedev@gmail.com
