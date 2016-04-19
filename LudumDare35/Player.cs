using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LudumDare35.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumDare35
{
    public class Player : Entity
    {
        public Shield shield;
        float offset;
        public Player()
        {
            Texture = TextureManager.Texture("SMHead");
            Scale = 0.3f;
            Position = new Vector2(-20, 335);
            shield = new Shield(Position);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            shield.CheckInput();
            shield.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            // body
            batch.Draw(TextureManager.Texture("SMBody"), Position + new Vector2(8, 145), null, Color.White, 0f, new Vector2(TextureManager.Texture("SMBody").Width / 2, TextureManager.Texture("SMBody").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);

            //left arm
            batch.Draw(TextureManager.Texture("SMUpperArm"), Position + new Vector2(-50, 110), null, Color.White, MathHelper.ToRadians(-25), new Vector2(TextureManager.Texture("SMUpperArm").Width / 2, TextureManager.Texture("SMUpperArm").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);
            batch.Draw(TextureManager.Texture("SMLowerArm"), Position + new Vector2(-10, 140), null, Color.White, MathHelper.ToRadians(-140), new Vector2(TextureManager.Texture("SMLowerArm").Width / 2, TextureManager.Texture("SMLowerArm").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);
            batch.Draw(TextureManager.Texture("SMGoodShit"), Position + new Vector2(15, 105), null, Color.White, MathHelper.ToRadians(75), new Vector2(TextureManager.Texture("SMGoodShit").Width / 2, TextureManager.Texture("SMGoodShit").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);

            //right arm
            batch.Draw(TextureManager.Texture("SMUpperArm"), Position + new Vector2(60, 110), null, Color.White, MathHelper.ToRadians(10), new Vector2(TextureManager.Texture("SMUpperArm").Width / 2, TextureManager.Texture("SMUpperArm").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);
            batch.Draw(TextureManager.Texture("SMLowerArm"), Position + new Vector2(33, 180), null, Color.White, MathHelper.ToRadians(60), new Vector2(TextureManager.Texture("SMLowerArm").Width / 2, TextureManager.Texture("SMLowerArm").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);
            batch.Draw(TextureManager.Texture("SMGoodShit"), Position + new Vector2(-10, 200), null, Color.White, MathHelper.ToRadians(-25), new Vector2(TextureManager.Texture("SMGoodShit").Width / 2, TextureManager.Texture("SMGoodShit").Height / 2), new Vector2(Scale, Scale), SpriteEffects.None, 0);

            shield.Draw(batch);
            batch.End();

            batch.Begin();
       //     batch.DrawString(TextureManager.Font("font"), "Last pressed key: " + shield.lastPressedKey.ToString(), Vector2.Zero, Color.White);
        //    batch.DrawString(TextureManager.Font("font"), "Current key combo: " + shield.currentCombo, new Vector2(0, 30), Color.White);
       //     batch.DrawString(TextureManager.Font("font"), "Current key: " + shield.currentKey, new Vector2(0, 60), Color.White);
       //     batch.DrawString(TextureManager.Font("font"), "Combo key: " + shield.comboKey, new Vector2(0, 90), Color.White);
         //   batch.DrawString(TextureManager.Font("font"), "Offset: " + offset, new Vector2(0, 120), Color.White);
            batch.End();
        }
    }
}
