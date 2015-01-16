using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProftaakGame
{
    public class Enemy : IGameObject
    {
        public Enemy(int lives, Texture2D texture)
        {
            Texture = texture;
            Lives = lives;
        }

        public int Lives { get; set; }
        public Texture2D Texture { get; private set; }

        public Rectangle Bounds
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height); }
        }

        public Vector2 Position { get; private set; }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}