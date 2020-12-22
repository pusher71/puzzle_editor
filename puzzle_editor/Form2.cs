using System;
using System.Windows.Forms;

namespace puzzle_editor
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0)
            {
                Form1 ifrm = Owner as Form1;
                ifrm.createLocation(textBoxName.Text, (int)numericTextures.Value, (int)numericWidth.Value, (int)numericHeight.Value,
                    (int)numericPlayerX.Value, (int)numericPlayerY.Value, (int)numericExitX.Value, (int)numericExitY.Value, (int)numericCapacity.Value);
                ifrm.Enabled = true;
                Close();
            }
            else
                MessageBox.Show("Задайте название локации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form1 ifrm = Owner as Form1;
            ifrm.Enabled = true;
            Close();
        }
    }
}
