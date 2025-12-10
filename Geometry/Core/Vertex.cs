using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Vertex: Point3D, ICloneable
    {
        public float U { get; set; } = 0.0f;
        public float V { get; set; } = 0.0f;

        public Vector3 Normal { get; set; } = new Vector3(0, 0, 0);
        public Vector3 Color { get; set; } = new Vector3(System.Drawing.Color.White);
        public Vector3 MaterialKs { get; private set; } = new Vector3(1f, 1f, 1f); // должен подходить для туншейдинга
        public float Shininess { get; private set; } = 50f; // должен подходить для туншейдинга

        public Vertex(float x, float y, float z, Vector3 Color, Vector3 Normal) : base(x, y, z)
        {
            this.Color = Color;
            this.Normal = Normal;
        }

        public Vertex(Point3D point, float u = 0.0f, float v = 0.0f) : base(point)
        {
            this.U = u; // Инициализация U
            this.V = v; // Инициализация V
        }

        public Vertex(float x, float y, float z, Vector3 Normal) : base(x, y, z)
        {
            this.Normal = Normal;
        }

        public Vertex(Vertex other) : base(other)
        {
            this.Color = other.Color;
            this.Normal = other.Normal;
            this.V = other.V;
            this.U = other.U;
        }
        public override object Clone() => new Vertex(this);
    }
}
