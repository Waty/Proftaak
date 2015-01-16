using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProftaakGame
{
    public class Player : IGameObject
    {
        private const float Acceleration = 0.5F;
        private const float JumpSpeed = 7.5F;
        private readonly Map map;
        private readonly Vector2 maxVelocity = new Vector2(4, JumpSpeed);
        private readonly Vector2 scale = new Vector2(0.5F);
        private Vector2 position;
        private Vector2 velocity;

        public Player(Map map, Texture2D texture, Vector2 position, int lives)
        {
            Texture = texture;
            this.position = position;
            this.map = map;
            Lives = lives;
        }

        private IEnumerable<IGameObject> GameObjects
        {
            get { return map.GameObjects.Where(o => o != this); }
        }

        public int Lives { get; private set; }
        public int Coins { get; private set; }

        public bool Flying
        {
            get
            {
                var bounds = new Rectangle(Bounds.Left, Bounds.Bottom + 1, Bounds.Width, 1);
                return !map.GameObjects.Any(obj => bounds.Intersects(obj.Bounds));
            }
        }

        public Rectangle NewBounds
        {
            get
            {
                Vector2 newPos = Position + velocity;
                return new Rectangle
                {
                    X = (int) newPos.X,
                    Y = (int) newPos.Y,
                    Width = (int) (Texture.Width*scale.X),
                    Height = (int) (Texture.Height*scale.Y)
                };
            }
        }

        public bool IsDead
        {
            get { return Lives <= 0; }
        }

        public Texture2D Texture { get; private set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle
                {
                    X = (int) Position.X,
                    Y = (int) Position.Y,
                    Width = (int) (Texture.Width*scale.X),
                    Height = (int) (Texture.Height*scale.Y)
                };
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void Update()
        {
            ApplyFriction();
            HandleInput();
            ApplyGravity();

            LimitVelocity();
            Move();

            if (position.Y > map.Heigth)
            {
                Lives--;
                map.Reset();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effects = (velocity.X > 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(Texture, Position, effect: effects, scale: scale);
        }

        private void HandleInput()
        {
            //TODO: replace regular keys with info from the arduino
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                velocity.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.Space) && !Flying)
            {
                velocity.Y -= JumpSpeed;
            }
        }

        private void KillEnemy(Enemy enemy)
        {
            enemy.Lives--;
            Coins += 10;
        }

        #region Movement

        private void Move()
        {
            //Apply gravity
            float verticalSpeed = velocity.Y;
            while (verticalSpeed > 0)
            {
                if (!Flying)
                {
                    velocity.Y = 0;
                    break;
                }
                position.Y += Acceleration;
                verticalSpeed -= Acceleration;
            }
            while (verticalSpeed < 0)
            {
                position.Y -= Acceleration;
                verticalSpeed += Acceleration;
                if (HasCollisions)
                {
                    position.Y += Acceleration;
                    velocity.Y = 0;
                    break;
                }
            }

            float horizontalSpeed = velocity.X;
            while (horizontalSpeed < 0)
            {
                position.X -= Acceleration;
                horizontalSpeed += Acceleration;

                if (HasCollisions)
                {
                    position.X += Acceleration;
                    break;
                }
            }
            while (horizontalSpeed > 0)
            {
                position.X += Acceleration;
                horizontalSpeed -= Acceleration;

                if (HasCollisions)
                {
                    position.X -= Acceleration;
                    break;
                }
            }

            Debug.WriteLine(GameObjects.Count(obj => obj.Bounds.Intersects(Bounds)));
        }

        private bool HasCollisions
        {
            get { return GameObjects.Any(obj => obj.Bounds.Intersects(Bounds)); }
        }

        private void LimitVelocity()
        {
            if (velocity.X > maxVelocity.X)
            {
                velocity.X = maxVelocity.X;
            }
            if (velocity.X < maxVelocity.X*-1)
            {
                velocity.X = maxVelocity.X*-1;
            }


            if (velocity.Y > maxVelocity.Y)
            {
                velocity.Y = maxVelocity.Y;
            }
            if (velocity.Y < maxVelocity.Y*-1)
            {
                velocity.Y = maxVelocity.Y*-1;
            }
        }

        private void ApplyFriction()
        {
            if (velocity.X > 0)
            {
                velocity.X -= 0.5F;
            }
            if (velocity.X < 0)
            {
                velocity.X += 0.5F;
            }
        }

        private void ApplyGravity()
        {
            velocity.Y += Acceleration;
        }

        #endregion
    }
}