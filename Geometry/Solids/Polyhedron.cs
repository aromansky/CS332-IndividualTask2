using System.Drawing;
using System.Text;

namespace Geometry
{
    public class Polyhedron : ICloneable, IFigure
    {
        public String Name { get; set; } = "Polyhedron";
        internal List<Face> faces;
        public Material Material { get; set; }
        public List<Face> Faces { get { return faces.Select(f => (Face)f.Clone()).ToList(); } }

        public Polyhedron(List<Face> faces)
        {
            this.faces = faces.Select(f => (Face)f.Clone()).ToList();
        }

        public Polyhedron(List<Face> faces, string name)
        {
            this.faces = faces.Select(f => (Face)f.Clone()).ToList();
            this.Name = name;
        }

        public Polyhedron(Polyhedron other)
        {
            this.Name = other.Name;
            this.faces = other.Faces.Select(f => (Face)f.Clone()).ToList();
        }
        
        
        public Point3D GetCenter()
        {
            var allCenters = Faces.Select(x => x.GetCenter()).ToList();
            float centerX = allCenters.Average(v => v.X);
            float centerY = allCenters.Average(v => v.Y);
            float centerZ = allCenters.Average(v => v.Z);
            return new Point3D(centerX, centerY, centerZ);
        }

        /// <summary>
        /// Сохраняет объект в формате Wavefront OBJ
        /// </summary>
        /// <param name="path">Путь для сохранения</param>
        public void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                List<Vertex> allVerticies = Faces.SelectMany(x => x.Vertices).Distinct().ToList();

                Dictionary<Point3D, int> vertexIndices = new Dictionary<Point3D, int>();
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < allVerticies.Count; i++) vertexIndices[allVerticies[i]] = i + 1;

                writer.WriteLine("o " + Name);

                foreach (Vertex vert in allVerticies) writer.WriteLine("v " + vert);

                foreach (Face face in Faces) writer.WriteLine("vn " + face.NormalVector);

                for (int i = 0; i < Faces.Count; i++)
                {
                    Face face = Faces[i];
                    List<int> vertIndices = face.Vertices.Select(x => vertexIndices[x]).ToList();

                    foreach (int ind in vertIndices)
                        builder.Append($"{ind}//{i + 1} ");

                    writer.WriteLine("f " + builder.ToString());
                    builder.Clear();
                }
            }
        }

        /// <summary>
        /// Загружает объект из файла в формате Wavefront OBJ
        /// </summary>
        /// <param name="fname">Путь к файлу</param>
        /// <returns>Новый объект</returns>
        public static Polyhedron Load(string fname)
        {
            using (StreamReader reader = new StreamReader(fname))
            {
                List<Point3D> allVerticies = new List<Point3D>();
                List<Vector3> allNormals = new List<Vector3>();
                List<Face> faces = new List<Face>();
                string? line;
                string name = "Polyhedron";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    if (parts[0] == "o" && parts.Length == 2)
                    {
                        name = parts[1];
                    }
                    else if (parts[0] == "v" && parts.Length == 4)
                    {
                        float x = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
                        float z = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);
                        allVerticies.Add(new Point3D(x, y, z));
                    }
                    else if (parts[0] == "vn" && parts.Length == 4)
                    {
                        float x = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                        float y = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
                        float z = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);
                        allNormals.Add(new Vector3(x, y, z));
                    }
                    else if (parts[0] == "f" && parts.Length >= 4)
                    {
                        List<int> indices = new List<int>();
                        Vector3? faceNormal = null;

                        for (int i = 1; i < parts.Length; i++)
                        {
                            string[] subParts = parts[i].Split('/');
                            int index = int.Parse(subParts[0]) - 1;
                            indices.Add(index);

                            if (subParts.Length == 3 && !string.IsNullOrEmpty(subParts[2]) && faceNormal == null)
                            {
                                int normalIndex = int.Parse(subParts[2]) - 1;
                                faceNormal = allNormals[normalIndex];
                            }
                        }

                        if (faceNormal != null)
                            faces.Add(new Face(indices.Select(x => allVerticies[x]).ToList(), (Vector3)faceNormal));
                        else
                            faces.Add(new Face(indices.Select(x => allVerticies[x]).ToList()));
                    }
                }
                return new Polyhedron(faces, name);
            }
        }

        public void SetTextureToAllFaces(MyImage texture)
        {
            foreach (var face in faces)
            {
                face.SetTexture(texture);
            }
        }

        public void InvertNormals()
        {
            foreach (Face face in faces)
            {
                face.InvertNormal();
            }
        }
        public object Clone() => new Polyhedron(this);

        private static readonly List<PointF> QuadUVs = new List<PointF>
        {
            new PointF(0.0f, 0.0f),
            new PointF(1.0f, 0.0f),
            new PointF(1.0f, 1.0f),
            new PointF(0.0f, 1.0f)
        };


        
        // Гексаэдр (куб)
        public static Polyhedron CreateHexahedron(float size = 1.0f)
        {
            var halfSize = size / 2;
            var vertices = new List<Point3D>
            {
                new Point3D(-halfSize, -halfSize, halfSize), // 0
                new Point3D(-halfSize, halfSize, halfSize),  // 1
                new Point3D(-halfSize, -halfSize, -halfSize),   // 2
                new Point3D(-halfSize, halfSize, -halfSize),  // 3
                new Point3D(halfSize, -halfSize, halfSize),  // 4
                new Point3D(halfSize, halfSize, halfSize),   // 5
                new Point3D(halfSize, -halfSize, -halfSize),    // 6
                new Point3D(halfSize, halfSize, -halfSize)    // 7
            };

            var faces = new List<Face>
            {
                new Face(new List<Point3D> { vertices[0], vertices[1], vertices[3], vertices[2] }, QuadUVs),
                new Face(new List<Point3D> { vertices[2], vertices[3], vertices[7], vertices[6] }, QuadUVs),
                new Face(new List<Point3D> { vertices[6], vertices[7], vertices[5], vertices[4] }, QuadUVs),
                new Face(new List<Point3D> { vertices[4], vertices[5], vertices[1], vertices[0] }, QuadUVs),
                new Face(new List<Point3D> { vertices[2], vertices[6], vertices[4], vertices[0] }, QuadUVs),
                new Face(new List<Point3D> { vertices[7], vertices[3], vertices[1], vertices[5] }, QuadUVs)
            };

            Polyhedron polyhedron = new Polyhedron(faces);
            polyhedron.Name = "Гексаэдр";

            return polyhedron;
        }

        public static readonly List<Vector3> DefaultFaceColors = new List<Vector3>
        {
            new Vector3(Color.Red),
            new Vector3(Color.Lime),
            new Vector3(Color.Blue),
            new Vector3(Color.Yellow),
            new Vector3(Color.Cyan),
            new Vector3(Color.Magenta),
            new Vector3(Color.Orange),
            new Vector3(Color.Purple),
            new Vector3(Color.Green),
            new Vector3(Color.DarkGray),
            new Vector3(Color.White),
            new Vector3(Color.Black)
        };

        public void ColorFacesAutomatically()
        {
            faces[0].SetColor(new Vector3(Color.Red));
            faces[1].SetColor(new Vector3(Color.Gray));
            faces[2].SetColor(new Vector3(Color.Aqua));
            faces[3].SetColor(new Vector3(Color.White));
            faces[4].SetColor(new Vector3(Color.Purple));
            faces[5].SetColor(new Vector3(Color.White));
        }

        public void ColorFacesMonotonously(Color col)
        {
            for (int i = 0; i < Faces.Count; i++)
            {
                faces[i].SetColor(new Vector3(col));
                faces[i].Material = new Material();
                faces[i].Material.Color = new Vector3(col);
            }
        }

        /// <summary>
        /// Алгоритм Моллера - Трумбора для определения пересечения луча и треугольника
        /// </summary>
        public float TriangleIntersecrion(Ray ray, Point3D p0, Point3D p1, Point3D p2, float eps = 0.0001f)
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
            float v = Vector3.Dot(tvec, qvec) * inv_det;
            if (v < 0 || u + v > 1)
                return 0;

            return Vector3.Dot(e2, qvec) * inv_det;
        }

        public bool FigureIntersection(Ray ray, out float distance, out Vector3 normal)
        {
            distance = float.MaxValue;
            normal = new Vector3(0, 0, 0);
            int indFace = -1; // индекс грани, которую пересек луч

            for(int i = 0; i < Faces.Count; i++)
            {
                Face face = Faces[i];
                float intersect = TriangleIntersecrion(ray, face.Vertices[0], face.vertices[1], face.Vertices[2]);
                if (intersect != 0 && intersect < distance)
                {
                    indFace = i;
                    distance = intersect;
                }

                intersect = TriangleIntersecrion(ray, face.Vertices[0], face.vertices[2], face.Vertices[3]);
                if (intersect != 0 && intersect < distance)
                {
                    indFace = i;
                    distance = intersect;
                }
            }

            if (distance == float.MaxValue)
            {
                distance = 0;
                return false;
            }

            normal = Faces[indFace].NormalVector;
            return true;
        }
    }
}