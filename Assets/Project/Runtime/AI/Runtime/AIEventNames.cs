namespace Project.Runtime.AI
{
    public struct AIEventNames
    {
        //Definitions for different AI event types
        
        // Spawning
        public static string SPAWN_CREATURE = "SpawnCreature";
        public static string DESPAWN_CREATURE = "DespawnCreature";
        public static string TELEPORT_CREATURE = "TeleportCreature";
        
        // Aggression
        public static string AGGRESSION_INCREASE = "IncreaseAggression";
        public static string AGGRESSION_DECREASE = "DecreaseAggression";
        public static string AGGRESSION_RESET = "ResetAggression";
        
        // Actions
        public static string ACTION_DESTROYBAGS = "DestroyBags";
        public static string ACTION_KILLPLAYER = "KillPlayer";
        public static string ACTION_PLAYCALL = "PlayCall";
        public static string ACTION_PLAYFOOTSTEPS = "PlayFootsteps";
    }
}