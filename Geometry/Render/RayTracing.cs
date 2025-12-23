using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public static class RayTracing
    {
        private struct RayTracingNode(Ray ray, int iter, float env)
        {
            public Ray ray = ray;
            public int iteration = iter;
            public float environment = env;
            public List<Ray> childrenRays = new List<Ray>();
            public int childInd = -1;
        }

        /// <summary>
        /// Видна ли точка из источника света
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="lightPoint"></param>
        /// <returns></returns>
        private static bool PointIsVisible(Ray ray, Point3D lightPoint, List<IFigure> figures, float eps = 0.0001f)
        {
            float lightDistance = (lightPoint - ray.Start).Length(); // растояние от точки до источника света
            foreach (IFigure figure in figures)
            {
                float distance;
                Vector3 normal;
                if (figure.FigureIntersection(ray, out distance, out normal))
                    if (distance < lightDistance && distance > eps) // если источник света в фигуре
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Расчёт дерева лучей для одного пикселя
        /// </summary>
        /// <param name="ray">Первичный луч</param>
        public static Vector3 ComputeRayTracingForPixel(Ray ray, List<IFigure> figures, List<LightSource> lights)
        {
            Stack<RayTracingNode> s = new Stack<RayTracingNode>();
            s.Push(new RayTracingNode(ray, 0, 1));

            Material nearMaterial;
            while (s.Count() != 0)
            {
                RayTracingNode currentRayNode = s.Peek();
                Ray currentRay = currentRayNode.ray;

                float minDistance = float.MaxValue;
                Vector3 nearNormal;

                foreach (IFigure figure in figures)
                {
                    float distance; Vector3 normal;
                    if (figure.FigureIntersection(currentRay, out distance, out normal) && distance < minDistance)
                    {
                        minDistance = distance;
                        nearNormal = normal;
                        nearMaterial = new Material(figure.Material);
                    }
                }

                if (minDistance == 0)
                    return new Vector3(0, 0, 0);

                bool shadowLightAdded = false;
                // теневые лучи
                foreach (LightSource light in lights)
                {
                    Point3D intersectionPoint = currentRay.GetPoint(minDistance);
                    Ray shadowRay = new Ray(intersectionPoint, light.GetCoords() - intersectionPoint);
                    shadowRay.Direction.Normalize();

                    // если точка освещена источниками света
                    if (PointIsVisible(shadowRay, light.GetCoords(), figures))
                    {
                        shadowLightAdded = true;

                    }
                }

            }
        }

        public static void ComputeRayTracing(Camera cam, int width, int height)
        {
            
        }
    }
}
