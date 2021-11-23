/// <summary>
/// Author: Janine Mayer
/// Class containing all needed constants to void string cluttering within project
/// </summary>
namespace Assets.Scripts
{
    public static class StringConstants
    {
        //gameobjects
        public static string PLAYER = "Player";
        public static string MANUAL_PLAYER = "ManualPlayer";
        public static string BREAKABLE_WALL = "Breakable Wall";

        //coroutine
        public static string MOVE_WALL = "MoveWall";

        public static string[] LABYRINTH_SCENES = { 
            "Labyrinth_A", 
            "Labyrinth_B" 
        };

        public static string ANIMATION_START = "Start";
        public static string ANIMATION_ISHURT = "IsHurt";

        public static string MATERIAL_BREAKABLE = "Materials/Breakable";
        public static string MATERIAL_BREAKABLE_HIGHLIGHTED = "Materials/Breakable Highlighted";
        public static string MATERIAL_BREAKABLE_SELECTED = "Materials/Breakable Selected";
        public static string MATERIAL_BREAKABLE_SELECTED_INVALID = "Materials/Breakable Selected Invalid";
    }
}
