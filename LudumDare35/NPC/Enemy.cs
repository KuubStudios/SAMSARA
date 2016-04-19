using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glide;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumDare35
{
    public class Enemy : Entity
    {
        public List<EnemyAttack> Bullets;
        private float currentTime;
        private float attackCounter = 1;
        private float attackDuration = 5f;

        public Texture2D BeamMiddle;
        public bool Attacking = false;
        public float BeamScale = 0f;
        public float BeamEndPos;
        public int Health = 100;
        public Tweener tween = new Tweener();
        Color attkColour = Color.White;
        public int Wave = 0;
        public float WaveDuration = 5f;
        public float WaveCounter;
        public float AttackRate = 1f;
        public float AttackCounter;

        public Enemy(Vector2 position)
        {
            Texture = TextureManager.Texture("Boss");
            Scale = 0.5f;
            Position = position;
            Bullets = new List<EnemyAttack>();
            BeamMiddle = TextureManager.Texture("BeamMiddle");
        }

        public void Regen()
        {
            tween.Tween(this, new { Health = 100f }, 5.5f).Ease(Ease.ExpoIn);
        }

        public Color randColor()
        {
            Random rng = new Random();

            switch (rng.Next(1, 4))
            {
                case 1:
                    return Color.Blue;
                    break;

                case 2:
                    return Color.Yellow;
                    break;

                case 3:
                    return Color.Green;
                    break;

                case 4:
                    return Color.Red;
                    break;
            }

            return Color.White;
        }

        public void NewWave(int wave)
        {
            Random rng = new Random();

            switch (wave)
            {
                case 1:
                    Bullets.Add(new EnemyAttack(Color.Red, 1, Position, new Vector2(0, 2)));
                    AttackRate = 6f;
                    WaveDuration = 3f;
                    break;
                case 2:
                    Bullets.Add(new EnemyAttack(Color.Red, rng.Next(1, 2), Position, new Vector2(0, 2)));
                    AttackRate = 6f;
                    WaveDuration = 6f;
                    break;
                case 3:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 2), Position, new Vector2(0, 2)));
                    AttackRate = 8f;
                    WaveDuration = 10f;
                    break;
                case 4:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 4), Position, new Vector2(0, 2)));
                    AttackRate = 8f;
                    WaveDuration = 12f;
                    break;
                case 5:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 4), Position, new Vector2(0, 2)));
                    AttackRate = 3f;
                    WaveDuration = 12f;
                    break;
                case 6:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 5), Position, new Vector2(0, rng.Next(2, 3))));
                    AttackRate = 6f;
                    WaveDuration = 20f;
                    break;
                case 7:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 6), Position, new Vector2(0, rng.Next(4, 5))));
                    AttackRate = 6f;
                    WaveDuration = 24f;
                    break;
                case 8:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 4), Position, new Vector2(0, rng.Next(6, 7))));
                    AttackRate = 3f;
                    WaveDuration = 20f;
                    break;
                case 9:
                    Bullets.Add(new EnemyAttack(randColor(), rng.Next(1, 6), Position, new Vector2(0, rng.Next(7, 8))));
                    AttackRate = 6f;
                    WaveDuration = 24f;
                    break;
            }
        }

        public void Attack()
        {
            Random rng = new Random();

            switch (rng.Next(1, 4))
            {
                case 1:
                    attkColour = Color.Blue;
                    break;

                case 2:
                    attkColour = Color.Green;
                    break;

                case 3:
                    attkColour = Color.Yellow;
                    break;

                case 4:
                    attkColour = Color.Red;
                    break;
            }

            switch (rng.Next(1, 6))
            {
                case 1:
                    Bullets.Add(new EnemyAttack(attkColour, 1, Position, new Vector2(0, 2)));
                    break;

                case 2:
                    Bullets.Add(new EnemyAttack(attkColour, 2, Position, new Vector2(0, 2)));
                    break;

                case 3:
                    Bullets.Add(new EnemyAttack(attkColour, 3, Position, new Vector2(0, 2)));
                    break;

                case 5:
                    Bullets.Add(new EnemyAttack(attkColour, 4, Position, new Vector2(0, 2)));
                    break;

                case 6:
                    Bullets.Add(new EnemyAttack(attkColour, 5, Position, new Vector2(0, 2)));
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            tween.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            currentTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
            AttackCounter += (float) gameTime.ElapsedGameTime.TotalSeconds;
            WaveCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime >= attackDuration)
            {
                attackCounter++;
                currentTime -= attackDuration;

        //        if (Health < 100)
        //        tween.Tween(this, new { Health = Health + 10 }, 0.5f).Ease(Ease.ExpoIn);

                Attacking = true;
                tween.Tween(this, new { BeamScale = 20f }, 5f).Ease(Ease.ExpoOut);
                tween.Tween(this, new { BeamEndPos = 750f }, 6f).Ease(Ease.ExpoOut);

            }

            if (Attacking)
            {
                if (WaveCounter >= WaveDuration)
                {
                    Wave += 1;
                    WaveCounter = 0;
                }

                if (AttackCounter >= AttackRate)
                {
                    NewWave(Wave);
                    AttackCounter = 0;
                }
            }


            foreach (EnemyAttack bullet in Bullets)
            {
                bullet.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {

            base.Draw(batch);

            batch.Draw(TextureManager.Texture("BossHL"), new Vector2(Position.X, Position.Y), null,
                attkColour, 0f, new Vector2(TextureManager.Texture("BossHL").Width / 2, TextureManager.Texture("BossHL").Height / 2), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);

            if (Attacking)
            {
                batch.Draw(TextureManager.Texture("BeamStart"), new Vector2(Position.X - 140, Position.Y - 180), null,
                    Color.White, 0f, Vector2.Zero, new Vector2(2f, 1.3f), SpriteEffects.None, 0);

                batch.Draw(BeamMiddle, Position + new Vector2(-140, -50), null, Color.White, 0f, Vector2.Zero,
                    new Vector2(2f, BeamScale), SpriteEffects.None, 0);

                batch.Draw(TextureManager.Texture("BeamEnd"), new Vector2(Position.X - 140, BeamEndPos + Position.Y), null,
                    Color.White, 0f, Vector2.Zero, new Vector2(2f, 1.3f), SpriteEffects.None, 0);
            }

            foreach (EnemyAttack bullet in Bullets)
            {
                bullet.Draw(batch);
            }

        }
    }
}
