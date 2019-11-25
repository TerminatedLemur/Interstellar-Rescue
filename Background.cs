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
    public class Background
    {
        public string type;
        public Texture2D image;
        public Vector2 pos;

        public Rectangle rec;

        public Animation animation;

        public int backgroundProportionallity;

        public Background(Vector2 loc, string Type, int proportion)
        {
            type = Type;
            pos = loc;

            backgroundProportionallity = proportion;

            switch (type)
            {
                case "BLANK":
                    {                        
                        break;
                    }
                case "EYES":
                    {
                        animation = new Animation(Game1.backgroundAnimationsTex[0], 4, 5, 20, 1, 1, Animation.ANIMATE_FOREVER, 12, pos, 1, true);
                        animation.destRec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, Game1.gridSize * 2, Game1.gridSize * 2);

                        break;
                    }
                case "EYES SMALL":
                    {
                        animation = new Animation(Game1.backgroundAnimationsTex[1], 2, 2, 4, 1, 1, Animation.ANIMATE_FOREVER, 17, pos, 1, true);
                        animation.destRec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, Game1.gridSize * 2, Game1.gridSize * 2);

                        break;
                    }
                case "STONE":
                    {
                        image = Game1.stoneBackgroundtextures[Game1.rand.Next(6,22)];
                        break;
                    }
                case "DIRT":
                    {
                        image = Game1.whiteSquare;
                        break;
                    }
                case "GOLD":
                    {

                        break;
                    }
                case "HALLWAY":
                    {

                        break;
                    }
            }

            rec = new Rectangle((int)pos.X * Game1.gridSize, (int)pos.Y * Game1.gridSize, backgroundProportionallity * Game1.gridSize, backgroundProportionallity * Game1.gridSize);

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
                    case "EYES":
                        {
                            animation.Draw(spriteBatch, Color.White, SpriteEffects.None);
                            break;
                        }
                    case "EYES SMALL":
                        {
                            animation.Draw(spriteBatch, Color.White, SpriteEffects.None);
                            break;
                        }
                    case "STONE":
                        {
                            spriteBatch.Draw(image, rec, Color.White);
                            break;
                        }
                    case "DIRT":
                        {
                            spriteBatch.Draw(image, rec, new Color(66, 34, 0));
                            break;
                        }
                    case "GOLD":
                        {

                            break;
                        }
                    case "HALLWAY":
                        {

                            break;
                        }
                }


            }


        }
    }
}
