using System;
using System.Windows.Forms;
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

                //закрыть соединение и выйти
                conn.Close();
                Application.Exit();

            }
            finally
            {
                //закрыть соединение
                conn.Close();
            }
        }

        //создать локацию
        public void createLocation(int number, Location location)
        {
            conn.Open();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO `model`.`location` (`Number`, `Name`, `TextureType`, `Width`, `Height`, `PlayerX`, `PlayerY`, `ExitX`, `ExitY`, `InventoryCapacity`) VALUES (@number, @name, @textureType, @width, @height, @playerX, @playerY, @exitX, @exitY, @capacity);";
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@name", location.name);
                cmd.Parameters.AddWithValue("@textureType", location.textureType);
                cmd.Parameters.AddWithValue("@width", location.width);
                cmd.Parameters.AddWithValue("@height", location.height);
                cmd.Parameters.AddWithValue("@playerX", location.playerX);
                cmd.Parameters.AddWithValue("@playerY", location.playerY);
                cmd.Parameters.AddWithValue("@exitX", location.exitX);
                cmd.Parameters.AddWithValue("@exitY", location.exitY);
                cmd.Parameters.AddWithValue("@capacity", location.capacity);
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

        //считать локацию из базы данных
        public Location readLocation(int locationNumber)
        {
            Location location = new Location();
            conn.Open();

            //считать параметры локации
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM `model`.`location` WHERE Number = " + locationNumber;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        location.name = reader.GetString(reader.GetOrdinal("Name"));
                        location.textureType = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TextureType")));
                        location.width = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Width")));
                        location.height = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Height")));
                        location.playerX = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerX")));
                        location.playerY = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PlayerY")));
                        location.exitX = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitX")));
                        location.exitY = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ExitY")));
                        location.capacity = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("InventoryCapacity")));
                    }
                }
            }

            //считать массив локации
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

                            location.levelArray[x, y] = ge;
                        }
                    }
                }
            }

            conn.Close();
            return location;
        }

        //записать локацию в базу данных
        public void writeLocation(int locationNumber, Location location)
        {
            conn.Open();

            //записать параметры локации
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = Properties.Resources.SetLocationParameters;
                cmd.Parameters.AddWithValue("@name", location.name);
                cmd.Parameters.AddWithValue("@textureType", location.textureType);
                cmd.Parameters.AddWithValue("@width", location.width);
                cmd.Parameters.AddWithValue("@height", location.height);
                cmd.Parameters.AddWithValue("@playerX", location.playerX);
                cmd.Parameters.AddWithValue("@playerY", location.playerY);
                cmd.Parameters.AddWithValue("@exitX", location.exitX);
                cmd.Parameters.AddWithValue("@exitY", location.exitY);
                cmd.Parameters.AddWithValue("@capacity", location.capacity);
                cmd.Parameters.AddWithValue("@locationNumber", locationNumber);
                using (cmd.ExecuteReader()) ;
            }

            //очистить поле локации в базе данных
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM `model`.`gameelement` WHERE LocationId = @locationNumber";
                cmd.Parameters.AddWithValue("@locationNumber", locationNumber);
                using (cmd.ExecuteReader()) ;
            }

            //сбросить счётчик AUTO INCREMENT
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "ALTER TABLE `model`.`gameelement` AUTO_INCREMENT = 1";
                using (cmd.ExecuteReader()) ;
            }

            //записать массив локации
            for (int x = 0; x < location.width; x++)
                for (int y = 0; y < location.height; y++)
                {
                    GameElement ge = location.levelArray[x, y];
                    if (ge.type != 0)
                    {
                        string command = "";
                        switch (ge.type)
                        {
                            case 1:
                                command = "CALL CreateWall(" + locationNumber + ", " + x + ", " + y + ");";
                                break;
                            case 2:
                                command = "CALL CreateCube(" + locationNumber + ", " + x + ", " + y + ");";
                                break;
                            case 3:
                                command = "CALL CreateKey(" + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                                break;
                            case 4:
                                command = "CALL CreateDoor(" + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                                break;
                            case 5:
                                command = "CALL CreateButton(" + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                                break;
                            case 6:
                                command = "CALL CreateBarrier(" + locationNumber + ", " + x + ", " + y + ", '" + ge.color + "');";
                                break;
                            case 7:
                                command = "CALL CreateLazerEmitter(" + locationNumber + ", " + x + ", " + y + ", " + ge.direction + ", " + ge.energy + ");";
                                break;
                            case 8:
                                command = "CALL CreateMedicineChest(" + locationNumber + ", " + x + ", " + y + ", " + ge.size + ");";
                                break;
                        }
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = command;
                            using (cmd.ExecuteReader()) ;
                        }
                    }
                }

            conn.Close();
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
