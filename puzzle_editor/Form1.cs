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

        private static Font font = new Font("Arial", 14); //шрифт для обозначений параметров
        private static SolidBrush brush = new SolidBrush(Color.Black); //цвет текста
        private const int step = 20; //шаг сетки

        private DBUtils db; //ссылка на адаптер к базе данных
        private int locationCount; //количество локаций
        private int locationNumber; //номер текущей локации
        private Location location; //текущая локация
        private bool createOrEdit; //создать локацию или редактировать параметры

        private Bitmap image; //изображение локации
        private Graphics g1; //графика с него

        public Form1()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;

            db = new DBUtils();
            db.SetDBConnection();

            //получить количество локаций
            locationCount = db.getLocationCount();

            //считать первую локацию в локальный массив
            locationNumber = 1;
            update();
        }

        //обновить интерфейс в соответствии с локацией
        private void update()
        {
            location = db.readLocation(locationNumber);
            drawLocation();
            labelNumber.Text = "Локация: " + locationNumber;
            buttonPrev.Enabled = locationNumber > 1;
            buttonNext.Enabled = locationNumber < locationCount;
            toolLocationDelete.Enabled = locationNumber == locationCount;
        }

        //подогнать окно под рабочую область
        private void changeWorkspace()
        {
            pictureBox1.Size = new Size(image.Width, image.Height);
            Size = new Size(pictureBox1.Size.Width + 186, Math.Max(pictureBox1.Size.Height + 78, 399));
        }

        //отрисовать текущую локацию
        private void drawLocation()
        {
            image = new Bitmap(location.width * step, location.height * step);
            g1 = Graphics.FromImage(image);
            g1.Clear(Color.Black);

            //отрисовать игровые элементы
            for (int x = 0; x < location.width; x++)
                for (int y = 0; y < location.height; y++)
                    drawItem(x, y);

            //отрисовать игрока и выход
            g1.DrawImage(Properties.Resources.player, location.playerX * step, location.playerY * step);
            g1.DrawImage(Properties.Resources.exit, location.exitX * step, location.exitY * step);

            pictureBox1.Image = image;
            changeWorkspace();
        }

        //отрисовать заданную позицию в g1
        private void drawItem(int x, int y)
        {
            //получить элемент по координатам
            GameElement ge = location.levelArray[x, y];

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
                //повернуть текстуру в зависимости от направления
                if (ge.direction == "north")
                    pict.RotateFlip(RotateFlipType.Rotate90FlipNone);
                else if (ge.direction == "east")
                    pict.RotateFlip(RotateFlipType.Rotate180FlipNone);
                else if (ge.direction == "south")
                    pict.RotateFlip(RotateFlipType.Rotate270FlipNone);

                //написать значение энергии на текстуре
                Graphics.FromImage(pict).DrawString(ge.energy.ToString(), font, brush, 2, -1);
            }
            else if (ge.type == 8) //если элемент является аптечкой
            {
                //написать значение размера на текстуре
                Graphics.FromImage(pict).DrawString(ge.size.ToString(), font, brush, 2, -1);
            }

            //отрисовать элемент на поле
            g1.DrawImage(pict, x * step, y * step);
            pictureBox1.Image = image;
        }

        //принять локацию из Form2
        public void acceptLocation(Location loc)
        {
            //если локация добавляется
            if (!createOrEdit)
            {
                //инкрементировать счётчик локаций
                locationCount++;
                locationNumber = locationCount;

                //добавить локацию в конец списка
                db.createLocation(locationNumber, loc);
                update();
            }
            else
                drawLocation();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            locationNumber--;
            update();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            locationNumber++;
            update();
        }

        private void toolLocationAdd_Click(object sender, EventArgs e)
        {
            //получить параметры новой локации из Form2
            createOrEdit = false;
            Location loc = new Location(); //новая локация
            Form ifrm = new Form2(loc);
            ifrm.Owner = this;
            ifrm.Show();
            Enabled = false;
        }

        private void toolLocationParams_Click(object sender, EventArgs e)
        {
            //обновить параметры локации в Form2
            createOrEdit = true;
            Form ifrm = new Form2(location);
            ifrm.Owner = this;
            ifrm.Show();
            Enabled = false;
        }

        private void toolLocationSave_Click(object sender, EventArgs e)
        {
            //обновить локацию в базе данных
            db.writeLocation(locationNumber, location);
        }

        private void toolLocationDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Удалить локацию", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //удалить текущую локацию
                db.deleteLocation(locationNumber);

                //декрементировать счётчик локаций
                locationCount--;
                if (locationNumber > locationCount)
                    locationNumber = locationCount; //ограничить номер текущей локации

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

            //поставить элемент, если курсор не выпрыгнул за границы, и не были задеты игрок и выход
            if (x >= 0 && y >= 0 &&
                x < location.width && y < location.height &&
                !(x == location.playerX && y == location.playerY) &&
                !(x == location.exitX && y == location.exitY))
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
                    location.levelArray[x, y] = ge;
                }
                else //иначе
                    location.levelArray[x, y].type = 0; //удалить элемент

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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonColor.Enabled = listBox1.SelectedIndex > 1 && listBox1.SelectedIndex < 6;
            comboDirection.Enabled = listBox1.SelectedIndex == 6;
            numericEnergy.Enabled = listBox1.SelectedIndex == 6;
            numericSize.Enabled = listBox1.SelectedIndex == 7;
        }
    }
}
