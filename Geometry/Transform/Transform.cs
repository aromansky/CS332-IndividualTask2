namespace Geometry
{
    public class Transform
    {
        /// <summary>
        /// Создаёт матрицу сдвига
        /// </summary>
        /// <param name="tx">Сдвиг по оси X</param>
        /// <param name="ty">Сдвиг по оси Y</param>
        /// <param name="tz">Сдвиг по оси Z</param>
        /// <returns>Матрица сдвига 4x4</returns>
        public static Matrix CreateTranslationMatrix(float tx, float ty, float tz)
        {
            return new Matrix(new float[,]
            {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {tx, ty, tz, 1 }
            });
        }

        /// <summary>
        /// Создаёт матрицу поворота вокруг оси X
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns>Матрица поворота вокруг оси X 4x4</returns>
        public static Matrix CreateRotationAroundXMatrix(float angle)
        {
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            return new Matrix(new float[,]
            {
                {1, 0, 0, 0 },
                {0, cos, sin, 0 },
                {0, -sin, cos, 0 },
                {0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Создаёт матрицу поворота вокруг оси Y
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns>Матрица поворота вокруг оси Y 4x4</returns>
        public static Matrix CreateRotationAroundYMatrix(float angle)
        {
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            return new Matrix(new float[,]
            {
                {cos, 0, -sin, 0 },
                {0, 1, 0, 0 },
                {sin, 0, cos, 0 },
                {0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Создаёт матрицу поворота вокруг оси Z
        /// </summary>
        /// <param name="angle">Угол поворота в радианах</param>
        /// <returns>Матрица поворота вокруг оси Z 4x4</returns>
        public static Matrix CreateRotationAroundZMatrix(float angle)
        {
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            return new Matrix(new float[,]
            {
                {cos, sin, 0, 0 },
                {-sin, cos, 0, 0 },
                {0, 0, 1, 0 },
                {0, 0, 0, 1 }
            });
        }

        /// <summary>
        /// Создаёт матрицу поворота вокруг произвольной прямой L,
        /// проходящей через точку point с направлением vector.
        /// <param name="point">Точка через которую проходит прямая</param>
        /// <param name="vector">Вектор прямой</param>
        /// <param name="angle">Угол поворота в радианах</param>
        /// </summary>
        public static Matrix CreateRotationAroundLineMatrix(Point3D point, Vector3 vector, float angle)
        {
            Matrix T1 = CreateTranslationMatrix(-point.X, -point.Y, -point.Z);

            Vector3 u = vector.Normalized();

            float l = u.X, m = u.Y, n = u.Z;

            float alpha = (float)Math.Atan2(m, n); // вокруг X
            Matrix Rx_alpha = CreateRotationAroundXMatrix(alpha);

            float beta = (float)Math.Atan2(l, -Math.Sqrt(m * m + n * n)); // вокруг Y
            Matrix Ry_beta = CreateRotationAroundYMatrix(beta);

            Matrix Rz_theta = CreateRotationAroundZMatrix(angle);

            Matrix Ry_minusBeta = CreateRotationAroundYMatrix(-beta);
            Matrix Rx_minusAlpha = CreateRotationAroundXMatrix(-alpha);

            Matrix T2 = CreateTranslationMatrix(point.X, point.Y, point.Z);

            return T1 * Rx_alpha * Ry_beta * Rz_theta * Ry_minusBeta * Rx_minusAlpha * T2;
            // T2 * Rx_minusAlpha * Ry_minusBeta * Rz_theta * Ry_beta * Rx_alpha * T1;
        }

        /// <summary>
        /// Создаёт матрицу масштабирования
        /// </summary>
        /// <param name="scaleX">Масштаб по оси X (от -1 до 1)</param>
        /// <param name="scaleY">Масштаб по оси Y(от -1 до 1)</param>
        /// <param name="scaleZ">Масштаб по оси Z(от -1 до 1)</param>
        /// <returns>Матрица масштабирования 4x4</returns>
        public static Matrix CreateScaleMatrix(float scaleX = 1, float scaleY = 1, float scaleZ = 1)
        {
            return new Matrix(new float[,]
            {
                {scaleX, 0, 0, 0 },
                {0, scaleY, 0, 0 },
                {0, 0, scaleZ, 0 },
                {0, 0, 0, 1 }
            });
        }


        /// <summary>
        /// Применяет 4x4 матрицу к точке.
        /// Возвращает новую точку с результатом преобразования.
        /// </summary>
        public static Point3D Apply(Matrix m, Point3D p)
        {
            var (px, py, pz) = p.GetCoords();

            // создать вектор-столбец 4x1
            var v = new Matrix(new float[,]{
                { px, py, pz, 1 }
                //{ px },
                //{ py },
                //{ pz },
                //{ 1f }
            });

            var r = v * m;

            float rx = r[0, 0];
            float ry = r[0, 1];
            float rz = r[0, 2];
            float rw = r[0, 3];

            if (Math.Abs(rw - 0f) > 1e-6f)
            {
                rx /= rw;
                ry /= rw;
                rz /= rw;
            }

            return new Point3D(rx, ry, rz);
        }

        /// <summary>
        /// Применяет 4x4 матрицу к многограннику.
        /// Возвращает новый многогранник с результатом преобразования.
        /// </summary>
        public static void Apply(Matrix m, Polyhedron p)
        {
            List<Face> transformedFaces = new List<Face>();
            foreach (Face f in p.Faces)
            {
                Face transformedFace = new Face(f.Vertices.Select(x => Apply(m, x)).ToList());
                
                if (f.NormalInverted)
                    transformedFace.InvertNormal();

                for (int i = 0; i < f.Vertices.Count; i++)
                {
                    transformedFace.vertices[i].U = f.Vertices[i].U;
                    transformedFace.vertices[i].V = f.Vertices[i].V;
                }

                transformedFace.SetColor(f.ObjectColor);
                transformedFaces.Add(transformedFace);
                transformedFace.SetTexture(f.Texture);
            }
            p.faces = transformedFaces;
        }
    }
}