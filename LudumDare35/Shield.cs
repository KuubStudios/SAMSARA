using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glide;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare35
{
    public class Shield
    {
        public Color ShieldColor;
        public int ShieldShape;
        public List<Keys[]> KeyCombos;
        public Keys lastPressedKey;
        public int currentKey = 0;
        public Keys[] currentCombo;
        public Keys comboKey;
        private KeyboardState oldState;
        public Vector2 Position;
        public int ActivatedShield = 0;
        public Color SelectedColor = Color.White;
        public List<ShieldItem> ActivatedShields;
        public ComboManager comboManager = new ComboManager();

        Tweener tween = new Tweener();
        public float scale = 0.1f;


        public Shield(Vector2 position)
        {
            comboManager.AddCombo(new[] { Keys.Down, Keys.Down }, () => AddShield(1));
            comboManager.AddCombo(new[] { Keys.Up, Keys.Up }, () => AddShield(2));
            comboManager.AddCombo(new[] { Keys.Left, Keys.Right, Keys.Up }, () => AddShield(3));
            comboManager.AddCombo(new[] { Keys.Right, Keys.Down, Keys.Left }, () => AddShield(4));
            comboManager.AddCombo(new[] { Keys.Left, Keys.Right, Keys.Left, Keys.Right }, () => AddShield(5));
            comboManager.AddCombo(new[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right }, () => AddShield(6));
            comboManager.AddCombo(new[] { Keys.H, Keys.Y, Keys.E, Keys.J, Keys.E, Keys.O, Keys.N, Keys.G }, () => AddShield(7));

            KeyCombos = new List<Keys[]>()
            {
                new Keys[3] { Keys.Up, Keys.Up, Keys.Up },
                new Keys[3] { Keys.Down, Keys.Down, Keys.Down },
                new Keys[3] { Keys.Left, Keys.Left, Keys.Left},
                new Keys[3] { Keys.Right, Keys.Right, Keys.Right}
            };

            comboManager.ComboReset += (sender, args) => {

            };

            ActivatedShields = new List<ShieldItem>();
            Position = position;
        }

        public void DrawShields(SpriteBatch batch)
        {
            Texture2D shieldTex = TextureManager.Texture("SquareShield");
            Texture2D shieldTexHL = TextureManager.Texture("SquareShieldHL");

                foreach (ShieldItem shield in ActivatedShields)
                {
                    if (shield.Type == 1)
                    {
                        shieldTex = TextureManager.Texture("SquareShield");
                        shieldTexHL = TextureManager.Texture("SquareShieldHL");
                    }

                    if (shield.Type == 4)
                    {
                        shieldTex = TextureManager.Texture("CircleShield");
                        shieldTexHL = TextureManager.Texture("CircleShieldHL");
                    }

                    if (shield.Type == 6)
                    {
                        shieldTex = TextureManager.Texture("StarShield");
                        shieldTexHL = TextureManager.Texture("StarShieldHL");
                    }

                    if (shield.Type == 3)
                    {
                        shieldTex = TextureManager.Texture("TriangleShield");
                        shieldTexHL = TextureManager.Texture("TriangleShieldHL");
                    }

                    if (shield.Type == 5)
                    {
                        shieldTex = TextureManager.Texture("HexShield");
                        shieldTexHL = TextureManager.Texture("HexShieldHL");
                    }

                    if (shield.Type == 2)
                    {
                        shieldTex = TextureManager.Texture("DiamondShield");
                        shieldTexHL = TextureManager.Texture("DiamondShieldHL");
                    }

                if (shield.Type == 7)
                {
                    shieldTex = TextureManager.Texture("DongShield");
                    shieldTexHL = TextureManager.Texture("DongShield");
                }

                batch.Draw(
                        shieldTexHL,
                        Position + new Vector2(0, -370),
                        null,
                        Color.White,
                        shield.Rotation,
                        new Vector2(shieldTexHL.Width/2,
                            shieldTexHL.Height/2),
                        shield.Scale,
                        SpriteEffects.None,
                        0f);

                    batch.Draw(
                        shieldTex,
                        Position + new Vector2(0, -370),
                        null,
                        shield.ShieldColor,
                        shield.Rotation,
                        new Vector2(shieldTex.Width/2,
                            shieldTex.Height/2),
                        shield.Scale,
                        SpriteEffects.None,
                        0f);
                }
            }

        public void Update(GameTime gameTime)
        {
            tween.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            comboManager.Update(gameTime);

            foreach (ShieldItem newShield in ActivatedShields.ToList())
            {
                newShield.tween.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                newShield.Update(gameTime);

                if (Math.Floor(newShield.TimeAlive) == 6f)
                {
                    currentCombo = new Keys[3];
                    currentKey = 0;

                    ActivatedShields.Remove(newShield);
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            DrawShields(batch);
        }

        public void AddShield(int type)
        {
            ShieldColor = SelectedColor;
            ShieldItem tempShield = new ShieldItem(this.ShieldColor, type);
            tempShield.Rotation += 10;
            ActivatedShields.Add(tempShield);
            tempShield.SpawnAnim();
        }

        public void CheckInput()
        {
            KeyboardState kState = Keyboard.GetState();

            Keys[] currentPressedKeys = kState.GetPressedKeys();

            foreach (Keys key in currentPressedKeys.ToList())
            {
                if (key == Keys.W)
                {
                    SelectedColor = Color.Blue;
                    currentPressedKeys.ToList().Remove(key);
                }

                if (key == Keys.S)
                {
                    SelectedColor = Color.Green;
                    currentPressedKeys.ToList().Remove(key);
                }

                if (key == Keys.A)
                {
                    SelectedColor = Color.Yellow;
                    currentPressedKeys.ToList().Remove(key);
                }

                if (key == Keys.D)
                {
                    SelectedColor = Color.Red;
                    currentPressedKeys.ToList().Remove(key);
                }


                if (kState.IsKeyDown(key) && oldState.IsKeyUp(key))
                {
                    lastPressedKey = key;

                    if (key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
                    {
                        foreach (Keys[] combo in KeyCombos)
                        {
                            
                        }
                        
                    }
                }
            }

            oldState = kState;
        }
    }
}
