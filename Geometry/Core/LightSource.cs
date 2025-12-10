using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class LightSource: Point3D, ICloneable
    {
        public Vector3 Color { get; set; } = new Vector3(0, 0, 0);
        public Vector3 AmbientColor { get; set; } = new Vector3(0.1f, 0.1f, 0.1f);
        public float K0 { get; set; } = 1;
        public float K1 { get; set; } = 0;
        public float K2 { get; set; } = 0;
        public LightSource(float x, float y, float z, Color color) : base(x, y, z)
        {
            this.Color = new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }
        public LightSource(Point3D point, Vector3 color) : base(point)
        {
            this.Color = color;
        }
        public LightSource(Point3D point, Color color) : base(point)
        {
            this.Color = new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }
        public LightSource(LightSource other) : base(other)
        {
            this.Color = other.Color;
            this.AmbientColor = other.AmbientColor;
            this.K0 = other.K0;
            this.K1 = other.K1;
            this.K2 = other.K2;
        }

        public override object Clone() => new LightSource(this);
    }
}
