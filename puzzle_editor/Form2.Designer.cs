namespace puzzle_editor
{
    partial class Form2
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.numericWidth = new System.Windows.Forms.NumericUpDown();
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericPlayerX = new System.Windows.Forms.NumericUpDown();
            this.numericPlayerY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericExitY = new System.Windows.Forms.NumericUpDown();
            this.numericExitX = new System.Windows.Forms.NumericUpDown();
            this.numericCapacity = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboTextures = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPlayerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPlayerY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExitY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExitX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(153, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(120, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(153, 65);
            this.numericWidth.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(120, 20);
            this.numericWidth.TabIndex = 2;
            this.numericWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(153, 91);
            this.numericHeight.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(120, 20);
            this.numericHeight.TabIndex = 3;
            this.numericHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Имя:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Тип оформления:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Длина:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Высота:";
            // 
            // numericPlayerX
            // 
            this.numericPlayerX.Location = new System.Drawing.Point(153, 117);
            this.numericPlayerX.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericPlayerX.Name = "numericPlayerX";
            this.numericPlayerX.Size = new System.Drawing.Size(57, 20);
            this.numericPlayerX.TabIndex = 8;
            this.numericPlayerX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericPlayerY
            // 
            this.numericPlayerY.Location = new System.Drawing.Point(216, 117);
            this.numericPlayerY.Maximum = new decimal(new int[] {
            29,
            0,
            0,
            0});
            this.numericPlayerY.Name = "numericPlayerY";
            this.numericPlayerY.Size = new System.Drawing.Size(57, 20);
            this.numericPlayerY.TabIndex = 9;
            this.numericPlayerY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Позиция игрока:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Позиция выхода:";
            // 
            // numericExitY
            // 
            this.numericExitY.Location = new System.Drawing.Point(216, 143);
            this.numericExitY.Maximum = new decimal(new int[] {
            29,
            0,
            0,
            0});
            this.numericExitY.Name = "numericExitY";
            this.numericExitY.Size = new System.Drawing.Size(57, 20);
            this.numericExitY.TabIndex = 12;
            this.numericExitY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericExitX
            // 
            this.numericExitX.Location = new System.Drawing.Point(153, 143);
            this.numericExitX.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericExitX.Name = "numericExitX";
            this.numericExitX.Size = new System.Drawing.Size(57, 20);
            this.numericExitX.TabIndex = 11;
            this.numericExitX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericCapacity
            // 
            this.numericCapacity.Location = new System.Drawing.Point(153, 169);
            this.numericCapacity.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericCapacity.Name = "numericCapacity";
            this.numericCapacity.Size = new System.Drawing.Size(120, 20);
            this.numericCapacity.TabIndex = 14;
            this.numericCapacity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Вместимость инвентаря:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(117, 195);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(198, 195);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboTextures
            // 
            this.comboTextures.FormattingEnabled = true;
            this.comboTextures.Items.AddRange(new object[] {
            "Лазурный",
            "Золотистый",
            "Золотисто-мраморный",
            "Сиреневый",
            "Аквамариновый",
            "Светло-лаймовый"});
            this.comboTextures.Location = new System.Drawing.Point(153, 38);
            this.comboTextures.Name = "comboTextures";
            this.comboTextures.Size = new System.Drawing.Size(120, 21);
            this.comboTextures.TabIndex = 18;
            this.comboTextures.Text = "Лазурный";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 230);
            this.ControlBox = false;
            this.Controls.Add(this.comboTextures);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericCapacity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericExitY);
            this.Controls.Add(this.numericExitX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericPlayerY);
            this.Controls.Add(this.numericPlayerX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericHeight);
            this.Controls.Add(this.numericWidth);
            this.Controls.Add(this.textBoxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры локации";
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPlayerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPlayerY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExitY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExitX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.NumericUpDown numericWidth;
        private System.Windows.Forms.NumericUpDown numericHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericPlayerX;
        private System.Windows.Forms.NumericUpDown numericPlayerY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericExitY;
        private System.Windows.Forms.NumericUpDown numericExitX;
        private System.Windows.Forms.NumericUpDown numericCapacity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboTextures;
    }
}