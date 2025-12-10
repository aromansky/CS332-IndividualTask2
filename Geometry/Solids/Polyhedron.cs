using System.Drawing;
using System.Text;

namespace Geometry
{
    public class Polyhedron : ICloneable
    {
        public String Name { get; set; } = "Polyhedron";
        internal List<Face> faces;
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

        private static readonly List<PointF> TriangleUVs = new List<PointF>
        {
            new PointF(0.0f, 0.0f),
            new PointF(1.0f, 0.0f),
            new PointF(0.0f, 1.0f)
        };

        private static readonly List<PointF> QuadUVs = new List<PointF>
        {
            new PointF(0.0f, 0.0f),
            new PointF(1.0f, 0.0f),
            new PointF(1.0f, 1.0f),
            new PointF(0.0f, 1.0f)
        };


        // Тетраэдр
        public static Polyhedron CreateTetrahedron(float size = 1.0f)
        {
            var vertices = new List<Point3D>
            {
                new Point3D(size, size, size),       // 0
                new Point3D(-size, -size, size),     // 1
                new Point3D(-size, size, -size),     // 2
                new Point3D(size, -size, -size)      // 3
            };

            var faces = new List<Face>
            {
                new Face(new List<Point3D> { vertices[0], vertices[2], vertices[1] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[0], vertices[3], vertices[2] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[0], vertices[1], vertices[3] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[1], vertices[2], vertices[3] }, TriangleUVs)
            };

            Polyhedron polyhedron = new Polyhedron(faces);
            polyhedron.Name = "Тетраэдр";

            return polyhedron;
        }

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

        // Октаэдр
        public static Polyhedron CreateOctahedron(float size = 1.0f)
        {
            var vertices = new List<Point3D>
            {
                new Point3D(size, 0, 0),   // 0
                new Point3D(-size, 0, 0),  // 1
                new Point3D(0, size, 0),   // 2
                new Point3D(0, -size, 0),  // 3
                new Point3D(0, 0, size),   // 4
                new Point3D(0, 0, -size)   // 5
            };

            var faces = new List<Face>
            {
                new Face(new List<Point3D> { vertices[4], vertices[0], vertices[2] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[4], vertices[2], vertices[1] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[4], vertices[1], vertices[3] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[4], vertices[3], vertices[0] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[5], vertices[2], vertices[0] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[5], vertices[1], vertices[2] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[5], vertices[3], vertices[1] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[5], vertices[0], vertices[3] }, TriangleUVs)
            };

            Polyhedron polyhedron = new Polyhedron(faces);
            polyhedron.Name = "Октаэдр";

            return polyhedron;
        }

        // Икосаэдр
        public static Polyhedron CreateIcosahedron(float size = 1.0f)
        {
            float phi = (1.0f + (float)Math.Sqrt(5)) / 2.0f; // золотое сечение
            float scale = size / (float)Math.Sqrt(1 + phi * phi);

            var vertices = new List<Point3D>();

            // 12 вершин икосаэдра
            var coords = new[]
            {
                (0.0f, 1.0f, phi), (0.0f, -1.0f, phi), (0.0f, 1.0f, -phi), (0.0f, -1.0f, -phi),
                (1.0f, phi, 0.0f), (-1.0f, phi, 0.0f), (1.0f, -phi, 0.0f), (-1.0f, -phi, 0.0f),
                (phi, 0.0f, 1.0f), (phi, 0.0f, -1.0f), (-phi, 0.0f, 1.0f), (-phi, 0.0f, -1.0f)
            };

            foreach (var (x, y, z) in coords)
            {
                vertices.Add(new Point3D(x * scale, y * scale, z * scale));
            }

            var faces = new List<Face>
            {
                // 20 треугольных граней
                new Face(new List<Point3D> { vertices[0], vertices[1], vertices[8] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[0], vertices[8], vertices[4] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[0], vertices[4], vertices[5] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[0], vertices[5], vertices[10] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[0], vertices[10], vertices[1] }, TriangleUVs),

                new Face(new List<Point3D> { vertices[1], vertices[6], vertices[8] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[8], vertices[6], vertices[9] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[8], vertices[9], vertices[4] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[4], vertices[9], vertices[2] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[4], vertices[2], vertices[5] }, TriangleUVs),

                new Face(new List<Point3D> { vertices[5], vertices[2], vertices[11] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[5], vertices[11], vertices[10] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[10], vertices[11], vertices[7] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[10], vertices[7], vertices[1] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[1], vertices[7], vertices[6] }, TriangleUVs),

                new Face(new List<Point3D> { vertices[3], vertices[6], vertices[7] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[3], vertices[7], vertices[11] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[3], vertices[11], vertices[2] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[3], vertices[2], vertices[9] }, TriangleUVs),
                new Face(new List<Point3D> { vertices[3], vertices[9], vertices[6] }, TriangleUVs)
            };

            Polyhedron polyhedron = new Polyhedron(faces);
            polyhedron.Name = "Икосаэдр";

            return polyhedron;
        }

        private static readonly List<PointF> PentagonUVs = new List<PointF>
        {
            new PointF(0.0f, 0.0f),
            new PointF(1.0f, 0.0f),
            new PointF(1.0f, 1.0f),
            new PointF(0.5f, 1.0f),
            new PointF(0.0f, 1.0f)
        };
        // Додекаэдр
        public static Polyhedron CreateDodecahedron(float size = 1.0f)
        {
            // Золотое сечение
            float phi = (1.0f + (float)Math.Sqrt(5.0)) / 2.0f;
            float invPhi = 1.0f / phi;

            var vertices = new List<Point3D>
            {
                //(±1, ±1, ±1)
                new Point3D( 1,  1,  1),  // 0
                new Point3D( 1,  1, -1),  // 1
                new Point3D( 1, -1,  1),  // 2
                new Point3D( 1, -1, -1),  // 3
                new Point3D(-1,  1,  1),  // 4
                new Point3D(-1,  1, -1),  // 5
                new Point3D(-1, -1,  1),  // 6
                new Point3D(-1, -1, -1),  // 7

                // (0, ±φ, ±1/φ)
                new Point3D(0,  phi,  invPhi),  // 8
                new Point3D(0,  phi, -invPhi),  // 9
                new Point3D(0, -phi,  invPhi),  // 10
                new Point3D(0, -phi, -invPhi),  // 11

                //(±1/φ, 0, ±φ)
                new Point3D( invPhi, 0, phi),  // 12
                new Point3D(-invPhi, 0, phi),  // 13
                new Point3D( invPhi, 0, -phi),  // 14
                new Point3D(-invPhi, 0, -phi),  // 15

                // (±φ, ±1/φ, 0)
                new Point3D( phi,  invPhi, 0),  // 16
                new Point3D(-phi,  invPhi, 0),  // 17
                new Point3D( phi, -invPhi, 0),  // 18
                new Point3D(-phi, -invPhi, 0)   // 19
            };

            vertices = vertices
                .Select(v =>
                {
                    float len = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
                    return new Point3D(v.X / len * size, v.Y / len * size, v.Z / len * size);
                })
                .ToList();

            // Грани (12 пятиугольников)
            var faces = new List<Face>
            {
                new Face(new List<Point3D> { vertices[0], vertices[12], vertices[2], vertices[18], vertices[16] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[0], vertices[16], vertices[1], vertices[9], vertices[8] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[0], vertices[8], vertices[4], vertices[13], vertices[12] }, PentagonUVs),

                new Face(new List<Point3D> { vertices[10], vertices[2], vertices[12], vertices[13], vertices[6] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[10], vertices[6], vertices[19], vertices[7], vertices[11] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[10], vertices[11], vertices[3], vertices[18], vertices[2] }, PentagonUVs),

                new Face(new List<Point3D> { vertices[5], vertices[17], vertices[4], vertices[8], vertices[9] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[5], vertices[9], vertices[1], vertices[14], vertices[15] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[5], vertices[15], vertices[7], vertices[19], vertices[17] }, PentagonUVs),

                new Face(new List<Point3D> { vertices[3], vertices[14], vertices[1], vertices[16], vertices[18] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[3], vertices[11], vertices[7], vertices[15], vertices[14] }, PentagonUVs),
                new Face(new List<Point3D> { vertices[4], vertices[17], vertices[19], vertices[6], vertices[13] }, PentagonUVs)
            };

            Polyhedron polyhedron = new Polyhedron(faces);
            polyhedron.Name = "Додекаэдр";

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
            if (this.Name == "Гексаэдр" && Faces.Count == 6)
            {
                faces[0].SetColor(new Vector3(Color.Red));
                faces[1].SetColor(new Vector3(Color.White));
                faces[2].SetColor(new Vector3(Color.Aqua));
                faces[3].SetColor(new Vector3(Color.White));
                faces[4].SetColor(new Vector3(Color.White));
                faces[5].SetColor(new Vector3(Color.White));

                return;
            }
            for (int i = 0; i < Faces.Count; i++)
            {
                faces[i].SetColor(DefaultFaceColors[i % DefaultFaceColors.Count]);
            }
        }

        public void ColorFacesMonotonously(Color col)
        {
            for (int i = 0; i < Faces.Count; i++)
            {
                faces[i].SetColor(new Vector3(col));
            }
        }
    }
}