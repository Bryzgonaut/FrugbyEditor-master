namespace FrugbyEditor
{
    /// <summary>
    /// Contains information about the Ball
    /// </summary>
    public static class Ball
    {
        const int BALL_TRANSFORM_ADDRESS = 0x1323F6C;
        const int BALL_POSITION_OFFSET = 0x0;
        const int BALL_VELOCITY_OFFSET = 0x4C;
        const int BALL_ROTATIONAL_VELOCITY_OFFSET = 0x64;
        /// <summary>
        /// The position of the ball
        /// </summary>
        public static FRUGVector Position
        {
            get { return MemoryEditor.ReadFRUGVector(BALL_TRANSFORM_ADDRESS + BALL_POSITION_OFFSET); }
            set { MemoryEditor.WriteFRUGVector(value, BALL_TRANSFORM_ADDRESS + BALL_POSITION_OFFSET); }
        }

        /// <summary>
        /// The velocity of the ball NEEDS UPDATING
        /// </summary>
        public static FRUGVector Velocity
        {
            get { return MemoryEditor.ReadFRUGVector(BALL_TRANSFORM_ADDRESS + BALL_VELOCITY_OFFSET); }
            set { MemoryEditor.WriteFRUGVector(value, BALL_TRANSFORM_ADDRESS + BALL_VELOCITY_OFFSET); }
        }

        /// <summary>
        /// The spin or rotational velocity of the ball NEEDS UPDATING
        /// </summary>
        public static FRUGVector RotationalVelocity
        {
            get { return MemoryEditor.ReadFRUGVector(BALL_TRANSFORM_ADDRESS + BALL_ROTATIONAL_VELOCITY_OFFSET); }
            set { MemoryEditor.WriteFRUGVector(value, BALL_TRANSFORM_ADDRESS + BALL_ROTATIONAL_VELOCITY_OFFSET); }
        }
    }
}
