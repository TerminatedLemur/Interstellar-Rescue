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
    public class World
    {
        public int width;
        public int depth;

        private float richPercent;

        public MineBlock[,] worldTiles;

        public int backgroundProportionallity = 2;
        public Background[,] backgrounds;
        public List<Background> backgroundsAnim = new List<Background>();

        public List<Objects> objects = new List<Objects>();

        Light sun;

        public List<Enemy> enemies = new List<Enemy>();
        public int numOfEnemies = 5;

        public World(int Width, int Depth, float Riches)
        {
            width = Width;
            depth = Depth;

            richPercent = Riches;

            backgroundProportionallity = 2;
        }


        public void initialize()
        {
            worldTiles = new MineBlock[width, depth];
            backgrounds = new Background[width, depth];


            sun = new PointLight { Position = new Vector2((width* Game1.gridSize)/2, -10000), Scale = new Vector2(1000000, 32500), ShadowType = ShadowType.Occluded, CastsShadows = false, Intensity = 0.8f};
            Game1.penumbra.Lights.Add(sun);

            //MAKES THE LAYERS OF TERRAIN
            for (int depth = 0; depth < this.depth; depth++)
            {
                for(int width = 0; width < this.width; width++)
                {

                    if(depth > Game1.rand.Next(10, 20))
                    {
                        worldTiles[width, depth] = new MineBlock("STONE", new Vector2(width, depth));
                    }
                    else if(depth > Game1.rand.Next(2, 7))
                    {
                        worldTiles[width, depth] = new MineBlock("DIRT", new Vector2(width, depth));
                    }
                    else
                    {
                        worldTiles[width, depth] = new MineBlock("TOP DIRT", new Vector2(width, depth));
                    }


                }
            }

            //MAKES THE BACKGROUNDS
            for (int depth = 0; depth < this.depth; depth += backgroundProportionallity)
            {
                for (int width = 0; width < this.width; width += backgroundProportionallity)
                {

                    if (depth > Game1.rand.Next(10, 20))
                    {
                        backgrounds[width, depth] = new Background(new Vector2(width , depth ), "STONE", backgroundProportionallity);
                    }
                    else if (depth > 3)
                    {
                        backgrounds[width, depth] = new Background(new Vector2(width, depth), "DIRT", backgroundProportionallity);
                    }
                    else
                    {
                        backgrounds[width, depth] = new Background(new Vector2(width , depth ), "BLANK", backgroundProportionallity);
                    }
                }
            }
            for(int num = 0; num < Game1.rand.Next(50, 100); num ++)
            {
                int randW = Game1.rand.Next(0, width - 2);
                int randH = Game1.rand.Next(30, depth - 2);

                while (randW % 2 != 0)
                {
                    randW = Game1.rand.Next(0, width - 2);
                }
                while (randH % 2 != 0)
                {
                    randH = Game1.rand.Next(30, depth - 2);
                }


                backgroundsAnim.Add(new Background(new Vector2(randW, randH ), "EYES", backgroundProportionallity));
            }
            for (int num = 0; num < Game1.rand.Next(50, 100); num++)
            {
                int randW = Game1.rand.Next(0, width - 2);
                int randH = Game1.rand.Next(30, depth - 2);

                while (randW % 2 != 0)
                {
                    randW = Game1.rand.Next(0, width - 2);
                }
                while (randH % 2 != 0)
                {
                    randH = Game1.rand.Next(30, depth - 2);
                }

                backgroundsAnim.Add(new Background(new Vector2(randW, randH), "EYES SMALL", backgroundProportionallity));
            }


            GenerateRandomTerrain(depth, 2);

            //MAKES CAVES, ORES, HALLWAYS
            for (int numOfCaves = 0; numOfCaves <= Game1.rand.Next(6, 15); numOfCaves++)
            {
                CaveGeneration(Game1.rand.Next(10, width-10), Game1.rand.Next(40, depth-30), Game1.rand.Next(100, 5000));
            }

            for (int numOfVeins = 0; numOfVeins <= Game1.rand.Next(50, 75); numOfVeins++)
            {
                OreGeneration(Game1.rand.Next(10, width - 10), Game1.rand.Next(10, depth - 10), Game1.rand.Next(4, 17), "GOLD");
            }

            for (int numOfHalls = 0; numOfHalls <= Game1.rand.Next(1, 2); numOfHalls++)
            {
                GenerateHallWay(Game1.rand.Next((int)(depth * 0.75), depth - 5), Game1.rand.Next(0, width - 10), Game1.rand.Next(6, 10), Game1.rand.Next(3, 5), "HALLWAY");
            }            

            for (int depth = 0; depth < this.depth; depth++)
            {
                for (int width = 0; width < this.width; width++)
                {
                    worldTiles[width, depth].Update();                
                }
            }


            while (objects.Count < 7)
            {
                Vector2 loc = new Vector2(Game1.rand.Next(2, width - 2), Game1.rand.Next(40, depth - 4));

                if (GetBlock(loc).type == "BLANK" && GetBlock(new Vector2(loc.X - 1, loc.Y)).type == "BLANK" && GetBlock(new Vector2(loc.X, loc.Y + 1)).type == "BLANK" && GetBlock(new Vector2(loc.X + 1, loc.Y + 1)).type == "BLANK" && GetBlock(new Vector2(loc.X, loc.Y + 2)).type != "BLANK" && GetBlock(new Vector2(loc.X + 1, loc.Y + 2)).type != "BLANK")
                {
                    objects.Add(new Objects("CHEST", loc));
                }

            }

            while (objects.Count < 3)
            {
                Vector2 loc = new Vector2(Game1.rand.Next(2, width - 2), Game1.rand.Next(40, depth - 4));

                if (GetBlock(loc).type == "BLANK" && GetBlock(new Vector2(loc.X - 1, loc.Y)).type == "BLANK" && GetBlock(new Vector2(loc.X, loc.Y + 1)).type == "BLANK" && GetBlock(new Vector2(loc.X + 1, loc.Y + 1)).type == "BLANK" && GetBlock(new Vector2(loc.X, loc.Y + 2)).type != "BLANK" && GetBlock(new Vector2(loc.X + 1, loc.Y + 2)).type != "BLANK")
                {
                    objects.Add(new Objects("CHEST TRAP", loc));
                }

            }
            //backgroundsAnim.Add(new Background(new Vector2(1, 1), "EYES", backgroundProportionallity));
            //backgroundsAnim.Add(new Background(new Vector2(3, 1), "EYES SMALL", backgroundProportionallity));

            objects.Add(new Objects("CHEST", new Vector2(0,-1)));
            objects.Add(new Objects("CHEST TRAP", new Vector2(5, -1)));

            //enemies.Add(new Enemy("RAT", new Vector2(5, -1)));

            //Console.WriteLine(backgroundsAnim);
        }


        public void Update()
        {
            Player.grounded = false;

            if(Player.pos.Y > depth* Game1.gridSize * 0.05)
            {
                Game1.penumbra.Visible = true;
            }
            else
            {
                Game1.penumbra.Visible = false;
            }

            for (int depth = 0; depth < this.depth; depth++)
            {
                for (int width = 0; width < this.width; width++)
                {


                    if(Game1.DistanceBetweenPoints(Player.pos, new Vector2(worldTiles[width, depth].rec.X, worldTiles[width, depth].rec.Y)) < Game1.renderSize * 3)
                    {
                        worldTiles[width, depth].Update();
                    }
                  

                    //if(Game1.frames % 300 == 1)
                    //{
                    //    if (IsValidCaveCell(width, depth - 1))
                    //    {
                    //        worldTiles[width, depth].edges += "UP ";
                    //    }
                    //    if (IsValidCaveCell(width, depth + 1))
                    //    {
                    //        worldTiles[width, depth].edges += "DOWN ";
                    //    }
                    //    if (IsValidCaveCell(width - 1, depth))
                    //    {
                    //        worldTiles[width, depth].edges += "LEFT ";
                    //    }
                    //    if (IsValidCaveCell(width + 1, depth))
                    //    {
                    //        worldTiles[width, depth].edges += "RIGHT ";
                    //    }
                    //}
                    



                }
            }


            for (int i = 0; i < backgroundsAnim.Count; i += 1)
            {
                if(Game1.DistanceBetweenPoints(Player.pos, new Vector2(backgroundsAnim[i].rec.X, backgroundsAnim[i].rec.Y)) < Game1.renderSize)
                {
                    backgroundsAnim[i].animation.Update(Game1.gameTimePublic);
                }

            }

            for (int i = 0; i < objects.Count; i += 1)
            {
                if (Game1.DistanceBetweenPoints(Player.pos, new Vector2(objects[i].rec.X, objects[i].rec.Y)) < Game1.renderSize)
                {
                    objects[i].Update();
                }

            }



            while (enemies.Count < numOfEnemies)
            {
                Vector2 loc = new Vector2(Game1.rand.Next(1, width), Game1.rand.Next(40, depth - 1));

                int creature = Game1.rand.Next(0, 2);

                if (GetBlock(loc).type == "BLANK" /*&& Game1.DistanceBetweenPoints(Player.pos, loc) > 600*/)
                {
                    if(creature == 1)
                    {
                        enemies.Add(new Enemy("BAT", loc));
                    }
                    else
                    {
                        enemies.Add(new Enemy("RAT", loc));
                    }

                }

            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (Game1.DistanceBetweenPoints(Player.pos, new Vector2(enemies[i].rec.X, enemies[i].rec.Y)) < enemies[i].vision * 2)
                {
                    enemies[i].Update();
                }
                

                if(Game1.DistanceBetweenPoints(Player.pos, new Vector2(enemies[i].rec.X, enemies[i].rec.Y)) > Game1.renderSize * 3)
                {
                    enemies[i].dead = true;
                }

                if(enemies[i].dead)
                {
                    enemies.RemoveAt(i);
                }
            }

            
            
            for (int i = 0; i < ToolBelt.tools[2].bullets.Count; i++)
            {
                ToolBelt.tools[2].bullets[i].Update();


                if (ToolBelt.tools[2].bullets[i].collide)
                {
                    ToolBelt.tools[2].bullets.RemoveAt(i);
                }
            }

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int depth = 0; depth < this.depth; depth += backgroundProportionallity)
            {
                for (int width = 0; width < this.width; width += backgroundProportionallity)
                {

                    backgrounds[width, depth].Draw(spriteBatch);

                }
            }

            for (int i = 0; i < backgroundsAnim.Count; i += 1)
            {
                backgroundsAnim[i].Draw(spriteBatch);
            }
                      

            for (int depth = 0; depth < this.depth; depth++)
            {
                for (int width = 0; width < this.width; width++)
                {

                    worldTiles[width, depth].Draw(spriteBatch);

                }
            }

            for (int i = 0; i < objects.Count; i += 1)
            {
                objects[i].Draw(spriteBatch);
            }

            for (int torch = 0; torch < ToolBelt.tools[1].torchesAni.Count; torch++)
            {
                if(ToolBelt.tools[1].torchesDir[torch] == 1)
                {
                    ToolBelt.tools[1].torchesAni[torch].Draw(spriteBatch, Color.White, SpriteEffects.FlipHorizontally);
                }
                else
                {
                    ToolBelt.tools[1].torchesAni[torch].Draw(spriteBatch, Color.White, SpriteEffects.None);
                }

            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }


            if (ToolBelt.currentTool.type == "GUN")
            {
                for (int i = 0; i < ToolBelt.currentTool.bullets.Count; i++)
                {
                    ToolBelt.currentTool.bullets[i].Draw(spriteBatch);
                }

            }

        }

        public  MineBlock GetBlock(Vector2 loc)
        {
            try
            {
                return worldTiles[(int)loc.X, (int)loc.Y];
            }
            catch
            {
                return null;
            }
        }

        public void CaveGeneration(int startx, int starty, int cellsToRemove)
        {

            // pick a point on the surface as the cave entrance
            int caveX = startx;
            int caveY = starty;
            while (worldTiles[caveX, caveY].type == null)
            {
                caveY++;
            }
            // drill down until we are deep underground
            while (worldTiles[caveX - 1, caveY].type == null ||
                worldTiles[caveX + 1, caveY].type == null)
            {
                worldTiles[caveX, caveY].health = 0;
                caveY++;
            }

            // starting from the entrance, walk randomly within the terrain,
            // "carving out" the cave
            // make sure we do not create new entrances; the carving must not go
            // adjacent to an "open air" cell
            for (int i = 0; i < cellsToRemove; i++)
            {
                // "carve out" the current cell
                worldTiles[caveX, caveY].health = 0;

                // Get random direction:
                // 0: up
                // 1: right
                // 2: down
                // 3: left
                int dir = Game1.rand.Next(4);
                switch (dir)
                {
                    case 0: // up
                        if (IsValidCaveCell(caveX, caveY + 1))
                        {
                            caveY++;
                        }
                        break;
                    case 1: // right
                        if (IsValidCaveCell(caveX + 1, caveY))
                        {
                            caveX++;
                        }
                        break;
                    case 2: // down
                        if (IsValidCaveCell(caveX, caveY - 1))
                        {
                            caveY--;
                        }
                        break;
                    case 3: // left
                        if (IsValidCaveCell(caveX - 1, caveY))
                        {
                            caveX--;
                        }
                        break;
                }
            }
           
        }
        public  bool IsValidCaveCell(int x, int y)
        {
            // a cave cell is valid if:
            // - it doesn't exceed the world boundaries
            // - it isn't adjacent to an "open air" cell
            if (x < 0 || x >= width || y < 0 || y >= depth)
            {
                // out of world bounds
                return false;
            }
            for (int dx = -1; dx <= 1; dx++)
            {
                if (x + dx < 0 || x + dx >= width)
                {
                    continue;
                }
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (y + dy < 0 || y + dy >= depth)
                    {
                        continue;
                    }
                    if (worldTiles[x + dx, y + dy].type == null)
                    {
                        // adjacent to "open air" cell
                        return false;
                    }
                }
            }
            return true;
        }
        public void GenerateRandomTerrain(int depth, int variation)
        {
            for (int x = 0; x < width; x++)
            {
                int elevation = Game1.rand.Next(depth - variation, depth);
                for (int y = 0; y < depth; y++)
                {
                    if (y < depth - elevation)
                    {
                        // Open air
                        worldTiles[x,y].health = 0;
                    }                   
                }
            }
        }

        public void OreGeneration(int startx, int starty, int amount, string oreType)
        {

            // pick a point on the surface as the cave entrance
            int caveX = startx;
            int caveY = starty;
            while (worldTiles[caveX, caveY].type == null)
            {
                caveY++;
            }
            // drill down until we are deep underground
            while (worldTiles[caveX - 1, caveY].type == null ||
                worldTiles[caveX + 1, caveY].type == null)
            {
                worldTiles[caveX, caveY] = new MineBlock(oreType, new Vector2(caveX, caveY));
                caveY++;
            }

            // starting from the entrance, walk randomly within the terrain,
            // "carving out" the cave
            // make sure we do not create new entrances; the carving must not go
            // adjacent to an "open air" cell
            for (int i = 0; i < amount; i++)
            {
                // "carve out" the current cell
                Game1.penumbra.Hulls.Remove(worldTiles[caveX, caveY].hull);
                worldTiles[caveX, caveY] = new MineBlock(oreType, new Vector2(caveX, caveY));

                // Get random direction:
                // 0: up
                // 1: right
                // 2: down
                // 3: left
                int dir = Game1.rand.Next(4);
                switch (dir)
                {
                    case 0: // up
                        if (IsValidCaveCell(caveX, caveY + 1))
                        {
                            caveY++;
                        }
                        break;
                    case 1: // right
                        if (IsValidCaveCell(caveX + 1, caveY))
                        {
                            caveX++;
                        }
                        break;
                    case 2: // down
                        if (IsValidCaveCell(caveX, caveY - 1))
                        {
                            caveY--;
                        }
                        break;
                    case 3: // left
                        if (IsValidCaveCell(caveX - 1, caveY))
                        {
                            caveX--;
                        }
                        break;
                }
            }

        }

        public void GenerateHallWay(int depth, int xpos, int length, int height, string block)
        {
            for (int hallHeight = 0; hallHeight <= height; hallHeight++)
            {
                for (int hallLength = 0; hallLength <= length; hallLength++)
                {
                    if(worldTiles[xpos + hallLength, depth + hallHeight] != null)
                    {
                        worldTiles[xpos + hallLength, depth + hallHeight].health = 0;
                    }


                }
            }

            for (int hallLength = 0; hallLength <= length; hallLength++)
            {
                worldTiles[xpos + hallLength, depth].health = 0;
                worldTiles[xpos + hallLength, depth] = new MineBlock(block, new Vector2(xpos + hallLength, depth));

                worldTiles[xpos + hallLength, depth + height].health = 0;
                worldTiles[xpos + hallLength, depth + height] = new MineBlock(block, new Vector2(xpos + hallLength, depth + height));
            }

        }

        public bool IsNextBlank(int x, int y)
        {
            // a cave cell is valid if:
            // - it doesn't exceed the world boundaries
            // - it isn't adjacent to an "open air" cell
            if (x < 0 || x >= width || y < 0 || y >= depth)
            {
                // out of world bounds
                return false;
            }

            for (int dx = -1; dx <= 1; dx++)
            {
                if (x + dx < 0 || x + dx >= width)
                {
                    continue;
                }
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (y + dy < 0 || y + dy >= depth)
                    {
                        continue;
                    }
                    if (worldTiles[x + dx, y + dy].type == "BLANK")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
    
}
