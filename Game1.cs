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
using Camera;
using Penumbra;


namespace InterstellarRescue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Texture2D whiteSquare;
        public static SpriteFont basicFont;

        public static int gridSize = 50;
        public static int renderSize = 600;

        public static List<World> worlds = new List<World>();

        bool runOnce = true;

        

        public static int windowHeight = 0;
        public static int windowWidth = 0;

        public static Cam2D playerCam;

        public static PenumbraComponent penumbra;

        public static Texture2D[] playerTex = new Texture2D[8];

        public static Random rand = new Random();

        public static int frames = 0;

        public static GameTime gameTimePublic;

        public static Texture2D[] stonetextures = new Texture2D[8];
        public static Texture2D[] stoneBackgroundtextures = new Texture2D[25];

        public static Texture2D[] backgroundAnimationsTex = new Texture2D[8];

        public static Texture2D[] oreTexture = new Texture2D[8];
        public static Texture2D[] topLayerTexture = new Texture2D[8];

        public static Texture2D[] objectsAnimationTex = new Texture2D[8];

        public static Texture2D[] enemyAnimationTex = new Texture2D[8];

        public static FrameCounter _frameCounter = new FrameCounter();


        public static KeyboardState kb;
        public static KeyboardState kbPre;
        public static MouseState mouse;
        public static MouseState mousePre;

        float mouseWheel = 0;
        float mouseWheelPre = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            penumbra = new PenumbraComponent(this) { AmbientColor = new Color(0, 0, 0) };
            Components.Add(penumbra);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //renderSize = windowWidth;

            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;

            //graphics.IsFullScreen = true;

            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            //IsFixedTimeStep = false;

            IsMouseVisible = true;

            penumbra.Initialize();

            penumbra.Lights.Add(Player.playerLight);
            penumbra.Lights.Add(Player.playerLight2);
            worlds.Add(new World(75, 250, 0.10f));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            whiteSquare = Content.Load<Texture2D>("WhiteSquare");
            basicFont = Content.Load<SpriteFont>("BasicFont");

            playerTex[0] = Content.Load<Texture2D>("Assets/Player/Player2");


            stonetextures[0] = Content.Load<Texture2D>("Assets/Stones/Rock_texture_6");
            stonetextures[1] = Content.Load<Texture2D>("Assets/Stones/Rock_texture_8");
            stonetextures[2] = Content.Load<Texture2D>("Assets/Stones/Rock_texture_9");
            stonetextures[3] = Content.Load<Texture2D>("Assets/Stones/Rock_texture_10");
            stonetextures[4] = Content.Load<Texture2D>("Assets/Stones/Rock_texture1");
            stonetextures[5] = Content.Load<Texture2D>("Assets/Stones/Rock_texture2");
            stonetextures[6] = Content.Load<Texture2D>("Assets/Stones/Rock_texture3");
            stonetextures[7] = Content.Load<Texture2D>("Assets/Stones/Rock_texture4");

            stoneBackgroundtextures[0] = Content.Load<Texture2D>("Assets/Backgrounds/Background1");
            stoneBackgroundtextures[1] = Content.Load<Texture2D>("Assets/Backgrounds/Background2");
            stoneBackgroundtextures[2] = Content.Load<Texture2D>("Assets/Backgrounds/Background3");
            stoneBackgroundtextures[3] = Content.Load<Texture2D>("Assets/Backgrounds/Background4");
            stoneBackgroundtextures[4] = Content.Load<Texture2D>("Assets/Backgrounds/Background5");
            stoneBackgroundtextures[5] = Content.Load<Texture2D>("Assets/Backgrounds/Background6");

            stoneBackgroundtextures[6] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[10] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[11] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[12] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[13] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[7] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundBlood1");
            stoneBackgroundtextures[14] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundBlood1");
            stoneBackgroundtextures[15] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundBlood1");
            stoneBackgroundtextures[8] = Content.Load<Texture2D>("Assets/Backgrounds/Poop");
            stoneBackgroundtextures[16] = Content.Load<Texture2D>("Assets/Backgrounds/Poop");
            stoneBackgroundtextures[17] = Content.Load<Texture2D>("Assets/Backgrounds/Poop");
            stoneBackgroundtextures[9] = Content.Load<Texture2D>("Assets/Backgrounds/poop3");
            stoneBackgroundtextures[18] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[19] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[20] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");
            stoneBackgroundtextures[21] = Content.Load<Texture2D>("Assets/Backgrounds/BackgroundFinal");

            backgroundAnimationsTex[0] = Content.Load<Texture2D>("Assets/Backgrounds/EyesFinal");
            backgroundAnimationsTex[1] = Content.Load<Texture2D>("Assets/Backgrounds/EyesSmall");

            objectsAnimationTex[0] = Content.Load<Texture2D>("Assets/Objects/chest_idle");
            objectsAnimationTex[1] = Content.Load<Texture2D>("Assets/Objects/chest_open");
            objectsAnimationTex[2] = Content.Load<Texture2D>("Assets/Objects/Trapped_chest");
            objectsAnimationTex[3] = Content.Load<Texture2D>("Assets/Objects/Drone");

            oreTexture[0] = Content.Load<Texture2D>("Assets/Ore/ore1");

            enemyAnimationTex[0] = Content.Load<Texture2D>("Assets/Enemies/Rat_run");

            topLayerTexture[0] = Content.Load<Texture2D>("Assets/TopLayers/Sand");

            playerCam = new Cam2D(GraphicsDevice.Viewport, new Rectangle(-20000, -5000, 42500, 100000), 1, 5, 0, Player.rec);
            // TODO: use this.Content to load your game content here
        }



        protected override void Update(GameTime gameTime)
        {
            gameTimePublic = gameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            penumbra.Transform = playerCam.GetTransformation();

            if (runOnce)
            {
                worlds[0].initialize();

                ToolBelt.Initialize();

                runOnce = false;
            }

            
            if (kb.IsKeyDown(Keys.Q))
            {
                renderSize += 10;

            }
            else if (kb.IsKeyDown(Keys.E))
            {
                playerCam.SetZoom(3f);
                renderSize = 600;
            }

            Zoom();

            kb = Keyboard.GetState();
            mouse = Mouse.GetState();

            worlds[0].Update();
            Player.Update();
            ToolBelt.Update();

            playerCam.LookAt(Player.rec);

            // TODO: Add your update logic here

            if (kb.IsKeyDown(Keys.R) && kbPre.IsKeyUp(Keys.R))
            {
                if (penumbra.Visible == false)
                {
                    penumbra.Visible = true;
                }
                else
                {
                    penumbra.Visible = false;
                }

            }

            frames++;
            mousePre = mouse;
            kbPre = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            penumbra.BeginDraw();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, playerCam.GetTransformation());


            worlds[0].Draw(spriteBatch);

            Player.Draw(spriteBatch);


            spriteBatch.End();

            penumbra.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawString(basicFont, "GOLD: " + Player.gold, new Vector2(1, 100), Color.White);

            ToolBelt.Draw(spriteBatch);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);

            spriteBatch.DrawString(basicFont, fps, new Vector2(1, 1), Color.White);


            spriteBatch.End();

            //base.Draw(gameTime);
        }

        public static string ButtonPressed(Rectangle buttonRec)
        {
            // Checks if the mouse is above the rectangle
            if (mouse.X > buttonRec.X && mouse.X < (buttonRec.X + buttonRec.Width))
            {
                if (mouse.Y > buttonRec.Y && mouse.Y < (buttonRec.Y + buttonRec.Height))
                {
                    // If the mouse is pressed while above the rectangle
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        // If mouse isn's being held down
                        if (mousePre.LeftButton != ButtonState.Pressed)
                        {
                            return "PRESSED";
                        }
                        else
                        {
                            return "HOLD";
                        }
                    }
                    else
                    {
                        return "HOVER";
                    }
                }
            }

            return "NOTHING";
        }

        public static string ButtonPressedCamera(Rectangle buttonRec, Cam2D cam)
        {
            // Checks if the mouse is above the rectangle
            if (cam.ScreenToWorld(new Vector2(mouse.X, mouse.Y)).X > buttonRec.X && cam.ScreenToWorld(new Vector2(mouse.X, mouse.Y)).X < (buttonRec.X + buttonRec.Width))
            {
                if (cam.ScreenToWorld(new Vector2(mouse.X, mouse.Y)).Y > buttonRec.Y && cam.ScreenToWorld(new Vector2(mouse.X, mouse.Y)).Y < (buttonRec.Y + buttonRec.Height))
                {
                    // If the mouse is pressed
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        // If the mouse is being held down
                        if (mousePre.LeftButton != ButtonState.Pressed/* && !fade.Active*/)
                        {
                            //buttonClick.Play();
                            return "PRESSED";
                        }
                        else
                        {
                            return "HOLD";
                        }
                    }
                    else
                    {
                        return "HOVER";
                    }
                }
            }

            return "NOTHING";
        }

        public static double DistanceBetweenPoints(Vector2 vector, Vector2 vector2)
        {
            return Math.Sqrt(Math.Pow(vector.X - vector2.X, 2) + Math.Pow(vector.Y - vector2.Y, 2));
        }

        public void Zoom()
        {
            mouseWheel = mouse.ScrollWheelValue / 25;

            playerCam.Zoom((mouseWheel - mouseWheelPre) / 100);

            mouseWheelPre = mouseWheel;

        }
    }
}
