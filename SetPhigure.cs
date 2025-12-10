using Geometry;
using IndividualTask2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndividualTask2
{
    public partial class SetPhigure : Form
    {
        private int rightClickedIndex = -1;
        private List<Polyhedron> lst;
        private Scene scene;
        public SetPhigure(List<Polyhedron> lst, Scene scene)
        {
            InitializeComponent();
            this.lst = lst;
            this.scene = scene;


            foreach (Polyhedron poly in lst)
            {
                listBox.Items.Add(poly.Name);
            }

            if (scene.polyInd > -1)
                listBox.SelectedIndex = scene.polyInd;
        }

        private void SetPhigure_Activated(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            foreach (Polyhedron poly in lst)
            {
                listBox.Items.Add(poly.Name);
            }

            if (scene.polyInd > -1)
                listBox.SelectedIndex = scene.polyInd;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            scene.polyInd = listBox.SelectedIndex;
            scene.Refresh();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rightClickedIndex >= 0 && rightClickedIndex < listBox.Items.Count)
            {
                scene.polyhedrons.RemoveAt(rightClickedIndex);
                listBox.Items.RemoveAt(rightClickedIndex);

                scene.polyInd = Math.Max(-1, rightClickedIndex - 1);
                scene.Refresh();

                rightClickedIndex = -1;
            }
        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox.IndexFromPoint(e.Location);

                if (index != ListBox.NoMatches)
                {
                    listBox.SelectedIndex = index;
                    rightClickedIndex = index; // сохраняем индекс
                }
                else
                {
                    rightClickedIndex = -1;
                }
            }
        }

        private void раскраситьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void добавитьТекстуруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scene.polyInd == -1) return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MyImage texture = new MyImage(openFileDialog.FileName);

                    scene.polyhedrons[scene.polyInd].SetTextureToAllFaces(texture);

                    scene.renderer.SetMode(RenderMode.Texture);

                    scene.Refresh();
                }
            }
        }

        private void инвертироватьНормальToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scene.polyInd == -1) return;
            scene.polyhedrons[scene.polyInd].InvertNormals();
        }

        private void разноцветноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene.polyhedrons[scene.polyInd].ColorFacesAutomatically();
        }

        private void монотонноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //scene.polyhedrons[scene.polyInd].ColorFacesMonotonously();
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene.polyhedrons[scene.polyInd].ColorFacesMonotonously(Color.Red);

        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene.polyhedrons[scene.polyInd].ColorFacesMonotonously(Color.Blue);

        }

        private void белыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene.polyhedrons[scene.polyInd].ColorFacesMonotonously(Color.White);

        }

        private void желтыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scene.polyhedrons[scene.polyInd].ColorFacesMonotonously(Color.Yellow);

        }
    }

}
