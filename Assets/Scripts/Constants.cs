/// <summary>
/// Author: Janine Mayer
/// Class containing all needed constants to void string cluttering within project
/// </summary>
namespace Assets.Scripts
{
    public static class Constants
    {
        //gameobjects
        public static string player = "Player";
        public static string manualPlayer = "ManualPlayer";
        public static string breakableWall = "Breakable Wall";

        //coroutine
        public static string moveWall = "MoveWall";

        public static string[] labyrinthScenes = { 
            "Labyrinth_A", 
            "Labyrinth_B" 
        };

        public static string animationStart = "Start";
        public static string animationHurt = "IsHurt";

        public static string materialBreakable = "Materials/Breakable";
        public static string materialBreakableHighlighted = "Materials/Breakable Highlighted";
        public static string materialBreakableSelected = "Materials/Breakable Selected";
    }
}
