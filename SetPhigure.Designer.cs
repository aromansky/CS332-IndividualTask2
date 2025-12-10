using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace IndividualTask2
{
    partial class SetPhigure
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listBox = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            раскраситьToolStripMenuItem = new ToolStripMenuItem();
            разноцветноToolStripMenuItem = new ToolStripMenuItem();
            монотонноToolStripMenuItem = new ToolStripMenuItem();
            красныйToolStripMenuItem = new ToolStripMenuItem();
            синийToolStripMenuItem = new ToolStripMenuItem();
            белыйToolStripMenuItem = new ToolStripMenuItem();
            желтыйToolStripMenuItem = new ToolStripMenuItem();
            добавитьТекстуруToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            инвертироватьНормальToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox.ContextMenuStrip = contextMenuStrip1;
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(10, 9);
            listBox.Margin = new Padding(3, 2, 3, 2);
            listBox.Name = "listBox";
            listBox.Size = new Size(208, 304);
            listBox.TabIndex = 1;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listBox.MouseDown += listBox_MouseDown;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { раскраситьToolStripMenuItem, добавитьТекстуруToolStripMenuItem, удалитьToolStripMenuItem, инвертироватьНормальToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(211, 92);
            // 
            // раскраситьToolStripMenuItem
            // 
            раскраситьToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { разноцветноToolStripMenuItem, монотонноToolStripMenuItem });
            раскраситьToolStripMenuItem.Name = "раскраситьToolStripMenuItem";
            раскраситьToolStripMenuItem.Size = new Size(210, 22);
            раскраситьToolStripMenuItem.Text = "Раскрасить";
            // 
            // разноцветноToolStripMenuItem
            // 
            разноцветноToolStripMenuItem.Name = "разноцветноToolStripMenuItem";
            разноцветноToolStripMenuItem.Size = new Size(180, 22);
            разноцветноToolStripMenuItem.Text = "Разноцветно";
            разноцветноToolStripMenuItem.Click += разноцветноToolStripMenuItem_Click;
            // 
            // монотонноToolStripMenuItem
            // 
            монотонноToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { красныйToolStripMenuItem, синийToolStripMenuItem, белыйToolStripMenuItem, желтыйToolStripMenuItem });
            монотонноToolStripMenuItem.Name = "монотонноToolStripMenuItem";
            монотонноToolStripMenuItem.Size = new Size(180, 22);
            монотонноToolStripMenuItem.Text = "Монотонно";
            // 
            // красныйToolStripMenuItem
            // 
            красныйToolStripMenuItem.Name = "красныйToolStripMenuItem";
            красныйToolStripMenuItem.Size = new Size(180, 22);
            красныйToolStripMenuItem.Text = "Красный";
            красныйToolStripMenuItem.Click += красныйToolStripMenuItem_Click;
            // 
            // синийToolStripMenuItem
            // 
            синийToolStripMenuItem.Name = "синийToolStripMenuItem";
            синийToolStripMenuItem.Size = new Size(180, 22);
            синийToolStripMenuItem.Text = "Синий";
            синийToolStripMenuItem.Click += синийToolStripMenuItem_Click;
            // 
            // белыйToolStripMenuItem
            // 
            белыйToolStripMenuItem.Name = "белыйToolStripMenuItem";
            белыйToolStripMenuItem.Size = new Size(180, 22);
            белыйToolStripMenuItem.Text = "Белый";
            белыйToolStripMenuItem.Click += белыйToolStripMenuItem_Click;
            // 
            // желтыйToolStripMenuItem
            // 
            желтыйToolStripMenuItem.Name = "желтыйToolStripMenuItem";
            желтыйToolStripMenuItem.Size = new Size(180, 22);
            желтыйToolStripMenuItem.Text = "Желтый";
            желтыйToolStripMenuItem.Click += желтыйToolStripMenuItem_Click;
            // 
            // добавитьТекстуруToolStripMenuItem
            // 
            добавитьТекстуруToolStripMenuItem.Name = "добавитьТекстуруToolStripMenuItem";
            добавитьТекстуруToolStripMenuItem.Size = new Size(210, 22);
            добавитьТекстуруToolStripMenuItem.Text = "Добавить текстуру";
            добавитьТекстуруToolStripMenuItem.Click += добавитьТекстуруToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(210, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
            // 
            // инвертироватьНормальToolStripMenuItem
            // 
            инвертироватьНормальToolStripMenuItem.Name = "инвертироватьНормальToolStripMenuItem";
            инвертироватьНормальToolStripMenuItem.Size = new Size(210, 22);
            инвертироватьНормальToolStripMenuItem.Text = "Инвертировать нормаль";
            инвертироватьНормальToolStripMenuItem.Click += инвертироватьНормальToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // SetPhigure
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(228, 338);
            Controls.Add(listBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "SetPhigure";
            Text = "Выбрать фигуру";
            Activated += SetPhigure_Activated;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox checkedListBox1;
        private ListBox listBox;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem раскраситьToolStripMenuItem;
        private ToolStripMenuItem добавитьТекстуруToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem инвертироватьНормальToolStripMenuItem;
        private ToolStripMenuItem разноцветноToolStripMenuItem;
        private ToolStripMenuItem монотонноToolStripMenuItem;
        private ToolStripMenuItem красныйToolStripMenuItem;
        private ToolStripMenuItem синийToolStripMenuItem;
        private ToolStripMenuItem белыйToolStripMenuItem;
        private ToolStripMenuItem желтыйToolStripMenuItem;
    }
}