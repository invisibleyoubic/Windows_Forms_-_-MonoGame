using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BOX_PUSHER
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D FLOOR;
        Texture2D WALL;
        Texture2D BOX;
        Texture2D PERSON;
        Texture2D GOAL;
        Texture2D BOX_GOAL;

        KeyboardState OKBS;
        KeyboardState KBS;

        Map map = new Map();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            map.array_fill();
            map.map_fill();
            map.objects_fill();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            FLOOR = Content.Load<Texture2D>("FLOOR");
            WALL = Content.Load<Texture2D>("WALL");
            BOX = Content.Load<Texture2D>("BOX");
            PERSON = Content.Load<Texture2D>("PERSON");
            GOAL = Content.Load<Texture2D>("GOAL");
            BOX_GOAL = Content.Load<Texture2D>("BOX_GOAL");
        }
        
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)   //логика игры 
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) //закрытие игры
                Exit();

            OKBS = KBS;
            KBS = Keyboard.GetState();
            
            bool flag = false;
            for (int i = 0; i < map.goals.Length; i += 2)
            {                               //проверка стоит ли персонаж на клетке цели
                if ((map.p_x == map.goals[i]) && (map.p_y == map.goals[i + 1]))   
                {
                    flag = true;
                }
            }

            if ((KBS.IsKeyDown(Keys.Up)) && OKBS.IsKeyUp(Keys.Up))  //нажата клавиша вверх
            {
                if (!flag)  //если персонаж не стоит на клетке цели
                {
                    if (Up())  //проверка возможности движения персонажа вверх
                    {
                        map.p_y--;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x, map.p_y + 1] = new Blocks(true, 0);
                    }
                    else
                    {
                        if (UpB())  //проверка возможности движения персонажа с ящиком вверх
                        {
                            map.p_y--;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                            map.objects[map.p_x, map.p_y + 1] = new Blocks(true, 0);
                            map.objects[map.p_x, map.p_y - 1] = new Blocks(false, map.p_x, map.p_y - 1, 3);
                        }
                    }
                }
                else   //если персонаж стоит на клетке цели
                {
                    if (Up())   //проверка возможности движения персонажа вверх
                    {
                        map.p_y--;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x, map.p_y + 1] = new Blocks(true, map.p_x, map.p_y + 1, 4);
                    }
                    else
                    {
                        if (UpB())   //проверка возможности движения персонажа с ящиком вверх
                        {
                            map.p_y--;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                            map.objects[map.p_x, map.p_y + 1] = new Blocks(true, map.p_x, map.p_y + 1, 4);
                            map.objects[map.p_x, map.p_y - 1] = new Blocks(false, map.p_x, map.p_y - 1, 3);
                        }
                    }
                }      
            
                UpG();  //ящик сдвинули вверх на его место
            }
            if ((KBS.IsKeyDown(Keys.Down)) && OKBS.IsKeyUp(Keys.Down))
            {
                if (!flag)
                {
                    if (Down())
                    {
                        map.p_y++;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x, map.p_y - 1] = new Blocks(true, 0);
                    }
                    else
                    {
                        if (DownB())
                        {
                            map.p_y++;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2); //person
                            map.objects[map.p_x, map.p_y - 1] = new Blocks(true, 0); //sled
                            map.objects[map.p_x, map.p_y + 1] = new Blocks(false, map.p_x, map.p_y + 1, 3); //box
                        }
                    }
                }
                else
                {
                    if (Down())
                    {
                        map.p_y++;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x, map.p_y - 1] = new Blocks(true, map.p_x, map.p_y - 1, 4);
                    }
                    else
                    {
                        if (DownB())
                        {
                            map.p_y++;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2); //person
                            map.objects[map.p_x, map.p_y - 1] = new Blocks(true, map.p_x, map.p_y - 1, 4); //sled
                            map.objects[map.p_x, map.p_y + 1] = new Blocks(false, map.p_x, map.p_y + 1, 3); //box
                        }
                    }
                }

                DownG();
            }
            if ((KBS.IsKeyDown(Keys.Right)) && OKBS.IsKeyUp(Keys.Right))
            {
                if (!flag)
                {
                    if (Right())  
                    {
                        map.p_x++;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x - 1, map.p_y] = new Blocks(true, 0);
                    }
                    else
                    {
                        if (RightB())
                        {
                            map.p_x++;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                            map.objects[map.p_x - 1, map.p_y] = new Blocks(true, 0);
                            map.objects[map.p_x + 1, map.p_y] = new Blocks(false, map.p_x + 1, map.p_y, 3);
                        }
                    }
                }
                else
                {
                    if (Right())
                    {
                        map.p_x++;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x - 1, map.p_y] = new Blocks(true, map.p_x-1, map.p_y, 4);
                    }
                    else
                    {
                        if (RightB())
                        {
                            map.p_x++;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                            map.objects[map.p_x - 1, map.p_y] = new Blocks(true, map.p_x - 1, map.p_y, 4);
                            map.objects[map.p_x + 1, map.p_y] = new Blocks(false, map.p_x + 1, map.p_y, 3);
                        }
                    }
                }
                RightG();
            }
            if ((KBS.IsKeyDown(Keys.Left)) && OKBS.IsKeyUp(Keys.Left))
            {
                if (!flag)
                {
                    if (Left())
                    {
                        map.p_x--;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x + 1, map.p_y] = new Blocks(true, 0);
                    }
                    else
                    {
                        if (LeftB())
                        {
                            map.p_x--;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                            map.objects[map.p_x + 1, map.p_y] = new Blocks(true, 0);
                            map.objects[map.p_x - 1, map.p_y] = new Blocks(false, map.p_x - 1, map.p_y, 3);
                        }
                    }
                }
                else
                {
                    if (Left())
                    {
                        map.p_x--;
                        map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                        map.objects[map.p_x + 1, map.p_y] = new Blocks(true, map.p_x+1, map.p_y, 4);
                    }
                    else
                    {
                        if (LeftB())
                        {
                            map.p_x--;
                            map.objects[map.p_x, map.p_y] = new Blocks(map.p_x, map.p_y, 2);
                            map.objects[map.p_x + 1, map.p_y] = new Blocks(true, map.p_x + 1, map.p_y, 4);
                            map.objects[map.p_x - 1, map.p_y] = new Blocks(false, map.p_x - 1, map.p_y, 3);
                        }
                    }
                }
                LeftG();
            }
            
            map.g_count = 0;                   //обнуление ящиков, которые стоят на местах

            foreach (var item in map.objects)  // пересчет ящиков, которые стоят на местах
            {
                if(item.flag == 5)
                {
                    map.g_count++;
                }
            }

            if ((KBS.IsKeyDown(Keys.R)) && OKBS.IsKeyUp(Keys.R)) //restart
            {
                map.array_fill();
                map.map_fill();
                map.objects_fill();
            }

            if (((KBS.IsKeyDown(Keys.W)) && OKBS.IsKeyUp(Keys.W)) && (map.level < 13)) //level +1
            {
                map.level++;
                map.array_fill();
                map.map_fill();
                map.objects_fill();
            }

            if (((KBS.IsKeyDown(Keys.Q)) && OKBS.IsKeyUp(Keys.Q)) && (map.level != 1)) //level -1
            {
                map.level--;
                map.array_fill();
                map.map_fill();
                map.objects_fill();
            }

            if (map.g_count == map.b_count) //проверка, все ли ящики на местах и запуск следующего уровня
            {
                if (map.level != 13)        //проверка, последний ли был пройден уровень
                {
                    map.level++;
                }
                else                       //если был пройден последний уровень, то игра выиграна
                {
                    map.level = 1;
                    Screen.Win();
                    Environment.Exit(0);
                }
                map.array_fill(); //если был пройден не последний уровень, то запускается следующий
                map.map_fill();
                map.objects_fill();
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)  //рисование на экране спрайтов
        {
            GraphicsAdapter adapter = graphics.GraphicsDevice.Adapter;
            graphics.PreferredBackBufferWidth = map.map.GetLength(0) * 50;
            graphics.PreferredBackBufferHeight = map.map.GetLength(1) * 50;
            graphics.ApplyChanges();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            
            foreach (var item in map.map) 
            {
                if (item.flag == 1)                                     //wall
                {
                    spriteBatch.Draw(WALL, item.blockPosition, Color.White);
                }
                if(item.flag == 0)                                      //floor
                {
                    spriteBatch.Draw(FLOOR, item.blockPosition, Color.White);
                }
            }

            foreach (var item in map.objects)
            {
                if (item.flag == 5)
                {                   //ящик, который стоит на нужном месте (цели)
                    spriteBatch.Draw(BOX_GOAL, item.blockPosition, Color.White); 
                }

                if (item.flag == 4)                                   //goal
                {
                    spriteBatch.Draw(GOAL, item.blockPosition, Color.White);    
                }

                if (item.flag == 3)                                 //box
                {
                    spriteBatch.Draw(BOX, item.blockPosition, Color.White);
                }

                if (item.flag == 2)                                 //person
                {
                    spriteBatch.Draw(PERSON, item.blockPosition, Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool Up()  //проверка возможности движения персонажа вверх
        {
            if (map.map[map.p_x, map.p_y - 1].prohodimostt)
            {
                if ((map.objects[map.p_x, map.p_y - 1].prohodimostt))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        protected bool UpB() //проверка возможности двиджения персонажа с ящиком вверх
        {
            if (map.map[map.p_x, map.p_y - 1].prohodimostt)
            {
                if ((map.objects[map.p_x, map.p_y - 2].prohodimostt) && 
                    (map.map[map.p_x, map.p_y-2].prohodimostt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected bool Down()
        {
            if (map.map[map.p_x, map.p_y + 1].prohodimostt)
            {
                if (map.objects[map.p_x, map.p_y + 1].prohodimostt)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected bool DownB()
        {
            if (map.map[map.p_x, map.p_y + 1].prohodimostt)
            {
                if ((map.objects[map.p_x, map.p_y + 2].prohodimostt) && (map.map[map.p_x, map.p_y + 2].prohodimostt)) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected bool Right()
        {
            if (map.map[map.p_x + 1, map.p_y].prohodimostt) 
            {
                if (map.objects[map.p_x + 1, map.p_y].prohodimostt)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected bool RightB()
        {
            if (map.map[map.p_x + 1, map.p_y].prohodimostt)
            {
                if ((map.objects[map.p_x + 2, map.p_y].prohodimostt)&& (map.map[map.p_x + 2, map.p_y].prohodimostt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected bool Left()
        {
            if (map.map[map.p_x - 1, map.p_y].prohodimostt)
            {
                if (map.objects[map.p_x - 1, map.p_y].prohodimostt)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected bool LeftB()
        {
            if (map.map[map.p_x - 1, map.p_y].prohodimostt)
            {
                if ((map.objects[map.p_x - 2, map.p_y].prohodimostt) && (map.map[map.p_x - 2, map.p_y].prohodimostt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        protected void UpG()    //ящик сдвинули вверх на его место
        {
            for (int i = 0; i < map.goals.Length; i += 2)
            {
                if ((Convert.ToInt32(map.objects[map.p_x, map.p_y - 1].blockPosition.Y / 50) == map.goals[i + 1]) &&        // ящик поставили на место
                    (map.objects[map.p_x, map.p_y - 1].flag == 3) && (Convert.ToInt32(map.objects[map.p_x, map.p_y - 1].blockPosition.X / 50) == map.goals[i])) 
                {
                    map.objects[map.goals[i], map.goals[i + 1]] = new Blocks(false, map.goals[i], map.goals[i + 1], 5);
                }
            }
        }

        protected void DownG()
        {
            for (int i = 0; i < map.goals.Length; i += 2)
            {
                if ((Convert.ToInt32(map.objects[map.p_x, map.p_y + 1].blockPosition.Y / 50) == map.goals[i + 1]) &&
                    (map.objects[map.p_x, map.p_y + 1].flag == 3) && (Convert.ToInt32(map.objects[map.p_x, map.p_y + 1].blockPosition.X / 50) == map.goals[i]))
                {
                    map.objects[map.goals[i], map.goals[i + 1]] = new Blocks(false, map.goals[i], map.goals[i + 1], 5);
                }
            }
        }

        protected void LeftG()
        {
            for (int i = 0; i < map.goals.Length; i += 2)
            {
                if ((Convert.ToInt32(map.objects[map.p_x-1, map.p_y].blockPosition.Y / 50) == map.goals[i + 1]) &&
                    (map.objects[map.p_x-1, map.p_y].flag == 3) && (Convert.ToInt32(map.objects[map.p_x-1, map.p_y].blockPosition.X / 50) == map.goals[i]))
                {
                    map.objects[map.goals[i], map.goals[i + 1]] = new Blocks(false, map.goals[i], map.goals[i + 1], 5);
                }
            }
        }

        protected void RightG()
        {
            for (int i = 0; i < map.goals.Length; i += 2)
            {
                if ((Convert.ToInt32(map.objects[map.p_x+1, map.p_y].blockPosition.Y / 50) == map.goals[i + 1]) &&
                    (map.objects[map.p_x+1, map.p_y].flag == 3) && (Convert.ToInt32(map.objects[map.p_x+1, map.p_y].blockPosition.X / 50) == map.goals[i]))
                {
                    map.objects[map.goals[i], map.goals[i + 1]] = new Blocks(false, map.goals[i], map.goals[i + 1], 5);
                }
            }
        }
    }
}
