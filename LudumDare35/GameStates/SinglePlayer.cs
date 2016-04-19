using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Glide;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LudumDare35.GameStates
{
    public class SinglePlayer : State.GameState
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        public Camera Cam;

        public List<Entity> Entities;
        public Enemy enemy;
        public Player player;

        public bool Playing = false;
        private KeyboardState oldKeyboardState;
        Tweener tween = new Tweener();
        public float CamZoom = 15f;
        public float CamY = -300;
        public float CamX = -20;
        private float enemyY;
        private float enemyX;
        public float logoAlpha = 1;
        public double startAlpha = 1;
        public float ShieldOpacity = 0f;

        public float MusicVolume = 0.1f;
        public float IntroOpacity = 1f;

        public List<BackgroundObject> bgObjects; 
        public SinglePlayer(ContentManager content, SpriteBatch batch, GraphicsDevice device, GameWindow window)
        {
            graphicsDevice = device;
            spriteBatch = batch;

            Cam = new Camera();
            Entities = new List<Entity>();

            TextureManager.Init();
            TextureManager.LoadContent(content);

            enemy = new Enemy(new Vector2(0, -3050));
            player = new Player();
            Entities.Add(enemy);
            Entities.Add(player);

            Cam.Zoom = 2f;
            Cam.Position = new Vector2(0, 300);
            enemyY = enemy.Position.Y;
            enemyX = enemy.Position.X;

            bgObjects = new List<BackgroundObject>();
            AddBGObject();

            MediaPlayer.Play(TextureManager.Song("Menu"));
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = MusicVolume;

            EventInput.Initialize(window);
            EventInput.KeyUp += (sender, args) => player.shield.comboManager.KeyPressed(args.KeyCode);

            tween.Tween(this, new { CamZoom = 1f }, 15f).Ease(Ease.LinearIn);
            tween.Tween(this, new { CamY = 300 }, 15f).Ease(Ease.LinearIn);
            tween.Tween(this, new { IntroOpacity = 0f }, 15f).Ease(Ease.LinearIn);
        }

        public void ShowTooltip()
        {
            
        }

        public void AddBGObject()
        {
            Texture2D tex = TextureManager.Texture("BGShip1");

            Random rng = new Random();
            int objVal = rng.Next(1, 6);
            int direction = 0;

            switch (objVal)
            {
                case 1:
                    tex = TextureManager.Texture("BGShip1");
                    direction = 0;
                    break;

                case 2:
                    tex = TextureManager.Texture("BGShip2");
                    direction = 0;
                    break;

                case 3:
                    tex = TextureManager.Texture("BGShip3");
                    direction = 0;
                    break;

                case 4:
                    tex = TextureManager.Texture("BGShip4");
                    direction = 1;
                    break;

                case 5:
                    tex = TextureManager.Texture("BGShip5");
                    direction = 1;
                    break;

                case 6:
                    tex = TextureManager.Texture("BGShip6");
                    direction = 1;
                    break;
            }

            BackgroundObject bgObj = new BackgroundObject(tex, direction);
            bgObjects.Add(bgObj);
        }

        public void DrawActiveShields(SpriteBatch batch)
        {
            Texture2D shieldTex = TextureManager.Texture("SquareShield");
            Texture2D shieldTexHL = TextureManager.Texture("SquareShieldHL");

            for (int i = 0; i < player.shield.ActivatedShields.Count; i++)
            {
                if (player.shield.ActivatedShields[i].Type == 1)
                {
                    shieldTex = TextureManager.Texture("SquareShield");
                    shieldTexHL = TextureManager.Texture("SquareShieldHL");
                }

                if (player.shield.ActivatedShields[i].Type == 4)
                {
                    shieldTex = TextureManager.Texture("CircleShield");
                    shieldTexHL = TextureManager.Texture("CircleShieldHL");
                }

                if (player.shield.ActivatedShields[i].Type == 6)
                {
                    shieldTex = TextureManager.Texture("StarShield");
                    shieldTexHL = TextureManager.Texture("StarShieldHL");
                }

                if (player.shield.ActivatedShields[i].Type == 3)
                {
                    shieldTex = TextureManager.Texture("TriangleShield");
                    shieldTexHL = TextureManager.Texture("TriangleShieldHL");
                }

                if (player.shield.ActivatedShields[i].Type == 2)
                {
                    shieldTex = TextureManager.Texture("DiamondShield");
                    shieldTexHL = TextureManager.Texture("DiamondShieldHL");
                }

                if (player.shield.ActivatedShields[i].Type == 5)
                {
                    shieldTex = TextureManager.Texture("HexShield");
                    shieldTexHL = TextureManager.Texture("HexShieldHL");
                }

                if (player.shield.ActivatedShields[i].Type == 7)
                {
                    shieldTex = TextureManager.Texture("DongShield");
                    shieldTexHL = TextureManager.Texture("DongShield");
                }

                batch.Draw(
                    shieldTexHL,
                    new Vector2(-1550, 300 - (i * 200)),
                    null,
                    Color.White,
                    0f,
                    new Vector2(TextureManager.Texture("SquareShield").Width/2,
                        TextureManager.Texture("SquareShield").Height/2),
                    0.1f,
                    SpriteEffects.None,
                    0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            MediaPlayer.Volume = MusicVolume;
            KeyboardState kState = Keyboard.GetState();
            tween.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            Cam.Zoom = CamZoom;
            Cam.Position.Y = CamY;
            Cam.Position.X = CamX;
            enemy.Position.Y = enemyY;
            enemy.Position.X = enemyX;

            foreach (BackgroundObject bgObj in bgObjects)
            {
                bgObj.Update(gameTime);
            }

            if (kState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                tween.Tween(this, new {CamZoom = 0.4f}, 3f).Ease(Ease.CircInOut);
                tween.Tween(this, new {CamY = -100f}, 3f).Ease(Ease.CircInOut);

                Playing = true;

                tween.Tween(this, new {enemyX = -20}, 10.5f).Ease(Ease.ElasticInOut);
                tween.Tween(this, new {enemyY = -690f}, 10.5f).Ease(Ease.ElasticInOut);

                tween.Tween(this, new {logoAlpha = 0f}, 10f).Ease(Ease.ExpoOut);
                tween.Tween(this, new { startAlpha = 0f }, 5f).Ease(Ease.ExpoOut);

                tween.Tween(this, new { ShieldOpacity = 1f }, 10f).Ease(Ease.ElasticInOut);

                tween.Tween(this, new { MusicVolume = 0f }, 10f).Ease(Ease.ExpoIn);

                MediaPlayer.Stop();
                MediaPlayer.Play(TextureManager.Song("Ingame"));
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = MusicVolume;

                tween.Tween(this, new { MusicVolume = 0.1f }, 10f).Ease(Ease.ExpoIn);
            }

            if (Playing)
            {
                foreach (Entity entity in Entities)
                {
                    entity.Update(gameTime);
                }

                foreach (EnemyAttack bullet in enemy.Bullets.ToList())
                {
                    bullet.Position.X = enemy.Position.X;
                    if (bullet.Position.Y > -150 && bullet.Position.Y < 300)
                    {
                        foreach (ShieldItem shield in player.shield.ActivatedShields)
                        {
                            if (bullet.AttackColor == shield.ShieldColor)
                            {
                                if (bullet.Type == shield.Type)
                                {
                                    enemy.Bullets.Remove(bullet);
                                    TextureManager.SoundEffect("BossDamage").Play();
                                    int newHealth = enemy.Health - 5;
                                    enemy.tween.Tween(enemy, new { Health = newHealth }, 1.5f).Ease(Ease.ExpoOut);
                                }
                            }
                        }
                    }
                }

                oldKeyboardState = kState;
                base.Update(gameTime);
            }
            else
            {
                float freq = 0.0005f;
                startAlpha = 0.5 * (1 + Math.Sin(2 * Math.PI * freq * gameTime.TotalGameTime.TotalMilliseconds));
            }
        }

        public void DrawButtonUI()
        {
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.W))
            spriteBatch.Draw(TextureManager.Texture("WKeyHL"), new Vector2(1600, -30), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
            spriteBatch.Draw(TextureManager.Texture("WKey"), new Vector2(1600, 0), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.S))
            spriteBatch.Draw(TextureManager.Texture("SKeyHL"), new Vector2(1600, 180), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
            spriteBatch.Draw(TextureManager.Texture("SKey"), new Vector2(1600, 150), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.A))
            spriteBatch.Draw(TextureManager.Texture("AKeyHL"), new Vector2(1450, 180), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
            spriteBatch.Draw(TextureManager.Texture("AKey"), new Vector2(1450, 150), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.D))
            spriteBatch.Draw(TextureManager.Texture("DKeyHL"), new Vector2(1750, 180), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
            spriteBatch.Draw(TextureManager.Texture("DKey"), new Vector2(1750, 150), null, Color.White, 0f, new Vector2(TextureManager.Texture("AKey").Width / 2, TextureManager.Texture("AKey").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.Up))
            spriteBatch.Draw(TextureManager.Texture("UpArrowHL"), new Vector2(1610, 380), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
            spriteBatch.Draw(TextureManager.Texture("UpArrow"), new Vector2(1610, 400), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.Down))
                spriteBatch.Draw(TextureManager.Texture("DownArrowHL"), new Vector2(1610, 530), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(TextureManager.Texture("DownArrow"), new Vector2(1610, 500), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.Left))
                spriteBatch.Draw(TextureManager.Texture("LeftArrowHL"), new Vector2(1520, 530), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(TextureManager.Texture("LeftArrow"), new Vector2(1520, 500), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);

            if (kState.IsKeyDown(Keys.Right))
                spriteBatch.Draw(TextureManager.Texture("RightArrowHL"), new Vector2(1700, 530), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(TextureManager.Texture("RightArrow"), new Vector2(1700, 500), null, Color.White, 0f, new Vector2(TextureManager.Texture("UpArrow").Width / 2, TextureManager.Texture("UpArrow").Height / 2), 0.5f, SpriteEffects.None, 0);

        }

        public override void Draw(GameTime gameTime, float interp)
        {
            graphicsDevice.Clear(new Color(1,5,30));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
            spriteBatch.Draw(TextureManager.Texture("Starfield"), new Vector2(0, 0), null, null, Vector2.Zero, 0f, new Vector2(1f, 1f), new Color(255, 255, 255));
            spriteBatch.Draw(TextureManager.Texture("Planet"), new Vector2(600, 0), null, Color.White, 0f, new Vector2(TextureManager.Texture("Planet").Width / 2, TextureManager.Texture("Planet").Height / 2), 1f, SpriteEffects.None, 0);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Cam.Transformation(graphicsDevice));

            foreach (BackgroundObject bgObj in bgObjects)
            {
                bgObj.Draw(spriteBatch);
            }

            Random rng = new Random();

            spriteBatch.Draw(TextureManager.Texture("Donghwa"), new Vector2(-20, -200), null, new Color(255, 255, 255, IntroOpacity), 0f, new Vector2(TextureManager.Texture("Donghwa").Width / 2, TextureManager.Texture("Donghwa").Height / 2), 0.15f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureManager.Texture("Splash1"), new Vector2(-20, -150), null, new Color(255, 255, 255, IntroOpacity), 0f, new Vector2(TextureManager.Texture("Splash1").Width / 2, TextureManager.Texture("Splash1").Height / 2), 0.15f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureManager.Texture("Splash2"), new Vector2(-20, -50), null, new Color(255, 255, 255, IntroOpacity), 0f, new Vector2(TextureManager.Texture("Splash2").Width / 2, TextureManager.Texture("Splash2").Height / 2), 0.15f, SpriteEffects.None, 0);

            float offset = (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds*0.005)*10;
            spriteBatch.Draw(TextureManager.Texture("Banner1"), new Vector2(-350 + offset * 1.5f, 450 + offset), null, Color.White, MathHelper.ToRadians(25), new Vector2(TextureManager.Texture("Banner1").Width / 2, TextureManager.Texture("Banner1").Height / 2), 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureManager.Texture("Banner2"), new Vector2(-450 + offset, 620 + offset * 0.07f), null, Color.White, MathHelper.ToRadians(5), new Vector2(TextureManager.Texture("Banner1").Width / 2, TextureManager.Texture("Banner1").Height / 2), 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureManager.Texture("Banner3"), new Vector2(340 + offset * 0.23f, 480 + offset * 0.001f), null, Color.White, MathHelper.ToRadians(-25), new Vector2(TextureManager.Texture("Banner1").Width / 2, TextureManager.Texture("Banner1").Height / 2), 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureManager.Texture("Banner4"), new Vector2(340 + offset * 0.006f, 620 + offset), null, Color.White, MathHelper.ToRadians(-5), new Vector2(TextureManager.Texture("Banner1").Width / 2, TextureManager.Texture("Banner1").Height / 2), 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(TextureManager.Texture("Ship"), new Vector2(-2500, -1500), null, Color.White);

            DrawButtonUI();

            int health = (int) Math.Floor(TextureManager.Texture("BossBar").Height*(enemy.Health/100f));
            spriteBatch.Draw(TextureManager.Texture("BossBar"), null, new Rectangle(-1900, -1000, TextureManager.Texture("BossBar").Width, health), new Rectangle(0, 0, TextureManager.Texture("BossBar").Width, health), new Vector2(0, 0), 0f, new Vector2(50f, 50f), Color.White);
            spriteBatch.Draw(TextureManager.Texture("ShieldBar"), new Vector2(-1550, -160), null, null, new Vector2(TextureManager.Texture("ShieldBar").Width / 2, TextureManager.Texture("ShieldBar").Height / 2), 0f, new Vector2(1, 1));
            DrawActiveShields(spriteBatch);

            spriteBatch.Draw(TextureManager.Texture("Cheat"), new Vector2(1600f, -700), null, Color.White, 0f, new Vector2(TextureManager.Texture("Cheat").Width / 2, TextureManager.Texture("Cheat").Height / 2), 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(TextureManager.Texture("ShieldFront"), new Vector2(-785, -820), null, new Color(player.shield.SelectedColor.R, player.shield.SelectedColor.G, player.shield.SelectedColor.B, ShieldOpacity), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureManager.Texture("ShieldFrontHL"), new Vector2(-785, -820), null, new Color(255, 255, 255, ShieldOpacity), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
            spriteBatch.Draw(TextureManager.Texture("Logo"), new Vector2(400, 50), null, null, Vector2.Zero, 0f, new Vector2(0.5f, 0.5f), new Color(255, 255, 255, logoAlpha));
            spriteBatch.DrawString(TextureManager.Font("font"), "PRESS [ENTER] TO BEGIN", new Vector2(650, 240), new Color(255, 255, 255, (float)startAlpha));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Cam.Transformation(graphicsDevice));
            foreach (Entity entity in Entities)
            {
                entity.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime, interp);
        }
    }
}
