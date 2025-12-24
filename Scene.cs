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
        public List<IFigure> figures = new List<IFigure>();
        public List<LightSource> lightSources = new List<LightSource>();

        private LightSource firstLightSource;
        private LightSource secondLightSource;

        public Polyhedron redCube;
        public Polyhedron blueCube;

        public Sphere yellowSphere;
        public Sphere greenSphere;

        public MyImage image;

        private void CreateEmptyRoom()
        {
            Polyhedron poly = Polyhedron.CreateHexahedron(2);
            Transform.Apply(Transform.CreateScaleMatrix(10, 10, 10), poly);

            poly.InvertNormals();
            poly.ColorFacesAutomatically();

            for (int i = 0; i < poly.Faces.Count; i++)
            {
                figures.Add(poly.Faces[i]);
            }
        }

        public void CreateFirstCube()
        {
            Polyhedron poly = Polyhedron.CreateHexahedron();
            Transform.Apply(Transform.CreateScaleMatrix(6, 6, 6), poly);
            Transform.Apply(Transform.CreateRotationAroundYMatrix(-45), poly);
            Transform.Apply(Transform.CreateTranslationMatrix(5, -7f, -6.0f), poly);
            poly.ColorFacesMonotonously(Color.Red);
            redCube = poly;
            figures.Add(poly);
        }

        public void CreateSecondCube()
        {
            Polyhedron poly = Polyhedron.CreateHexahedron();
            Transform.Apply(Transform.CreateScaleMatrix(4, 4, 4), poly);
            Transform.Apply(Transform.CreateRotationAroundYMatrix(-60), poly);
            Transform.Apply(Transform.CreateTranslationMatrix(-5, -8f, -4), poly);
            poly.ColorFacesMonotonously(Color.Blue);
            blueCube = poly;
            figures.Add(poly);
        }

        public void CreateFirstSphere()
        {
            yellowSphere = new Sphere(2f, new Point3D(0f, -6f, -2), new Material(new Vector3(Color.Yellow)));
            figures.Add(yellowSphere);
        }

        public void CreateSecondSphere()
        {
            greenSphere = new Sphere(2.5f, new Point3D(5f, -1.5f, -6), new Material(new Vector3(Color.Green)));
            figures.Add(greenSphere);
        }

        private void CreateFirstLightSource()
        {
            firstLightSource = new LightSource(0f, 9f, -5f, Color.White);
            lightSources.Add(firstLightSource);
        }

        private void CreateSecondLightSource()
        {
            secondLightSource = new LightSource(9f, 0f, 0f, Color.White);
            lightSources.Add(secondLightSource);
        }


        public Scene()
        {
            InitializeComponent();

            CreateEmptyRoom();

            CreateFirstCube();
            CreateSecondCube();

            CreateFirstSphere();
            CreateSecondSphere();

            CreateFirstLightSource();
            CreateSecondLightSource();

            cam = new Camera(
                new Point3D(0, 0, 10),
                new Point3D(0, 0, 0),
                panel1.Width, panel1.Height
                );
        }
        private void Scene_SizeChanged(object sender, EventArgs e)
        {
            if (cam == null) return;

            cam.ScreenWidth = panel1.Width;
            cam.ScreenHeight = panel1.Height;

            panel1.Invalidate();
        }

        private async void Render_Click(object sender, EventArgs e)
        {
            var progressIndicator = new Progress<int>(percent =>
            {
                if (percent > 0 && percent < 100)
                {
                    progressBar.Value = percent;
                    progressBar.Value = percent - 1;
                    progressBar.Value = percent;
                }
                else
                {
                    progressBar.Value = percent;
                }
            });

            int w = panel1.Width;
            int h = panel1.Height;

            MyImage result = await Task.Run(() =>
                RayTracing.ComputeRayTracing(cam, figures, lightSources, w, h, 5, 0.001f, progressIndicator)
            );

            panel1.Image = result.Img;
        }


        private IFigure GetCurrentFigure()
        {
            if (RedCubeRadioButton.Checked) return redCube;
            if (blueCubeRadioButton.Checked) return blueCube;
            if (yellowSphereRadioButton.Checked) return yellowSphere;
            return greenSphere;
        }

        private void RedCubeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (RedCubeRadioButton.Checked) UpdateMaterialUI(redCube.Material);
        }

        private void blueCubeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (blueCubeRadioButton.Checked) UpdateMaterialUI(blueCube.Material);
        }

        private void yellowSphereRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (yellowSphereRadioButton.Checked) UpdateMaterialUI(yellowSphere.Material);
        }

        private void greenSphereRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (greenSphereRadioButton.Checked) UpdateMaterialUI(greenSphere.Material);
        }

        private void nothingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!nothingRadioButton.Checked) return;
            var fig = GetCurrentFigure();

            fig.Material.Reflection = 0;
            fig.Material.Refraction = 0;
            fig.Material.Ambient = 0.1f;
            fig.Material.Diffuse = 0.7f;

            if (fig == redCube) redCube.ColorFacesMonotonously(Color.Red);
            if (fig == blueCube) blueCube.ColorFacesMonotonously(Color.Blue);
        }

        private void mirrorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!mirrorRadioButton.Checked) return;
            var mat = GetCurrentFigure().Material;

            mat.Reflection = 0.95f;
            mat.Refraction = 0;
            mat.Ambient = 0.0f;
            mat.Diffuse = 0.0f;
        }

        private void transparencyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!transparencyRadioButton.Checked) return;
            var mat = GetCurrentFigure().Material;

            mat.Refraction = 0.9f;
            mat.Reflection = 0.1f;
            mat.Environment = 1.5f;
            mat.Ambient = 0.0f;
            mat.Diffuse = 0.0f;
        }
        private void UpdateMaterialUI(Material mat)
        {
            if (mat.Refraction > 0)
            {
                transparencyRadioButton.Checked = true;
            }
            else if (mat.Reflection > 0)
            {
                mirrorRadioButton.Checked = true;
            }
            else
            {
                nothingRadioButton.Checked = true;
            }
        }

        private void SetWallMirror(int index, bool isMirror)
        {
            if (index < 0 || index >= figures.Count) return;

            var mat = figures[index].Material;
            if (isMirror)
            {
                mat.Reflection = 0.9f; // Делаем стену зеркальной
                mat.Diffuse = 0.0f;    // Убираем матовость
                mat.Ambient = 0.0f;
            }
            else
            {
                mat.Reflection = 0.0f; // Возвращаем обычную стену
                mat.Diffuse = 0.7f;
                mat.Ambient = 0.1f;
            }
        }

        private void leftWallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetWallMirror(0, leftWallCheckBox.Checked);
        }

        private void rightWallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetWallMirror(1, rightWallCheckBox.Checked);
        }

        private void topWallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetWallMirror(2, topWallCheckBox.Checked);
        }

        private void bottomWallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetWallMirror(3, bottomWallCheckBox.Checked);
        }

        private void farWallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetWallMirror(4, farWallCheckBox.Checked);
        }

        private void backWallCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Стена, которая находится за спиной камеры (если она есть в списке)
            SetWallMirror(5, backWallCheckBox.Checked);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            // Снимаем все галочки
            leftWallCheckBox.Checked = false;
            rightWallCheckBox.Checked = false;
            topWallCheckBox.Checked = false;
            bottomWallCheckBox.Checked = false;
            farWallCheckBox.Checked = false;
            backWallCheckBox.Checked = false;

            // Сбрасываем настройки фигур (кубов и сфер)
            nothingRadioButton.Checked = true;
        }
    }
}
