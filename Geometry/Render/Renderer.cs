using System;
using System.Drawing;

namespace Geometry
{
    public enum RenderMode
    {
        None,
        Gouraud,
        Phong,
        Texture
    }
    public class Renderer
    {
        private readonly Camera camera;
        private readonly int width;
        private readonly int height;
        private readonly float[,] zBuffer;
        private MyImage myImage;

        private RenderMode renderMode = RenderMode.None;

        public LightSource Light { get; set; }

        public Renderer(Camera camera, int width, int height)
        {
            this.camera = camera;
            this.width = width;
            this.height = height;
            myImage = MyImage.CreateWhiteImage(width, height);
            zBuffer = new float[width, height];
        }

        public Renderer(Camera camera, int width, int height, LightSource lightSource)
        {
            this.camera = camera;
            this.width = width;
            this.height = height;
            myImage = MyImage.CreateWhiteImage(width, height);
            zBuffer = new float[width, height];

            this.Light = lightSource;
        }

        public void SetMode(RenderMode mode)
        {
            this.renderMode = mode;
        }

        public void Clear(Color background)
        {
            myImage.Lock();
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    myImage.SetPixel(x, y, background);
                    zBuffer[x, y] = float.MaxValue;
                }
            myImage.Unlock();
        }

        public void Render(Polyhedron polyhedron, Color color)
        {
            myImage.Lock();
            foreach (var face in polyhedron.Faces)
            {
                if (!face.IsFrontFace(camera))
                    continue;
                DrawFaceZ(face, color);
            }
            myImage.Unlock();
        }


        public void Render(Polyhedron polyhedron)
        {
            myImage.Lock();
            foreach (var face in polyhedron.Faces)
            {
                if (!face.IsFrontFace(camera))
                    continue;
                switch (renderMode)
                {
                    case RenderMode.Gouraud:
                        {
                            Vector3 faceObjectColor = face.ObjectColor;

                            List<Vertex> shadedVertices = face.Vertices.Select(v => {
                                v.Color = ShadingUtils.CalculateLambertColor(this.Light, v, faceObjectColor);
                                return v;
                            }).ToList();
                            DrawFaceGouraudZ(shadedVertices);
                            break;
                        }
                    case RenderMode.Phong:
                        {
                            Vector3 faceObjectColor = face.ObjectColor;
                            
                            DrawFacePhongZ(face.Vertices);
                            break;
                        }
                    case RenderMode.None:
                        DrawFaceZ(face, face.ObjectColor);
                        break;
                    case RenderMode.Texture:
                        DrawFaceTextureZ(face);
                        break;
                        
                }
            }
            myImage.Unlock();
        }
        public void DrawFaceTextureZ(Face face)
        {
            if (face.Vertices.Count < 3 || face.Texture == null) return;

            Vertex v0 = face.Vertices[0];

            for (int i = 1; i < face.Vertices.Count - 1; i++)
            {
                Vertex v1 = face.Vertices[i];
                Vertex v2 = face.Vertices[i + 1];

                DrawTriangleTextureZ(v0, v1, v2, face.Texture);
            }
        }

        /// <summary>
        /// Отрисовывает треугольник с перспективно-корректным текстурированием и Z-буфером.
        /// </summary>
        /// <summary>
        /// Отрисовывает треугольник с перспективно-корректным текстурированием и Z-буфером.
        /// Использует билинейную фильтрацию для сглаживания текстур.
        /// </summary>
        private void DrawTriangleTextureZ(Vertex v0, Vertex v1, Vertex v2, MyImage texture)
        {
            texture.Lock();
            // Проекция в 2D координаты экрана
            PointF? p0 = camera.ProjectPoint2D(v0);
            PointF? p1 = camera.ProjectPoint2D(v1);
            PointF? p2 = camera.ProjectPoint2D(v2);

            if (!p0.HasValue || !p1.HasValue || !p2.HasValue)
                return;

            PointF a = p0.Value;
            PointF b = p1.Value;
            PointF c = p2.Value;

            int minX = (int)Math.Max(0, Math.Min(a.X, Math.Min(b.X, c.X)));
            int maxX = (int)Math.Min(width - 1, Math.Max(a.X, Math.Max(b.X, c.X)));
            int minY = (int)Math.Max(0, Math.Min(a.Y, Math.Min(b.Y, c.Y)));
            int maxY = (int)Math.Min(height - 1, Math.Max(a.Y, Math.Max(b.Y, c.Y)));

            // Проекции вершин для Z-буфера (глубина)
            Point3D p0_3 = camera.ProjectPoint(v0);
            Point3D p1_3 = camera.ProjectPoint(v1);
            Point3D p2_3 = camera.ProjectPoint(v2);

            // Предварительные вычисления 1/Z, U/Z, V/Z
            float z_inv0 = 1.0f / p0_3.Z;
            float z_inv1 = 1.0f / p1_3.Z;
            float z_inv2 = 1.0f / p2_3.Z;

            float u_div_z0 = v0.U * z_inv0;
            float u_div_z1 = v1.U * z_inv1;
            float u_div_z2 = v2.U * z_inv2;

            float v_div_z0 = v0.V * z_inv0;
            float v_div_z1 = v1.V * z_inv1;
            float v_div_z2 = v2.V * z_inv2;

            float area = EdgeFunction(a, b, c);
            if (Math.Abs(area) < 1e-6f)
            {
                texture.Unlock();
                return;
            }

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var p = new PointF(x + 0.5f, y + 0.5f);

                    float w0 = EdgeFunction(b, c, p);
                    float w1 = EdgeFunction(c, a, p);
                    float w2 = EdgeFunction(a, b, p);

                    // Проверка на принадлежность треугольнику
                    if (w0 * area >= 0 && w1 * area >= 0 && w2 * area >= 0)
                    {
                        // Барицентрические координаты
                        w0 /= area;
                        w1 /= area;
                        w2 /= area;

                        // Интерполяция 1/Z (Z-buffer)
                        float z_inv_interpolated = w0 * z_inv0 + w1 * z_inv1 + w2 * z_inv2;

                        // Z для сравнения в zBuffer (перспективная или ортогональная глубина)
                        float z_compare;
                        if (camera.Mode == ProjectionMode.Perspective)
                        {
                            z_compare = z_inv_interpolated;
                        }
                        else
                        {
                            z_compare = w0 * p0_3.Z + w1 * p1_3.Z + w2 * p2_3.Z;
                        }

                        if (z_compare < zBuffer[x, y])
                        {
                            zBuffer[x, y] = z_compare;

                            // Перспективно-корректная интерполяция U и V
                            float u_div_z_interpolated = w0 * u_div_z0 + w1 * u_div_z1 + w2 * u_div_z2;
                            float v_div_z_interpolated = w0 * v_div_z0 + w1 * v_div_z1 + w2 * v_div_z2;

                            // Восстановление U и V: (U/Z) / (1/Z) = U
                            float u_final = u_div_z_interpolated / z_inv_interpolated;
                            float v_final = v_div_z_interpolated / z_inv_interpolated;

                            // Преобразование UV (0..1) в float-координаты текселя (0..W, 0..H)
                            float tex_coord_x = u_final * texture.Width;
                            float tex_coord_y = v_final * texture.Height;

                            // Ограничение координат (исключаем выход за пределы, но сохраняем дробную часть)
                            tex_coord_x = Math.Clamp(tex_coord_x - 1, 0.0f, texture.Width - 1e-6f);
                            tex_coord_y = Math.Clamp(tex_coord_y - 1, 0.0f, texture.Height - 1e-6f);

                            // Определение 4 соседних текселей
                            int x0 = (int)Math.Floor(tex_coord_x);
                            int y0 = (int)Math.Floor(tex_coord_y);

                            int x1 = Math.Min(x0 + 1, texture.Width - 1);
                            int y1 = Math.Min(y0 + 1, texture.Height - 1);

                            // Вычисление дробных частей (весов интерполяции)
                            float u_frac = tex_coord_x - x0;
                            float v_frac = tex_coord_y - y0;

                            // Выборка 4 текселей
                            Color c00 = texture.GetRGB(x0, y0); // Top-Left
                            Color c10 = texture.GetRGB(x1, y0); // Top-Right
                            Color c01 = texture.GetRGB(x0, y1); // Bottom-Left
                            Color c11 = texture.GetRGB(x1, y1); // Bottom-Right

                            // Билинейная интерполяция

                            // Интерполяция по U (горизонтальная)
                            // T_top = lerp(C00, C10, u_frac)
                            float R_top = c00.R * (1 - u_frac) + c10.R * u_frac;
                            float G_top = c00.G * (1 - u_frac) + c10.G * u_frac;
                            float B_top = c00.B * (1 - u_frac) + c10.B * u_frac;

                            // T_bottom = lerp(C01, C11, u_frac)
                            float R_bottom = c01.R * (1 - u_frac) + c11.R * u_frac;
                            float G_bottom = c01.G * (1 - u_frac) + c11.G * u_frac;
                            float B_bottom = c01.B * (1 - u_frac) + c11.B * u_frac;

                            // Интерполяция по V (вертикальная)
                            int R_final = (int)Math.Round(R_top * (1 - v_frac) + R_bottom * v_frac);
                            int G_final = (int)Math.Round(G_top * (1 - v_frac) + G_bottom * v_frac);
                            int B_final = (int)Math.Round(B_top * (1 - v_frac) + B_bottom * v_frac);

                            Color pixelColor = Color.FromArgb(R_final, G_final, B_final);

                            // Запись цвета в MyImage
                            myImage.SetPixel(x, y, pixelColor);
                        }
                    }
                }
            }
            texture.Unlock();
        }

        public void DrawFacePhongZ(List<Vertex> faceVertices)
        {
            if (faceVertices.Count < 3) return;

            Vertex v0 = faceVertices[0];
            for (int i = 1; i < faceVertices.Count - 1; i++)
            {
                Vertex v1 = faceVertices[i];
                Vertex v2 = faceVertices[i + 1];
                DrawTrianglePhongZ(v0, v1, v2);
            }
        }

        private void DrawTrianglePhongZ(Vertex v0, Vertex v1, Vertex v2)
        {
            PointF? p0 = camera.ProjectPoint2D(v0);
            PointF? p1 = camera.ProjectPoint2D(v1);
            PointF? p2 = camera.ProjectPoint2D(v2);

            if (!p0.HasValue || !p1.HasValue || !p2.HasValue)
                return;

            PointF a = p0.Value;
            PointF b = p1.Value;
            PointF c = p2.Value;

            int minX = (int)Math.Max(0, Math.Min(a.X, Math.Min(b.X, c.X)));
            int maxX = (int)Math.Min(width - 1, Math.Max(a.X, Math.Max(b.X, c.X)));
            int minY = (int)Math.Max(0, Math.Min(a.Y, Math.Min(b.Y, c.Y)));
            int maxY = (int)Math.Min(height - 1, Math.Max(a.Y, Math.Max(b.Y, c.Y)));

            // Проекции вершин для Z-буфера и интерполяции позиции
            Point3D p0_3 = camera.ProjectPoint(v0);
            Point3D p1_3 = camera.ProjectPoint(v1);
            Point3D p2_3 = camera.ProjectPoint(v2);

            // Начальные 3D позиции вершин 
            Point3D pos0 = v0;
            Point3D pos1 = v1;
            Point3D pos2 = v2;

            float area = EdgeFunction(a, b, c);
            if (Math.Abs(area) < 1e-6f) return;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var p = new PointF(x + 0.5f, y + 0.5f);

                    float w0 = EdgeFunction(b, c, p);
                    float w1 = EdgeFunction(c, a, p);
                    float w2 = EdgeFunction(a, b, p);

                    if (w0 * area >= 0 && w1 * area >= 0 && w2 * area >= 0)
                    {
                        // Барицентрические координаты
                        w0 /= area;
                        w1 /= area;
                        w2 /= area;

                        // 1. Интерполяция Z (глубина)
                        float z = w0 * p0_3.Z + w1 * p1_3.Z + w2 * p2_3.Z;

                        if (camera.Mode == ProjectionMode.Perspective)
                        {
                            z = 1 / z;
                        }

                        if (z < zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;

                            // 2. Интерполяция 3D-позиции
                            Point3D interpolatedPosition = pos0 * w0 + pos1 * w1 + pos2 * w2;

                            // 3. Интерполяция Нормали 
                            Vector3 interpolatedNormal = v0.Normal * w0 + v1.Normal * w1 + v2.Normal * w2;

                            // 4. Нормализация интерполированной нормали
                            Vector3 N_pixel = interpolatedNormal.Normalized();

                            // 5. Вычисление цвета с помощью модели Фонга с туншейдингом
                            Vector3 faceObjectColor = v0.Color;
                            Vector3 materialKs = v0.MaterialKs;
                            float shininess = v0.Shininess;

                            Vector3 colorVector = ShadingUtils.CalculatePhongColor(
                                this.Light,
                                new Vertex(interpolatedPosition) { Normal = N_pixel },
                                faceObjectColor,
                                camera.Position,
                                materialKs,
                                shininess
                            );

                            Color color = Color.FromArgb(
                                Math.Min(255, (int)(colorVector.X * 255)),
                                Math.Min(255, (int)(colorVector.Y * 255)),
                                Math.Min(255, (int)(colorVector.Z * 255))
                            );

                            myImage.SetPixel(x, y, color);
                        }
                    }
                }
            }
        }


        public void DrawFaceGouraudZ(List<Vertex> shadedVertices)
        {
            if (shadedVertices.Count < 3) return;

            Vertex v0 = shadedVertices[0];
            for (int i = 1; i < shadedVertices.Count - 1; i++)
            {
                Vertex v1 = shadedVertices[i];
                Vertex v2 = shadedVertices[i + 1];
                DrawTriangleGouraudZ(v0, v1, v2);
            }
        }

        private void DrawTriangleGouraudZ(Vertex v0, Vertex v1, Vertex v2)
        {
            var p0 = camera.ProjectPoint2D(v0);
            var p1 = camera.ProjectPoint2D(v1);
            var p2 = camera.ProjectPoint2D(v2);

            if (!p0.HasValue || !p1.HasValue || !p2.HasValue)
                return;

            PointF a = p0.Value;
            PointF b = p1.Value;
            PointF c = p2.Value;

            int minX = (int)Math.Max(0, Math.Min(a.X, Math.Min(b.X, c.X)));
            int maxX = (int)Math.Min(width - 1, Math.Max(a.X, Math.Max(b.X, c.X)));
            int minY = (int)Math.Max(0, Math.Min(a.Y, Math.Min(b.Y, c.Y)));
            int maxY = (int)Math.Min(height - 1, Math.Max(a.Y, Math.Max(b.Y, c.Y)));

            var p0_3 = camera.ProjectPoint(v0);
            var p1_3 = camera.ProjectPoint(v1);
            var p2_3 = camera.ProjectPoint(v2);

            float area = EdgeFunction(a, b, c);
            if (Math.Abs(area) < 1e-6f) return;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var p = new PointF(x + 0.5f, y + 0.5f);

                    float w0 = EdgeFunction(b, c, p);
                    float w1 = EdgeFunction(c, a, p);
                    float w2 = EdgeFunction(a, b, p);

                    if (w0 * area >= 0 && w1 * area >= 0 && w2 * area >= 0)
                    {
                        w0 /= area;
                        w1 /= area;
                        w2 /= area;

                        float z = w0 * p0_3.Z + w1 * p1_3.Z + w2 * p2_3.Z;

                        if (camera.Mode == ProjectionMode.Perspective)
                        {
                            z = 1 / z;
                        }

                        if (z < zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;

                            float r = w0 * v0.Color.X + w1 * v1.Color.X + w2 * v2.Color.X;
                            float g = w0 * v0.Color.Y + w1 * v1.Color.Y + w2 * v2.Color.Y;
                            float bCol = w0 * v0.Color.Z + w1 * v1.Color.Z + w2 * v2.Color.Z;

                            Color color = Color.FromArgb(
                                Math.Min(255, (int)(r * 255)),
                                Math.Min(255, (int)(g * 255)),
                                Math.Min(255, (int)(bCol * 255))
                            );

                            myImage.SetPixel(x, y, color);
                        }
                    }
                }
            }
        }

        private void DrawFaceZ(Face face, Vector3 color)
        {
            int n = face.Vertices.Count;
            Color col = Color.FromArgb(
                (int)(color.X * 255),
                (int)(color.Y * 255),
                (int)(color.Z * 255)
            );

            if (n == 2)
            {
                DrawLineZ(face.Vertices[0], face.Vertices[1], col);
            }
            else if (n > 2)
            {
                for (int i = 1; i < n - 1; i++)
                {
                    DrawTriangleZ(face.Vertices[0], face.Vertices[i], face.Vertices[i + 1], col);
                }
            }
        }

        private void DrawFaceZ(Face face, Color color)
        {
            int n = face.Vertices.Count;
            if (n == 2)
            {
                DrawLineZ(face.Vertices[0], face.Vertices[1], color);
            }
            else if (n > 2)
            {
                for (int i = 1; i < n - 1; i++)
                {
                    DrawTriangleZ(face.Vertices[0], face.Vertices[i], face.Vertices[i + 1], color);
                }
            }
        }
        private void DrawLineZ(Point3D v0, Point3D v1, Color color)
        {
            var p0 = camera.ProjectPoint2D(v0);
            var p1 = camera.ProjectPoint2D(v1);

            if (!p0.HasValue || !p1.HasValue) return;

            PointF a = p0.Value;
            PointF b = p1.Value;

            int x0 = (int)a.X, y0 = (int)a.Y;
            int x1 = (int)b.X, y1 = (int)b.Y;

            // простая линия Брезенхема с z-буфером
            int dx = Math.Abs(x1 - x0), dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                {
                    float t = dx != 0 ? (float)(x0 - a.X) / (b.X - a.X) : (float)(y0 - a.Y) / (b.Y - a.Y);
                    float z = v0.Z * (1 - t) + v1.Z * t;

                    if (z < zBuffer[x0, y0])
                    {
                        zBuffer[x0, y0] = z;
                        myImage.SetPixel(x0, y0, color);
                    }
                }

                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
        }

        private void DrawTriangleZ(Point3D v0, Point3D v1, Point3D v2, Color color)
        {
            var p0 = camera.ProjectPoint2D(v0);
            var p1 = camera.ProjectPoint2D(v1);
            var p2 = camera.ProjectPoint2D(v2);

            if (!p0.HasValue || !p1.HasValue || !p2.HasValue)
                return;

            PointF a = p0.Value;
            PointF b = p1.Value;
            PointF c = p2.Value;

            int minX = (int)Math.Max(0, Math.Min(a.X, Math.Min(b.X, c.X)));
            int maxX = (int)Math.Min(width - 1, Math.Max(a.X, Math.Max(b.X, c.X)));
            int minY = (int)Math.Max(0, Math.Min(a.Y, Math.Min(b.Y, c.Y)));
            int maxY = (int)Math.Min(height - 1, Math.Max(a.Y, Math.Max(b.Y, c.Y)));

            var p0_3 = camera.ProjectPoint(v0);
            var p1_3 = camera.ProjectPoint(v1);
            var p2_3 = camera.ProjectPoint(v2);

            float area = EdgeFunction(a, b, c);
            if (Math.Abs(area) < 1e-6f) return;

            // Рендерим треугольник
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var p = new PointF(x + 0.5f, y + 0.5f);

                    float w0 = EdgeFunction(b, c, p);
                    float w1 = EdgeFunction(c, a, p);
                    float w2 = EdgeFunction(a, b, p);

                    if (w0 * area >= 0 && w1 * area >= 0 && w2 * area >= 0)
                    {
                        w0 /= area;
                        w1 /= area;
                        w2 /= area;

                        float z = w0 * p0_3.Z + w1 * p1_3.Z + w2 * p2_3.Z;

                        if (camera.Mode == ProjectionMode.Perspective)
                        {
                            z = 1 / z;
                        }

                        if (z < zBuffer[x, y])
                        {
                            zBuffer[x, y] = z;
                            myImage.SetPixel(x, y, color);
                        }
                    }
                }
            }
        }

            
        private static float EdgeFunction(PointF a, PointF b, PointF c)
        {
            return (c.X - a.X) * (b.Y - a.Y) - (c.Y - a.Y) * (b.X - a.X);
        }

        public Bitmap GetImage() => myImage.Img;
    }
}
