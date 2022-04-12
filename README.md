# Random ( Spelunky Like ) Level Maker
A Random Level Generator for Unity.

## Install Instructions 

Assets > Import Package > Custom Package > Select the downloaded [UnityPackage](https://github.com/Devanshu19/SpelunkyLevelMaker/releases/latest/download/LevelMaker.unitypackage)
 
## How does it works?

This level maker works recursively. Each Level Piece placed is responsible for placing the next Level Piece. This happens untill no more valid Piece placements are available. 

* Place the first level piece at any random tile on the top row. 
* From the available empty tiles, select a random tile and place next Level Piece there.
* Update the former Level Piece according to the later one.

**Components** : 
* Da Grid : Grid is responsible for keeping track of whether a tile is empty or not. New Level Pieces are only placed on empty tiles. 
* Level Generator : Level Generator is responsible for spawning the first level piece, spawning the hurdles and populating the other Level Pieces that are not the continuous path.
* Level Piece : Level Piece is a gameObject that can be spawned by the LevelGenerator. It is responsible for spawning the next Level Piece. It is spanwed as empty Level Piece (with no contents) at first and then changes its content according to the next Level Piece. 

![Grid](https://user-images.githubusercontent.com/66104268/163002480-d13bb7b2-f483-4629-9903-9241ed00d54f.gif)

_took me hours to make this gif ;-;_
 
## Usefull Resources 

Game Maker's Toolkit : [Spelunky Level Making Magic](https://www.youtube.com/watch?v=Uqk5Zf0tw3o)
