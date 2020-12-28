namespace puzzle_editor
{
    struct Location
    {
        public GameElement[,] levelArray;
        public int width;
        public int height;
        public int playerX;
        public int playerY;
        public int exitX;
        public int exitY;
    }
}
