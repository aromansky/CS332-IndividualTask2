using System;
using System.Collections.Generic;
using System.Drawing;
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
            public float k = 0;

            public int hasReflection = 0; // 0 - не известно, -1 - не имеет, 1 - имеет, 2 - учтён
            public RayTracingNode reflectionNode = null;

            public int hasRefraction = 0; // 0 - не известно, -1 - не имеет, 1 - имеет, 2 - учтён
            public RayTracingNode refractionNode = null;

            public Vector3 color = new Vector3(0, 0, 0);

            public bool isLocalLightingCalculated = false;


            public bool IntersectionsFound = false;
            public Point3D HitPoint;
            public Vector3 Normal;
            public Material HitMaterial;

            public bool IsExiting = false;
        }

        /// <summary>
        /// Видна ли точка из источника света
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="lightPoint"></param>
        /// <returns></returns>
        private static bool PointIsVisible(Ray ray, Point3D lightPoint, List<IFigure> figures, float eps)
        {
            float lightDistance = (lightPoint - ray.Start).Length(); // растояние от точки до источника света
            foreach (IFigure figure in figures)
            {
                float distance;
                Vector3 normal;
                if (figure.FigureIntersection(ray, out distance, out normal, eps))
                    if (distance < lightDistance && distance > eps) // если источник света в фигуре
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Расчёт дерева лучей для одного пикселя
        /// </summary>
        /// <param name="ray">Первичный луч</param>
        public static Vector3 ComputeRayTracingForPixel(Ray ray, List<IFigure> figures, List<LightSource> lights, float eps, int maxDepth)
        {
            Stack<RayTracingNode> s = new Stack<RayTracingNode>();
            RayTracingNode root = new RayTracingNode(ray, 0);
            s.Push(root);

            

            while (s.Count > 0)
            {
                RayTracingNode currentRayNode = s.Peek();
                Ray currentRay = currentRayNode.ray;

                if (currentRayNode.hasReflection == 1)
                {
                    currentRayNode.color += currentRayNode.reflectionNode.color * currentRayNode.reflectionNode.k;
                    currentRayNode.hasReflection = 2;
                }

                if (currentRayNode.hasRefraction == 1)
                {
                    currentRayNode.color += currentRayNode.refractionNode.color * currentRayNode.refractionNode.k;
                    currentRayNode.hasRefraction = 2;
                }

                if ((currentRayNode.hasReflection == -1 || currentRayNode.hasReflection == 2) &&
                    (currentRayNode.hasRefraction == -1 || currentRayNode.hasRefraction == 2) ||
                    currentRayNode.iteration >= maxDepth)
                {
                    s.Pop();
                    continue;
                }


                if (!currentRayNode.IntersectionsFound)
                {
                    //float minDistance = float.MaxValue;
                    //Vector3 nearNormal = new Vector3();
                    //Material nearMaterial = new Material();

                    //foreach (IFigure figure in figures)
                    //{
                    //    if (figure.FigureIntersection(currentRay, out float distance, out Vector3 normal, eps) && distance < minDistance)
                    //    {
                    //        minDistance = distance;
                    //        nearNormal = normal;
                    //        nearMaterial = figure.Material;
                    //    }
                    //}

                    //if (Math.Abs(minDistance) < eps || minDistance == float.MaxValue)
                    //{
                    //    currentRayNode.hasReflection = -1;
                    //    currentRayNode.hasRefraction = -1;
                    //    s.Pop();
                    //    continue;
                    //}


                    ////если угол между нормалью к поверхности объекта и направлением луча положительный, => угол острый, => луч выходит из объекта в среду
                    //if (Vector3.Dot(currentRay.Direction, nearNormal) > 0)
                    //{
                    //    nearNormal *= -1;
                    //    currentRayNode.IsExiting = true;
                    //}

                    //currentRayNode.IntersectionsFound = true;
                    //currentRayNode.HitPoint = currentRay.GetPoint(minDistance);
                    //currentRayNode.Normal = nearNormal;
                    //currentRayNode.HitMaterial = nearMaterial;

                    Vector3 n = currentRayNode.Normal;

                    // Переводим диапазон [-1, 1] в [0, 1]
                    return new Vector3(
                        (n.X + 1.0f) * 0.5f,
                        (n.Y + 1.0f) * 0.5f,
                        (n.Z + 1.0f) * 0.5f
                    );
                }
                return new Vector3(0, 0, 0);
                // теневые лучи
                if (!currentRayNode.isLocalLightingCalculated)
                {
                    Vector3 ambientColor = new Vector3(0.1f, 0.1f, 0.1f); // Тот самый GlobalAmbient
                    currentRayNode.color += new Vector3(
                        ambientColor.X * currentRayNode.HitMaterial.Color.X * currentRayNode.HitMaterial.Ambient,
                        ambientColor.Y * currentRayNode.HitMaterial.Color.Y * currentRayNode.HitMaterial.Ambient,
                        ambientColor.Z * currentRayNode.HitMaterial.Color.Z * currentRayNode.HitMaterial.Ambient
                    );


                    foreach (LightSource light in lights)
                    {
                        Ray shadowRay = new Ray(currentRayNode.HitPoint + new Point3D(currentRayNode.Normal * eps), light - currentRayNode.HitPoint);
                        shadowRay.Direction.Normalize();

                        // если точка освещена источниками света
                        if (PointIsVisible(shadowRay, light, figures, eps))
                            currentRayNode.color += ShadingUtils.CalculateLambertColor(light, currentRayNode.HitPoint, currentRayNode.Normal, currentRayNode.HitMaterial.Color);
                    }
                }
                currentRayNode.isLocalLightingCalculated = true;

                // луч отражения
                if (currentRayNode.hasReflection == 0)
                {
                    if (currentRayNode.HitMaterial.Reflecrion > 0)
                    {
                        Vector3 D = currentRay.Direction;
                        D.Normalize();
                        Vector3 N = currentRayNode.Normal;
                        Vector3 reflectVector = D - 2f * Vector3.Dot(D, N) * N;

                        Ray reflectRay = new Ray(currentRayNode.HitPoint + new Point3D(currentRayNode.Normal * eps), reflectVector);
                        RayTracingNode node = new RayTracingNode(reflectRay, currentRayNode.iteration + 1);
                        currentRayNode.reflectionNode = node;
                        currentRayNode.hasReflection = 1;
                        node.k = currentRayNode.HitMaterial.Reflecrion;

                        s.Push(node);

                        continue;
                    }
                    else
                        currentRayNode.hasReflection = -1;

                }

                // луч преломлеиня
                if (currentRayNode.hasRefraction == 0)
                {
                    if (currentRayNode.HitMaterial.Refraction != 0)
                    {
                        float eta;                 //коэффициент преломления
                        if (currentRayNode.IsExiting) //луч выходит в среду
                            eta = currentRayNode.HitMaterial.Environment;
                        else
                            eta = 1 / currentRayNode.HitMaterial.Environment;

                        float sclr = Vector3.Dot(currentRayNode.Normal, currentRay.Direction);
                        float k = 1 - eta * eta * (1 - sclr * sclr);
                        if (k >= 0)
                        {
                            float cos_eta = (float)Math.Sqrt(k);
                            Vector3 T = eta * currentRay.Direction - (cos_eta + eta * Vector3.Dot(currentRay.Direction, currentRayNode.Normal)) * currentRayNode.Normal;
                            Ray refractRay = new Ray(currentRayNode.HitPoint + new Point3D(currentRayNode.Normal * eps), T);

                            RayTracingNode node = new RayTracingNode(refractRay, currentRayNode.iteration + 1);
                            node.k = currentRayNode.HitMaterial.Refraction;
                            s.Push(node);

                            currentRayNode.refractionNode = node;
                            currentRayNode.hasRefraction = 1;
                        }
                        continue;
                    }
                    else
                        currentRayNode.hasRefraction = -1;
                }
            }

            return root.color;
        }

        public static MyImage ComputeRayTracing(Camera cam, List<IFigure> figures, List<LightSource> lights, int width, int height, int maxDepth, float eps = 0.001f)
        {
            MyImage img = new MyImage(width, height);
            img.Lock();
            Parallel.For(0, img.Height, j =>
            {
                for (int i = 0; i < img.Width; i++)
                {
                    Ray ray = cam.GetRay(i, j);
                    Vector3 color = ComputeRayTracingForPixel(ray, figures, lights, eps, maxDepth);

                    color.X = (float)Math.Pow(color.X, 1.0 / 2.2);
                    color.Y = (float)Math.Pow(color.Y, 1.0 / 2.2);
                    color.Z = (float)Math.Pow(color.Z, 1.0 / 2.2);

                    int r = (int)Math.Clamp(color.X * 255, 0, 255);
                    int g = (int)Math.Clamp(color.Y * 255, 0, 255);
                    int b = (int)Math.Clamp(color.Z * 255, 0, 255);
                    img.SetPixel(i, j, Color.FromArgb(255, r, g, b));
                }
            });

            img.Unlock();

            return img;
        }
    }
}
