using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProftaakGame
{
    public class Map
    {
        private readonly ContentManager content;
        private readonly string[] data;

        public Map(ContentManager content, string[] data)
        {
            this.content = content;
            this.data = data;
            Parse();
        }

        public List<IGameObject> GameObjects { get; private set; }
        public Player Player { get; private set; }
        public int Heigth { get; private set; }

        private void Parse()
        {
            Heigth = data.OrderBy(s => s.Length).First().Length*16;

            GameObjects = new List<IGameObject>();

            const int size = 16;
            for (int y = 0; y < data.Length; y++)
            {
                string str = data[y];
                for (int x = 0; x < str.Length; x++)
                {
                    var position = new Vector2(x*size, y*size);
                    switch (str[x])
                    {
                        case 'X':
                            GameObjects.Add(new Block(content.Load<Texture2D>("block.png"), position));
                            break;

                        case 'Y':
                            if (Player == null)
                            {
                                Player = new Player(this, content.Load<Texture2D>("mario.png"), position, 3);
                            }
                            else Player.Position = position;
                            GameObjects.Add(Player);
                            break;

                        case 'Z':
                            GameObjects.Add(new QuestionBlock(content.Load<Texture2D>("questionBlock.png"), position));
                            break;
                    }
                }
            }
        }

        public void Reset()
        {
            Parse();
        }
    }
}