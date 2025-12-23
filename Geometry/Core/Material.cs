using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Material
    {
        /// <summary>
        /// коэффициент отражения
        /// </summary>
        public float Reflecrion { get; set; }

        /// <summary>
        /// коэффициент преломления
        /// </summary>
        public float Refraction { get; set; }

        /// <summary>
        /// коэффициент преломления среды
        /// </summary>
        public float Environment { get; set; }

        /// <summary>
        /// коэффициент принятия фонового освещения
        /// </summary>
        public float Ambient { get; set; }

        /// <summary>
        /// коэффициент принятия диффузного освещения
        /// </summary>
        public float Diffuse { get; set; }

        /// <summary>
        /// цвет материала
        /// </summary>
        public Vector3 Color { get; set; }

        public Material(float refl, float refr, float amb, float dif, float env = 1)
        {
            Reflecrion = refl;
            Refraction = refr;
            Ambient = amb;
            Diffuse = dif;
            Environment = env;
        }

        public Material(Material other)
        {
            Reflecrion = other.Reflecrion;
            Refraction = other.Refraction;
            Environment = other.Environment;
            Ambient = other.Ambient;
            Diffuse = other.Diffuse;
            Color = other.Color;
        }

        public Material() { }
    }
}
