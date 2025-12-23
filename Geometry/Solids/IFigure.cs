using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    internal interface IFigure
    {
        public bool FigureIntersection(Ray ray, out float distance, out Vector3 normal);
        public Material Material { get; set; }
    }
}
