# Virtual Reality Escape Labyrinth
Esclab implements the idea of an Escape Labyrinth in Virtual Reality using a `HTC Vive Pro` and the `HTC Vive Controllers`. 

The player needs to navigate through an unknown labyrinth and collect coins. An enemy dragon also moves through the halls and will attack the player if in reach. There are health and protection items to be collected to help the player. 

<div align="center">
    <image src="https://github.com/jdmayer/VR_Esclab/blob/main/PlayThrough%20Gifs/01%20-%20Start.gif"/>
</div>

A whole play through of this small VR game can be watched <a href="https://www.youtube.com/watch?v=SSBLjp_vM28">here on youtube</a>. <br/>
The documentation about the wall interaction, stats, and portal is available <a href="https://github.com/jdmayer/VR_Esclab/blob/main/Documentation/Esclab_Mayer.pdf">here on github</a>.

🤝 **This project was planned, designed, and implemented with <a href="https://github.com/Xovval">@Xovval</a>.**

## Labyrinth
The player can move `walls` to navigate through the labyrinth. This interaction can be used to find new ways or block ways to avoid the enemy. A wall can be grabbed, moved by synchronising their position with the movement of the controller, and released at an open area. If they are released in an occupied or invalid area they will automatically jump back to their original position. To move them into other paths, they can also be rotated.

<div align="center">
    <image src="https://github.com/jdmayer/VR_Esclab/blob/main/PlayThrough%20Gifs/02%20-%20Portal%20and%20Stats%20trimmed.gif"/>
</div>

`Portals` are used to connect different parts of the labyrinth. They use a fade in and fade out to smoothen the transition between the different virtual environments. This method was used to allow the game to reuse the small and possibly restricted real world environment of the user.

## Coins and Items
While moving through the paths of the labyrinth, the player needs to find all obviously placed but also hidden `coins`. In total there are ten coins which need to be collected to win this game. The collected coins can be checked in the stats which are aligned with the user's left controller. The stats are only shown when the user presses the necessary button so the environment looks as real as possible as no flying menus or stats are shown.

<div align="center">
    <image src="https://github.com/jdmayer/VR_Esclab/blob/main/PlayThrough%20Gifs/03%20-%20Coin%20and%20Wall.gif"/>
</div>

Apart from coins, the user can also find `health items` and `invincibility items`. Health items are represented by blue crosses and refill a part of the health bar shown in the stats. They are placed all over the labyrinth but do not reappear, so they need to be used carefully. The invincibility items are visualised by a collection of sparkles. Their usage protects the user from attacks of the enemy for multiple seconds. They also do not reappear.

<div align="center">
    <image src="https://github.com/jdmayer/VR_Esclab/blob/main/PlayThrough%20Gifs/04%20-%20Health%20Item.gif"/>
</div>

## Enemy
In this game <a href="https://assetstore.unity.com/packages/3d/characters/creatures/dragon-for-boss-monster-hp-79398">a small dragon</a> poses as an `enemy` which tracks down the user and attacks when in range. The enemy uses the A* algorithm to find a path through the labyrinth. When the player is too close to the dragon, the player becomes the next target until he or she managed to get out of range. To escape, the player can use a portal, run a way fast enough, use an invincibility item or close off the path to the enemy using a wall.

<div align="center">
    <image src="https://github.com/jdmayer/VR_Esclab/blob/main/PlayThrough%20Gifs/05%20-%20Dragon%20Attack.gif"/>
</div>

## Controller Handling
This VR game can be played using an `HTC Vive Pro` in combination with `HTC Vive Controllers`. The left controller can be used to interact with items (coins, health items, invincibility items) and to display the statusbar. The right controller allows to interact with movable walls.

<div align="center">
    <image src="https://github.com/jdmayer/VR_Esclab/blob/main/Documentation/ControllerGuide.JPG"/>
</div>



