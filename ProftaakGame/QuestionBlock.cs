using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProftaakGame
{
    internal class QuestionBlock : Block
    {
        private int counter;
        private int imageIndex;
        public QuestionBlock(Texture2D texture2D, Vector2 position) : base(texture2D, position) {}

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, new Rectangle(imageIndex*16, 0, 16, 16), Color.White);
            if (++counter > 15)
            {
                counter = 0;
                if (++imageIndex > 3)
                {
                    imageIndex = 0;
                }
            }
        }
    }
}