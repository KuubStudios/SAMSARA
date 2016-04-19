using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glide;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;

namespace LudumDare35
{
    public class ShieldItem
    {
        public Color ShieldColor;
        public int Type;
        public float Scale;
        public Tweener tween = new Tweener();
        public float Rotation;
        private Random rng;
        private float offset;
        public float TimeAlive;

        public ShieldItem(Color color, int type)
        {
            ShieldColor = color;
            Type = type;
            rng = new Random();
            offset = MathHelper.ToRadians(rng.Next(0, 360));

            switch (type)
            {
                case 1:
                    TextureManager.SoundEffect("crystal0").Play();
                    break;

                case 2:
                    TextureManager.SoundEffect("crystal1").Play();
                    break;

                case 3:
                    TextureManager.SoundEffect("crystal2").Play();
                    break;

                case 4:
                    TextureManager.SoundEffect("crystal3").Play();
                    break;

                case 5:
                    TextureManager.SoundEffect("crystal4").Play();
                    break;

                case 6:
                    TextureManager.SoundEffect("crystal5").Play();
                    break;

                case 7:
                    var instance = TextureManager.SoundEffect("EasterSound").CreateInstance();
                    instance.Volume = 1f;
                    instance.Play();
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            TimeAlive += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Type != 7)
            {
                Rotation = offset + MathHelper.WrapAngle((float)gameTime.TotalGameTime.TotalMilliseconds * 0.0005f);
            }
            else
            {
                Rotation = 0f;
                Scale = 3f;
            }

            Console.WriteLine(TimeAlive);

            if (Math.Floor(TimeAlive) == 3f)
            {
                DespawnAnim();
            }
        }

        public void SpawnAnim()
        {
            Scale = 0.2f;
            tween.Tween(this, new { Scale = 1f }, 0.5f).Ease(Ease.ElasticInOut);
        }

        public void DespawnAnim()
        {
            Scale = 1f;
            tween.Tween(this, new { Scale = 0.0f }, 0.5f).Ease(Ease.ElasticInOut);
        }
    }
}
