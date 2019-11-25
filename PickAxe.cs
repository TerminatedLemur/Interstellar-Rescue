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

namespace InterstellarRescue
{
    public class PickAxe
    {
        Rectangle rec;

        Rectangle player;

        double angle;

        public PickAxe(Rectangle Owner)
        {
            player = Owner;
            rec.Width = Game1.gridSize;
            rec.Height = Game1.gridSize;
        }


        public void Update()
        {
            rec.X = (int)Player.pos.X;
            rec.Y = (int)Player.pos.Y;

            if(Game1.mouse.LeftButton == ButtonState.Pressed)
            {
                angle = Math.Atan2(player.Y - Game1.playerCam.ScreenToWorld(new Vector2(Game1.mouse.X, Game1.mouse.Y)).Y, player.X - Game1.playerCam.ScreenToWorld(new Vector2(Game1.mouse.X, Game1.mouse.Y)).X);

                rec.X += Game1.gridSize * (int)Math.Cos(angle);
                rec.Y += Game1.gridSize * (int)Math.Sin(angle);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.whiteSquare, rec, Color.Purple);
        }
    }
}
