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
    public class Enemy
    {
        public Rectangle rec;
        public Rectangle boundU;
        public Rectangle boundD;
        public Rectangle boundL;
        public Rectangle boundR;

        public Rectangle recPre;
        public Rectangle boundLPre;
        public Rectangle boundRPre;

        public Vector2 posGrid;

        public string type;

        string action;
        string direction;
        public Animation runAni;

        public int speed;
        public Vector2 acl;
        float aclVal;

        public double gravityAcl = 0.5;
        public double gravity = 0;
        public double gravityMax = 4;
        public bool grounded = false;
        public bool jump = false;

        public bool dead = false;

        public int vision;

        public int health;

        public int damage;


        public Enemy(string Type, Vector2 pos)
        {
            type = Type;
            posGrid = pos;

            switch (type)
            {
                case "BAT":
                    {
                        vision = 700;

                        aclVal = 0.05f;
                        speed = 5;

                        rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, 20, 20);

                        boundU = new Rectangle(rec.X + rec.Width / 4, rec.Y, rec.Width / 2, 1);
                        boundD = new Rectangle(rec.X + rec.Width / 4, rec.Y + rec.Height - 1, rec.Width / 2, 1);
                        boundL = new Rectangle(rec.X, rec.X + rec.Height / 4, 1, rec.Height / 2);
                        boundR = new Rectangle(rec.Y + rec.Width - 1, rec.X + rec.Height / 4, 1, rec.Height / 2);

                        damage = 5;

                        health = 2;

                        break;
                    }
                case "MOLE MAN":
                    {
                        vision = 800;

                        speed = 1;

                        health = 20;

                        rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, 80, 40);

                        boundU = new Rectangle(rec.X + rec.Width / 4, rec.Y, rec.Width / 2, 1);
                        boundD = new Rectangle(rec.X + 1, rec.Y + rec.Height - 1, rec.Width - 2, 1);
                        boundL = new Rectangle(rec.X, rec.X + rec.Height / 4, 1, rec.Height / 2);
                        boundR = new Rectangle(rec.Y + rec.Width - 1, rec.X + rec.Height / 4, 1, rec.Height / 2);

                        break;
                    }
                case "RAT":
                    {
                        vision = 600;

                        speed = 1;

                        health = 2;

                        damage = 2;

                        rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, 40, 20);

                        boundU = new Rectangle(rec.X + rec.Width / 4, rec.Y, rec.Width / 2, 1);
                        boundD = new Rectangle(rec.X + 1, rec.Y + rec.Height - 1, rec.Width - 2, 1);
                        boundL = new Rectangle(rec.X, rec.X + rec.Height / 4, 1, rec.Height / 2);
                        boundR = new Rectangle(rec.Y + rec.Width - 1, rec.X + rec.Height / 4, 1, rec.Height / 2);

                        runAni = new Animation(Game1.enemyAnimationTex[0], 1, 3, 3, 1, 1, Animation.ANIMATE_FOREVER, 10, new Vector2(rec.X, rec.Y), 1, true);
                        runAni.destRec = new Rectangle(rec.X, rec.Y, 80, 40);

                        break;
                    }
            }
        }

        public void Update()
        {
            switch (type)
            {
                case "BAT":
                    {
                        if (Game1.DistanceBetweenPoints(new Vector2(rec.X, rec.Y), Player.pos) < vision)
                        {

                            if (Player.rec.X > rec.X && acl.X <= speed)
                            {
                                acl.X += aclVal;
                            }
                            if (Player.rec.X < rec.X && acl.X >= -speed)
                            {
                                acl.X -= aclVal;
                            }

                            if (Player.rec.Y < rec.Y && acl.Y >= -speed)
                            {
                                acl.Y -= aclVal;
                            }
                            if (Player.rec.Y > rec.Y && acl.Y <= speed)
                            {
                                acl.Y += aclVal;
                            }
                            // Moves the enemyRectangle
                            rec.X += (int)acl.X;
                            rec.Y += (int)acl.Y;

                            boundU.X = rec.X + rec.Width / 4;
                            boundU.Y = rec.Y;
                            boundD.X = rec.X + +rec.Width / 4;
                            boundD.Y = rec.Y + rec.Height - 1;
                            boundL.X = rec.X;
                            boundL.Y = rec.Y + rec.Height/4;
                            boundR.X = rec.X + rec.Width - 1;
                            boundR.Y = rec.Y + rec.Height / 4;

                            if (rec.Intersects(Player.rec) && !recPre.Intersects(Player.rec))
                            {
                                Player.health -= damage;
                            }
                        }

                        break;
                    }

                case "MOLE MAN":
                    {
                        if (Game1.DistanceBetweenPoints(new Vector2(rec.X, rec.Y), Player.pos) < vision)
                        {
                            boundU.X = rec.X + rec.Width / 4;
                            boundU.Y = rec.Y;
                            boundD.X = rec.X + 1;
                            boundD.Y = rec.Y + rec.Height - 1;
                            boundL.X = rec.X;
                            boundL.Y = rec.Y + rec.Height / 4;
                            boundR.X = rec.X + rec.Width - 1;
                            boundR.Y = rec.Y + rec.Height / 4;

                            if (Player.rec.X < rec.X)
                            {
                                rec.X -= speed;
                            }
                            else if (Player.rec.X > rec.X)
                            {
                                rec.X += speed;
                            }


                            if (jump /*&& grounded*/)
                            {
                                grounded = false;
                                gravity = -7;
                                //rec.Y -= 10;
                                jump = false;

                                rec.Width = 40;
                                rec.Height = 80;

                            }
                            else
                            {
                                rec.Width = 80;
                                rec.Height = 40;
                            }

                            if (gravity < gravityMax /*&& !grounded*/)
                            {
                                gravity += gravityAcl;
                                //jump = false;
                            }

                            //if (grounded)
                            //{
                            //    gravity = 0;
                            //}

                            rec.Y += (int)gravity;
                            //grounded = false;


                            


                            //if(Game1.DistanceBetweenPoints(new Vector2(Player.rec.X, Player.rec.Y), new Vector2(rec.X, rec.Y)) < 300)
                            //{
                            //    speed = 2;
                            //}
                            //else
                            //{
                            //    speed = 1;
                            //}
                        }
                        else
                        {
                            gravity = 0;
                        }




                        break;
                    }
                case "RAT":
                    {
                        if (Game1.DistanceBetweenPoints(new Vector2(rec.X, rec.Y), Player.pos) < vision)
                        {
                            boundU.X = rec.X + rec.Width / 4;
                            boundU.Y = rec.Y;
                            boundD.X = rec.X + 1;
                            boundD.Y = rec.Y + rec.Height - 1;
                            boundL.X = rec.X;
                            boundL.Y = rec.Y + rec.Height / 4;
                            boundR.X = rec.X + rec.Width - 1;
                            boundR.Y = rec.Y + rec.Height / 4;

                            action = "IDLE";

                            if (Player.rec.X < rec.X)
                            {
                                rec.X -= speed;
                                action = "RUN";
                                direction = "LEFT";
                            }
                            else if (Player.rec.X > rec.X)
                            {
                                rec.X += speed;
                                action = "RUN";
                                direction = "RIGHT";
                            }


                            if (jump && grounded)
                            {
                                grounded = false;
                                gravity = -7;
                                jump = false;

                            }


                            if (gravity < gravityMax)
                            {
                                gravity += gravityAcl;
                            }

                            rec.Y += (int)gravity;
                            grounded = false;


                        }
                        else
                        {
                            gravity = 0;
                        }


                        if(Player.rec.Intersects(boundL) && !recPre.Intersects(Player.rec))
                        {
                            rec.X += speed;

                            if (!jump)
                            {
                                Player.health -= damage;
                            }

                            jump = true;
                        }
                        if (Player.rec.Intersects(boundR))
                        {
                            rec.X -= speed;

                            if(!jump)
                            {
                                Player.health -= damage;
                            }

                            jump = true;
                        }





                        runAni.destRec.X = rec.X - runAni.destRec.Width/4;
                        runAni.destRec.Y = rec.Y - runAni.destRec.Height/4 - 5;

                        //runAni.destRec = new  Rectangle(rec.X, rec.Y + 5, rec.Width, rec.Height);

                        runAni.Update(Game1.gameTimePublic);


                        break;
                    }


            }

            if(health <= 0)
            {
                dead = true;
            }


            if(rec.X > Game1.worlds[0].width * Game1.gridSize)
            {
                dead = true;
            }
            else if(rec.X < 0)
            {
                dead = true;
            }
            else if (rec.Y < -1000)
            {
                dead = true;
            }
            else if (rec.Y > Game1.worlds[0].depth * Game1.gridSize)
            {
                dead = true;
            }

            recPre = rec;
            boundLPre = boundL;
            boundRPre = boundR;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < Game1.renderSize)
            {

                switch (type)
                {
                    case "BAT":
                        {
                            spriteBatch.Draw(Game1.whiteSquare, rec, Color.White);

                            spriteBatch.Draw(Game1.whiteSquare, boundD, Color.Red);
                            spriteBatch.Draw(Game1.whiteSquare, boundU, Color.Red);
                            spriteBatch.Draw(Game1.whiteSquare, boundL, Color.Red);
                            spriteBatch.Draw(Game1.whiteSquare, boundR, Color.Red);

                            break;
                        }
                    case "MOLE MAN":
                        {
      
                            break;
                        }
                    case "RAT":
                        {
                            //spriteBatch.Draw(Game1.whiteSquare, rec, Color.White);

                            if (action == "RUN" && direction == "LEFT")
                            {
                                runAni.Draw(spriteBatch, Color.White, SpriteEffects.FlipHorizontally);
                            }
                            else if (action == "RUN" && direction == "RIGHT")
                            {
                                runAni.Draw(spriteBatch, Color.White, SpriteEffects.None);
                            }

                            if (action == "ATTACK")
                            {
                                spriteBatch.Draw(Game1.whiteSquare, rec, Color.White);
                            }

                            break;

                        }
                }

            }
                

        }
    }
}
