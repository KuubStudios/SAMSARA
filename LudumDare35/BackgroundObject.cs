using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare35
{
    public class BackgroundObject
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Velocity;
        public Texture2D Texture;
        public float Scale = 1f;
        public int Direction;
        public Random rng;
        public int Speed;
        public BackgroundObject(Texture2D texture, int direction)
        {
            rng = new Random();
            Speed = rng.Next(1, 5);

            Texture = texture;
            Direction = direction;
            Position.Y = rng.Next(-2000, 900);
        }

        public void Update(GameTime gameTime)
        {
            if (Direction == 0)
            {
                Position.X += Speed;
            }
            else
            {
                Position.X -= Speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                Scale,
                SpriteEffects.None,
                0f);
        }
    }
}
