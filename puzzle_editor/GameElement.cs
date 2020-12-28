namespace puzzle_editor
{
    //игровой элемент на локации
    public struct GameElement
    {
        public int type; //тип элемента (0 - элемента нет)
        public string color; //цвет
        public string direction; //направление (west/north/east/south)
        public int energy; //мощность лазерного излучателя
        public int size; //объём аптечки
    }
}
