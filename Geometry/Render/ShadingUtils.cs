using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class ShadingUtils
    {
        /// <summary>
        /// Вычисление цвета точки на основе модели Ламберта
        /// </summary>
        /// <param name="light">Источник цвета</param>
        /// <param name="point">Координаты точки</param>
        /// <param name="objectColor">Цвет материала объекта</param>
        /// <returns>Нормализованный на (0-255) вектор цвета</returns>
        public static Vector3 CalculateLambertColor(LightSource light, Vertex point, Vector3 objectColor)
        {
            // Вектор от точки к источнику света
            Vector3 L = (light - point).Normalized();
            float cos = Math.Max(Vector3.Dot(point.Normal.Normalized(), L), 0);
            
            return new Vector3
            {
                X = objectColor.X * light.Color.X * cos,
                Y = objectColor.Y * light.Color.Y * cos,
                Z = objectColor.Z * light.Color.Z * cos
            };
        }

        /// <summary>
        /// Вычисление цвета точки на основе модели Ламберта
        /// </summary>
        /// <param name="light">Источник цвета</param>
        /// <param name="point">Координаты точки</param>
        /// <param name="objectColor">Цвет материала объекта</param>
        /// <returns>Нормализованный на (0-255) вектор цвета</returns>
        public static Vector3 CalculateLambertColor(LightSource light, Point3D point, Vector3 normal, Vector3 objectColor)
        {
            // Вектор от точки к источнику света
            Vector3 L = (light - point);
            L.Normalize();
            float cos = Math.Max(Vector3.Dot(normal, L), 0);
            
            return new Vector3
            {
                X = objectColor.X * light.Color.X * cos,
                Y = objectColor.Y * light.Color.Y * cos,
                Z = objectColor.Z * light.Color.Z * cos
            };
        }
    }
}
