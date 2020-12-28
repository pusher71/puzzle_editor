namespace puzzle_editor
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLocationAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLocationSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLocationDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolWallsCount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolGetIO = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericSize = new System.Windows.Forms.NumericUpDown();
            this.numericEnergy = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboDirection = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonColor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.labelNumber = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.toolLocationParams = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEnergy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLocation,
            this.toolAnalysis});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(410, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolLocation
            // 
            this.toolLocation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLocationAdd,
            this.toolLocationParams,
            this.toolLocationSave,
            this.toolLocationDelete});
            this.toolLocation.Name = "toolLocation";
            this.toolLocation.Size = new System.Drawing.Size(66, 20);
            this.toolLocation.Text = "Локация";
            // 
            // toolLocationAdd
            // 
            this.toolLocationAdd.Name = "toolLocationAdd";
            this.toolLocationAdd.Size = new System.Drawing.Size(197, 22);
            this.toolLocationAdd.Text = "Добавить локацию...";
            this.toolLocationAdd.Click += new System.EventHandler(this.toolLocationAdd_Click);
            // 
            // toolLocationSave
            // 
            this.toolLocationSave.Name = "toolLocationSave";
            this.toolLocationSave.Size = new System.Drawing.Size(197, 22);
            this.toolLocationSave.Text = "Сохранить изменения";
            this.toolLocationSave.Click += new System.EventHandler(this.toolLocationSave_Click);
            // 
            // toolLocationDelete
            // 
            this.toolLocationDelete.Name = "toolLocationDelete";
            this.toolLocationDelete.Size = new System.Drawing.Size(197, 22);
            this.toolLocationDelete.Text = "Удалить текущую";
            this.toolLocationDelete.Click += new System.EventHandler(this.toolLocationDelete_Click);
            // 
            // toolAnalysis
            // 
            this.toolAnalysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolWallsCount,
            this.toolGetIO});
            this.toolAnalysis.Name = "toolAnalysis";
            this.toolAnalysis.Size = new System.Drawing.Size(78, 20);
            this.toolAnalysis.Text = "Аналитика";
            // 
            // toolWallsCount
            // 
            this.toolWallsCount.Name = "toolWallsCount";
            this.toolWallsCount.Size = new System.Drawing.Size(369, 22);
            this.toolWallsCount.Text = "Получить количество стен в каждом уровне";
            this.toolWallsCount.Click += new System.EventHandler(this.toolWallsCount_Click);
            // 
            // toolGetIO
            // 
            this.toolGetIO.Name = "toolGetIO";
            this.toolGetIO.Size = new System.Drawing.Size(369, 22);
            this.toolGetIO.Text = "Получить позиции игрока и выхода в каждом уровне";
            this.toolGetIO.Click += new System.EventHandler(this.toolGetIO_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Стена",
            "Куб",
            "Ключ",
            "Дверь",
            "Кнопка",
            "Барьер",
            "Лазерный излучатель",
            "Аптечка"});
            this.listBox1.Location = new System.Drawing.Point(12, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(140, 108);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Палитра элементов:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericSize);
            this.groupBox1.Controls.Add(this.numericEnergy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboDirection);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonColor);
            this.groupBox1.Location = new System.Drawing.Point(12, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры объекта";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Объём:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Мощность:";
            // 
            // numericSize
            // 
            this.numericSize.Location = new System.Drawing.Point(84, 101);
            this.numericSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSize.Name = "numericSize";
            this.numericSize.Size = new System.Drawing.Size(50, 20);
            this.numericSize.TabIndex = 6;
            this.numericSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericEnergy
            // 
            this.numericEnergy.Location = new System.Drawing.Point(84, 75);
            this.numericEnergy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericEnergy.Name = "numericEnergy";
            this.numericEnergy.Size = new System.Drawing.Size(50, 20);
            this.numericEnergy.TabIndex = 4;
            this.numericEnergy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Направление:";
            // 
            // comboDirection
            // 
            this.comboDirection.FormattingEnabled = true;
            this.comboDirection.Items.AddRange(new object[] {
            "west",
            "north",
            "east",
            "south"});
            this.comboDirection.Location = new System.Drawing.Point(84, 48);
            this.comboDirection.Name = "comboDirection";
            this.comboDirection.Size = new System.Drawing.Size(50, 21);
            this.comboDirection.TabIndex = 4;
            this.comboDirection.Text = "West";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Цвет:";
            // 
            // buttonColor
            // 
            this.buttonColor.BackColor = System.Drawing.Color.White;
            this.buttonColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColor.Location = new System.Drawing.Point(111, 19);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(23, 23);
            this.buttonColor.TabIndex = 0;
            this.buttonColor.UseVisualStyleBackColor = false;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(158, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 240);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonNext);
            this.groupBox2.Controls.Add(this.buttonPrev);
            this.groupBox2.Controls.Add(this.labelNumber);
            this.groupBox2.Location = new System.Drawing.Point(12, 287);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 61);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Навигация";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(73, 32);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(61, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(6, 32);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(61, 23);
            this.buttonPrev.TabIndex = 1;
            this.buttonPrev.Text = "<";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(33, 16);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(63, 13);
            this.labelNumber.TabIndex = 0;
            this.labelNumber.Text = "Локация: 1";
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            // 
            // toolLocationParams
            // 
            this.toolLocationParams.Name = "toolLocationParams";
            this.toolLocationParams.Size = new System.Drawing.Size(197, 22);
            this.toolLocationParams.Text = "Параметры локации...";
            this.toolLocationParams.Click += new System.EventHandler(this.toolLocationParams_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 360);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор головоломок";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEnergy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolLocation;
        private System.Windows.Forms.ToolStripMenuItem toolLocationAdd;
        private System.Windows.Forms.ToolStripMenuItem toolLocationDelete;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericSize;
        private System.Windows.Forms.NumericUpDown numericEnergy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDirection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem toolAnalysis;
        private System.Windows.Forms.ToolStripMenuItem toolWallsCount;
        private System.Windows.Forms.ToolStripMenuItem toolGetIO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem toolLocationSave;
        private System.Windows.Forms.ToolStripMenuItem toolLocationParams;
    }
}

