using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Sphere: Figure
    {
        public float Radius { get; private set; }
        public Point3D Center { get; private set; }

        public Sphere(float radius, Point3D center)
        {
            Radius = radius;
            Center = center;
        }

        /// <summary>
        /// Определяет, пересекается ли луч со сферой и определяет дистанцию от начала луча до точки пересечения
        /// </summary>
        public bool SphereIntersection(Ray ray, out float distance, float eps = 0.0001f)
        {
            distance = 0;

            // ветор L от начала луча до центра сферы
            Vector3 L = ray.Start - Center;


            float B = Vector3.Dot(L, ray.Direction);
            float C = L.X * L.X + L.Y * L.Y + L.Z * L.Z - Radius * Radius;

            // дискриминант
            float D = B * B - C;

            if (D < 0) 
                return false;
            float sqrtD = (float) Math.Sqrt(D);

            // дистанции до точек пересечения
            float t1 = -B - sqrtD;
            float t2 = -B + sqrtD;

            if (t1 > eps)
            {
                distance = t1;
                return true;
            }
            if (t2 > eps)
            {
                distance = t2;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Определяет, пересекается ли луч со сферой, определяет дистанцию от начала луча до точки пересечения и нормаль
        /// </summary>
        public bool FigureIntersection(Ray ray, out float distance, out Vector3 normal)
        {
            normal = new Vector3(0, 0, 0);
            
            if (SphereIntersection(ray, out distance))
            {
                Point3D intersectionPoint = new Point3D(
                    ray.Start.X + ray.Direction.X * distance,
                    ray.Start.Y + ray.Direction.Y * distance,
                    ray.Start.Z + ray.Direction.Z * distance
                    );

                normal = intersectionPoint - Center;
                normal.Normalize();
                return true;
            }
            return false;
        }
    }
}
