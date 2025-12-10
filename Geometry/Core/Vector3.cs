using System.Drawing;
using System.Globalization;

namespace Geometry
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /// <summary>
        /// Создаёт ненормированный вектор
        /// </summary>
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Создаёт ненормированный вектор из цвета
        /// </summary>
        /// <param name="col">Цвет</param>
        public Vector3(Color col)
        {
            this.X = col.R / 255.0f;
            this.Y = col.G / 255.0f;
            this.Z = col.B / 255.0f;
        }

        /// <summary>
        /// Создаёт ненормированный вектор
        /// </summary>
        public Vector3(Point3D from, Point3D to)
        {
            this.X = to.X - from.X;
            this.Y = to.Y - from.Y;
            this.Z = to.Z - from.Z;
        }

        /// <summary>
        /// Создаёт ненормированный вектор
        /// </summary>
        public Vector3(Point3D p1)
        {
            this.X = p1.X;
            this.Y = p1.Y;
            this.Z = p1.Z;
        }

        public Vector3 Normalized()
        {
            float length = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            if (length == 0)
                throw new InvalidOperationException("Невозможно нормализовать нулевой вектор");
            return new Vector3(X / length, Y / length, Z / length);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0 && Z == 0;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }


        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3 operator- (Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator -(Vector3 to, Vector3 from)
        {
            return new Vector3(to.X - from.X, to.Y - from.Y, to.Z - from.Z);
        }

        public static Vector3 operator *(Vector3 v, float n)
        {
            return new Vector3(v.X * n, v.Y * n, v.Z * n);
        }

        public static Vector3 operator /(Vector3 v, float n)
        {
            return new Vector3(v.X / n, v.Y / n, v.Z / n);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z
            );
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", X, Y, Z);
        }
    }
}
