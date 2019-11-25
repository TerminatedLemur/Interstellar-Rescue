using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;
using Penumbra;

namespace InterstellarRescue
{
    public class MineBlock
    {
        public string type;
        public string typePre;

        public int health;

        private int drop;

        private Vector2 pos;

        public Rectangle rec;

        public Hull hull;

        public Texture2D stone;


        public string edges;

        public MineBlock(string Type, Vector2 loc)
        {
            type = Type;
            pos = loc;
            rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, Game1.gridSize, Game1.gridSize);

            edges = "";

            switch (type)
            {
                case "BLANK":
                    {
                        break;
                    }
                case "AIR":
                    {

                        break;
                    }
                case "STONE":
                    {
                        hull = new Hull(new Vector2(0f, 0f), new Vector2(Game1.gridSize, 0f), new Vector2(Game1.gridSize, Game1.gridSize), new Vector2(0f, Game1.gridSize));
                        hull.Position = new Vector2(rec.X, rec.Y);
                        health = 2;
                        Game1.penumbra.Hulls.Add(hull);

                        stone = Game1.stonetextures[Game1.rand.Next(0, 8)];

                        break;
                    }
                case "DIRT":
                    {
                        hull = new Hull(new Vector2(0f, 0f), new Vector2(Game1.gridSize, 0f), new Vector2(Game1.gridSize, Game1.gridSize), new Vector2(0f, Game1.gridSize));
                        hull.Position = new Vector2(rec.X, rec.Y);
                        health = 1;
                        Game1.penumbra.Hulls.Add(hull);
                        break;
                    }
                case "TOP DIRT":
                    {
                        hull = new Hull(new Vector2(0f, 0f), new Vector2(Game1.gridSize, 0f), new Vector2(Game1.gridSize, Game1.gridSize), new Vector2(0f, Game1.gridSize));
                        hull.Position = new Vector2(rec.X, rec.Y);
                        health = 1;
                        Game1.penumbra.Hulls.Add(hull);
                        break;
                    }
                case "GOLD":
                    {
                        hull = new Hull(new Vector2(0f, 0f), new Vector2(Game1.gridSize, 0f), new Vector2(Game1.gridSize, Game1.gridSize), new Vector2(0f, Game1.gridSize));
                        hull.Position = new Vector2(rec.X, rec.Y);
                        health = 5;
                        Game1.penumbra.Hulls.Add(hull);
                        break;
                    }
                case "HALLWAY":
                    {
                        hull = new Hull(new Vector2(0f, 0f), new Vector2(Game1.gridSize, 0f), new Vector2(Game1.gridSize, Game1.gridSize), new Vector2(0f, Game1.gridSize));
                        hull.Position = new Vector2(rec.X, rec.Y);
                        health = 7;
                        Game1.penumbra.Hulls.Add(hull);
                        break;
                    }
            }
        }

        public void Update()
        {

            switch(type)
            {
                case "BLANK":
                    {

                        break;
                    }
                case "AIR":
                    {

                        break;
                    }
                case "STONE":
                    {
                        if(Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < 400)
                        {
                            Collison();

                            Mine();

                        }

                        CollisonEnemies();

                        if (health <= 0)
                        {
                            Game1.penumbra.Hulls.Remove(hull);
                            type = "BLANK";

                        }
                        break;
                    }
                case "DIRT":
                    {
                        if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < 400)
                        {
                            Collison();

                            Mine();
                        }

                        CollisonEnemies();

                        if (health <= 0)
                        {
                            Game1.penumbra.Hulls.Remove(hull);
                            type = "BLANK";
                        }
                        break;
                    }
                case "TOP DIRT":
                    {
                        if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < 400)
                        {
                            Collison();

                            Mine();

                        }

                        CollisonEnemies();

                        if (health <= 0)
                        {
                            Game1.penumbra.Hulls.Remove(hull);
                            type = "BLANK";
                        }
                        break;
                    }
                case "GOLD":
                    {
                        if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < 400)
                        {
                            Collison();

                            Mine();
                            
                        }

                        CollisonEnemies();

                        if (health <= 0)
                        {
                            Game1.penumbra.Hulls.Remove(hull);
                            type = "BLANK";
                            Player.gold++;
                        }
                        break;
                    }
                case "HALLWAY":
                    {
                        if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < 400)
                        {
                            Collison();

                            Mine();

                            
                        }


                        CollisonEnemies();

                        if (health <= 0)
                        {
                            Game1.penumbra.Hulls.Remove(hull);
                            type = "BLANK";
                        }
                        break;
                    }
            }

            typePre = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < Game1.renderSize)
            {
                switch (type)
                {
                    case "BLANK":
                        {

                            break;
                        }
                    case "AIR":
                        {

                            break;
                        }
                    case "STONE":
                        {
                            spriteBatch.Draw(stone, rec, Color.White);
                            break;
                        }
                    case "DIRT":
                        {
                            spriteBatch.Draw(Game1.topLayerTexture[0], rec, Color.RosyBrown);
                            break;
                        }
                    case "TOP DIRT":
                        {
                            spriteBatch.Draw(Game1.topLayerTexture[0], rec, Color.White);
                            break;
                        }
                    case "GOLD":
                        {
                            spriteBatch.Draw(Game1.oreTexture[0], rec, Color.White);
                            break;
                        }
                    case "HALLWAY":
                        {
                            spriteBatch.Draw(Game1.whiteSquare, rec, Color.LightBlue);
                            break;
                        }
                }
            }
            
            
        }

        public void Collison()
        {
            if (rec.Intersects(Player.boundD))
            {
                Player.pos.Y -= (int)Player.gravity;
                Player.grounded = true;
            }

            if (rec.Intersects(Player.boundL))
            {
                Player.pos.X += Player.speed;
            }
            if (rec.Intersects(Player.boundR))
            {
                Player.pos.X -= Player.speed;
            }
            if (rec.Intersects(Player.boundU))
            {
                Player.gravity = 2;
            }
        }

        public void CollisonEnemies()
        {
            for(int i = 0; i < Game1.worlds[0].enemies.Count; i++)
            {

                switch(Game1.worlds[0].enemies[i].type)
                {
                    case "BAT":
                        {
                            if (rec.Intersects(Player.boundU))
                            {
                                Game1.worlds[0].enemies[i].acl.Y = 0;
                                Game1.worlds[0].enemies[i].rec.Y += Game1.worlds[0].enemies[i].speed;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundD))
                            {
                                Game1.worlds[0].enemies[i].acl.Y = 0;
                                Game1.worlds[0].enemies[i].rec.Y -= Game1.worlds[0].enemies[i].speed;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundL))
                            {
                                Game1.worlds[0].enemies[i].acl.X = 0;
                                Game1.worlds[0].enemies[i].rec.X += Game1.worlds[0].enemies[i].speed;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundR))
                            {
                                Game1.worlds[0].enemies[i].acl.X = 0;
                                Game1.worlds[0].enemies[i].rec.X -= Game1.worlds[0].enemies[i].speed;
                            }

                            break;
                        }

                    case "MOLE MAN":
                        {
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundD))
                            {
                                Game1.worlds[0].enemies[i].rec.Y -= (int)Math.Abs(Game1.worlds[0].enemies[i].gravity);
                                Game1.worlds[0].enemies[i].grounded = true;
                                Game1.worlds[0].enemies[i].gravity = 0;
                            }

                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundL))
                            {
                                Game1.worlds[0].enemies[i].rec.X += Game1.worlds[0].enemies[i].speed;
                                //Game1.worlds[0].enemies[i].speed = 0;
                                Game1.worlds[0].enemies[i].jump = true;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundR))
                            {
                                Game1.worlds[0].enemies[i].rec.X -= Game1.worlds[0].enemies[i].speed ;
                                //Game1.worlds[0].enemies[i].speed = 0;
                                Game1.worlds[0].enemies[i].jump = true;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundU))
                            {
                                Game1.worlds[0].enemies[i].gravity = 2;
                            }

                            break;
                        }
                    case "RAT":
                        {
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundD))
                            {
                                Game1.worlds[0].enemies[i].rec.Y -= (int)Math.Abs(Game1.worlds[0].enemies[i].gravity);
                                Game1.worlds[0].enemies[i].grounded = true;
                                Game1.worlds[0].enemies[i].gravity = 0;
                            }

                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundL))
                            {
                                Game1.worlds[0].enemies[i].rec.X += Game1.worlds[0].enemies[i].speed;
                                //Game1.worlds[0].enemies[i].speed = 0;
                                Game1.worlds[0].enemies[i].jump = true;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundR))
                            {
                                Game1.worlds[0].enemies[i].rec.X -= Game1.worlds[0].enemies[i].speed;
                                //Game1.worlds[0].enemies[i].speed = 0;
                                Game1.worlds[0].enemies[i].jump = true;
                            }
                            if (rec.Intersects(Game1.worlds[0].enemies[i].boundU))
                            {
                                Game1.worlds[0].enemies[i].gravity = 2;
                            }

                            break;
                        }
                }
                

            }
            
        }


        public void Mine()
        {
            
        }
    }
}
