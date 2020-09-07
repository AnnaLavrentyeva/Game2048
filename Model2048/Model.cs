using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model2048
{
    public class Model
    {
        static Random rand = new Random();

        Map map;

        bool isGameOver;
        bool moved;

        public int size { get { return map.size; } }

        public Model(int size)
        {
            map = new Map(size);
        }

        public void Start()
        {
            isGameOver = false;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    map.Set(x, y, 0);
                }
            }
            
            AddRandomNumber();
            AddRandomNumber();
        }

        private void AddRandomNumber()
        {
            if (isGameOver) return;
            for (int j= 0; j<100; j++)
            {
                int x = rand.Next(0, map.size);
                int y = rand.Next(0, map.size);

                if (map.GetCoord(x, y) == 0)
                {
                    map.Set(x, y, rand.Next(1, 3) * 2);
                    return;
                }
            }
        }

        void Move(int x, int y, int newX, int newY)
        {
            if(map.GetCoord(x, y) > 0)
            {
                while(map.GetCoord( x + newX, y + newY) == 0)
                {
                    map.Set(x + newX, y + newY, map.GetCoord(x, y));
                    map.Set(x, y, 0);
                    x += newX;
                    y += newY;
                    moved = true;
                }
            }
        }

        public void Left()
        {
            moved = false;
            for(int y = 0; y < map.size; y++)
            {
                for(int x=1; x< map.size; x++)
                {
                    Move(x, y, -1, 0);
                }
                for (int x = 1; x < map.size; x++)
                {
                    Join(x, y, -1, 0);
                }
            }
            if(moved) AddRandomNumber();
        }

        public void Right()
        {
            moved = false;
            for (int y = 0; y < map.size; y++)
            {
                for (int x = map.size-2; x >= 0; x--)
                {
                    Move(x, y, +1, 0);
                }
                for (int x = map.size - 2; x >= 0; x--)
                {
                    Join(x, y, +1, 0);
                }
            }
            if (moved) AddRandomNumber();
        }

        public void Up()
        {
            moved = false;
            for (int x = 0; x < map.size; x++)
            {
                for (int y = 1; y < map.size; y++)
                {
                    Move(x, y, 0, -1);
                }
                for (int y = 1; y < map.size; y++)
                {
                    Join(x, y, 0, -1);
                }
            }
            if (moved) AddRandomNumber();
        }

        public void Down()
        {
            moved = false;
            for (int x = 0; x < map.size; x++)
            {
                for (int y = map.size - 2; y >= 0; y--)
                {
                    Move(x, y, 0, +1);
                }
                for (int y = map.size - 2; y >= 0; y--)
                {
                    Join(x, y, 0, +1);
                }
            }
            if (moved) AddRandomNumber();
        }

        void Join(int x, int y, int newX, int newY)
        {
            if(map.GetCoord(x, y) > 0)
            {
                if(map.GetCoord(x+newX, y+ newY) == map.GetCoord(x, y))
                {
                    map.Set(x + newX, y + newY, map.GetCoord(x,y) * 2);
                    while(map.GetCoord(x - newX, y - newY) > 0)
                    {
                        map.Set(x, y, map.GetCoord(x - newX, y - newY));
                        x -= newX;
                        y -= newY;
                    }
                    map.Set(x, y, 0);
                    moved = true;
                }
            }
        }

        public int GetMap(int x, int y)
        {
            return map.GetCoord(x,y);
        }

        public bool IsGameOver()
        {
            if (isGameOver) return isGameOver;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map.GetCoord(x, y) == 0)
                    {
                        return false;
                    }
                }
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map.GetCoord(x, y) == map.GetCoord(x + 1, y) || map.GetCoord(x, y) == map.GetCoord(x, y + 1))
                        return false;
                }
            }
            isGameOver = true;
            return isGameOver;
        }
    }
}
