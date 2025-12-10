using System.Globalization;

namespace Geometry
{
    public class Point3D: ICloneable
    {

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point3D(Point3D other)
        {
            (this.X, this.Y, this.Z) = other.GetCoords();
        }

        public (float x, float y, float z) GetCoords()
        {
            return (X, Y, Z);
        }

        public virtual object Clone() => new Point3D(this);

        public static Point3D operator *(Point3D point, float scalar)
        {
            (float x, float y, float z) = point.GetCoords();
            point = new Point3D(x * scalar, y * scalar, z * scalar);
            return point;
        }

        public static Vector3 operator -(Point3D to, Point3D from)
        {
            return new Vector3(to.X - from.X, to.Y - from.Y, to.Z - from.Z);
        }

        public static Point3D operator +(Point3D to, Point3D from)
        {
            return new Point3D(to.X + from.X, to.Y + from.Y, to.Z + from.Z);
        }

        public Vector3 ToVector3() => new Vector3(X, Y, Z);

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", X, Y, Z);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Point3D other) return false;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    }
}
