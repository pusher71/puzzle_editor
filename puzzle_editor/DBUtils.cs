using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace puzzle_editor
{
    class DBUtils
    {
        private MySqlConnection conn; //соединение с базой данных
        public void SetDBConnection()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "model";
            string username = "root";
            string password = "root1234";

            // Connection String.
            string connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + Properties.Resources.String1;

            conn = new MySqlConnection(connString);

            //проверить соединение
            try
            {
                //открыть соединение
                conn.Open();
                MessageBox.Show("Соединение с базой данных установлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Соединение с базой данных не установлено.\n" + e.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //закрыть соединение
                conn.Close();
            }
        }

        //создать локацию
        public void createLocation(int number, string name, int textureType, int width, int height, int playerX, int playerY, int exitX, int exitY, int capacity)
        {
            conn.Open();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO `model`.`location` (`Number`, `Name`, `TextureType`, `Width`, `Height`, `PlayerX`, `PlayerY`, `ExitX`, `ExitY`, `InventoryCapacity`) VALUES (@number, @name, @textureType, @width, @height, @playerX, @playerY, @exitX, @exitY, @capacity);";
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@textureType", textureType);
                cmd.Parameters.AddWithValue("@width", width);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@playerX", playerX);
                cmd.Parameters.AddWithValue("@playerY", playerY);
                cmd.Parameters.AddWithValue("@exitX", exitX);
                cmd.Parameters.AddWithValue("@exitY", exitY);
                cmd.Parameters.AddWithValue("@capacity", capacity);
                using (cmd.ExecuteReader()) ;
            }

            conn.Close();
        }

        //удалить локацию
        public void deleteLocation(int number)
        {
            conn.Open();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM `model`.`location` WHERE Number = " + number;
                using (cmd.ExecuteReader()) ;
            }

            conn.Close();
        }

        // получить максимальный id элемента
        public int getMaxId()
        {
            int idMax = 0;
            conn.Open();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(id) FROM `model`.`gameelement`";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        idMax = Convert.ToInt32(reader.GetValue(0)) + 1;
                    }
                }
            }

            conn.Close();
            return idMax;
        }

        //получить количество локаций
        public int getLocationCount()
        {
            int locationCount = 0;
            conn.Open();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM `model`.`location`";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        locationCount = Convert.ToInt32(reader.GetValue(0));
                    }
                }
            }

            conn.Close();
            return locationCount;
        }

        //получить параметры локации (width, height, playerPosition и exitPosition)
        private void getLocationParams(int locationNumber, out int width, out int height, out Point playerPosition, out Point exitPosition)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM `model`.`location` WHERE Number = " + locationNumber;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        width = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Width")));
                        height = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Height")));
                        playerPosition = new Point(
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerX"))),
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerY"))));
                        exitPosition = new Point(
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitX"))),
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitY"))));
                    }
                    else
                    {
                        width = -1;
                        height = -1;
                        playerPosition = new Point(-1, -1);
                        exitPosition = new Point(-1, -1);
                    }
                }
            }
        }

        //получить массив локации с параметрами
        public GameElement[,] getLocation(int locationNumber, out int width, out int height, out Point playerPosition, out Point exitPosition)
        {
            conn.Open();

            //получить параметры локации
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM `model`.`location` WHERE Number = " + locationNumber;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        width = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Width")));
                        height = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Height")));
                        playerPosition = new Point(
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerX"))),
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerY"))));
                        exitPosition = new Point(
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitX"))),
                            Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitY"))));
                    }
                    else
                    {
                        width = -1;
                        height = -1;
                        playerPosition = new Point(-1, -1);
                        exitPosition = new Point(-1, -1);
                    }
                }
            }

            GameElement[,] levelArray = new GameElement[width, height];

            //получить массив локации
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Properties.Resources.GetElements + " WHERE LocationId = @locationNumber";
                cmd.Parameters.AddWithValue("@locationNumber", locationNumber);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            GameElement ge = new GameElement(); //очередной элемент

                            //определить координаты элемента
                            int x = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("X")));
                            int y = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Y")));

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

                            levelArray[x, y] = ge;
                        }
                    }
                }
            }

            conn.Close();
            return levelArray;
        }

        //получить игровой элемент с заданной позиции
        private GameElement getGameElementAt(int locationNumber, int x, int y)
        {
            GameElement ge = new GameElement();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Properties.Resources.GetElement + " WHERE X = " + x + " AND Y = " + y + " and LocationId = " + locationNumber;
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
            }

            return ge;
        }

        //поставить игровой элемент на заданную позицию
        private void setGameElementAt(GameElement ge, int id, int locationNumber, int x, int y)
        {
            string command = "";
            switch (ge.type)
            {
                case 1:
                    command = "CALL CreateWall(" + id + ", " + locationNumber + ", " + x + ", " + y + ");";
                    break;
                case 2:
                    command = "CALL CreateCube(" + id + ", " + locationNumber + ", " + x + ", " + y + ");";
                    break;
                case 3:
                    command = "CALL CreateKey(" + id + ", " + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 4:
                    command = "CALL CreateDoor(" + id + ", " + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 5:
                    command = "CALL CreateButton(" + id + ", " + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 6:
                    command = "CALL CreateBarrier(" + id + ", " + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                    break;
                case 7:
                    command = "CALL CreateLazerEmitter(" + id + ", " + locationNumber + ", " + x + ", " + y + ", " + ge.direction + ", " + ge.energy + ");";
                    break;
                case 8:
                    command = "CALL CreateMedicineChest(" + id + ", " + locationNumber + ", " + x + ", " + y + ", " + ge.size + ");";
                    break;
            }
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = command;
                using (cmd.ExecuteReader()) ;
            }
        }

        //удалить игровой элемент с заданной позиции
        private void deleteGameElementAt(int locationNumber, int x, int y)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "CALL DeleteAt(" + locationNumber + ", " + x + ", " + y + ");";
                using (cmd.ExecuteReader()) ;
            }
        }

        //отобразить количество стен в каждом уровне
        public void showWallCount()
        {
            conn.Open();

            string result = ""; //результирующая строка

            using (MySqlCommand cmd = conn.CreateCommand())
            {
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
            }

            conn.Close();
            MessageBox.Show(result, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //отобразить позиции игрока и выхода в каждом уровне
        public void showIO()
        {
            conn.Open();

            string result = ""; //результирующая строка

            using (MySqlCommand cmd = conn.CreateCommand())
            {
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
            }

            conn.Close();
            MessageBox.Show(result, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
