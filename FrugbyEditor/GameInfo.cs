namespace FrugbyEditor
{
    /// <summary>
    /// Contains information about the scores of the 2 teams
    /// </summary>
    public static class GameInfo
    {
        const int TIME_ADDRESS = 0x7233C8;
        const int TIME_OFFSET = 0x0;
        const int PERIOD_OFFSET = -0x4;
        const int INTERMISSION_TIME_OFFSET = 0x10;

        //not working yet (i think post goal time and halftime are merged now)
        const int STOP_TIME_ADDRESS = 0x07D33DA0;

        const int SCOREBOARD_ADDRESS = 0x7233CC;
        const int LEFT_SCORE_OFFSET = 0x0;
        const int RIGHT_SCORE_OFFSET = 0x4;
        const int LAST_TRY_OFFSET = 0x8;
        const int LEFT_TEAM_OFFSET = 0x20;
        const int RIGHT_TEAM_OFFSET = 0x24;

        //not working yet
        const int GAME_OVER = 0x07D349AC;

        /// <summary>
        /// The left team color
        /// </summary>
        public static HQMTeam LeftTeam
        {
            get { return(HQMTeam) MemoryEditor.ReadInt(SCOREBOARD_ADDRESS + LEFT_TEAM_OFFSET); }            
        }

        /// <summary>
        /// The right team color
        /// </summary>
        public static HQMTeam RightTeam
        {
            get { return(HQMTeam) MemoryEditor.ReadInt(SCOREBOARD_ADDRESS + RIGHT_TEAM_OFFSET); }
        }

        /// <summary>
        /// The Team that scored most recently
        /// </summary>
        public static HQMTeam LastTryTeam
        {
            get { return (HQMTeam)MemoryEditor.ReadInt(SCOREBOARD_ADDRESS + LAST_TRY_OFFSET); }
        }

        /// <summary>
        /// The left team's score
        /// </summary>
        public static int LeftScore
        {
            get { return MemoryEditor.ReadInt(SCOREBOARD_ADDRESS + LEFT_SCORE_OFFSET); }
            set { MemoryEditor.WriteInt(value, SCOREBOARD_ADDRESS + LEFT_SCORE_OFFSET); }
        }

        /// <summary>
        /// The right team's score
        /// </summary>
        public static int RightScore
        {
            get { return MemoryEditor.ReadInt(SCOREBOARD_ADDRESS + RIGHT_SCORE_OFFSET); }
            set { MemoryEditor.WriteInt(value, SCOREBOARD_ADDRESS + RIGHT_SCORE_OFFSET); }
        }

        /// <summary>
        /// The game time in hundredths of a second
        /// </summary>
        public static int GameTime
        {
            get { return MemoryEditor.ReadInt(TIME_ADDRESS + TIME_OFFSET); }
            set { MemoryEditor.WriteInt(value, TIME_ADDRESS + TIME_OFFSET); }
        }

        /// <summary>
        /// The period. 0 = warmup, 1 = 1st half, 2 = halftime, 3 = 2nd half
        /// </summary>
        public static int Period
        {
            get { return MemoryEditor.ReadInt(TIME_ADDRESS + PERIOD_OFFSET); }
            set { MemoryEditor.WriteInt(value, TIME_ADDRESS + PERIOD_OFFSET); }
        }

        /// <summary>
        /// The amount of time in hundredths of a second before the next period starts
        /// </summary>
        public static int IntermissionTime
        {
            get { return MemoryEditor.ReadInt(TIME_ADDRESS + INTERMISSION_TIME_OFFSET); }
            set { MemoryEditor.WriteInt(value, TIME_ADDRESS + INTERMISSION_TIME_OFFSET); }
        }

        /// <summary>
        /// The amount of time in hundredths of a second before the next faceoff starts (after a goal)
        /// </summary>
        public static int AfterGoalFaceoffTime
        {
            get { return MemoryEditor.ReadInt(STOP_TIME_ADDRESS); }
            set { MemoryEditor.WriteInt(value, STOP_TIME_ADDRESS); }
        }

        /// <summary>
        /// If the game is over
        /// </summary>
        public static bool IsGameOver
        {
            get { return MemoryEditor.ReadInt(GAME_OVER) == 1; }
        }


        
    }
}
