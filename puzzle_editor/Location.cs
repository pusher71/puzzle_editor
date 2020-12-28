namespace puzzle_editor
{
    public class Location
    {
        public GameElement[,] levelArray;
        public string name;
        public int textureType;
        public int width;
        public int height;
        public int playerX;
        public int playerY;
        public int exitX;
        public int exitY;
        public int capacity;

        public Location()
        {
            levelArray = new GameElement[60, 30];
            width = 12;
            height = 12;
            playerX = 1;
            playerY = 1;
            exitX = 1;
            exitY = 1;
            capacity = 6;
        }
    }
}
