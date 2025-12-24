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
            figuresGroupBox = new GroupBox();
            greenSphereRadioButton = new RadioButton();
            yellowSphereRadioButton = new RadioButton();
            blueCubeRadioButton = new RadioButton();
            RedCubeRadioButton = new RadioButton();
            figuresSettingsGroupBox = new GroupBox();
            transparencyRadioButton = new RadioButton();
            mirrorRadioButton = new RadioButton();
            nothingRadioButton = new RadioButton();
            WalsGroupBox = new GroupBox();
            backWallCheckBox = new CheckBox();
            resetButton = new Button();
            farWallCheckBox = new CheckBox();
            bottomWallCheckBox = new CheckBox();
            topWallCheckBox = new CheckBox();
            leftWallCheckBox = new CheckBox();
            rightWallCheckBox = new CheckBox();
            Render = new Button();
            progressBar = new ProgressBar();
            panel1 = new PictureBox();
            figuresGroupBox.SuspendLayout();
            figuresSettingsGroupBox.SuspendLayout();
            WalsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panel1).BeginInit();
            SuspendLayout();
            // 
            // figuresGroupBox
            // 
            figuresGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            figuresGroupBox.Controls.Add(greenSphereRadioButton);
            figuresGroupBox.Controls.Add(yellowSphereRadioButton);
            figuresGroupBox.Controls.Add(blueCubeRadioButton);
            figuresGroupBox.Controls.Add(RedCubeRadioButton);
            figuresGroupBox.Location = new Point(614, 12);
            figuresGroupBox.Name = "figuresGroupBox";
            figuresGroupBox.Size = new Size(200, 122);
            figuresGroupBox.TabIndex = 47;
            figuresGroupBox.TabStop = false;
            figuresGroupBox.Text = "Фигура";
            // 
            // greenSphereRadioButton
            // 
            greenSphereRadioButton.AutoSize = true;
            greenSphereRadioButton.Location = new Point(6, 97);
            greenSphereRadioButton.Name = "greenSphereRadioButton";
            greenSphereRadioButton.Size = new Size(107, 19);
            greenSphereRadioButton.TabIndex = 3;
            greenSphereRadioButton.Text = "Зелёная сфера";
            greenSphereRadioButton.UseVisualStyleBackColor = true;
            greenSphereRadioButton.CheckedChanged += greenSphereRadioButton_CheckedChanged;
            // 
            // yellowSphereRadioButton
            // 
            yellowSphereRadioButton.AutoSize = true;
            yellowSphereRadioButton.Location = new Point(6, 72);
            yellowSphereRadioButton.Name = "yellowSphereRadioButton";
            yellowSphereRadioButton.Size = new Size(103, 19);
            yellowSphereRadioButton.TabIndex = 2;
            yellowSphereRadioButton.Text = "Желтая сфера";
            yellowSphereRadioButton.UseVisualStyleBackColor = true;
            yellowSphereRadioButton.CheckedChanged += yellowSphereRadioButton_CheckedChanged;
            // 
            // blueCubeRadioButton
            // 
            blueCubeRadioButton.AutoSize = true;
            blueCubeRadioButton.Location = new Point(6, 47);
            blueCubeRadioButton.Name = "blueCubeRadioButton";
            blueCubeRadioButton.Size = new Size(83, 19);
            blueCubeRadioButton.TabIndex = 1;
            blueCubeRadioButton.Text = "Синий куб";
            blueCubeRadioButton.UseVisualStyleBackColor = true;
            blueCubeRadioButton.CheckedChanged += blueCubeRadioButton_CheckedChanged;
            // 
            // RedCubeRadioButton
            // 
            RedCubeRadioButton.AutoSize = true;
            RedCubeRadioButton.Checked = true;
            RedCubeRadioButton.Location = new Point(6, 22);
            RedCubeRadioButton.Name = "RedCubeRadioButton";
            RedCubeRadioButton.Size = new Size(96, 19);
            RedCubeRadioButton.TabIndex = 0;
            RedCubeRadioButton.TabStop = true;
            RedCubeRadioButton.Text = "Красный куб";
            RedCubeRadioButton.UseVisualStyleBackColor = true;
            RedCubeRadioButton.CheckedChanged += RedCubeRadioButton_CheckedChanged;
            // 
            // figuresSettingsGroupBox
            // 
            figuresSettingsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            figuresSettingsGroupBox.Controls.Add(transparencyRadioButton);
            figuresSettingsGroupBox.Controls.Add(mirrorRadioButton);
            figuresSettingsGroupBox.Controls.Add(nothingRadioButton);
            figuresSettingsGroupBox.Location = new Point(614, 140);
            figuresSettingsGroupBox.Name = "figuresSettingsGroupBox";
            figuresSettingsGroupBox.Size = new Size(200, 101);
            figuresSettingsGroupBox.TabIndex = 48;
            figuresSettingsGroupBox.TabStop = false;
            figuresSettingsGroupBox.Text = "Управление фигурой";
            // 
            // transparencyRadioButton
            // 
            transparencyRadioButton.AutoSize = true;
            transparencyRadioButton.Location = new Point(6, 72);
            transparencyRadioButton.Name = "transparencyRadioButton";
            transparencyRadioButton.Size = new Size(104, 19);
            transparencyRadioButton.TabIndex = 2;
            transparencyRadioButton.Text = "Прозрачность";
            transparencyRadioButton.UseVisualStyleBackColor = true;
            transparencyRadioButton.CheckedChanged += transparencyRadioButton_CheckedChanged;
            // 
            // mirrorRadioButton
            // 
            mirrorRadioButton.AutoSize = true;
            mirrorRadioButton.Location = new Point(6, 47);
            mirrorRadioButton.Name = "mirrorRadioButton";
            mirrorRadioButton.Size = new Size(101, 19);
            mirrorRadioButton.TabIndex = 1;
            mirrorRadioButton.Text = "Зеркальность";
            mirrorRadioButton.UseVisualStyleBackColor = true;
            mirrorRadioButton.CheckedChanged += mirrorRadioButton_CheckedChanged;
            // 
            // nothingRadioButton
            // 
            nothingRadioButton.AutoSize = true;
            nothingRadioButton.Checked = true;
            nothingRadioButton.Location = new Point(6, 22);
            nothingRadioButton.Name = "nothingRadioButton";
            nothingRadioButton.Size = new Size(66, 19);
            nothingRadioButton.TabIndex = 0;
            nothingRadioButton.TabStop = true;
            nothingRadioButton.Text = "Ничего";
            nothingRadioButton.UseVisualStyleBackColor = true;
            nothingRadioButton.CheckedChanged += nothingRadioButton_CheckedChanged;
            // 
            // WalsGroupBox
            // 
            WalsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            WalsGroupBox.Controls.Add(backWallCheckBox);
            WalsGroupBox.Controls.Add(farWallCheckBox);
            WalsGroupBox.Controls.Add(bottomWallCheckBox);
            WalsGroupBox.Controls.Add(topWallCheckBox);
            WalsGroupBox.Controls.Add(leftWallCheckBox);
            WalsGroupBox.Controls.Add(rightWallCheckBox);
            WalsGroupBox.Location = new Point(614, 247);
            WalsGroupBox.Name = "WalsGroupBox";
            WalsGroupBox.Size = new Size(200, 171);
            WalsGroupBox.TabIndex = 48;
            WalsGroupBox.TabStop = false;
            WalsGroupBox.Text = "Зеркальность стен";
            // 
            // backWallCheckBox
            // 
            backWallCheckBox.AutoSize = true;
            backWallCheckBox.Location = new Point(6, 147);
            backWallCheckBox.Name = "backWallCheckBox";
            backWallCheckBox.Size = new Size(97, 19);
            backWallCheckBox.TabIndex = 6;
            backWallCheckBox.Text = "Задняя стена";
            backWallCheckBox.UseVisualStyleBackColor = true;
            backWallCheckBox.CheckedChanged += backWallCheckBox_CheckedChanged;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            resetButton.Location = new Point(620, 424);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(182, 23);
            resetButton.TabIndex = 5;
            resetButton.Text = "Сброс";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // farWallCheckBox
            // 
            farWallCheckBox.AutoSize = true;
            farWallCheckBox.Location = new Point(6, 122);
            farWallCheckBox.Name = "farWallCheckBox";
            farWallCheckBox.Size = new Size(105, 19);
            farWallCheckBox.TabIndex = 4;
            farWallCheckBox.Text = "Дальняя стена";
            farWallCheckBox.UseVisualStyleBackColor = true;
            farWallCheckBox.CheckedChanged += farWallCheckBox_CheckedChanged;
            // 
            // bottomWallCheckBox
            // 
            bottomWallCheckBox.AutoSize = true;
            bottomWallCheckBox.Location = new Point(6, 97);
            bottomWallCheckBox.Name = "bottomWallCheckBox";
            bottomWallCheckBox.Size = new Size(103, 19);
            bottomWallCheckBox.TabIndex = 3;
            bottomWallCheckBox.Text = "Нижняя стена";
            bottomWallCheckBox.UseVisualStyleBackColor = true;
            bottomWallCheckBox.CheckedChanged += bottomWallCheckBox_CheckedChanged;
            // 
            // topWallCheckBox
            // 
            topWallCheckBox.AutoSize = true;
            topWallCheckBox.Location = new Point(6, 72);
            topWallCheckBox.Name = "topWallCheckBox";
            topWallCheckBox.Size = new Size(103, 19);
            topWallCheckBox.TabIndex = 2;
            topWallCheckBox.Text = "Верхняя стена";
            topWallCheckBox.UseVisualStyleBackColor = true;
            topWallCheckBox.CheckedChanged += topWallCheckBox_CheckedChanged;
            // 
            // leftWallCheckBox
            // 
            leftWallCheckBox.AutoSize = true;
            leftWallCheckBox.Location = new Point(6, 47);
            leftWallCheckBox.Name = "leftWallCheckBox";
            leftWallCheckBox.Size = new Size(91, 19);
            leftWallCheckBox.TabIndex = 1;
            leftWallCheckBox.Text = "Левая стена";
            leftWallCheckBox.UseVisualStyleBackColor = true;
            leftWallCheckBox.CheckedChanged += leftWallCheckBox_CheckedChanged;
            // 
            // rightWallCheckBox
            // 
            rightWallCheckBox.AutoSize = true;
            rightWallCheckBox.Location = new Point(6, 22);
            rightWallCheckBox.Name = "rightWallCheckBox";
            rightWallCheckBox.Size = new Size(99, 19);
            rightWallCheckBox.TabIndex = 0;
            rightWallCheckBox.Text = "Правая стена";
            rightWallCheckBox.UseVisualStyleBackColor = true;
            rightWallCheckBox.CheckedChanged += rightWallCheckBox_CheckedChanged;
            // 
            // Render
            // 
            Render.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Render.Location = new Point(614, 459);
            Render.Name = "Render";
            Render.Size = new Size(194, 23);
            Render.TabIndex = 0;
            Render.Text = "Рендер";
            Render.UseVisualStyleBackColor = true;
            Render.Click += Render_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            progressBar.Location = new Point(614, 488);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(194, 23);
            progressBar.TabIndex = 50;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(596, 499);
            panel1.TabIndex = 51;
            panel1.TabStop = false;
            // 
            // Scene
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 525);
            Controls.Add(panel1);
            Controls.Add(resetButton);
            Controls.Add(progressBar);
            Controls.Add(Render);
            Controls.Add(WalsGroupBox);
            Controls.Add(figuresSettingsGroupBox);
            Controls.Add(figuresGroupBox);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Scene";
            Text = "Cornish Room";
            SizeChanged += Scene_SizeChanged;
            figuresGroupBox.ResumeLayout(false);
            figuresGroupBox.PerformLayout();
            figuresSettingsGroupBox.ResumeLayout(false);
            figuresSettingsGroupBox.PerformLayout();
            WalsGroupBox.ResumeLayout(false);
            WalsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panel1).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private GroupBox figuresGroupBox;
        private RadioButton blueCubeRadioButton;
        private RadioButton RedCubeRadioButton;
        private RadioButton greenSphereRadioButton;
        private RadioButton yellowSphereRadioButton;
        private GroupBox figuresSettingsGroupBox;
        private RadioButton transparencyRadioButton;
        private RadioButton mirrorRadioButton;
        private RadioButton nothingRadioButton;
        private GroupBox WalsGroupBox;
        private CheckBox farWallCheckBox;
        private CheckBox bottomWallCheckBox;
        private CheckBox topWallCheckBox;
        private CheckBox leftWallCheckBox;
        private CheckBox rightWallCheckBox;
        private Button Render;
        private ProgressBar progressBar;
        private PictureBox panel1;
        private Button resetButton;
        private CheckBox backWallCheckBox;
    }
}

