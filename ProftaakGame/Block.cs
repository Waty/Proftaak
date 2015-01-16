using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProftaakGame
{
    public class Block : IGameObject
    {
        public Block(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }

        public Rectangle Bounds
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height); }
        }

        public void Update() {}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}