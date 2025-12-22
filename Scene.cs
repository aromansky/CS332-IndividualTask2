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
        internal int polyInd = -1;
        internal Renderer renderer;
        private LightSource lightSource;

        private bool useZBufferRendering = false;


        public Scene()
        {
            InitializeComponent();

            cam = new Camera(
                new Point3D(0f, 0f, 1f),
                new Point3D(0, 0, 0),
                panel1.Width, panel1.Height
                );

            lightSource = new LightSource(0f, 1f, 0f, Color.Red);
            renderer = new Renderer(cam, panel1.Width, panel1.Height, lightSource);
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
                PointF? lightPointNullable = cam.ProjectPoint2D(lightSource);
                if (lightPointNullable.HasValue)
                {
                    PointF lightPoint = lightPointNullable.Value;
                    Color lightColor = Color.FromArgb((int)(lightSource.Color.X * 255), (int)(lightSource.Color.Y * 255), (int)(lightSource.Color.Z * 255));

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
                        if (i == polyInd) pen.Color = Color.Purple;
                        else pen.Color = Color.Black;

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

            renderer = new Renderer(cam, panel1.Width, panel1.Height, lightSource);

            panel1.Invalidate();
        }


        private void zBufferCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useZBufferRendering = zBufferCheckBox.Checked;
            panel1.Invalidate();
        }
    }
}
