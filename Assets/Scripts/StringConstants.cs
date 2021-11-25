/// <summary>
/// Author: Axel Bauer, Janine Mayer
/// Class containing all needed constants to void string cluttering within project
/// </summary>
namespace Assets.Scripts
{
    public static class StringConstants
    {
        //gameobjects
        public static string PLAYER = "Player";
        public static string HAND_COLLIDER = "HandCollider";
        public static string VIVE_CHARACTER = "HeadCollider";
        public static string MANUAL_PLAYER = "ManualPlayer";
        public static string BREAKABLE_WALL = "Breakable Wall";

        public static string TEXT_CURRVAL = "CurrValue";
        public static string TEXT_MAXVAL = "MaxValue";

        //coroutine
        public static string MOVE_WALL = "MoveWall";
        public static string INVINCIBILITY_MODE = "InvincibilityMode";

        public static string[] LABYRINTH_SCENES = { 
            "Labyrinth_A", 
            "Labyrinth_B" 
        };

        public static string ANIMATION_START = "Start";
        public static string ANIMATION_FADE = "FadeOut";
        public static string ANIMATION_ISHURT = "IsHurt";

        public static string MATERIAL_BREAKABLE = "Materials/Breakable";
        public static string MATERIAL_BREAKABLE_HIGHLIGHTED = "Materials/Breakable Highlighted";
        public static string MATERIAL_BREAKABLE_SELECTED = "Materials/Breakable Selected";
        public static string MATERIAL_BREAKABLE_SELECTED_INVALID = "Materials/Breakable Selected Invalid";
    }
}
