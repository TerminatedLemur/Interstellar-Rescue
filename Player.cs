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
    public static class Player
    {
        public static double healthMax = 100;
        public static double health = 100;
        public static int speed = 5;
        public static int jump = 12;

        public static double gravityAcl = 0.5;
        public static double gravity = 0;
        public static double gravityMax = 10;
        public static bool grounded = false;


        public static Vector2 pos = new Vector2(1000, -500);
        public static Rectangle rec = new Rectangle( 0, -200, 40, 80);

        public static Rectangle boundD = new Rectangle(5, 78, 30, 1);
        public static Rectangle boundU = new Rectangle(5, 0, 30, 1);
        public static Rectangle boundL = new Rectangle(0, 10, 1, 60);
        public static Rectangle boundR = new Rectangle(38, 10, 1, 60);

        public static ParticleEngine particleEngine = new ParticleEngine(pos, 5, true, false, 0, false, false, Color.SaddleBrown);

        public static int gold = 0;

        public static PointLight playerLight = new PointLight { Scale = new Vector2(600), CastsShadows = true, ShadowType = ShadowType.Solid, Intensity = 0.5f, /*Color = Color.Yellow*/ };
        public static PointLight playerLight2 = new PointLight { Scale = new Vector2(300), CastsShadows = true, ShadowType = ShadowType.Illuminated, Intensity = 0.75f, /*Color = Color.Yellow*/ };
       


        public static void Update()
        {
            if (Game1.kb.IsKeyDown(Keys.A) && pos.X > 0)
            {
                pos.X -= speed;
                //particleEngine.create = true;
            }
            else if (Game1.kb.IsKeyDown(Keys.D) && pos.X + rec.Width < Game1.worlds[0].width * Game1.gridSize)
            {
                pos.X += speed;
                //particleEngine.create = true;
            }
            else
            {
                particleEngine.create = false;
            }

            if (Game1.kb.IsKeyDown(Keys.W) && grounded)
            {
                grounded = false;
                gravity = -jump;
                pos.Y -= 10;
            }

            rec.X = (int)pos.X;
            rec.Y = (int)pos.Y;

            boundD.X = rec.X + 5;
            boundD.Y = rec.Y + 78;

            boundU.X = rec.X + 5;
            boundU.Y = rec.Y;

            boundL.X = rec.X;
            boundL.Y = rec.Y + 10;

            boundR.X = rec.X + 38;
            boundR.Y = rec.Y + 10;

            if (gravity < gravityMax && !grounded)
            {
                gravity += gravityAcl;
                //particleEngine.create = true;
            }

            if(grounded || rec.Y + rec.Height > Game1.worlds[0].depth * Game1.gridSize)
            {
                gravity = 0;
            }

            pos.Y += (int)gravity;


            playerLight.Position = new Vector2(pos.X + 20, pos.Y + 40);
            playerLight2.Position = new Vector2(pos.X + 20, pos.Y + 40);

            //particleEngine.EmitterLocation = new Vector2(pos.X + rec.Width / 2, pos.Y + rec.Height);
            particleEngine.Update();






        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.whiteSquare, rec, Color.Orange);

            //spriteBatch.Draw(Game1.whiteSquare, boundD, Color.Red);
            //spriteBatch.Draw(Game1.whiteSquare, boundU, Color.Red);
            //spriteBatch.Draw(Game1.whiteSquare, boundL, Color.Red);
            //spriteBatch.Draw(Game1.whiteSquare, boundR, Color.Red);

            //spriteBatch.DrawString(Game1.basicFont, mineDirection, pos, Color.Black);

            spriteBatch.Draw(Game1.playerTex[0], rec, Color.White);

            particleEngine.Draw(spriteBatch);

            //pickAxe.Draw(spriteBatch);
        }


       
            

        

    }
}
