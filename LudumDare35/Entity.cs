using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare35
{
    public class Entity
    {
        public Vector2 Velocity;
        public Vector2 Position;
        public Texture2D Texture;
        public float Direction;
        public float Scale;

        public Entity()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(
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
