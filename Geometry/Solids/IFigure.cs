using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public interface IFigure
    {
        public bool FigureIntersection(Ray ray, out float distance, out Vector3 normal, float eps);
        public Material Material { get; set; }
    }
}
