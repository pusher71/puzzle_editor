using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace puzzle_editor
{
    public partial class Form1 : Form
    {
        private static string[] elementNames = new string[9] //названия таблиц с игровыми элементами
        {
            "gameelement",
            "wall",
            "cube",
            "key",
            "door",
            "button",
            "barrier",
            "lazeremitter",
            "medicinechest"
        };

        private static Bitmap[] elementTextures = new Bitmap[9] //текстуры игровых элементов
        {
            Properties.Resources.empty,
            Properties.Resources.wall,
            Properties.Resources.cube,
            Properties.Resources.key,
            Properties.Resources.door,
            Properties.Resources.button,
            Properties.Resources.barrier,
            Properties.Resources.lazeremitter,
            Properties.Resources.medicinechest
        };

        private const int step = 20; //шаг сетки

        private MySqlConnection conn; //соединение с базой данных
        private int locationCount; //количество локаций
        private int currentLocation; //текущая просматриваемая локация
        private int width; //её длина
        private int height; //её высота
        private Point playerPosition; //позиция игрока
        private Point exitPosition; //позиция выхода
        private int idMax; //максимальный используемый id элемента

        private Bitmap image; //изображение локации
        private Graphics g1; //графика с него

        public Form1()
        {
            InitializeComponent();
            conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                MessageBox.Show("Соединение с базой данных установлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //получить максимальный используемый id элемента
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT MAX(id) FROM `model`.`gameelement`";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        idMax = Convert.ToInt32(reader.GetValue(0)) + 1;
                    }
                }

                //получить количество локаций
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `model`.`location`";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        locationCount = Convert.ToInt32(reader.GetValue(0));
                    }
                }

                //отрисовать первую локацию
                currentLocation = 1;
                update();
            }
            catch (Exception e)
            {
                MessageBox.Show("Соединение с базой данных не установлено.\n" + e.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Закрыть соединение.
                conn.Close();
                // Уничтожить объект, освободить ресурс.
                conn.Dispose();
                Application.Exit();
            }
        }

        //обновить статус локации
        private void update()
        {
            getLocationParams();
            changeWorkspace();
            drawLocation();
            labelNumber.Text = "Локация: " + currentLocation;
            buttonPrev.Enabled = currentLocation > 1;
            buttonNext.Enabled = currentLocation < locationCount;
        }

        //получить параметры текущей локации (width, height, playerPosition и exitPosition)
        private void getLocationParams()
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM `model`.`location` WHERE Number = " + currentLocation;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    width = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Width")));
                    height = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Height")));
                    playerPosition = new Point(
                        Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerX"))),
                        Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerY"))) );
                    exitPosition = new Point(
                        Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitX"))),
                        Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitY"))));
                }
            }
        }

        //получить игровой элемент с заданной позиции
        private GameElement getGameElement(int x, int y)
        {
            GameElement ge = new GameElement();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Properties.Resources.GetElement + " WHERE X = " + x + " AND Y = " + y + " and LocationId = " + currentLocation;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    //определить тип элемента и дополнительные параметры
                    if (!reader.IsDBNull(reader.GetOrdinal("WallId"))) ge.type = 1;
                    else if (!reader.IsDBNull(reader.GetOrdinal("CubeId"))) ge.type = 2;
                    else if (!reader.IsDBNull(reader.GetOrdinal("KeyId")))
                    {
                        ge.type = 3;
                        ge.color = reader.GetString(reader.GetOrdinal("KeyColor"));
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("DoorId")))
                    {
                        ge.type = 4;
                        ge.color = reader.GetString(reader.GetOrdinal("DoorColor"));
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("ButtonId")))
                    {
                        ge.type = 5;
                        ge.color = reader.GetString(reader.GetOrdinal("ButtonColor"));
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("BarrierId")))
                    {
                        ge.type = 6;
                        ge.color = reader.GetString(reader.GetOrdinal("BarrierColor"));
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("LazerEmitterId")))
                    {
                        ge.type = 7;
                        ge.direction = reader.GetString(reader.GetOrdinal("LazerDirection"));
                        ge.energy = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LazerEnergy")));
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("MedicineChestId")))
                    {
                        ge.type = 8;
                        ge.size = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("MedicineSize")));
                    }
                    else ge.type = 0;
                }
            }

            return ge;
        }

        //поставить игровой элемент на заданную позицию
        private void setGameElement(GameElement ge, int x, int y)
        {
            string command = "";
            switch (ge.type)
            {
                case 1:
                    command = "CALL CreateWall(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ");";
                    break;
                case 2:
                    command = "CALL CreateCube(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ");";
                    break;
                case 3:
                    command = "CALL CreateKey(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 4:
                    command = "CALL CreateDoor(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 5:
                    command = "CALL CreateButton(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 6:
                    command = "CALL CreateBarrier(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 7:
                    command = "CALL CreateLazerEmitter(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ", " + ge.direction + ", " + ge.energy + ");";
                    break;
                case 8:
                    command = "CALL CreateMedicineChest(" + idMax++ + ", " + currentLocation + ", " + x + ", " + y + ", " + ge.size + ");";
                    break;
            }
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = command;
            using (cmd.ExecuteReader()) ;
        }

        //удалить игровой элемент с заданной позиции
        private void deleteGameElementAt(int x, int y)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "CALL DeleteAt(" + currentLocation + ", " + x + ", " + y + ");";
            using (cmd.ExecuteReader()) ;
        }

        //подогнать окно под рабочую область
        private void changeWorkspace()
        {
            pictureBox1.Size = new Size(width * step, height * step);
            Size = new Size(pictureBox1.Size.Width + 186, Math.Max(pictureBox1.Size.Height + 78, 399));
        }

        //отрисовать текущую локацию
        private void drawLocation()
        {
            image = new Bitmap(width * step, height * step);
            g1 = Graphics.FromImage(image);
            g1.Clear(Color.Black);

            //отрисовать игровые элементы
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    drawItem(x, y);

            //отрисовать игрока и выход
            g1.DrawImage(Properties.Resources.player, playerPosition.X * step, playerPosition.Y * step);
            g1.DrawImage(Properties.Resources.exit, exitPosition.X * step, exitPosition.Y * step);

            pictureBox1.Image = image;
        }

        //отрисовать заданную позицию в g1
        private void drawItem(int x, int y)
        {
            //получить элемент по координатам
            GameElement ge = getGameElement(x, y);

            //обработать текстуру в зависимости от параметров ge
            Bitmap pict = new Bitmap(elementTextures[ge.type]);
            if (ge.type > 2 && ge.type < 7) //если элемент имеет цвет
            {
                //перевести цвет в Color
                ColorConverter cc = new ColorConverter();
                Color color = (Color)cc.ConvertFromString(ge.color);

                //применить цветовой фильтр
                for (int i = 0; i < pict.Width; i++)
                    for (int j = 0; j < pict.Height; j++)
                    {
                        Color c = pict.GetPixel(i, j);
                        pict.SetPixel(i, j, Color.FromArgb(255, c.R * color.R / 255, c.G * color.G / 255, c.B * color.B / 255));
                    }
            }
            else if (ge.type == 7) //если элемент является лазерным излучателем
            {
                //повернуть текстуру в зависимости от направления...
                //написать значение энергии на текстуре
            }
            else if (ge.type == 8) //если элемент является аптечкой
            {
                //написать значение размера на текстуре
            }

            //отрисовать элемент на поле
            g1.DrawImage(pict, x * step, y * step);

            //сетка
            //if (grid)
            //{
            //    g1.DrawLine(pen, positionW, positionH, positionW + lengthW, positionH);
            //    g1.DrawLine(pen, positionW, positionH, positionW, positionH + lengthH);
            //}
        }

        //создать новую локацию
        public void createLocation(string name, int textureType, int width, int height, int playerX, int playerY, int exitX, int exitY, int capacity)
        {
            //инкрементировать счётчик локаций
            locationCount++;
            currentLocation = locationCount;

            //добавить локацию в конец списка
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO `model`.`location` (`Number`, `Name`, `TextureType`, `Width`, `Height`, `PlayerX`, `PlayerY`, `ExitX`, `ExitY`, `InventoryCapacity`) VALUES ('" + currentLocation +"', '" + name + "', '" + textureType + "', '" + width + "', '" + height + "', '" + playerX + "', '" + playerY + "', '" + exitX + "', '" + exitY + "', '" + capacity + "');";
            using (cmd.ExecuteReader()) ;

            update();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            currentLocation--;
            update();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            currentLocation++;
            update();
        }

        private void toolLocationAdd_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form2();
            ifrm.Owner = this;
            ifrm.Show();
            Enabled = false;
        }

        private void toolLocationDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Удалить локацию", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //удалить текущую локацию
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM `model`.`location` WHERE Number = " + currentLocation;
                using (cmd.ExecuteReader()) ;

                //декрементировать счётчик локаций
                locationCount--;
                if (currentLocation > locationCount)
                    currentLocation = locationCount; //ограничить номер текущей локации
                update();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //получить координаты
            Point cp = PointToClient(Cursor.Position);
            cp.X -= pictureBox1.Location.X;
            cp.Y -= pictureBox1.Location.Y;
            int x = cp.X / step;
            int y = cp.Y / step;

            //сформировать игровой элемент для расставления
            Color c = buttonColor.BackColor;
            GameElement ge = new GameElement();
            ge.type = listBox1.SelectedIndex + 1;
            ge.color = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            ge.direction = comboDirection.Text;
            ge.energy = (int)numericEnergy.Value;
            ge.size = (int)numericSize.Value;

            //поставить элемент, если курсор не выпрыгнул за границы
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                if (e.Button == MouseButtons.Left) //если нажата ЛКМ
                    setGameElement(ge, x, y); //поставить элемент
                else //иначе
                    deleteGameElementAt(x, y); //удалить элемент
            }

            update();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                buttonColor.BackColor = colorDialog.Color;
        }

        private void toolWallsCount_Click(object sender, EventArgs e)
        {
            string result = ""; //результирующая строка
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Properties.Resources.GetWallsCount;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int counter = 1;
                    while (reader.Read())
                        result += counter++ + ": " + reader.GetString(0) + "\n";
                }
            }

            MessageBox.Show(result, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolGetIO_Click(object sender, EventArgs e)
        {
            string result = ""; //результирующая строка
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Properties.Resources.GetLocationIO;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int counter = 1;
                    while (reader.Read())
                        result += counter++ + ": Player(" + reader.GetString(0) + "; " + reader.GetString(1) + "), Exit(" + reader.GetString(2) + "; " + reader.GetString(3) + ")\n";
                }
            }

            MessageBox.Show(result, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
