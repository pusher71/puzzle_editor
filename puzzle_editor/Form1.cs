using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace puzzle_editor
{
    public partial class Form1 : Form
    {
        private MySqlConnection conn; //соединение с базой данных
        private int currentLocation; //текущая просматриваемая локация
        private int width; //её длина
        private int height; //её высота
        private const int step = 20; //шаг сетки

        public Form1()
        {
            InitializeComponent();
            conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                MessageBox.Show("Соединение с базой данных установлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //получить длину и высоту первой локации
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT width FROM `model`.`location` WHERE Number = 0";

                drawLocation();
            }
            catch (Exception e)
            {
                MessageBox.Show("Соединение с базой данных не установлено.\n" + e.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                // Закрыть соединение.
                conn.Close();
                // Уничтожить объект, освободить ресурс.
                conn.Dispose();
            }
        }

        //подогнать окно под рабочую область
        private void changeWorkspace()
        {
            pictureBox1.Size = new Size(width * step, height * step);
            Size = new Size(pictureBox1.Size.Width + 186, Math.Max(pictureBox1.Size.Height + 78, 332));
        }

        //отрисовать текущую локацию
        private void drawLocation()
        {

        }
    }
}
