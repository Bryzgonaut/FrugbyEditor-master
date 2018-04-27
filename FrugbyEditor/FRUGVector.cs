using System;

namespace FrugbyEditor
{
    public class FRUGVector
    {
        public float X;
        public float Y;
        public float Z;

        /// <summary>
        /// Creates a new vector with given x, y, z components
        /// </summary>
        public FRUGVector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// A HQMVector with all components set to 0
        /// </summary>
        public static FRUGVector Zero
        {
            get { return new FRUGVector(0, 0, 0); }
        }

        /// <summary>
        /// A HQMVector set to the centre of the ice
        /// </summary>
        public static FRUGVector Centre
        {
            get { return new FRUGVector(15, 0, 30.5f); }
        }

        /// <summary>
        /// A HQMVector set to the centre of the goal line in the red net (default position)
        /// </summary>
        public static FRUGVector RedNet
        {
            get { return new FRUGVector(15, 0, 57); }
        }

        /// <summary>
        /// A HQMVector set to the centre of the goal line in the blue net (default position)
        /// </summary>
        public static FRUGVector BlueNet
        {
            get { return new FRUGVector(15, 0, 4); }
        }


        /// <summary>
        /// The length of the vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)); }
        }

        /// <summary>
        /// The same vector with a magnitude of 1
        /// </summary>
        public FRUGVector Normalized
        {
            get
            {
                float m = Magnitude;
                return new FRUGVector(X / m, Y / m, Z / m);
            }
        }

        public static FRUGVector operator +(FRUGVector left, FRUGVector right)
        {
            return new FRUGVector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static FRUGVector operator -(FRUGVector left, FRUGVector right)
        {
            return new FRUGVector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static FRUGVector operator *(FRUGVector vector, float scale)
        {
            return new FRUGVector(vector.X * scale, vector.Y * scale, vector.Z * scale);
        }

        public static FRUGVector operator /(FRUGVector vector, float scale)
        {
            return new FRUGVector(vector.X / scale, vector.Y / scale, vector.Z / scale);
        }

        public static bool operator ==(FRUGVector left, FRUGVector right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator !=(FRUGVector left, FRUGVector right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return X.ToString("F2") + "," + Y.ToString("F2") + "," + Z.ToString("F2");
        }
    }
}
