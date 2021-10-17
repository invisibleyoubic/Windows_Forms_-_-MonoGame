using System;
using System.Text;
using System.IO;

namespace BOX_PUSHER
{
    class Map
    {
        public Blocks[,] map;
        public Blocks[,] objects;

        int[,] map_v;

        public int p_x;
        public int p_y;

        public int b_count;
        public int g_count = 0;

        public int[] boxes;
        public int[] goals;

        public int level = 1;

        public void array_fill()
        {
            int buf;
            int buff;
            string s;
            StreamReader str;
            str = new StreamReader($"./Content/map{level}.txt", Encoding.UTF8);
            
            buf = Convert.ToInt32(str.ReadLine());
            buff = Convert.ToInt32(str.ReadLine());
            map = new Blocks[buf, buff];
            objects = new Blocks[buf, buff];

            map_v = new int[buf, buff];

            for (int i = 0; i < map_v.GetLength(1); i++)
            {
                s = str.ReadLine();
                for (int j = 0; j < map_v.GetLength(0); j++)
                {
                    map_v[j, i] = s[j] - 48; // почему 48? сам не знаю
                }
            }

            buf = Convert.ToInt32(str.ReadLine());
            buff = Convert.ToInt32(str.ReadLine());
            p_x = Convert.ToInt32(buff);
            p_y = Convert.ToInt32(buf);

            buf = Convert.ToInt32(str.ReadLine());
            b_count = buf;
            boxes = new int[b_count * 2];
            goals = new int[b_count * 2];

            s = "";
            for (int i = 0; i < boxes.Length; i++)
            {
                buf = Convert.ToInt32(str.ReadLine());
                boxes[i] = buf;
            }
            s = "";
            for (int i = 0; i < goals.Length; i++)
            {
                buff = Convert.ToInt32(str.ReadLine());
                goals[i] = buff;
            }
            
            str.Close();
        }

        public void map_fill()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map_v[i, j] == 1)
                    {
                        int flag = 1;
                        map[i, j] = new Blocks(false, i, j, flag); // walls in map
                    }
                    if (map_v[i, j] == 0)
                    {
                        int flag = 0;
                        map[i, j] = new Blocks(true, i, j, flag);  // floor in map
                    }
                    if (map_v[i, j] == 6)
                    {
                        map[i, j] = new Blocks(false, 6);          // empty blocks in map
                    }
                }
            }
        }

        public void objects_fill()
        {
            for (int i = 0; i < objects.GetLength(0); i++)
            {
                for (int j = 0; j < objects.GetLength(1); j++)
                {
                    objects[i, j] = new Blocks(true, 0);          // empty blocks in objects
                }

            }
            
            for (int i = 0; i < boxes.Length; i += 2)
            {
                int b_x = boxes[i];
                int b_y = boxes[i + 1];

                objects[b_x, b_y] = new Blocks(false, b_x, b_y, 3);  // boxes in objects
            }

            for (int i = 0; i < goals.Length; i += 2)
            {
                int g_x = goals[i];
                int g_y = goals[i + 1];

                objects[g_x, g_y] = new Blocks(true, g_x, g_y, 4);   // goals in objects
            }

            objects[p_x, p_y] = new Blocks(p_x, p_y, 2);    // person
        }
    }
}
