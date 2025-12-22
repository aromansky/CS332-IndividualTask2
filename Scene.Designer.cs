using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace IndividualTask2
{
    partial class Scene
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            фигурыToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            zBufferCheckBox = new CheckBox();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Location = new Point(10, 11);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(494, 539);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { фигурыToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(118, 26);
            // 
            // фигурыToolStripMenuItem
            // 
            фигурыToolStripMenuItem.Name = "фигурыToolStripMenuItem";
            фигурыToolStripMenuItem.Size = new Size(117, 22);
            фигурыToolStripMenuItem.Text = "Фигуры";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // zBufferCheckBox
            // 
            zBufferCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            zBufferCheckBox.AutoSize = true;
            zBufferCheckBox.Location = new Point(510, 12);
            zBufferCheckBox.Name = "zBufferCheckBox";
            zBufferCheckBox.Size = new Size(65, 19);
            zBufferCheckBox.TabIndex = 46;
            zBufferCheckBox.Text = "Рендер";
            zBufferCheckBox.UseVisualStyleBackColor = true;
            zBufferCheckBox.CheckedChanged += zBufferCheckBox_CheckedChanged;
            // 
            // Scene
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(zBufferCheckBox);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Scene";
            Text = "Cornish Room";
            SizeChanged += Scene_SizeChanged;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem фигурыToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private CheckBox zBufferCheckBox;
    }
}

