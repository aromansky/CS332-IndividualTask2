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
            RotateAboutXRadioButton = new RadioButton();
            RotateAboutYRadioButton = new RadioButton();
            RotateAboutZRadioButton = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            customRotatingRadioButton = new RadioButton();
            lineStartXNumericUpDown = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            lineStartYNumericUpDown = new NumericUpDown();
            label6 = new Label();
            lineStartZNumericUpDown = new NumericUpDown();
            label7 = new Label();
            lineVecZNumericUpDown = new NumericUpDown();
            label8 = new Label();
            lineVecYNumericUpDown = new NumericUpDown();
            label9 = new Label();
            lineVecXNumericUpDown = new NumericUpDown();
            contextMenuStrip1 = new ContextMenuStrip(components);
            фигурыToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            фигурыToolStripMenuItem1 = new ToolStripMenuItem();
            TetrahedronToolStripMenuItem = new ToolStripMenuItem();
            HexahedronToolStripMenuItem = new ToolStripMenuItem();
            OctahedronToolStripMenuItem = new ToolStripMenuItem();
            IcosahedronToolStripMenuItem = new ToolStripMenuItem();
            DodecahedronToolStripMenuItem = new ToolStripMenuItem();
            проекцияToolStripMenuItem = new ToolStripMenuItem();
            PerspectiveToolStripMenuItem = new ToolStripMenuItem();
            TrimetricToolStripMenuItem = new ToolStripMenuItem();
            DimetricToolStripMenuItem = new ToolStripMenuItem();
            IsometricToolStripMenuItem = new ToolStripMenuItem();
            типОсвещенияToolStripMenuItem = new ToolStripMenuItem();
            нетToolStripMenuItem = new ToolStripMenuItem();
            шейдингГуроДляМоделиЛамбертаToolStripMenuItem = new ToolStripMenuItem();
            шейдингФонгаДляМодToolStripMenuItem = new ToolStripMenuItem();
            currentPhigureToolStripMenuItem = new ToolStripMenuItem();
            ScaleRadioButton = new RadioButton();
            TranslateAboutZRadioButton = new RadioButton();
            TranslateAboutYRadioButton = new RadioButton();
            TranslateAboutXRadioButton = new RadioButton();
            ScaleAboutXRadioButton = new RadioButton();
            ScaleAboutYRadioButton = new RadioButton();
            ScaleAboutZRadioButton = new RadioButton();
            label10 = new Label();
            label11 = new Label();
            lineEndYNumericUpDown = new NumericUpDown();
            label12 = new Label();
            lineEndXNumericUpDown = new NumericUpDown();
            label13 = new Label();
            lineEndZNumericUpDown = new NumericUpDown();
            refclectXYbutton = new Button();
            refclectXZbutton = new Button();
            refclectYZbutton = new Button();
            openFileDialog1 = new OpenFileDialog();
            groupBox1 = new GroupBox();
            lightSourceRadioButton = new RadioButton();
            setCamersRadioButton = new RadioButton();
            setPolyhedronRadioButton = new RadioButton();
            zBufferCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)lineStartXNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineStartYNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineStartZNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineVecZNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineVecYNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineVecXNumericUpDown).BeginInit();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lineEndYNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineEndXNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lineEndZNumericUpDown).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Location = new Point(10, 32);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1337, 748);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            // 
            // RotateAboutXRadioButton
            // 
            RotateAboutXRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RotateAboutXRadioButton.AutoSize = true;
            RotateAboutXRadioButton.Location = new Point(1382, 242);
            RotateAboutXRadioButton.Margin = new Padding(3, 2, 3, 2);
            RotateAboutXRadioButton.Name = "RotateAboutXRadioButton";
            RotateAboutXRadioButton.Size = new Size(123, 19);
            RotateAboutXRadioButton.TabIndex = 7;
            RotateAboutXRadioButton.Text = "Поворот по оси X";
            RotateAboutXRadioButton.UseVisualStyleBackColor = true;
            RotateAboutXRadioButton.CheckedChanged += RotateAboutXRadioButton_CheckedChanged;
            // 
            // RotateAboutYRadioButton
            // 
            RotateAboutYRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RotateAboutYRadioButton.AutoSize = true;
            RotateAboutYRadioButton.Location = new Point(1382, 266);
            RotateAboutYRadioButton.Margin = new Padding(3, 2, 3, 2);
            RotateAboutYRadioButton.Name = "RotateAboutYRadioButton";
            RotateAboutYRadioButton.Size = new Size(123, 19);
            RotateAboutYRadioButton.TabIndex = 8;
            RotateAboutYRadioButton.Text = "Поворот по оси Y";
            RotateAboutYRadioButton.UseVisualStyleBackColor = true;
            RotateAboutYRadioButton.CheckedChanged += RotateAboutYRadioButton_CheckedChanged;
            // 
            // RotateAboutZRadioButton
            // 
            RotateAboutZRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RotateAboutZRadioButton.AutoSize = true;
            RotateAboutZRadioButton.Location = new Point(1382, 293);
            RotateAboutZRadioButton.Margin = new Padding(3, 2, 3, 2);
            RotateAboutZRadioButton.Name = "RotateAboutZRadioButton";
            RotateAboutZRadioButton.Size = new Size(123, 19);
            RotateAboutZRadioButton.TabIndex = 9;
            RotateAboutZRadioButton.Text = "Поворот по оси Z";
            RotateAboutZRadioButton.UseVisualStyleBackColor = true;
            RotateAboutZRadioButton.CheckedChanged += RotateAboutZRadioButton_CheckedChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(1382, 323);
            label1.Name = "label1";
            label1.Size = new Size(181, 15);
            label1.TabIndex = 10;
            label1.Text = "Поворот относительно прямой";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(1383, 352);
            label2.Name = "label2";
            label2.Size = new Size(121, 15);
            label2.TabIndex = 11;
            label2.Text = "Координаты точки A";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(1384, 400);
            label3.Name = "label3";
            label3.Size = new Size(121, 15);
            label3.TabIndex = 12;
            label3.Text = "Координаты вектора";
            // 
            // customRotatingRadioButton
            // 
            customRotatingRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            customRotatingRadioButton.AutoSize = true;
            customRotatingRadioButton.Location = new Point(1386, 497);
            customRotatingRadioButton.Margin = new Padding(3, 2, 3, 2);
            customRotatingRadioButton.Name = "customRotatingRadioButton";
            customRotatingRadioButton.Size = new Size(191, 19);
            customRotatingRadioButton.TabIndex = 13;
            customRotatingRadioButton.Text = "Поворот относительно линии";
            customRotatingRadioButton.UseVisualStyleBackColor = true;
            customRotatingRadioButton.CheckedChanged += customRotatingRadioButton_CheckedChanged;
            // 
            // lineStartXNumericUpDown
            // 
            lineStartXNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineStartXNumericUpDown.DecimalPlaces = 2;
            lineStartXNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineStartXNumericUpDown.Location = new Point(1404, 374);
            lineStartXNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineStartXNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineStartXNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineStartXNumericUpDown.Name = "lineStartXNumericUpDown";
            lineStartXNumericUpDown.Size = new Size(53, 23);
            lineStartXNumericUpDown.TabIndex = 14;
            lineStartXNumericUpDown.ValueChanged += lineStartNumericUpDown_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(1383, 376);
            label4.Name = "label4";
            label4.Size = new Size(17, 15);
            label4.TabIndex = 15;
            label4.Text = "X:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(1463, 376);
            label5.Name = "label5";
            label5.Size = new Size(17, 15);
            label5.TabIndex = 17;
            label5.Text = "Y:";
            // 
            // lineStartYNumericUpDown
            // 
            lineStartYNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineStartYNumericUpDown.DecimalPlaces = 2;
            lineStartYNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineStartYNumericUpDown.Location = new Point(1484, 374);
            lineStartYNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineStartYNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineStartYNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineStartYNumericUpDown.Name = "lineStartYNumericUpDown";
            lineStartYNumericUpDown.Size = new Size(53, 23);
            lineStartYNumericUpDown.TabIndex = 16;
            lineStartYNumericUpDown.ValueChanged += lineStartNumericUpDown_ValueChanged;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(1536, 376);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 19;
            label6.Text = "Z:";
            // 
            // lineStartZNumericUpDown
            // 
            lineStartZNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineStartZNumericUpDown.DecimalPlaces = 2;
            lineStartZNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineStartZNumericUpDown.Location = new Point(1558, 374);
            lineStartZNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineStartZNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineStartZNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineStartZNumericUpDown.Name = "lineStartZNumericUpDown";
            lineStartZNumericUpDown.Size = new Size(53, 23);
            lineStartZNumericUpDown.TabIndex = 18;
            lineStartZNumericUpDown.ValueChanged += lineStartNumericUpDown_ValueChanged;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(1537, 419);
            label7.Name = "label7";
            label7.Size = new Size(17, 15);
            label7.TabIndex = 25;
            label7.Text = "Z:";
            // 
            // lineVecZNumericUpDown
            // 
            lineVecZNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineVecZNumericUpDown.DecimalPlaces = 2;
            lineVecZNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineVecZNumericUpDown.Location = new Point(1558, 418);
            lineVecZNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineVecZNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineVecZNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineVecZNumericUpDown.Name = "lineVecZNumericUpDown";
            lineVecZNumericUpDown.Size = new Size(53, 23);
            lineVecZNumericUpDown.TabIndex = 24;
            lineVecZNumericUpDown.ValueChanged += lineVecNumericUpDown_ValueChanged;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(1464, 419);
            label8.Name = "label8";
            label8.Size = new Size(17, 15);
            label8.TabIndex = 23;
            label8.Text = "Y:";
            // 
            // lineVecYNumericUpDown
            // 
            lineVecYNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineVecYNumericUpDown.DecimalPlaces = 2;
            lineVecYNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineVecYNumericUpDown.Location = new Point(1485, 418);
            lineVecYNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineVecYNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineVecYNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineVecYNumericUpDown.Name = "lineVecYNumericUpDown";
            lineVecYNumericUpDown.Size = new Size(53, 23);
            lineVecYNumericUpDown.TabIndex = 22;
            lineVecYNumericUpDown.ValueChanged += lineVecNumericUpDown_ValueChanged;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(1384, 419);
            label9.Name = "label9";
            label9.Size = new Size(17, 15);
            label9.TabIndex = 21;
            label9.Text = "X:";
            // 
            // lineVecXNumericUpDown
            // 
            lineVecXNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineVecXNumericUpDown.DecimalPlaces = 2;
            lineVecXNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineVecXNumericUpDown.Location = new Point(1405, 418);
            lineVecXNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineVecXNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineVecXNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineVecXNumericUpDown.Name = "lineVecXNumericUpDown";
            lineVecXNumericUpDown.Size = new Size(53, 23);
            lineVecXNumericUpDown.TabIndex = 20;
            lineVecXNumericUpDown.ValueChanged += lineVecNumericUpDown_ValueChanged;
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
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, фигурыToolStripMenuItem1, проекцияToolStripMenuItem, типОсвещенияToolStripMenuItem, currentPhigureToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 2, 0, 2);
            menuStrip1.Size = new Size(1627, 24);
            menuStrip1.TabIndex = 27;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(132, 22);
            saveToolStripMenuItem.Text = "Сохранить";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click_1;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(132, 22);
            loadToolStripMenuItem.Text = "Загрузить";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // фигурыToolStripMenuItem1
            // 
            фигурыToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { TetrahedronToolStripMenuItem, HexahedronToolStripMenuItem, OctahedronToolStripMenuItem, IcosahedronToolStripMenuItem, DodecahedronToolStripMenuItem });
            фигурыToolStripMenuItem1.Name = "фигурыToolStripMenuItem1";
            фигурыToolStripMenuItem1.Size = new Size(62, 20);
            фигурыToolStripMenuItem1.Text = "Фигуры";
            // 
            // TetrahedronToolStripMenuItem
            // 
            TetrahedronToolStripMenuItem.Name = "TetrahedronToolStripMenuItem";
            TetrahedronToolStripMenuItem.Size = new Size(132, 22);
            TetrahedronToolStripMenuItem.Text = "Тетраэдр";
            TetrahedronToolStripMenuItem.Click += TetrahedronToolStripMenuItem_Click;
            // 
            // HexahedronToolStripMenuItem
            // 
            HexahedronToolStripMenuItem.Name = "HexahedronToolStripMenuItem";
            HexahedronToolStripMenuItem.Size = new Size(132, 22);
            HexahedronToolStripMenuItem.Text = "Гексаэдр ";
            HexahedronToolStripMenuItem.Click += HexahedronToolStripMenuItem_Click;
            // 
            // OctahedronToolStripMenuItem
            // 
            OctahedronToolStripMenuItem.Name = "OctahedronToolStripMenuItem";
            OctahedronToolStripMenuItem.Size = new Size(132, 22);
            OctahedronToolStripMenuItem.Text = "Октаэдр";
            OctahedronToolStripMenuItem.Click += OctahedronToolStripMenuItem_Click;
            // 
            // IcosahedronToolStripMenuItem
            // 
            IcosahedronToolStripMenuItem.Name = "IcosahedronToolStripMenuItem";
            IcosahedronToolStripMenuItem.Size = new Size(132, 22);
            IcosahedronToolStripMenuItem.Text = "Икосаэдр";
            IcosahedronToolStripMenuItem.Click += IcosahedronToolStripMenuItem_Click;
            // 
            // DodecahedronToolStripMenuItem
            // 
            DodecahedronToolStripMenuItem.Name = "DodecahedronToolStripMenuItem";
            DodecahedronToolStripMenuItem.Size = new Size(132, 22);
            DodecahedronToolStripMenuItem.Text = "Додекаэдр";
            DodecahedronToolStripMenuItem.Click += DodecahedronToolStripMenuItem_Click;
            // 
            // проекцияToolStripMenuItem
            // 
            проекцияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { PerspectiveToolStripMenuItem, TrimetricToolStripMenuItem, DimetricToolStripMenuItem, IsometricToolStripMenuItem });
            проекцияToolStripMenuItem.Name = "проекцияToolStripMenuItem";
            проекцияToolStripMenuItem.Size = new Size(74, 20);
            проекцияToolStripMenuItem.Text = "Проекция";
            // 
            // PerspectiveToolStripMenuItem
            // 
            PerspectiveToolStripMenuItem.Name = "PerspectiveToolStripMenuItem";
            PerspectiveToolStripMenuItem.Size = new Size(166, 22);
            PerspectiveToolStripMenuItem.Text = "Перспективная ";
            PerspectiveToolStripMenuItem.Click += PerspectiveToolStripMenuItem_Click;
            // 
            // TrimetricToolStripMenuItem
            // 
            TrimetricToolStripMenuItem.Name = "TrimetricToolStripMenuItem";
            TrimetricToolStripMenuItem.Size = new Size(166, 22);
            TrimetricToolStripMenuItem.Text = "Триметрическая";
            TrimetricToolStripMenuItem.Click += TrimetricToolStripMenuItem_Click;
            // 
            // DimetricToolStripMenuItem
            // 
            DimetricToolStripMenuItem.Name = "DimetricToolStripMenuItem";
            DimetricToolStripMenuItem.Size = new Size(166, 22);
            DimetricToolStripMenuItem.Text = "Диметрическая";
            DimetricToolStripMenuItem.Click += DimetricToolStripMenuItem_Click;
            // 
            // IsometricToolStripMenuItem
            // 
            IsometricToolStripMenuItem.Name = "IsometricToolStripMenuItem";
            IsometricToolStripMenuItem.Size = new Size(166, 22);
            IsometricToolStripMenuItem.Text = "Изометрическая";
            IsometricToolStripMenuItem.Click += IsometricToolStripMenuItem_Click;
            // 
            // типОсвещенияToolStripMenuItem
            // 
            типОсвещенияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { нетToolStripMenuItem, шейдингГуроДляМоделиЛамбертаToolStripMenuItem, шейдингФонгаДляМодToolStripMenuItem });
            типОсвещенияToolStripMenuItem.Name = "типОсвещенияToolStripMenuItem";
            типОсвещенияToolStripMenuItem.Size = new Size(105, 20);
            типОсвещенияToolStripMenuItem.Text = "Тип освещения";
            // 
            // нетToolStripMenuItem
            // 
            нетToolStripMenuItem.Name = "нетToolStripMenuItem";
            нетToolStripMenuItem.Size = new Size(299, 22);
            нетToolStripMenuItem.Text = "Нет";
            нетToolStripMenuItem.Click += нетToolStripMenuItem_Click;
            // 
            // шейдингГуроДляМоделиЛамбертаToolStripMenuItem
            // 
            шейдингГуроДляМоделиЛамбертаToolStripMenuItem.Name = "шейдингГуроДляМоделиЛамбертаToolStripMenuItem";
            шейдингГуроДляМоделиЛамбертаToolStripMenuItem.Size = new Size(299, 22);
            шейдингГуроДляМоделиЛамбертаToolStripMenuItem.Text = "Шейдинг Гуро для модели Ламберта";
            шейдингГуроДляМоделиЛамбертаToolStripMenuItem.Click += шейдингГуроДляМоделиЛамбертаToolStripMenuItem_Click;
            // 
            // шейдингФонгаДляМодToolStripMenuItem
            // 
            шейдингФонгаДляМодToolStripMenuItem.Name = "шейдингФонгаДляМодToolStripMenuItem";
            шейдингФонгаДляМодToolStripMenuItem.Size = new Size(299, 22);
            шейдингФонгаДляМодToolStripMenuItem.Text = "Шейдинг Фонга для модели Туншейдинг";
            шейдингФонгаДляМодToolStripMenuItem.Click += шейдингФонгаДляМодToolStripMenuItem_Click;
            // 
            // currentPhigureToolStripMenuItem
            // 
            currentPhigureToolStripMenuItem.Name = "currentPhigureToolStripMenuItem";
            currentPhigureToolStripMenuItem.Size = new Size(110, 20);
            currentPhigureToolStripMenuItem.Text = "Текущая фигура";
            currentPhigureToolStripMenuItem.Click += currentPhigureToolStripMenuItem_Click;
            // 
            // ScaleRadioButton
            // 
            ScaleRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScaleRadioButton.AutoSize = true;
            ScaleRadioButton.Location = new Point(1389, 143);
            ScaleRadioButton.Margin = new Padding(3, 2, 3, 2);
            ScaleRadioButton.Name = "ScaleRadioButton";
            ScaleRadioButton.Size = new Size(196, 19);
            ScaleRadioButton.TabIndex = 28;
            ScaleRadioButton.Text = "Масштабирование отн. центра";
            ScaleRadioButton.UseVisualStyleBackColor = true;
            ScaleRadioButton.CheckedChanged += ScaleRadioButton_CheckedChanged;
            // 
            // TranslateAboutZRadioButton
            // 
            TranslateAboutZRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TranslateAboutZRadioButton.AutoSize = true;
            TranslateAboutZRadioButton.Location = new Point(1379, 581);
            TranslateAboutZRadioButton.Margin = new Padding(3, 2, 3, 2);
            TranslateAboutZRadioButton.Name = "TranslateAboutZRadioButton";
            TranslateAboutZRadioButton.Size = new Size(135, 19);
            TranslateAboutZRadioButton.TabIndex = 31;
            TranslateAboutZRadioButton.Text = "Смещение по оси Z";
            TranslateAboutZRadioButton.UseVisualStyleBackColor = true;
            TranslateAboutZRadioButton.CheckedChanged += TranslateAboutZRadioButton_CheckedChanged;
            // 
            // TranslateAboutYRadioButton
            // 
            TranslateAboutYRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TranslateAboutYRadioButton.AutoSize = true;
            TranslateAboutYRadioButton.Location = new Point(1379, 556);
            TranslateAboutYRadioButton.Margin = new Padding(3, 2, 3, 2);
            TranslateAboutYRadioButton.Name = "TranslateAboutYRadioButton";
            TranslateAboutYRadioButton.Size = new Size(135, 19);
            TranslateAboutYRadioButton.TabIndex = 30;
            TranslateAboutYRadioButton.Text = "Смещение по оси Y";
            TranslateAboutYRadioButton.UseVisualStyleBackColor = true;
            TranslateAboutYRadioButton.CheckedChanged += TranslateAboutYRadioButton_CheckedChanged;
            // 
            // TranslateAboutXRadioButton
            // 
            TranslateAboutXRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TranslateAboutXRadioButton.AutoSize = true;
            TranslateAboutXRadioButton.Location = new Point(1379, 532);
            TranslateAboutXRadioButton.Margin = new Padding(3, 2, 3, 2);
            TranslateAboutXRadioButton.Name = "TranslateAboutXRadioButton";
            TranslateAboutXRadioButton.Size = new Size(135, 19);
            TranslateAboutXRadioButton.TabIndex = 29;
            TranslateAboutXRadioButton.Text = "Смещение по оси X";
            TranslateAboutXRadioButton.UseVisualStyleBackColor = true;
            TranslateAboutXRadioButton.CheckedChanged += TranslateAboutXRadioButton_CheckedChanged;
            // 
            // ScaleAboutXRadioButton
            // 
            ScaleAboutXRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScaleAboutXRadioButton.AutoSize = true;
            ScaleAboutXRadioButton.Location = new Point(1389, 166);
            ScaleAboutXRadioButton.Margin = new Padding(3, 2, 3, 2);
            ScaleAboutXRadioButton.Name = "ScaleAboutXRadioButton";
            ScaleAboutXRadioButton.Size = new Size(188, 19);
            ScaleAboutXRadioButton.TabIndex = 32;
            ScaleAboutXRadioButton.Text = "Масштабирование отн. оси X";
            ScaleAboutXRadioButton.UseVisualStyleBackColor = true;
            ScaleAboutXRadioButton.CheckedChanged += ScaleAboutXRadioButton_CheckedChanged;
            // 
            // ScaleAboutYRadioButton
            // 
            ScaleAboutYRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScaleAboutYRadioButton.AutoSize = true;
            ScaleAboutYRadioButton.Location = new Point(1388, 190);
            ScaleAboutYRadioButton.Margin = new Padding(3, 2, 3, 2);
            ScaleAboutYRadioButton.Name = "ScaleAboutYRadioButton";
            ScaleAboutYRadioButton.Size = new Size(188, 19);
            ScaleAboutYRadioButton.TabIndex = 33;
            ScaleAboutYRadioButton.Text = "Масштабирование отн. оси Y";
            ScaleAboutYRadioButton.UseVisualStyleBackColor = true;
            ScaleAboutYRadioButton.CheckedChanged += ScaleAboutYRadioButton_CheckedChanged;
            // 
            // ScaleAboutZRadioButton
            // 
            ScaleAboutZRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ScaleAboutZRadioButton.AutoSize = true;
            ScaleAboutZRadioButton.Location = new Point(1388, 212);
            ScaleAboutZRadioButton.Margin = new Padding(3, 2, 3, 2);
            ScaleAboutZRadioButton.Name = "ScaleAboutZRadioButton";
            ScaleAboutZRadioButton.Size = new Size(188, 19);
            ScaleAboutZRadioButton.TabIndex = 34;
            ScaleAboutZRadioButton.Text = "Масштабирование отн. оси Z";
            ScaleAboutZRadioButton.UseVisualStyleBackColor = true;
            ScaleAboutZRadioButton.CheckedChanged += ScaleAboutZRadioButton_CheckedChanged;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Location = new Point(1541, 464);
            label10.Name = "label10";
            label10.Size = new Size(17, 15);
            label10.TabIndex = 40;
            label10.Text = "Z:";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(1467, 464);
            label11.Name = "label11";
            label11.Size = new Size(17, 15);
            label11.TabIndex = 39;
            label11.Text = "Y:";
            // 
            // lineEndYNumericUpDown
            // 
            lineEndYNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineEndYNumericUpDown.DecimalPlaces = 2;
            lineEndYNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineEndYNumericUpDown.Location = new Point(1488, 460);
            lineEndYNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineEndYNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineEndYNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineEndYNumericUpDown.Name = "lineEndYNumericUpDown";
            lineEndYNumericUpDown.Size = new Size(53, 23);
            lineEndYNumericUpDown.TabIndex = 38;
            lineEndYNumericUpDown.ValueChanged += lineEndNumericUpDown_ValueChanged;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(1388, 464);
            label12.Name = "label12";
            label12.Size = new Size(17, 15);
            label12.TabIndex = 37;
            label12.Text = "X:";
            // 
            // lineEndXNumericUpDown
            // 
            lineEndXNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineEndXNumericUpDown.DecimalPlaces = 2;
            lineEndXNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineEndXNumericUpDown.Location = new Point(1409, 460);
            lineEndXNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineEndXNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineEndXNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineEndXNumericUpDown.Name = "lineEndXNumericUpDown";
            lineEndXNumericUpDown.Size = new Size(53, 23);
            lineEndXNumericUpDown.TabIndex = 36;
            lineEndXNumericUpDown.ValueChanged += lineEndNumericUpDown_ValueChanged;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Location = new Point(1388, 443);
            label13.Name = "label13";
            label13.Size = new Size(120, 15);
            label13.TabIndex = 35;
            label13.Text = "Координаты точки B";
            // 
            // lineEndZNumericUpDown
            // 
            lineEndZNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lineEndZNumericUpDown.DecimalPlaces = 2;
            lineEndZNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            lineEndZNumericUpDown.Location = new Point(1565, 460);
            lineEndZNumericUpDown.Margin = new Padding(3, 2, 3, 2);
            lineEndZNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lineEndZNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            lineEndZNumericUpDown.Name = "lineEndZNumericUpDown";
            lineEndZNumericUpDown.Size = new Size(53, 23);
            lineEndZNumericUpDown.TabIndex = 41;
            lineEndZNumericUpDown.ValueChanged += lineEndNumericUpDown_ValueChanged;
            // 
            // refclectXYbutton
            // 
            refclectXYbutton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refclectXYbutton.Location = new Point(1367, 616);
            refclectXYbutton.Margin = new Padding(4);
            refclectXYbutton.Name = "refclectXYbutton";
            refclectXYbutton.Size = new Size(251, 26);
            refclectXYbutton.TabIndex = 42;
            refclectXYbutton.Text = "Отразить по XY";
            refclectXYbutton.UseVisualStyleBackColor = true;
            refclectXYbutton.Click += refclectXYbutton_Click;
            // 
            // refclectXZbutton
            // 
            refclectXZbutton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refclectXZbutton.Location = new Point(1367, 648);
            refclectXZbutton.Margin = new Padding(4);
            refclectXZbutton.Name = "refclectXZbutton";
            refclectXZbutton.Size = new Size(251, 26);
            refclectXZbutton.TabIndex = 43;
            refclectXZbutton.Text = "Отразить по XZ";
            refclectXZbutton.UseVisualStyleBackColor = true;
            refclectXZbutton.Click += refclectXZbutton_Click;
            // 
            // refclectYZbutton
            // 
            refclectYZbutton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refclectYZbutton.Location = new Point(1369, 682);
            refclectYZbutton.Margin = new Padding(4);
            refclectYZbutton.Name = "refclectYZbutton";
            refclectYZbutton.Size = new Size(249, 26);
            refclectYZbutton.TabIndex = 44;
            refclectYZbutton.Text = "Отразить по YZ";
            refclectYZbutton.UseVisualStyleBackColor = true;
            refclectYZbutton.Click += refclectYZbutton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(lightSourceRadioButton);
            groupBox1.Controls.Add(setCamersRadioButton);
            groupBox1.Controls.Add(setPolyhedronRadioButton);
            groupBox1.Location = new Point(1369, 32);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(219, 106);
            groupBox1.TabIndex = 45;
            groupBox1.TabStop = false;
            groupBox1.Text = "Выбор объекта";
            // 
            // lightSourceRadioButton
            // 
            lightSourceRadioButton.AutoSize = true;
            lightSourceRadioButton.Location = new Point(12, 64);
            lightSourceRadioButton.Margin = new Padding(3, 2, 3, 2);
            lightSourceRadioButton.Name = "lightSourceRadioButton";
            lightSourceRadioButton.Size = new Size(111, 19);
            lightSourceRadioButton.TabIndex = 2;
            lightSourceRadioButton.TabStop = true;
            lightSourceRadioButton.Text = "Источник света";
            lightSourceRadioButton.UseVisualStyleBackColor = true;
            lightSourceRadioButton.CheckedChanged += lightSourceRadioButton_CheckedChanged;
            // 
            // setCamersRadioButton
            // 
            setCamersRadioButton.AutoSize = true;
            setCamersRadioButton.Location = new Point(12, 42);
            setCamersRadioButton.Margin = new Padding(3, 2, 3, 2);
            setCamersRadioButton.Name = "setCamersRadioButton";
            setCamersRadioButton.Size = new Size(66, 19);
            setCamersRadioButton.TabIndex = 1;
            setCamersRadioButton.TabStop = true;
            setCamersRadioButton.Text = "Камера";
            setCamersRadioButton.UseVisualStyleBackColor = true;
            setCamersRadioButton.CheckedChanged += setCamersRadioButton_CheckedChanged;
            // 
            // setPolyhedronRadioButton
            // 
            setPolyhedronRadioButton.AutoSize = true;
            setPolyhedronRadioButton.Location = new Point(12, 20);
            setPolyhedronRadioButton.Margin = new Padding(3, 2, 3, 2);
            setPolyhedronRadioButton.Name = "setPolyhedronRadioButton";
            setPolyhedronRadioButton.Size = new Size(107, 19);
            setPolyhedronRadioButton.TabIndex = 0;
            setPolyhedronRadioButton.TabStop = true;
            setPolyhedronRadioButton.Text = "Многогранник";
            setPolyhedronRadioButton.UseVisualStyleBackColor = true;
            setPolyhedronRadioButton.CheckedChanged += setPolyhedronRadioButton_CheckedChanged;
            // 
            // zBufferCheckBox
            // 
            zBufferCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            zBufferCheckBox.AutoSize = true;
            zBufferCheckBox.Location = new Point(1369, 725);
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
            ClientSize = new Size(1627, 791);
            Controls.Add(zBufferCheckBox);
            Controls.Add(groupBox1);
            Controls.Add(refclectYZbutton);
            Controls.Add(refclectXZbutton);
            Controls.Add(refclectXYbutton);
            Controls.Add(lineEndZNumericUpDown);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(lineEndYNumericUpDown);
            Controls.Add(label12);
            Controls.Add(lineEndXNumericUpDown);
            Controls.Add(label13);
            Controls.Add(ScaleAboutZRadioButton);
            Controls.Add(ScaleAboutYRadioButton);
            Controls.Add(ScaleAboutXRadioButton);
            Controls.Add(TranslateAboutZRadioButton);
            Controls.Add(TranslateAboutYRadioButton);
            Controls.Add(TranslateAboutXRadioButton);
            Controls.Add(ScaleRadioButton);
            Controls.Add(menuStrip1);
            Controls.Add(label7);
            Controls.Add(lineVecZNumericUpDown);
            Controls.Add(label8);
            Controls.Add(lineVecYNumericUpDown);
            Controls.Add(label9);
            Controls.Add(lineVecXNumericUpDown);
            Controls.Add(label6);
            Controls.Add(lineStartZNumericUpDown);
            Controls.Add(label5);
            Controls.Add(lineStartYNumericUpDown);
            Controls.Add(label4);
            Controls.Add(lineStartXNumericUpDown);
            Controls.Add(customRotatingRadioButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(RotateAboutZRadioButton);
            Controls.Add(RotateAboutYRadioButton);
            Controls.Add(RotateAboutXRadioButton);
            Controls.Add(panel1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Scene";
            Text = "Scene";
            WindowState = FormWindowState.Maximized;
            SizeChanged += Scene_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)lineStartXNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineStartYNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineStartZNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineVecZNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineVecYNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineVecXNumericUpDown).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lineEndYNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineEndXNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)lineEndZNumericUpDown).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RotateAboutXRadioButton;
        private System.Windows.Forms.RadioButton RotateAboutYRadioButton;
        private System.Windows.Forms.RadioButton RotateAboutZRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton customRotatingRadioButton;
        private System.Windows.Forms.NumericUpDown lineStartXNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown lineStartYNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown lineStartZNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown lineVecZNumericUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown lineVecYNumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown lineVecXNumericUpDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem фигурыToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem фигурыToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem TetrahedronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HexahedronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OctahedronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IcosahedronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DodecahedronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проекцияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PerspectiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TrimetricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DimetricToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IsometricToolStripMenuItem;
        private System.Windows.Forms.RadioButton ScaleRadioButton;
        private System.Windows.Forms.RadioButton TranslateAboutZRadioButton;
        private System.Windows.Forms.RadioButton TranslateAboutYRadioButton;
        private System.Windows.Forms.RadioButton TranslateAboutXRadioButton;
        private System.Windows.Forms.RadioButton ScaleAboutXRadioButton;
        private System.Windows.Forms.RadioButton ScaleAboutYRadioButton;
        private System.Windows.Forms.RadioButton ScaleAboutZRadioButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown lineEndYNumericUpDown;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown lineEndXNumericUpDown;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown lineEndZNumericUpDown;
        private System.Windows.Forms.Button refclectXYbutton;
        private System.Windows.Forms.Button refclectXZbutton;
        private System.Windows.Forms.Button refclectYZbutton;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private GroupBox groupBox1;
        private RadioButton setCamersRadioButton;
        private RadioButton setPolyhedronRadioButton;
        private ToolStripMenuItem currentPhigureToolStripMenuItem;
        private CheckBox zBufferCheckBox;
        private ToolStripMenuItem типОсвещенияToolStripMenuItem;
        private ToolStripMenuItem нетToolStripMenuItem;
        private ToolStripMenuItem шейдингГуроДляМоделиЛамбертаToolStripMenuItem;
        private ToolStripMenuItem шейдингФонгаДляМодToolStripMenuItem;
        private RadioButton lightSourceRadioButton;
    }
}

