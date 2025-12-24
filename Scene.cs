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

            for(int i = 0; i < poly.Faces.Count; i++)
            {
                //if (i == 3) continue;
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

            //CreateFirstLightSource();
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
    }
}
