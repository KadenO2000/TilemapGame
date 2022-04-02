using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace TilemapGame
{
    public class Tilemap
    {
        private int tileWidth, tileHeight, mapWidth, mapHeight;

        private Texture2D tilesetTexture;

        private Rectangle[] tiles;

        private int[] map;

        private string filename;

        public Tilemap(string file)
        {
            filename = file;
        }

        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, filename));
            var lines = data.Split('\n');

            var tilesetFilename = lines[0].Trim();
            tilesetTexture = content.Load<Texture2D>(tilesetFilename);

            var secondLine = lines[1].Split(',');
            tileWidth = int.Parse(secondLine[0]);
            tileHeight = int.Parse(secondLine[1]);

            int tilesetColumns = tilesetTexture.Width / tileWidth;
            int tilesetRows = tilesetTexture.Height / tileHeight;
            tiles = new Rectangle[tilesetColumns * tilesetRows];

            for(int y=0; y< tilesetColumns; y++)
            {
                for(int x = 0; x< tilesetRows; x++)
                {
                    int index = y * tilesetColumns + x;
                    tiles[index] = new Rectangle(x * tileWidth,y * tileHeight, tileWidth, tileHeight);
                }
            }

            var thirdLine = lines[2].Split(',');
            mapWidth = int.Parse(thirdLine[0]);
            mapHeight = int.Parse(thirdLine[1]);

            var fourthLine = lines[3].Split(',');
            map = new int[mapWidth * mapHeight];
            for(int i = 0; i < mapWidth * mapHeight; i++)
            {
                map[i] = int.Parse(fourthLine[i]);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                for(int x = 0; x < mapWidth; x++)
                {
                    int index = map[y * mapWidth + x] - 1;
                    if (index == -1) continue;
                    spriteBatch.Draw(tilesetTexture, new Vector2(x * tileWidth, y * tileHeight), tiles[index], Color.White);
                }
            }
        }
    }
}
