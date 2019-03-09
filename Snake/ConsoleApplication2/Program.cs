    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;


    namespace ConsoleApplication1
    {
    class Program
    {
        static void Main(string[] args)
        {
            bool isDone = false;
            bool hasEaten = false;
            int mapSize = 20;
            char[,] map = new char[mapSize, mapSize];
            char direction = 'L';

            fillMap(map, mapSize);
            int HeadX = 14;
            int HeadY = 14;
            int TailX = 14;
            int TailY = 18;
            int fruitX = 0;
            int fruitY = 0;
            map[HeadX, HeadY] = '0';
            map[14, 15] = 'L';
            map[14, 16] = 'L';
            map[14, 17] = 'L';
            map[TailX, TailY] = 'L';

            createFood(ref fruitX, ref fruitY, ref mapSize, map);
            printMap(map, mapSize);


            while (isDone == false)
            {
                while (!Console.KeyAvailable)
                {
                    if (hasEaten == true)
                    {
                        createFood(ref fruitX, ref fruitY, ref mapSize, map);
                        hasEaten = false;
                    }
                    moveHead(ref HeadX, ref HeadY, ref TailX, ref TailY, ref fruitX, ref fruitY, map, ref direction, ref hasEaten, ref isDone, mapSize);
                    if (isDone == true)
                    {
                        Console.WriteLine("YOU LOSE!");
                    }
                    System.Threading.Thread.Sleep(50000000);
                }
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        direction = 'U';
                        break;
                    case ConsoleKey.DownArrow:
                        direction = 'D';
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = 'L';
                        break;
                    case ConsoleKey.RightArrow:
                        direction = 'R';
                        break;
                }               
            }
            Console.ReadLine();

        }
        public static void fillMap(char[,] array, int mapSize)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int k = 0; k < mapSize; k++)
                {
                    if (i == 0 || k == 0 || i == (mapSize - 1) || k == (mapSize - 1))
                    {
                        array[i, k] = '*';
                    }
                    else
                    {
                        array[i, k] = ' ';
                    }
                }
            }
        }
        public static void printMap(char[,] array, int mapSize)
        {
            Console.Clear();
            for (int i = 0; i < mapSize; i++)
            {
                for (int k = 0; k < mapSize; k++)
                {
                    Console.Write(array[i, k]);
                }
                Console.Write("\n");
            }
        }

        public static void createFood(ref int x, ref int y, ref int z, char[,] map)
        {
            Random rnd = new Random();
            x = rnd.Next(1, (z - 1));
            y = rnd.Next(1, (z - 1));
            if (map[x, y] == ' ')
            {
                map[x, y] = 'F';
            }
            else
            {
                createFood(ref x, ref y, ref z, map);
            }
        }

        public static void moveTail(ref int x, ref int y, char[,] map)
        {
            if (map[x, y] == 'L') // check left
            {
                map[x, y] = ' ';
                y = y - 1;
            }

            else if (map[x, y] == 'R') // check right
            {
                map[x, y] = ' ';
                y = y + 1;
            }

            else if (map[x, y] == 'U') // check up
            {
                map[x, y] = ' ';
                x = x - 1;
            }

            else if (map[x, y] == 'D') // check down
            {
                map[x, y] = ' ';
                x = x + 1;
            }
        }
        public static void eat(int HeadX, int HeadY, int fruitX, int fruitY, char[,] map, ref bool hasEaten)
        {
            if (map[HeadX, HeadY] == map[fruitX, fruitY])
            {
                hasEaten = true;
            }
        }

        public static void bump(int HeadX, int HeadY, char[,] map, bool hasEaten, ref bool isDone)
        {
            if (map[HeadX, HeadY] != ' ' && hasEaten == false)
            {
                isDone = true;
            }
        }

        public static void moveHead(ref int HeadX, ref int HeadY, ref int TailX, ref int TailY, ref int fruitX, ref int fruitY, char[,] map, ref char direction, ref bool hasEaten, ref bool isDone, int mapSize)
        {
            if (direction == 'U')
            {
                map[HeadX, HeadY] = 'U';
                HeadX = HeadX - 1;
            }
            else if (direction == 'D')
            {
                map[HeadX, HeadY] = 'D';
                HeadX = HeadX + 1;
            }
            else if (direction == 'R')
            {
                map[HeadX, HeadY] = 'R';
                HeadY = HeadY + 1;
            }
            else if (direction == 'L')
            {
                map[HeadX, HeadY] = 'L';
                HeadY = HeadY - 1;
            }
            eat(HeadX, HeadY, fruitX, fruitY, map, ref hasEaten);
            bump(HeadX, HeadY, map, hasEaten, ref isDone);
            map[HeadX, HeadY] = '0';
            if (hasEaten == false)
            {
                moveTail(ref TailX, ref TailY, map);
            }
            printMap(map, mapSize);
        }
    }
    }


