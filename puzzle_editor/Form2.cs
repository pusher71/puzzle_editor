using System;
using System.Windows.Forms;

namespace puzzle_editor
{
    public partial class Form2 : Form
    {
        private Location location; //ссылка на локацию
        public Form2(Location loc)
        {
            InitializeComponent();
            location = loc;

            //перенести параметры из локации в поля ввода
            textBoxName.Text = location.name;
            comboTextures.SelectedIndex = location.textureType;
            numericWidth.Value = location.width;
            numericHeight.Value = location.height;
            numericPlayerX.Value = location.playerX;
            numericPlayerY.Value = location.playerY;
            numericExitX.Value = location.exitX;
            numericExitY.Value = location.exitY;
            numericCapacity.Value = location.capacity;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //проверить правильность введённых данных
            if (textBoxName.Text.Length == 0)
                MessageBox.Show("Задайте название локации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numericPlayerX.Value >= numericWidth.Value ||
                    numericPlayerY.Value >= numericHeight.Value)
                MessageBox.Show("Игрок находится за пределами локации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numericExitX.Value >= numericWidth.Value ||
                    numericExitY.Value >= numericHeight.Value)
                MessageBox.Show("Выход находится за пределами локации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numericPlayerX.Value == numericExitX.Value &&
                    numericPlayerY.Value == numericExitY.Value)
                MessageBox.Show("Игрок и выход находятся на одной позиции!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Form1 ifrm = Owner as Form1;

                //перенести параметры из полей ввода в локацию
                location.name = textBoxName.Text;
                location.textureType = comboTextures.SelectedIndex;
                location.width = (int)numericWidth.Value;
                location.height = (int)numericHeight.Value;
                location.playerX = (int)numericPlayerX.Value;
                location.playerY = (int)numericPlayerY.Value;
                location.exitX = (int)numericExitX.Value;
                location.exitY = (int)numericExitY.Value;
                location.capacity = (int)numericCapacity.Value;

                //отправить изменённую локацию
                ifrm.acceptLocation(location);

                ifrm.Enabled = true;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form1 ifrm = Owner as Form1;
            ifrm.Enabled = true;
            Close();
        }
    }
}
