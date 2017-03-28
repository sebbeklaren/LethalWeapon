using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LethalWeapon
{
    class LevelManager
    {
        Texture2D texture;
        Vector2 position;
        ContentManager content;
        Tiles[,] tiles;
        int tileSize;

        public LevelManager(ContentManager content)
        {
            this.content = content;
            List<string> lvlStrings = new List<string>();
            StreamReader streamReader = new StreamReader("Content/Map/map01.txt");
            texture = content.Load<Texture2D>(@"Tileset01");
            tileSize = 32;
            while(!streamReader.EndOfStream)
            {
                lvlStrings.Add(streamReader.ReadLine());
            }
            streamReader.Close();

            tiles = new Tiles[lvlStrings[0].Length, lvlStrings.Count];

            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (lvlStrings[j][i] == 'A')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(64, 0, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'B')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(0, 0, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'C')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(32, 0, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'F')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(0, 32, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'H')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(32, 32, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'K')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(96, 32, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'J')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(64, 32, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'E')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(128, 0, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '1')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(0, 64, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '2')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(32,64, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '3')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(64, 64, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '5')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(128, 64, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '6')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(128, 32, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '8')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(32, 96, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '9')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(64, 96, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '0')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(96, 96, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == '4')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(96, 64, 32, 32), true);
                    }
                    else if (lvlStrings[j][i] == 'X')
                    {
                        tiles[i, j] = new Tiles(texture, new Vector2(tileSize * i, tileSize * j), new Rectangle(0, 0, 0, 0), true);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].Draw(spriteBatch);
                    
                }
            }
        }

    }
}
