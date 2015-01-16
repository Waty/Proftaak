using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProftaakGame
{
    public interface IGameObject
    {
        Texture2D Texture { get; }
        Rectangle Bounds { get; }
        Vector2 Position { get; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}