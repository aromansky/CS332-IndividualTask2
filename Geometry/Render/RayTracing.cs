using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public static class RayTracing
    {
        private class RayTracingNode(Ray ray, int iter)
        {
            public Ray ray = ray;
            public int iteration = iter;

            public RayTracingNode parent = null;

            public int hasReflection = 0; // 0 - не известно, -1 - не имеет, 1 - имеет
            public RayTracingNode reflectionNode = null;

            public int hasRefraction = 0; // 0 - не известно, -1 - не имеет, 1 - имеет
            public RayTracingNode refractionNode = null;

            public Vector3 color = new Vector3(0, 0, 0);
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
            s.Push(new RayTracingNode(ray, 0));

            Material nearMaterial = new Material();
            while (s.Count() != 0)
            {
                RayTracingNode currentRayNode = s.Peek();
                Ray currentRay = currentRayNode.ray;

                // TODO
                if (currentRayNode.hasReflection != 0 && currentRayNode.hasRefraction != 0)
                {
                    s.Pop();
                    continue;
                }

                if (currentRayNode.hasReflection == 1)
                {
                    if (currentRayNode.parent != null)
                        currentRayNode.color += 
                }

                float minDistance = float.MaxValue;
                Vector3 nearNormal = new Vector3();

                bool refractOutOfFigure = false; //  луч преломления выходит из объекта?

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

                //если угол между нормалью к поверхности объекта и направлением луча положительный, => угол острый, => луч выходит из объекта в среду
                if (Vector3.Dot(currentRay.Direction, nearNormal) > 0)
                {
                    nearNormal *= -1;
                    refractOutOfFigure = true;
                }

                Point3D intersectionPoint = currentRay.GetPoint(minDistance);

                // теневые лучи
                foreach (LightSource light in lights)
                {
                    // амбиентный свет
                    Vector3 ambient = light.Color * nearMaterial.Ambient;
                    ambient.X += ambient.X * nearMaterial.Color.X;
                    ambient.Y += ambient.Y * nearMaterial.Color.Y;
                    ambient.Z += ambient.Z * nearMaterial.Color.Z;

                    currentRayNode.color = ambient;
                    
                    Ray shadowRay = new Ray(intersectionPoint, light.GetCoords() - intersectionPoint);
                    shadowRay.Direction.Normalize();

                    // если точка освещена источниками света
                    if (PointIsVisible(shadowRay, light.GetCoords(), figures))
                        currentRayNode.color += ShadingUtils.CalculateLambertColor(light, intersectionPoint, nearNormal, nearMaterial.Color);
                }

                // луч отражения
                if (currentRayNode.hasReflection == 0 && nearMaterial.Reflecrion != 0)
                {
                    Vector3 D = currentRay.Direction;
                    D.Normalize();
                    Vector3 N = nearNormal;
                    Vector3 reflectVector = D - 2f * Vector3.Dot(D, N) * N;

                    Ray reflectRay = new Ray(intersectionPoint, reflectVector);
                    RayTracingNode node = new RayTracingNode(reflectRay, currentRayNode.iteration + 1);
                    currentRayNode.reflectionNode = node;
                    currentRayNode.hasReflection = 1;
                    node.parent = currentRayNode;

                    s.Push(node);

                    continue;
                }
                currentRayNode.hasReflection = -1;

                // луч преломлеиня
                if (currentRayNode.hasRefraction == 0 && nearMaterial.Refraction != 0)
                {
                    float eta;                 //коэффициент преломления
                    if (refractOutOfFigure) //луч выходит в среду
                        eta = nearMaterial.Environment;
                    else
                        eta = 1 / nearMaterial.Environment;

                    float sclr = Vector3.Dot(nearNormal, ray.Direction);
                    float k = 1 - eta * eta * (1 - sclr * sclr);
                    if (k >= 0)
                    {
                        float cos_eta = (float)Math.Sqrt(k);
                        Vector3 T = eta * currentRay.Direction - (cos_eta + eta * Vector3.Dot(currentRay.Direction, nearNormal)) * nearNormal;
                        Ray refractRay = new Ray(intersectionPoint, T);

                        RayTracingNode node = new RayTracingNode(refractRay, currentRayNode.iteration + 1);
                        s.Push(node);

                        currentRayNode.refractionNode = node;
                        currentRayNode.hasRefraction = 1;
                        node.parent = currentRayNode;
                    }
                    continue;
                }

                currentRayNode.hasRefraction = -1;

                // если нет отражения и преломления
                s.Pop();
            }

            return resColor;
        }

        public static void ComputeRayTracing(Camera cam, int width, int height)
        {
            
        }
    }
}
