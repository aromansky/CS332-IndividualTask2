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
        SetPhigure form;

        private Point _prevMousePos;
        private bool _isDragging = false;
        private Camera cam;
        public List<Polyhedron> polyhedrons = new List<Polyhedron>();
        internal int polyInd = -1;
        internal Renderer renderer;
        private LightSource lightSource;

        private Point3D linePoint = new Point3D(0, 0, 0);
        private Vector3 lineVector;

        bool setPoly = false;
        bool setCam = false;
        bool setLight = false;

        private bool scalingMode = false;
        private bool scalingXMode = false;
        private bool scalingYMode = false;
        private bool scalingZMode = false;


        private bool rotatingXMode = false;
        private bool rotatingYMode = false;
        private bool rotatingZMode = false;

        private bool translatingXMode = false;
        private bool translatingYMode = false;
        private bool translatingZMode = false;

        private bool rotatingCustomAxisMode = false;

        private int deltaX = 0;

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

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panel1, new object[] { true });

            panel1.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(panel1, true, null);



        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _prevMousePos = e.Location;
                _isDragging = true;
                panel1.Cursor = Cursors.SizeAll;
            }
        }



        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && e.Button == MouseButtons.Left)
            {
                deltaX = e.X - _prevMousePos.X;

                if (setPoly && polyInd != -1)
                {
                    PolyhedronProcessing();
                }

                if (setCam)
                {
                    CameraProcessing();
                }

                if (setLight)
                    LightProcessing();


                HandleMouseDrag(deltaX);

                _prevMousePos = e.Location;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
                panel1.Cursor = Cursors.Default;
            }
        }

        private void HandleMouseDrag(int deltaX)
        {
            panel1.Invalidate();
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

                // ðèñóåì îñè êîîðäèíàò ïîâåðõ
                float axisLength = 5.0f;
                var origin = new Point3D(0, 0, 0);
                var xAxisEnd = new Point3D(axisLength, 0, 0);
                var yAxisEnd = new Point3D(0, axisLength, 0);
                var zAxisEnd = new Point3D(0, 0, axisLength);

                renderer.Render(new Polyhedron(new List<Face> { CreateAxis(origin, xAxisEnd) }), Color.Red);
                renderer.Render(new Polyhedron(new List<Face> { CreateAxis(origin, yAxisEnd) }), Color.Green);
                renderer.Render(new Polyhedron(new List<Face> { CreateAxis(origin, zAxisEnd) }), Color.Blue);

                Bitmap frame = renderer.GetImage();
                g.DrawImage(frame, 0, 0, panel1.Width, panel1.Height);
            }
            else
            {
                // === ÐÈÑÎÂÀÍÈÅ ÈÑÒÎ×ÍÈÊÀ ÑÂÅÒÀ ===
                //Polyhedron lightPointPoly = new Polyhedron(new List<Face> { new Face(lightSource) });
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

                // === ÐÈÑÎÂÀÍÈÅ ÎÑÅÉ ÊÎÎÐÄÈÍÀÒ ===
                float axisLength = 5.0f;
                var origin = new Point3D(0, 0, 0);
                var xAxisEnd = new Point3D(axisLength, 0, 0);
                var yAxisEnd = new Point3D(0, axisLength, 0);
                var zAxisEnd = new Point3D(0, 0, axisLength);

                var axes = new PointF[][]
                {
                    cam.Project(new Polyhedron(new List<Face> { CreateAxis(origin, xAxisEnd) }))[0],
                    cam.Project(new Polyhedron(new List<Face> { CreateAxis(origin, yAxisEnd) }))[0],
                    cam.Project(new Polyhedron(new List<Face> { CreateAxis(origin, zAxisEnd) }))[0]
                };

                using (var penX = new Pen(Color.Red, 2f))
                using (var penY = new Pen(Color.Green, 2f))
                using (var penZ = new Pen(Color.Blue, 2f))
                {
                    if (axes[0].Length >= 2)
                        g.DrawLine(penX, axes[0][0], axes[0][1]);
                    if (axes[1].Length >= 2)
                        g.DrawLine(penY, axes[1][0], axes[1][1]);
                    if (axes[2].Length >= 2)
                        g.DrawLine(penZ, axes[2][0], axes[2][1]);
                }

                // === ÎÑÜ ÂÐÀÙÅÍÈß (åñëè âûáðàíà ïîëüçîâàòåëüñêàÿ) ===
                if (rotatingCustomAxisMode && !lineVector.IsZero())
                {
                    Vector3 dir = lineVector.Normalized();
                    float len = 5.0f;

                    Point3D start = new Point3D(
                        linePoint.X - dir.X * len,
                        linePoint.Y - dir.Y * len,
                        linePoint.Z - dir.Z * len);

                    Point3D end = new Point3D(
                        linePoint.X + dir.X * len,
                        linePoint.Y + dir.Y * len,
                        linePoint.Z + dir.Z * len);

                    var projectedAxis = cam.Project(new Polyhedron(new List<Face> { CreateAxis(start, end) }))[0];

                    using (var penAxis = new Pen(Color.Gold, 2f) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                    {
                        g.DrawLine(penAxis, projectedAxis[0], projectedAxis[1]);
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


        private static Face CreateAxis(Point3D start, Point3D end)
        {
            return new Face(start, end);
        }

        private void PerspectiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cam.SetMode(ProjectionMode.Perspective);
            cam.SetPosition(new Point3D(0, 0, 1));
            panel1.Invalidate();
        }

        private void TrimetricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cam.SetMode(ProjectionMode.Trimetric);
            cam.SetPosition(new Point3D(0, 0, 1));
            panel1.Invalidate();
        }

        private void DimetricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cam.SetMode(ProjectionMode.Dimetric);
            cam.SetPosition(new Point3D(0, 0, 1));
            panel1.Invalidate();
        }

        private void IsometricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cam.SetMode(ProjectionMode.Isometric);
            cam.SetPosition(new Point3D(0, 0, 1));
            panel1.Invalidate();
        }

        private void TetrahedronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polyhedrons.Add(Polyhedron.CreateTetrahedron());
            polyInd++;
            panel1.Invalidate();
        }

        private void HexahedronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polyhedrons.Add(Polyhedron.CreateHexahedron());
            polyInd++;
            panel1.Invalidate();
        }

        private void OctahedronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polyhedrons.Add(Polyhedron.CreateOctahedron());
            polyInd++;
            panel1.Invalidate();
        }

        private void IcosahedronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polyhedrons.Add(Polyhedron.CreateIcosahedron());
            polyInd++;
            panel1.Invalidate();
        }

        private void DodecahedronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polyhedrons.Add(Polyhedron.CreateDodecahedron());
            polyInd++;
            panel1.Invalidate();
        }

        private void CameraProcessing()
        {
            if (rotatingXMode)
                cam.SetPosition(Transform.Apply(Transform.CreateRotationAroundXMatrix(deltaX * 0.01f), cam.Position));
            else if (rotatingYMode)
                cam.SetPosition(Transform.Apply(Transform.CreateRotationAroundYMatrix(deltaX * 0.01f), cam.Position));
            else if (rotatingZMode)
                cam.SetPosition(Transform.Apply(Transform.CreateRotationAroundZMatrix(deltaX * 0.01f), cam.Position));
        }

        private void LightProcessing()
        {
            if (rotatingXMode)
                lightSource = new LightSource(Transform.Apply(Transform.CreateRotationAroundXMatrix(deltaX * 0.01f), lightSource), lightSource.Color);
            else if (rotatingYMode)
                lightSource = new LightSource(Transform.Apply(Transform.CreateRotationAroundYMatrix(deltaX * 0.01f), lightSource), lightSource.Color);
            else if (rotatingZMode)
                lightSource = new LightSource(Transform.Apply(Transform.CreateRotationAroundZMatrix(deltaX * 0.01f), lightSource), lightSource.Color);
            else if (translatingXMode)
                lightSource = new LightSource(Transform.Apply(Transform.CreateTranslationMatrix(deltaX * 0.01f, 0, 0), lightSource), lightSource.Color);
            else if (translatingYMode)
                lightSource = new LightSource(Transform.Apply(Transform.CreateTranslationMatrix(0, deltaX * 0.01f, 0), lightSource), lightSource.Color);
            else if (translatingZMode)
                lightSource = new LightSource(Transform.Apply(Transform.CreateTranslationMatrix(0, 0, deltaX * 0.01f), lightSource), lightSource.Color);

            renderer.Light = lightSource;
            panel1.Invalidate();
        }

        private void PolyhedronProcessing()
        {
            float scaleFactor = 1.0f + deltaX * 0.01f;
            if (!(scaleFactor > 0.1f && scaleFactor < 10.0f)) return;

            if (scalingMode)
                Transform.Apply(Transform.CreateScaleMatrix(scaleFactor, scaleFactor, scaleFactor), polyhedrons[polyInd]);
            else if (scalingXMode)
                Transform.Apply(Transform.CreateScaleMatrix(scaleFactor, 1, 1), polyhedrons[polyInd]);
            else if (scalingYMode)
                Transform.Apply(Transform.CreateScaleMatrix(1, scaleFactor, 1), polyhedrons[polyInd]);
            else if (scalingZMode)
                Transform.Apply(Transform.CreateScaleMatrix(1, 1, scaleFactor), polyhedrons[polyInd]);
            else if (rotatingXMode)
                Transform.Apply(
                    Transform.CreateRotationAroundLineMatrix(
                        polyhedrons[polyInd].GetCenter(),
                        new Vector3(1, 0, 0),
                        deltaX * 0.01f),
                    polyhedrons[polyInd]);
            else if (rotatingYMode)
                Transform.Apply(
                    Transform.CreateRotationAroundLineMatrix(
                        polyhedrons[polyInd].GetCenter(),
                        new Vector3(0, 1, 0),
                        deltaX * 0.01f),
                    polyhedrons[polyInd]);
            else if (rotatingZMode)
                Transform.Apply(
                    Transform.CreateRotationAroundLineMatrix(
                        polyhedrons[polyInd].GetCenter(),
                        new Vector3(0, 0, 1),
                        deltaX * 0.01f),
                    polyhedrons[polyInd]);
            else if (translatingXMode)
                Transform.Apply(Transform.CreateTranslationMatrix(deltaX * 0.01f, 0, 0), polyhedrons[polyInd]);
            else if (translatingYMode)
                Transform.Apply(Transform.CreateTranslationMatrix(0, deltaX * 0.01f, 0), polyhedrons[polyInd]);
            else if (translatingZMode)
                Transform.Apply(Transform.CreateTranslationMatrix(0, 0, deltaX * 0.01f), polyhedrons[polyInd]);
            else if (rotatingCustomAxisMode)
                Transform.Apply(
                    Transform.CreateRotationAroundLineMatrix(
                        linePoint,
                        lineVector,
                        deltaX * 0.01f),
                    polyhedrons[polyInd]);


        }

        private void ScaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            scalingMode = ScaleRadioButton.Checked;
        }

        private void RotateAboutXRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            rotatingXMode = RotateAboutXRadioButton.Checked;
        }

        private void RotateAboutYRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            rotatingYMode = RotateAboutYRadioButton.Checked;
        }

        private void RotateAboutZRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            rotatingZMode = RotateAboutZRadioButton.Checked;
        }

        private void TranslateAboutXRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            translatingXMode = TranslateAboutXRadioButton.Checked;
        }

        private void TranslateAboutYRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            translatingYMode = TranslateAboutYRadioButton.Checked;
        }

        private void TranslateAboutZRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            translatingZMode = TranslateAboutZRadioButton.Checked;
        }

        private void ScaleAboutXRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            scalingXMode = ScaleAboutXRadioButton.Checked;
        }

        private void ScaleAboutYRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            scalingYMode = ScaleAboutYRadioButton.Checked;
        }

        private void ScaleAboutZRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            scalingZMode = ScaleAboutZRadioButton.Checked;
        }

        private void lineStartNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateLineFromPoints();
            panel1.Invalidate();
        }

        private void lineVecNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            lineVector = new Vector3(
                (float)lineVecXNumericUpDown.Value,
                (float)lineVecYNumericUpDown.Value,
                (float)lineVecZNumericUpDown.Value);

            panel1.Invalidate();
        }

        private void customRotatingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            rotatingCustomAxisMode = customRotatingRadioButton.Checked;
            panel1.Invalidate();
        }

        private void UpdateLineFromPoints()
        {
            Point3D pointA = new Point3D(
                (float)lineStartXNumericUpDown.Value,
                (float)lineStartYNumericUpDown.Value,
                (float)lineStartZNumericUpDown.Value);

            Point3D pointB = new Point3D(
                (float)lineEndXNumericUpDown.Value,
                (float)lineEndYNumericUpDown.Value,
                (float)lineEndZNumericUpDown.Value);

            Vector3 direction = new Vector3(
                pointB.X - pointA.X,
                pointB.Y - pointA.Y,
                pointB.Z - pointA.Z);

            linePoint = pointA;
            lineVector = direction;
        }

        private void lineEndNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateLineFromPoints();
            panel1.Invalidate();
        }

        private void refclectXYbutton_Click(object sender, EventArgs e)
        {
            if (polyhedrons[polyInd] != null)
            {
                Transform.Apply(Transform.CreateScaleMatrix(1, 1, -1), polyhedrons[polyInd]);
                panel1.Invalidate();
            }
        }

        private void refclectXZbutton_Click(object sender, EventArgs e)
        {
            if (polyhedrons[polyInd] != null)
            {
                Transform.Apply(Transform.CreateScaleMatrix(1, -1, 1), polyhedrons[polyInd]);
                panel1.Invalidate();
            }
        }

        private void refclectYZbutton_Click(object sender, EventArgs e)
        {
            if (polyhedrons[polyInd] != null)
            {
                Transform.Apply(Transform.CreateScaleMatrix(-1, 1, 1), polyhedrons[polyInd]);
                panel1.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (polyhedrons[polyInd] is null) return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Wavefront OBJ (*.obj)|*.obj|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "obj";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    polyhedrons[polyInd].Save(saveFileDialog.FileName);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Wavefront OBJ (*.obj)|*.obj|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    polyhedrons.Add(Polyhedron.Load(openFileDialog.FileName));
                    polyInd++;
                    panel1.Invalidate();
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

        private void setPolyhedronRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            setPoly = setPolyhedronRadioButton.Checked;
            setCam = false;
            setLight = false;
        }

        private void setCamersRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            setCam = setCamersRadioButton.Checked;
            setLight = false;
            setPoly = false;
        }

        private void currentPhigureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form != null && !form.IsDisposed && form.Visible)
            {
                form.Activate();
                form.BringToFront();
                return;
            }

            form = new SetPhigure(polyhedrons, this);
            form.Show();
        }

        private void zBufferCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useZBufferRendering = zBufferCheckBox.Checked;
            panel1.Invalidate();
        }

        private void íåòToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.SetMode(RenderMode.None);
            panel1.Invalidate();
        }

        private void øåéäèíãÃóðîÄëÿÌîäåëèËàìáåðòàToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.SetMode(RenderMode.Gouraud);
            panel1.Invalidate();
        }

        private void øåéäèíãÔîíãàÄëÿÌîäToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.SetMode(RenderMode.Phong);
            panel1.Invalidate();
        }

        private void çàãðóçèòüÒåêñòóðóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (polyInd == -1) return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MyImage texture = new MyImage(openFileDialog.FileName);

                    polyhedrons[polyInd].SetTextureToAllFaces(texture);

                    renderer.SetMode(RenderMode.Texture);

                    panel1.Invalidate();
                }
            }
        }

        private void lightSourceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            setLight = lightSourceRadioButton.Checked;
            setCam = false;
            setPoly = false;
        }
    }
}
