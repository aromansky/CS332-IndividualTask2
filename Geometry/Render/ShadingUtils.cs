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

        public static Vector3 CalculatePhongColor(LightSource light, Vertex point, Vector3 objectColor, Point3D cameraPosition, Vector3 materialKs, float shininess)
        {
            // Вектор от точки к источнику света
            Vector3 L = light - point;
            float distance = L.Length();
            L = L.Normalized();

            // Нормаль в точке (уже интерполирована и нормализована в DrawTrianglePhongZ)
            Vector3 N = point.Normal.Normalized();

            // Вектор от точки к наблюдателю (камере)
            Vector3 V = (cameraPosition - point).Normalized();

            // Вектор направления отраженного света
            // R = 2 * (N ⋅ L) * N - L
            Vector3 R = (N * 2 * Vector3.Dot(N, L) - L).Normalized();

            // Косинус угла между нормалью и светом (диффузный фактор)
            float cosNL_raw = Math.Max(Vector3.Dot(N, L), 0.0f);

            // Косинус угла между отражением и наблюдателем (зеркальный фактор)
            float cosRV_raw = Math.Max(Vector3.Dot(R, V), 0.0f);

            // Сырой зеркальный фактор Фонга
            float specularFactor_raw = (float)Math.Pow(cosRV_raw, shininess);

            float cosNL_quantized;
            if (cosNL_raw < 0.4f)
            {
                cosNL_quantized = 0.3f;
            }
            else if (cosNL_raw < 0.7f)
            {
                cosNL_quantized = 0.6f;
            }
            else
            {
                cosNL_quantized = 1.0f;
            }
            float cosNL = cosNL_quantized;


            // Квантование Зеркального света 
            float specularFactor;
            if (specularFactor_raw > 0.9f)
            {
                specularFactor = 1.0f;
            }
            else
            {
                specularFactor = 0.0f;
            }


            // Затухание
            float denominator = light.K0 + light.K1 * distance + light.K2 * distance * distance;
            float attenuation = 1.0f / denominator;
            if (denominator < 1.0f) attenuation = 1.0f;

            // Ambient
            Vector3 ambientColor = new Vector3
            {
                X = point.Color.X * light.AmbientColor.X,
                Y = point.Color.Y * light.AmbientColor.Y,
                Z = point.Color.Z * light.AmbientColor.Z
            };

            Vector3 directionalColor = new Vector3();

            directionalColor.X = point.Color.X * light.Color.X * cosNL;
            directionalColor.Y = point.Color.Y * light.Color.Y * cosNL;
            directionalColor.Z = point.Color.Z * light.Color.Z * cosNL;

            directionalColor.X += materialKs.X * light.Color.X * specularFactor;
            directionalColor.Y += materialKs.Y * light.Color.Y * specularFactor;
            directionalColor.Z += materialKs.Z * light.Color.Z * specularFactor;

            directionalColor *= attenuation;

            return ambientColor + directionalColor;
        }
    }
}
