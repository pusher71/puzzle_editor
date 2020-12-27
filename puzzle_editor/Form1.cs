using System;
using System.Drawing;
using System.Windows.Forms;

namespace puzzle_editor
{
    public partial class Form1 : Form
    {
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

        private DBUtils db; //ссылка на адаптер к базе данных
        private int locationCount; //количество локаций
        private int currentLocation; //текущая просматриваемая локация
        private int width; //её длина
        private int height; //её высота
        private Point playerPosition; //позиция игрока
        private Point exitPosition; //позиция выхода
        private GameElement[,] levelArray; //массив локации

        private Bitmap image; //изображение локации
        private Graphics g1; //графика с него

        public Form1()
        {
            InitializeComponent();
            db = new DBUtils();
            db.SetDBConnection();

            //получить количество локаций
            locationCount = db.getLocationCount();

            //считать первую локацию в локальный массив
            currentLocation = 1;
            update();
        }

        //обновить статус локации
        private void update()
        {
            levelArray = db.getLocation(currentLocation, out width, out height, out playerPosition, out exitPosition);
            changeWorkspace();
            drawLocation();
            labelNumber.Text = "Локация: " + currentLocation;
            buttonPrev.Enabled = currentLocation > 1;
            buttonNext.Enabled = currentLocation < locationCount;
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
            GameElement ge = levelArray[x, y];

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

            pictureBox1.Image = image;
        }

        //создать новую локацию
        public void createLocation(string name, int textureType, int width, int height, int playerX, int playerY, int exitX, int exitY, int capacity)
        {
            //инкрементировать счётчик локаций
            locationCount++;
            currentLocation = locationCount;

            //добавить локацию в конец списка
            db.createLocation(currentLocation, name, textureType, width, height, playerX, playerY, exitX, exitY, capacity);

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
                db.deleteLocation(currentLocation);

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

            //поставить элемент, если курсор не выпрыгнул за границы
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                if (e.Button == MouseButtons.Left) //если нажата ЛКМ
                {
                    //сформировать игровой элемент для расставления
                    Color c = buttonColor.BackColor;
                    GameElement ge = new GameElement();
                    ge.type = listBox1.SelectedIndex + 1;
                    ge.color = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
                    ge.direction = comboDirection.Text;
                    ge.energy = (int)numericEnergy.Value;
                    ge.size = (int)numericSize.Value;

                    //поставить элемент
                    levelArray[x, y] = ge;
                }
                else //иначе
                    levelArray[x, y].type = 0; //удалить элемент

                //отрисовать элемент
                drawItem(x, y);
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                buttonColor.BackColor = colorDialog.Color;
        }

        private void toolWallsCount_Click(object sender, EventArgs e)
        {
            db.showWallCount();
        }

        private void toolGetIO_Click(object sender, EventArgs e)
        {
            db.showIO();
        }
    }
}
