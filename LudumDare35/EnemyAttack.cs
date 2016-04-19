using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare35
{
    public class EnemyAttack
    {
        public float Direction;
        public Vector2 Velocity;
        public Vector2 Position;
        public int Type;
        public Color AttackColor;
        public Texture2D Texture;

        public EnemyAttack(Color color, int type, Vector2 position, Vector2 velocity)
        {
            AttackColor = color;
            Type = type;
            Position = position;
            Velocity = velocity;

            switch (type)
            {
                case 1:
                    Texture = TextureManager.Texture("SquareBullet");
                    break;

                case 2:
                    Texture = TextureManager.Texture("CircleBullet");
                    break;

                case 3:
                    Texture = TextureManager.Texture("DiamondBullet");
                    break;

                case 4:
                    Texture = TextureManager.Texture("TriangleBullet");
                    break;

                case 5:
                    Texture = TextureManager.Texture("StarBullet");
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Texture,
                Position,
                null,
                AttackColor,
                0f,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                1.5f,
                SpriteEffects.None,
                0f);
        }
    }
}
