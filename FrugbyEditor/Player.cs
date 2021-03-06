﻿namespace FrugbyEditor
{
    public class Player
    {
        const int PLAYER_LIST_ADDRESS = 0x4364CC0;
        const int PLAYER_STRUCT_SIZE = 0xC4;

        const int IN_SERVER_OFFSET = 0x0;
        const int ID_OFFSET = 0x38;
        const int TEAM_OFFSET = 0x8;
        const int ROLE_OFFSET = 0xC;
        const int LOCKOUT_TIME_OFFSET = 0x10;
        const int PLAYER_NAME_OFFSET = 0x3C;
        const int PLAYER_NUMBER_OFFSET = 0x7C;
        const int STRAFING_OFFSET = 0x84;
        const int FORWARD_BACK_OFFSET = 0x80;
        const int TURNING_OFFSET = 0x90;
        const int STICK_Y_ROTATION_OFFSET = 0x68;
        const int INPUT_STATE_OFFSET = 0xC0;
        const int HEAD_X_ROTATION_OFFSET = 0x78;
        const int HEAD_Y_ROTATION_OFFSET = 0x7C;
        const int GOALS_OFFSET = 0x88;
        const int ASSISTS_OFFSET = 0x8C;

        const int PLAYER_TRANSFORM_LIST_ADDRESS = 0x23B798;
        const int PLAYER_TRANSFORM_SIZE = 0x2934;

        const int PLAYER_POSITION_OFFSET = 0x0;
        const int PLAYER_SIN_ROTATION_OFFSET = 0x24;
        const int PLAYER_COS_ROTATION_OFFSET = 0x2C;
        const int STICK_POSITION_OFFSET = 0xA0; //deprecated. will be replaced with hand position

        const int AUTOSTOP_FORWARD_BACK = 0x268D8;
        const int AUTOSTOP_TURNING = 0x268E9;

        private byte[] disableAutostop = new byte[6] { 0xDD, 0xD8, 0x90, 0x90, 0x90, 0x90 };

        private int m_Slot;

        /// <summary>
        /// Creates a new Player object using the specified server slot
        /// </summary>
        /// <param name="slot">The slot in the server list (0 based)</param>
        internal Player(int slot)
        {
            this.m_Slot = slot;
        }

        /// <summary>
        /// Returns true if the player is in the server
        /// </summary>
        public bool InServer
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + IN_SERVER_OFFSET) == 1; }
        }

        /// <summary>
        /// The player's id, used to get it's location data
        /// </summary>
        public int ID
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + ID_OFFSET); }
        }

        /// <summary>
        /// The team that the player is on
        /// </summary>
        public HQMTeam Team
        {
            get { return (HQMTeam)MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + TEAM_OFFSET); }
            set { MemoryEditor.WriteInt((int)value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + TEAM_OFFSET); }
        }

        /// <summary>
        /// The role that the player is occupying
        /// </summary>
        public HQMRole Role
        {
            get { return (HQMRole)MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + ROLE_OFFSET); }
            set { MemoryEditor.WriteInt((int)value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + ROLE_OFFSET); }
        }

        /// <summary>
        /// The amount of time in hundredths of a second that before the player can change team again
        /// </summary>
        public int LockoutTime
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + LOCKOUT_TIME_OFFSET); }
        }

        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name
        {
            get { return MemoryEditor.ReadString(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + PLAYER_NAME_OFFSET, 3); }
        }

        /// <summary>
        /// The player's jersey number
        /// </summary>
        public int JerseyNumber
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + PLAYER_NUMBER_OFFSET); }
            set { MemoryEditor.WriteInt(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + PLAYER_NUMBER_OFFSET); }
        }

        /// <summary>
        /// The direction the player is sidestepping. -1 = Left, 1 = Right, 0 = not turning
        /// </summary>
        public float Strafing
        {
            get { return MemoryEditor.ReadFloat(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + STRAFING_OFFSET); }
            set { MemoryEditor.WriteFloat(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + STRAFING_OFFSET); }
        }

        /// <summary>
        /// Whether the player is moving forwards (1), reversing (-1) or not moving (0)
        /// </summary>
        public float ForwardBack
        {
            get { return MemoryEditor.ReadFloat(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + FORWARD_BACK_OFFSET); }
            set { MemoryEditor.WriteFloat(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + FORWARD_BACK_OFFSET); }
        }

        /// <summary>
        /// The rotation of the stick around the player (in radians). Ranges from -Pi / 2 to Pi / 2
        /// </summary>
        public float Turning
        {
            get { return MemoryEditor.ReadFloat(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + TURNING_OFFSET); }
            set { MemoryEditor.WriteFloat(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + TURNING_OFFSET); }
        }

        /// <summary>
        /// The rotation of the stick away from the player (in radians)
        /// </summary>
        public float StickYRotation
        {
            get { return MemoryEditor.ReadFloat(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + STICK_Y_ROTATION_OFFSET); }
            set { MemoryEditor.WriteFloat(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + STICK_Y_ROTATION_OFFSET); }
        }

        /// <summary>
        /// 1 = Jumping, 2 = Crouched, 16 = Stopped with Shift
        /// </summary>
        public int InputState
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + INPUT_STATE_OFFSET); }
            set { MemoryEditor.WriteInt(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + INPUT_STATE_OFFSET); }
        }

        /// <summary>
        /// The rotation of the player's head looking left or right
        /// </summary>
        public float HeadXRotation
        {
            get { return MemoryEditor.ReadFloat(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + HEAD_X_ROTATION_OFFSET); }
            set { MemoryEditor.WriteFloat(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + HEAD_X_ROTATION_OFFSET); }
        }

        /// <summary>
        /// The rotation of the player's head looking up or down
        /// </summary>
        public float HeadYRotation
        {
            get { return MemoryEditor.ReadFloat(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + HEAD_Y_ROTATION_OFFSET); }
            set { MemoryEditor.WriteFloat(value, PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + HEAD_Y_ROTATION_OFFSET); }
        }

        /// <summary>
        /// The number of goals that the player has scored
        /// </summary>
        public int Goals
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + GOALS_OFFSET); }
        }

        /// <summary>
        /// The number of assists that the player has got
        /// </summary>
        public int Assists
        {
            get { return MemoryEditor.ReadInt(PLAYER_LIST_ADDRESS + m_Slot * PLAYER_STRUCT_SIZE + ASSISTS_OFFSET); }
        }

        /// <summary>
        /// The player's position
        /// </summary>
        public FRUGVector Position
        {
            get { return MemoryEditor.ReadFRUGVector(PLAYER_TRANSFORM_LIST_ADDRESS + ID * PLAYER_TRANSFORM_SIZE + PLAYER_POSITION_OFFSET); }
        }

        /// <summary>
        /// The Sine of the angle of the direction the player is facing
        /// </summary>
        public float SinRotation
        {
            get { return MemoryEditor.ReadFloat(PLAYER_TRANSFORM_LIST_ADDRESS + ID * PLAYER_TRANSFORM_SIZE + PLAYER_SIN_ROTATION_OFFSET); }
        }

        /// <summary>
        /// The Cosine of the angle of the direction the player is facing
        /// </summary>
        public float CosRotation
        {
            get { return MemoryEditor.ReadFloat(PLAYER_TRANSFORM_LIST_ADDRESS + ID * PLAYER_TRANSFORM_SIZE + PLAYER_COS_ROTATION_OFFSET); }
        }

        /// <summary>
        /// The position of the player's stick
        /// </summary>
        public FRUGVector StickPosition
        {
            get { return MemoryEditor.ReadFRUGVector(PLAYER_TRANSFORM_LIST_ADDRESS + ID * PLAYER_TRANSFORM_SIZE + STICK_POSITION_OFFSET); }
        }

        /// <summary>
        /// Disable writing 0 to inputs when no key is pressed (required for bots)
        /// </summary>
        public bool autostopFBDisabled
        {
            get { return MemoryEditor.ReadBytes(AUTOSTOP_FORWARD_BACK, 1) != System.BitConverter.GetBytes(68); }
            set { MemoryEditor.WriteBytes(disableAutostop,AUTOSTOP_FORWARD_BACK) ; }
        }

        /// <summary>
        /// Disable writing 00 to inputs when no key is pressed (required for bots)
        /// </summary>
        public bool autostopLRDisabled
        {
            get { return MemoryEditor.ReadBytes(AUTOSTOP_TURNING, 1) != System.BitConverter.GetBytes(68); }
            set { MemoryEditor.WriteBytes(disableAutostop,AUTOSTOP_TURNING) ; }
        }
    }

    public enum HQMRole
    {
        C = 0,
        LD = 1,
        RD = 2,
        LW = 3,
        RW = 4,
        G = 5
    }

    public enum HQMTeam
    {
        NoTeam = -1,
        Red = 0,
        Blue = 1,
        Green = 2,
        Yellow = 3
    }
}
