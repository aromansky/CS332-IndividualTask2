using Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IndividualTask2
{
    public partial class Scene : Form
    {
        private Camera cam;
        public List<Polyhedron> polyhedrons = new List<Polyhedron>();
        internal Renderer renderer;

        private LightSource firstLightSource;
        private LightSource secondLightSource;

        public Polyhedron firstCube;
        public Polyhedron secondCube;

        private bool useZBufferRendering = true;

        private void CreateEmptyRoom()
        {
            Polyhedron poly = Polyhedron.CreateHexahedron(2);
            Transform.Apply(Transform.CreateScaleMatrix(10, 10, 10), poly);

            poly.ColorFacesAutomatically();
            poly.InvertNormals();

            polyhedrons.Add(poly);
        }

        public void CreateFirstCube()
        {
            Polyhedron poly = Polyhedron.CreateHexahedron();
            Transform.Apply(Transform.CreateScaleMatrix(6, 6, 6), poly);
            Transform.Apply(Transform.CreateRotationAroundYMatrix(-45), poly);
            Transform.Apply(Transform.CreateTranslationMatrix(5, -7f, -6.0f), poly);
            poly.ColorFacesMonotonously(Color.Red);
            firstCube = poly;
            polyhedrons.Add(poly);
        }

        public void CreateSecondCube()
        {
            Polyhedron poly = Polyhedron.CreateHexahedron();
            Transform.Apply(Transform.CreateScaleMatrix(4, 4, 4), poly);
            Transform.Apply(Transform.CreateRotationAroundYMatrix(-60), poly);
            Transform.Apply(Transform.CreateTranslationMatrix(-5, -7f, 1f), poly);
            poly.ColorFacesMonotonously(Color.Blue);
            secondCube = poly;
            polyhedrons.Add(poly);
        }


        private void CreateFirstLightSource()
        {
            firstLightSource = new LightSource(0f, 10f, 0f, Color.White);
        }

        private void CreateSecondLightSource()
        {
            secondLightSource = new LightSource(-5f, 5f, 0f, Color.Red);
        }


        public Scene()
        {
            InitializeComponent();

            CreateEmptyRoom();

            CreateFirstCube();
            CreateSecondCube();

            CreateFirstLightSource();
            CreateSecondLightSource();

            cam = new Camera(
                new Point3D(0, 0, 20),
                new Point3D(0, 0, 0),
                panel1.Width, panel1.Height
                );

            renderer = new Renderer(cam, panel1.Width, panel1.Height, firstLightSource);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(SystemColors.ActiveBorder);

            if (useZBufferRendering)
            {
                // ==== ÐÅÍÄÅÐ ×ÅÐÅÇ Z-ÁÓÔÅÐ ====
                renderer.Clear(Color.Gray);

                // ðèñóåì âñå ïîëèýäðû
                foreach (var p in polyhedrons)
                    renderer.Render(p);

                Bitmap frame = renderer.GetImage();
                g.DrawImage(frame, 0, 0, panel1.Width, panel1.Height);
            }
            else
            {
                PointF? lightPointNullable = cam.ProjectPoint2D(firstLightSource);
                if (lightPointNullable.HasValue)
                {
                    PointF lightPoint = lightPointNullable.Value;
                    Color lightColor = Color.FromArgb((int)(firstLightSource.Color.X * 255), (int)(firstLightSource.Color.Y * 255), (int)(firstLightSource.Color.Z * 255));

                    int lightPointSize = 12;

                    using (var brush = new SolidBrush(lightColor))
                    {
                        g.FillEllipse(brush, lightPoint.X - lightPointSize / 2, lightPoint.Y - lightPointSize / 2, lightPointSize, lightPointSize);
                    }
                }

                for (int i = 0; i < polyhedrons.Count; i++)
                {
                    Polyhedron p = polyhedrons[i];
                    PointF[][] projectedFaces = cam.Project(p);

                    using (Pen pen = new Pen(Color.Black, 1f))
                    {
                        pen.Color = Color.Black;

                        foreach (PointF[] face in projectedFaces)
                        {
                            if (face == null)
                                continue;

                            for (int j = 0; j < face.Length; j++)
                            {
                                PointF a = face[j];
                                PointF b = face[(j + 1) % face.Length];
                                if (!float.IsNaN(a.X) && !float.IsNaN(b.X))
                                {
                                    g.DrawLine(pen, a, b);
                                    g.FillEllipse(Brushes.Red, a.X - 3, a.Y - 3, 6, 6);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Scene_SizeChanged(object sender, EventArgs e)
        {
            if (cam == null) return;

            cam.ScreenWidth = panel1.Width;
            cam.ScreenHeight = panel1.Height;

            renderer = new Renderer(cam, panel1.Width, panel1.Height, firstLightSource);

            panel1.Invalidate();
        }


        private void zBufferCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Render_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
    }
}
