using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_editor
{
    //игровой элемент на локации
    struct GameElement
    {
        public int type; //тип элемента
        public string color; //цвет
        public string direction; //направление (west/north/east/south)
        public int energy; //мощность лазерного излучателя
        public int size; //объём аптечки
    }
}
