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
    public class Bullet
    {
        Vector2 velocity;

        // The location and size of the bullet
        public Rectangle rec;

        // Bool to check if the bullet collided
        public bool collide;

        int bulletRange;
        int bulletDamage;

        public Vector2 startPoint;

        public string owner;

        public string bulletType;

        public PointLight bulletLight = new PointLight { Scale = new Vector2(50), CastsShadows = true, ShadowType = ShadowType.Occluded, Intensity = 1f, Color = Color.Red };

        public double angle;

        public Bullet(Vector2 startPoint, Vector2 speed, int bulletRange, int damage, string owner, string type, double angle)
        {
            velocity = speed;

            collide = false;

            rec = new Rectangle((int)startPoint.X, (int)startPoint.Y, 15, 7);
            this.startPoint = startPoint;
            this.bulletRange = bulletRange;
            bulletDamage = damage;
            this.owner = owner;

            this.angle = angle;

            bulletType = type;
            //Game1.maps.bulletParticles.Add(new ParticleEngine(new Vector2(startPoint.X, startPoint.Y), 3));

            Game1.penumbra.Lights.Add(bulletLight);
        }

        public void Update()
        {
            switch (bulletType)
            {
                case "BASIC":
                    {
                        rec.X -= (int)velocity.X;
                        rec.Y -= (int)velocity.Y;

                        bulletLight.Position = new Vector2(rec.X + rec.Width / 2, rec.Y + rec.Height / 2);

                        if (Math.Sqrt(Math.Pow(rec.X - startPoint.X, 2) + Math.Pow(rec.Y - startPoint.Y, 2)) > bulletRange)
                        {
                            collide = true;
                            Game1.penumbra.Lights.Remove(bulletLight);
                        }

                        if ((owner == "PLAYER") && Game1.frames % 2 == 0)
                        {
                            for (int i = 0; i < Game1.worlds[0].enemies.Count; i++)
                            {
                                if (rec.Intersects(Game1.worlds[0].enemies[i].rec))
                                {
                                    Game1.worlds[0].enemies[i].health -= bulletDamage;
                                    collide = true;
                                    Game1.penumbra.Lights.Remove(bulletLight);
                                }
                            }
                        }

                        for(int depth = 0; depth < Game1.worlds[0].depth; depth++)
                        {
                            for (int width = 0; width < Game1.worlds[0].width; width++)
                            {
                                if (Game1.worlds[0].worldTiles[width, depth].type != "BLANK" && rec.Intersects(Game1.worlds[0].worldTiles[width, depth].rec))
                                {                                   
                                    collide = true;

                                    Game1.penumbra.Lights.Remove(bulletLight);
                                }
                            }
                        }

                        break;
                    }
                

            }



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Checks the type of bullet, and draws it according to it's position and image
            spriteBatch.Draw(Game1.whiteSquare, new Vector2(rec.X + rec.Width / 2, rec.Y + rec.Height / 2), rec, Color.Red, (float)angle, new Vector2(rec.Width / 2, rec.Height / 2), 1f, SpriteEffects.None, 1f);
            
            //spriteBatch.Draw(Game1.whiteSquare, rec, Color.Red);
        }
    }
}
