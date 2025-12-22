using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Ray
    {
        public Point3D Start { get; set; }
        public Vector3 Direction { get; set; }

        public Ray() { }

        public Ray(Point3D start, Vector3 direction)
        {
            Start = start;
            Direction = direction.Normalized();
        }

        public Ray(Ray ray)
        {
            Start = ray.Start;
            Direction = ray.Direction;
        }
    }
}
