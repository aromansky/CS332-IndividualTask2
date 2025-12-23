using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Camera
    {
        public Point3D Position { get; set; }
        public Vector3 ViewDirection { get; set; }
        public float FovDegrees { get; set; } = 90f;
        public ProjectionMode Mode { get; set; } = ProjectionMode.Perspective;
        public Point3D Target { get; set; } = new Point3D(0, 0, 0);


        public Matrix ViewMatrix { get; private set; }
        public Vector3 Up { get; private set; } = new Vector3(0, 1, 0);
        public float NearPlane { get; set; } = 0.1f;
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        private Vector3 n;
        private Vector3 u;
        private Vector3 v;

        public Camera(Point3D position, Point3D target, int screenWidth, int screenHeight)
        {
            Position = position;
            ViewDirection = new Vector3(Position, Target).Normalized();

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            ComputeBasisVectors();
            ComputeViewMatrix();
        }

        public void SetPosition(Point3D newPosition)
        {
            Position = newPosition;
            ViewDirection = new Vector3(Position, Target).Normalized();
            ComputeBasisVectors();
            ComputeViewMatrix();
        }

        private void ComputeBasisVectors()
        {
            n = -ViewDirection;
            u = Vector3.Cross(Up, n).Normalized();
            v = Vector3.Cross(n, u).Normalized();
        }

        private void SetParallelCameraOrientation(float angleXDeg, float angleYDeg)
        {
            float ax = angleXDeg * (float)Math.PI / 180f;
            float ay = angleYDeg * (float)Math.PI / 180f;

            ViewDirection = new Vector3(
                (float)(Math.Cos(ax) * Math.Sin(ay)),
                (float)(Math.Sin(ax)),
                (float)(Math.Cos(ax) * Math.Cos(ay))
            ).Normalized();

            float distance = 1000f;
            Vector3 v = ViewDirection * distance;
            Position = new Point3D(Target.X - v.X, Target.Y - v.Y, Target.Z - v.Z);

            n = -ViewDirection;
            u = Vector3.Cross(Up, n).Normalized();
            v = Vector3.Cross(n, u).Normalized();

            ViewMatrix = new Matrix(new float[,]
            {
                { u.X, v.X, n.X, 0 },
                { u.Y, v.Y, n.Y, 0 },
                { u.Z, v.Z, n.Z, 0 },
                { -Vector3.Dot(Position.ToVector3(), u),
                  -Vector3.Dot(Position.ToVector3(), v),
                  -Vector3.Dot(Position.ToVector3(), n),
                  1 }
            });
        }


        private void ComputeViewMatrix()
        {
            float angleX, angleY;
            Matrix Ry, Rx;

            switch (Mode)
            {
                case ProjectionMode.Perspective:
                    ViewMatrix = new Matrix(new float[,]
                    {
                        { u.X, v.X, n.X, 0 },
                        { u.Y, v.Y, n.Y, 0 },
                        { u.Z, v.Z, n.Z, 0 },
                        { -Vector3.Dot(Position.ToVector3(), u),
                          -Vector3.Dot(Position.ToVector3(), v),
                          -Vector3.Dot(Position.ToVector3(), n),
                          1 }
                    });
                    break;

                case ProjectionMode.Trimetric:
                    angleX = 45;
                    angleY = 30;

                    Ry = Transform.CreateRotationAroundYMatrix(angleY);
                    Rx = Transform.CreateRotationAroundXMatrix(angleX);

                    ViewMatrix = Ry * Rx;
                    SetParallelCameraOrientation(angleX, angleY);
                    break;

                case ProjectionMode.Dimetric:
                    angleX = 26.23f;
                    angleY = -29.52f;

                    Ry = Transform.CreateRotationAroundYMatrix(angleY);
                    Rx = Transform.CreateRotationAroundXMatrix(angleX);

                    ViewMatrix = Ry * Rx;
                    SetParallelCameraOrientation(angleX, angleY);
                    break;

                case ProjectionMode.Isometric:
                    angleX = 35.26f;
                    angleY = 45;

                    Ry = Transform.CreateRotationAroundYMatrix(angleY);
                    Rx = Transform.CreateRotationAroundXMatrix(angleX);

                    ViewMatrix = Ry * Rx;
                    SetParallelCameraOrientation(angleX, angleY);
                    break;
            }


        }

        public void SetMode(ProjectionMode mode)
        {
            Mode = mode;
            ComputeViewMatrix();
        }

        public Point3D ProjectPoint(Point3D point)
        {
            Point3D transformedPoint = Transform.Apply(ViewMatrix, point);
            return transformedPoint;
        }

        public Point3D[] ProjectFace(Face face)
        {
            return face.Vertices.Select(x => ProjectPoint(x)).ToArray();
        }

        public Point3D[][] ProjectPolyhedron(Polyhedron polyhedron)
        {
            return polyhedron.Faces.Select(x => ProjectFace(x)).ToArray();
        }

        public PointF? ProjectPoint2D(Point3D point)
        {
            Point3D cameraPoint = ProjectPoint(point);
            float screenX = 0, screenY = 0;
            float orthoScale = 100f;

            switch (Mode)
            {
                case ProjectionMode.Perspective:
                    if (-cameraPoint.Z < NearPlane)
                        return null;

                    float fovRad = FovDegrees * (float)Math.PI / 180f;

                    float viewHeight = 2f * NearPlane * (float)Math.Tan(fovRad / 2f);

                    // Соотношение сторон экрана
                    float aspect = (float)ScreenWidth / ScreenHeight;
                    float viewWidth = viewHeight * aspect;

                    float x = NearPlane * cameraPoint.X / -cameraPoint.Z;
                    float y = NearPlane * cameraPoint.Y / -cameraPoint.Z;

                    screenX = (x / viewWidth + 0.5f) * ScreenWidth;
                    screenY = (0.5f - y / viewHeight) * ScreenHeight;
                    break;
                case ProjectionMode.Isometric:
                    {
                        // Масштабируем координаты, так как глубина не влияет
                        screenX = (cameraPoint.X * orthoScale) + ScreenWidth / 2f;
                        screenY = (-cameraPoint.Y * orthoScale) + ScreenHeight / 2f;
                        break;
                    }

                case ProjectionMode.Dimetric:
                    {
                        screenX = (cameraPoint.X * orthoScale) + ScreenWidth / 2f;
                        screenY = (-cameraPoint.Y * orthoScale) + ScreenHeight / 2f;
                        break;
                    }

                case ProjectionMode.Trimetric:
                    {
                        screenX = (cameraPoint.X * orthoScale) + ScreenWidth / 2f;
                        screenY = (-cameraPoint.Y * orthoScale) + ScreenHeight / 2f;
                        break;
                    }
            }
            return new PointF(screenX, screenY);
        }

        public PointF[] ProjectFace2D(Face face)
        {
            var points2D = face.Vertices
                                .Select(x => ProjectPoint2D(x))
                                .Where(p => p.HasValue)
                                .Select(p => p.Value)
                                .ToArray();

            return points2D.Length > 0 ? points2D : null;
        }

        public PointF[][] Project(Polyhedron polyhedron)
        {
            return polyhedron.Faces.Where(x => x.IsFrontFace(this)).Select(x => ProjectFace2D(x)).ToArray();
        }

        public Ray GetRay(int i, int j)
        {
            float aspect = (float)ScreenWidth / ScreenHeight;
            float fovRad = FovDegrees * (float)Math.PI / 180f;

            // Высота и ширина экранного прямоугольника на расстоянии NearPlane
            float halfHeight = (float)Math.Tan(fovRad / 2f) * NearPlane;
            float halfWidth = halfHeight * aspect;

            // Смещение внутри пикселя (+0.5f для центра пикселя)
            float x = (2.0f * (i + 0.5f) / ScreenWidth - 1.0f) * halfWidth;
            float y = (1.0f - 2.0f * (j + 0.5f) / ScreenHeight) * halfHeight;

            // Формируем направление луча используя базис камеры
            // Используем -n, так как n направлен "от сцены" к камере
            Vector3 rayDir = (u * x) + (v * y) - (n * NearPlane);

            return new Ray(Position, rayDir.Normalized());
        }
    }
}
