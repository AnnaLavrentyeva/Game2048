using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model2048
{
    class Map
    {
        int[,] map;
        public int size { get; private set; }
        public Map(int size)
        {
            this.size = size;
            map = new int[size, size];
        }

        public int GetCoord(int x, int y)
        {
            if (OnMap(x, y))
            {

                return map[x, y];
            }
            else
                return -1;
        }

        public void Set(int x, int y, int num)
        {
            if (OnMap(x, y))
            {
                map[x, y] = num;
            }
        }
        public bool OnMap(int x, int y)
        {
            return (x >= 0 && x < size && y >= 0 && y < size);
        }
    }
}
