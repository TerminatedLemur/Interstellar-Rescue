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
    public class Objects
    {
        public Rectangle rec;

        public string type;

        public Animation animation;

        public bool oneTime = true;

        //public PointLight shimmer = new PointLight { Scale = new Vector2(5), CastsShadows = true, ShadowType = ShadowType.Illuminated, Intensity = 1f, /*Color = Color.Yellow*/ };

        public Objects(string Type, Vector2 pos)
        {
            type = Type;

            switch (type)
            {
                case "CHEST":
                    {
                        rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize + 15, 100, 100);

                        animation = new Animation(Game1.objectsAnimationTex[0], 3, 3, 9, 1, 1, Animation.ANIMATE_FOREVER, 10, new Vector2(pos.X * Game1.gridSize, pos.Y * Game1.gridSize), 1f, true);

                        animation.destRec = rec;

                        //shimmer.Position = new Vector2(rec.X + rec.Width / 2 + 5, rec.Y + rec.Height/2);
                        //Game1.penumbra.Lights.Add(shimmer);

                        break;
                    }
                case "CHEST TRAP":
                    {
                        rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize + 15, 100, 100);

                        animation = new Animation(Game1.objectsAnimationTex[0], 3, 3, 9, 1, 1, Animation.ANIMATE_FOREVER, 8, new Vector2(pos.X * Game1.gridSize, pos.Y * Game1.gridSize), 1f, true);

                        animation.destRec = rec;

                        //shimmer.Position = new Vector2(rec.X + rec.Width / 2 + 5, rec.Y + rec.Height/2);
                        //Game1.penumbra.Lights.Add(shimmer);

                        break;
                    }
            }

        }

        public void Update()
        {

            switch (type)
            {
                case "CHEST":
                    {
                        if(Game1.DistanceBetweenPoints(new Vector2(rec.X, rec.Y), Player.pos) < 100 && Game1.ButtonPressedCamera(rec, Game1.playerCam) == "PRESSED" && oneTime)
                        {
                            animation = new Animation(Game1.objectsAnimationTex[1], 5, 5, 23, 1, 23, Animation.ANIMATE_ONCE, 10, new Vector2(rec.X, rec.Y), 1f, true);
                            animation.destRec = rec;


                            oneTime = false;
                        }

                        break;
                    }
                case "CHEST TRAP":
                    {
                        if (Game1.DistanceBetweenPoints(new Vector2(rec.X, rec.Y), Player.pos) < 100 && Game1.ButtonPressedCamera(rec, Game1.playerCam) == "PRESSED" && oneTime)
                        {
                            animation = new Animation(Game1.objectsAnimationTex[2], 6, 7, 41, 1, 41, Animation.ANIMATE_ONCE, 10, new Vector2(rec.X, rec.Y), 1f, true);
                            animation.destRec = rec;


                            oneTime = false;
                        }

                        break;
                    }
            }


            animation.Update(Game1.gameTimePublic);



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Math.Sqrt(Math.Pow(Player.rec.X - rec.X, 2) + Math.Pow(Player.rec.Y - rec.Y, 2)) < Game1.renderSize)
            {
                switch (type)
                {
                    case "CHEST":
                        {
                            animation.Draw(spriteBatch, Color.White, SpriteEffects.None);

                            break;
                        }
                    case "CHEST TRAP":
                        {
                            animation.Draw(spriteBatch, Color.White, SpriteEffects.None);

                            break;
                        }
                }


            }
        }

    }
}
