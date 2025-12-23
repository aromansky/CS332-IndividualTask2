using System.Drawing;

namespace Geometry
{
    public class Face: ICloneable, IFigure
    {

        internal List<Vertex> vertices;

        /// <summary>
        /// Набор несвязанных вершин грани.
        /// </summary>
        public List<Vertex> Vertices => vertices.Select(v => (Vertex)v.Clone()).ToList();
        public Vector3 ObjectColor { get; private set; } = new Vector3(Color.White);
        public Vector3 NormalVector { get; set; }
        public bool NormalInverted { get; private set; } = false;
        public Material Material { get; set; }

        public MyImage Texture { get; private set; }

        public Face(List<Point3D> points, List<PointF> uvs, MyImage texture = null, Vector3? normalVector = null, Color? col = null)
        {
            this.Texture = texture;
            InitFace(points, uvs, normalVector, col);
        }


        public Face(List<Point3D> points, Vector3? normalVector = null, Color? col = null)
        {
            InitFace(points, null, normalVector, col);
        }

        public Face(params Point3D[] points)
        {
            InitFace(points.ToList(), null, null, null);
        }

        public Face(List<Point3D> points, Vector3 NormalVector)
        {
            InitFace(points, null, NormalVector, null);
        }


        private void InitFace(List<Point3D> points, List<PointF> uvs, Vector3? normalVector, Color? col)
        {
            this.vertices = new List<Vertex>();
            //points = points.Distinct().ToList();

            for (int i = 0; i < points.Count; i++)
            {
                float u = (uvs != null) ? uvs[i].X : 0.0f;
                float v = (uvs != null) ? uvs[i].Y : 0.0f;

                this.vertices.Add(new Vertex(points[i], u, v));
            }

            if (normalVector.HasValue)
                this.NormalVector = normalVector.Value;
            else if (this.vertices.Count >= 3)
            {
                Vector3 v1 = new Vector3(vertices[0], vertices[1]);
                Vector3 v2 = new Vector3(vertices[0], vertices[2]);
                this.NormalVector = Vector3.Cross(v1, v2).Normalized();
            }
            else
                this.NormalVector = new Vector3(0, 0, 0);

            foreach (Vertex v in vertices)
                v.Normal = this.NormalVector;

            this.ObjectColor = new Vector3(col ?? Color.White);

            foreach (Vertex v in vertices)
                v.Color = this.ObjectColor;
        }


        public Face(Face other)
        {
            this.NormalVector = other.NormalVector;
            this.ObjectColor = other.ObjectColor;
            this.Texture = other.Texture; // NEW: Копирование ссылки на текстуру
            this.vertices = other.Vertices.Select(e => (Vertex)e.Clone()).ToList();
        }

        public void InvertNormal()
        {
            this.NormalVector = -this.NormalVector;
            this.NormalInverted = !this.NormalInverted;
        }


        public void SetTexture(MyImage texture)
        {
            this.Texture = texture;
        }

        public void SetColor(Vector3 col)
        {
            this.ObjectColor = col;

            foreach (Vertex v in vertices)
                v.Color = this.ObjectColor;
        }

        public Point3D GetCenter()
        {
            float centerX = vertices.Average(v => v.X);
            float centerY = vertices.Average(v => v.Y);
            float centerZ = vertices.Average(v => v.Z);
            return new Point3D(centerX, centerY, centerZ);
        }

        public bool IsFrontFace(Camera camera)
        {
            if (vertices.Count < 3) return true;
            Vector3 viewVector;

            if (camera.Mode == ProjectionMode.Perspective)
            {
                viewVector = new Vector3(GetCenter(), camera.Position).Normalized();
            }
            else
            {
                viewVector = camera.ViewDirection.Normalized();
            }

            float dot = Vector3.Dot(NormalVector, viewVector);
            return dot > 0;
        }

        /// <summary>
        /// Алгоритм Моллера - Трумбора для определения пересечения луча и треугольника
        /// </summary>
        public float TriangleIntersecrion(Ray ray, Point3D p0, Point3D p1, Point3D p2, float eps)
        {
            Vector3 e1 = p1 - p0;
            Vector3 e2 = p2 - p0;


            // Вектор нормали к плоскости
            Vector3 pvec = Vector3.Cross(ray.Direction, e2);
            float det = Vector3.Dot(e1, pvec);

            // Луч параллелен плоскости
            if (Math.Abs(det) < eps)
                return 0;

            float inv_det = 1 / det;
            Vector3 tvec = ray.Start - p0;
            float u = Vector3.Dot(tvec, pvec) * inv_det;
            if (u < 0 || u > 1)
                return 0;

            Vector3 qvec = Vector3.Cross(tvec, e1);
            float v = Vector3.Dot(ray.Direction, qvec) * inv_det;
            if (v < 0 || u + v > 1)
                return 0;

            float t = Vector3.Dot(e2, qvec) * inv_det;
            if (t > eps)
                return t;
            return 0;
        }

        public bool FigureIntersection(Ray ray, out float distance, out Vector3 normal, float eps)
        {
            distance = float.MaxValue;
            normal = new Vector3(0, 0, 0);

            float intersect = TriangleIntersecrion(ray, vertices[0], vertices[1], vertices[2], eps);
            if (intersect != 0 && intersect < distance)
            {
                distance = intersect;
            }

            intersect = TriangleIntersecrion(ray, vertices[0], vertices[2], vertices[3], eps);
            if (intersect != 0 && intersect < distance)
            {
                distance = intersect;
            }

            if (distance == float.MaxValue)
            {
                return false;
            }

            normal = NormalVector;
            return true;
        }
        public object Clone() => new Face(this);
    }
}
