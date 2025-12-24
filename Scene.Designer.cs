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
            vlueCubeRadioButton = new RadioButton();
            RedCuberadioButton = new RadioButton();
            figuresSettingsGroupBox = new GroupBox();
            transparencyRadioButton = new RadioButton();
            mirrorRadioButton = new RadioButton();
            nothingRadioButton = new RadioButton();
            WalsGroupBox = new GroupBox();
            farWallRadioButton = new RadioButton();
            bottomWallRadioButton = new RadioButton();
            topWallRadioButton = new RadioButton();
            leftWallRadioButton = new RadioButton();
            rightWallRadioButton = new RadioButton();
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
            figuresGroupBox.Controls.Add(vlueCubeRadioButton);
            figuresGroupBox.Controls.Add(RedCuberadioButton);
            figuresGroupBox.Location = new Point(552, 12);
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
            greenSphereRadioButton.TabStop = true;
            greenSphereRadioButton.Text = "Зелёная сфера";
            greenSphereRadioButton.UseVisualStyleBackColor = true;
            // 
            // yellowSphereRadioButton
            // 
            yellowSphereRadioButton.AutoSize = true;
            yellowSphereRadioButton.Location = new Point(6, 72);
            yellowSphereRadioButton.Name = "yellowSphereRadioButton";
            yellowSphereRadioButton.Size = new Size(103, 19);
            yellowSphereRadioButton.TabIndex = 2;
            yellowSphereRadioButton.TabStop = true;
            yellowSphereRadioButton.Text = "Желтая сфера";
            yellowSphereRadioButton.UseVisualStyleBackColor = true;
            // 
            // vlueCubeRadioButton
            // 
            vlueCubeRadioButton.AutoSize = true;
            vlueCubeRadioButton.Location = new Point(6, 47);
            vlueCubeRadioButton.Name = "vlueCubeRadioButton";
            vlueCubeRadioButton.Size = new Size(83, 19);
            vlueCubeRadioButton.TabIndex = 1;
            vlueCubeRadioButton.TabStop = true;
            vlueCubeRadioButton.Text = "Синий куб";
            vlueCubeRadioButton.UseVisualStyleBackColor = true;
            // 
            // RedCuberadioButton
            // 
            RedCuberadioButton.AutoSize = true;
            RedCuberadioButton.Location = new Point(6, 22);
            RedCuberadioButton.Name = "RedCuberadioButton";
            RedCuberadioButton.Size = new Size(96, 19);
            RedCuberadioButton.TabIndex = 0;
            RedCuberadioButton.TabStop = true;
            RedCuberadioButton.Text = "Красный куб";
            RedCuberadioButton.UseVisualStyleBackColor = true;
            // 
            // figuresSettingsGroupBox
            // 
            figuresSettingsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            figuresSettingsGroupBox.Controls.Add(transparencyRadioButton);
            figuresSettingsGroupBox.Controls.Add(mirrorRadioButton);
            figuresSettingsGroupBox.Controls.Add(nothingRadioButton);
            figuresSettingsGroupBox.Location = new Point(552, 140);
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
            // 
            // WalsGroupBox
            // 
            WalsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            WalsGroupBox.Controls.Add(farWallRadioButton);
            WalsGroupBox.Controls.Add(bottomWallRadioButton);
            WalsGroupBox.Controls.Add(topWallRadioButton);
            WalsGroupBox.Controls.Add(leftWallRadioButton);
            WalsGroupBox.Controls.Add(rightWallRadioButton);
            WalsGroupBox.Location = new Point(552, 247);
            WalsGroupBox.Name = "WalsGroupBox";
            WalsGroupBox.Size = new Size(200, 152);
            WalsGroupBox.TabIndex = 48;
            WalsGroupBox.TabStop = false;
            WalsGroupBox.Text = "Зеркальность стен";
            // 
            // farWallRadioButton
            // 
            farWallRadioButton.AutoSize = true;
            farWallRadioButton.Location = new Point(6, 122);
            farWallRadioButton.Name = "farWallRadioButton";
            farWallRadioButton.Size = new Size(104, 19);
            farWallRadioButton.TabIndex = 4;
            farWallRadioButton.TabStop = true;
            farWallRadioButton.Text = "Дальняя стена";
            farWallRadioButton.UseVisualStyleBackColor = true;
            // 
            // bottomWallRadioButton
            // 
            bottomWallRadioButton.AutoSize = true;
            bottomWallRadioButton.Location = new Point(6, 97);
            bottomWallRadioButton.Name = "bottomWallRadioButton";
            bottomWallRadioButton.Size = new Size(102, 19);
            bottomWallRadioButton.TabIndex = 3;
            bottomWallRadioButton.TabStop = true;
            bottomWallRadioButton.Text = "Нижняя стена";
            bottomWallRadioButton.UseVisualStyleBackColor = true;
            // 
            // topWallRadioButton
            // 
            topWallRadioButton.AutoSize = true;
            topWallRadioButton.Location = new Point(6, 72);
            topWallRadioButton.Name = "topWallRadioButton";
            topWallRadioButton.Size = new Size(102, 19);
            topWallRadioButton.TabIndex = 2;
            topWallRadioButton.TabStop = true;
            topWallRadioButton.Text = "Верхняя стена";
            topWallRadioButton.UseVisualStyleBackColor = true;
            // 
            // leftWallRadioButton
            // 
            leftWallRadioButton.AutoSize = true;
            leftWallRadioButton.Location = new Point(6, 47);
            leftWallRadioButton.Name = "leftWallRadioButton";
            leftWallRadioButton.Size = new Size(90, 19);
            leftWallRadioButton.TabIndex = 1;
            leftWallRadioButton.TabStop = true;
            leftWallRadioButton.Text = "Левая стена";
            leftWallRadioButton.UseVisualStyleBackColor = true;
            // 
            // rightWallRadioButton
            // 
            rightWallRadioButton.AutoSize = true;
            rightWallRadioButton.Location = new Point(6, 22);
            rightWallRadioButton.Name = "rightWallRadioButton";
            rightWallRadioButton.Size = new Size(98, 19);
            rightWallRadioButton.TabIndex = 0;
            rightWallRadioButton.TabStop = true;
            rightWallRadioButton.Text = "Правая стена";
            rightWallRadioButton.UseVisualStyleBackColor = true;
            // 
            // Render
            // 
            Render.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Render.Location = new Point(558, 423);
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
            progressBar.Location = new Point(558, 452);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(194, 23);
            progressBar.TabIndex = 50;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(534, 463);
            panel1.TabIndex = 51;
            panel1.TabStop = false;
            // 
            // Scene
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 491);
            Controls.Add(panel1);
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
        private RadioButton vlueCubeRadioButton;
        private RadioButton RedCuberadioButton;
        private RadioButton greenSphereRadioButton;
        private RadioButton yellowSphereRadioButton;
        private GroupBox figuresSettingsGroupBox;
        private RadioButton transparencyRadioButton;
        private RadioButton mirrorRadioButton;
        private RadioButton nothingRadioButton;
        private GroupBox WalsGroupBox;
        private RadioButton farWallRadioButton;
        private RadioButton bottomWallRadioButton;
        private RadioButton topWallRadioButton;
        private RadioButton leftWallRadioButton;
        private RadioButton rightWallRadioButton;
        private Button Render;
        private ProgressBar progressBar;
        private PictureBox panel1;
    }
}

