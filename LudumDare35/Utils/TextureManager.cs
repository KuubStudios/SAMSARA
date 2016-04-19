using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace LudumDare35.Utils
{
    public class TextureManager
    {
        private static Dictionary<string, Texture2D> textures;
        private static Dictionary<string, SpriteFont> fonts;
        private static Dictionary<string, SoundEffect> sounds;
        private static Dictionary<string, Song> songs; 
        private static GraphicsDevice device;

        public static void Init()
        {
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            sounds = new Dictionary<string, SoundEffect>();
            songs = new Dictionary<string, Song>();
        }

        public static void LoadContent(ContentManager content)
        {
            LoadTextures(content);
            LoadFonts(content);
            LoadSounds(content);
            LoadSongs(content);
        }

        public static void LoadSongs(ContentManager content)
        {
            songs.Add("Menu", content.Load<Song>("Menu"));
            songs.Add("Ingame", content.Load<Song>("Ingame"));
        }

        public static void LoadSounds(ContentManager content)
        {
            sounds.Add("crystal0", content.Load<SoundEffect>("crystal0"));
            sounds.Add("crystal1", content.Load<SoundEffect>("crystal1"));
            sounds.Add("crystal2", content.Load<SoundEffect>("crystal2"));
            sounds.Add("crystal3", content.Load<SoundEffect>("crystal3"));
            sounds.Add("crystal4", content.Load<SoundEffect>("crystal4"));
            sounds.Add("crystal5", content.Load<SoundEffect>("crystal5"));

            sounds.Add("BossDamage", content.Load<SoundEffect>("BossDamage"));

            sounds.Add("EasterSound", content.Load<SoundEffect>("EasterSound"));
        }

        public static void LoadTextures(ContentManager content)
        {
            textures.Add("Hill", content.Load<Texture2D>("Hill"));
            textures.Add("Boss", content.Load<Texture2D>("Boss"));
            textures.Add("BossHL", content.Load<Texture2D>("BossHL"));
            textures.Add("Player", content.Load<Texture2D>("Player"));
            textures.Add("Ship", content.Load<Texture2D>("Ship"));

            textures.Add("Donghwa", content.Load<Texture2D>("Donghwa"));
            textures.Add("Splash1", content.Load<Texture2D>("Splash2"));
            textures.Add("Splash2", content.Load<Texture2D>("Splash3"));

            textures.Add("DongShield", content.Load<Texture2D>("Easter"));

            textures.Add("Cheat", content.Load<Texture2D>("Cheat"));

            textures.Add("SquareShield", content.Load<Texture2D>("Square"));
            textures.Add("SquareShieldHL", content.Load<Texture2D>("SquareHL"));

            textures.Add("CircleShield", content.Load<Texture2D>("Circle"));
            textures.Add("CircleShieldHL", content.Load<Texture2D>("CircleHL"));

            textures.Add("StarShield", content.Load<Texture2D>("Star"));
            textures.Add("StarShieldHL", content.Load<Texture2D>("StarHL"));

            textures.Add("TriangleShield", content.Load<Texture2D>("Triangle"));
            textures.Add("TriangleShieldHL", content.Load<Texture2D>("TriangleHL"));

            textures.Add("DiamondShield", content.Load<Texture2D>("Diamond"));
            textures.Add("DiamondShieldHL", content.Load<Texture2D>("DiamondHL"));

            textures.Add("HexShield", content.Load<Texture2D>("Hex"));
            textures.Add("HexShieldHL", content.Load<Texture2D>("HexHL"));

            textures.Add("SquareBullet", content.Load<Texture2D>("SquareBullet"));
            textures.Add("CircleBullet", content.Load<Texture2D>("CircleBullet"));
            textures.Add("StarBullet", content.Load<Texture2D>("StarBullet"));
            textures.Add("DiamondBullet", content.Load<Texture2D>("DiamondBullet"));
            textures.Add("TriangleBullet", content.Load<Texture2D>("TriangleBullet"));

            textures.Add("ShieldFront", content.Load<Texture2D>("ShieldFront"));
            textures.Add("ShieldFrontHL", content.Load<Texture2D>("ShieldFrontHL"));

            textures.Add("BeamMiddle", content.Load<Texture2D>("BeamMiddle"));
            textures.Add("BeamEnd", content.Load<Texture2D>("BeamEnd"));
            textures.Add("BeamStart", content.Load<Texture2D>("BeamStart"));

            textures.Add("BossBar", content.Load<Texture2D>("BossUI"));
            textures.Add("ShieldBar", content.Load<Texture2D>("ShieldUI"));

            textures.Add("Logo", content.Load<Texture2D>("Logo"));
            textures.Add("Starfield", content.Load<Texture2D>("Starfield"));

            textures.Add("WKey", content.Load<Texture2D>("WKey"));
            textures.Add("SKey", content.Load<Texture2D>("SKey"));
            textures.Add("AKey", content.Load<Texture2D>("AKey"));
            textures.Add("DKey", content.Load<Texture2D>("DKey"));

            textures.Add("WKeyHL", content.Load<Texture2D>("WKeyHL"));
            textures.Add("SKeyHL", content.Load<Texture2D>("SKeyHL"));
            textures.Add("AKeyHL", content.Load<Texture2D>("AKeyHL"));
            textures.Add("DKeyHL", content.Load<Texture2D>("DKeyHL"));

            textures.Add("UpArrow", content.Load<Texture2D>("UpArrow"));
            textures.Add("DownArrow", content.Load<Texture2D>("DownArrow"));
            textures.Add("LeftArrow", content.Load<Texture2D>("LeftArrow"));
            textures.Add("RightArrow", content.Load<Texture2D>("RightArrow"));

            textures.Add("UpArrowHL", content.Load<Texture2D>("UpArrowHL"));
            textures.Add("DownArrowHL", content.Load<Texture2D>("DownArrowHL"));
            textures.Add("LeftArrowHL", content.Load<Texture2D>("LeftArrowHL"));
            textures.Add("RightArrowHL", content.Load<Texture2D>("RightArrowHL"));

            textures.Add("Banner1", content.Load<Texture2D>("Banner1"));
            textures.Add("Banner2", content.Load<Texture2D>("Banner2"));
            textures.Add("Banner3", content.Load<Texture2D>("Banner3"));
            textures.Add("Banner4", content.Load<Texture2D>("Banner4"));

            textures.Add("Planet", content.Load<Texture2D>("Planet"));

            textures.Add("BGShip1", content.Load<Texture2D>("BGShip1"));
            textures.Add("BGShip2", content.Load<Texture2D>("BGShip2"));
            textures.Add("BGShip3", content.Load<Texture2D>("BGShip3"));
            textures.Add("BGShip4", content.Load<Texture2D>("BGShip4"));
            textures.Add("BGShip5", content.Load<Texture2D>("BGShip5"));
            textures.Add("BGShip6", content.Load<Texture2D>("BGShip6"));

            // Spacemonk Appendages
            textures.Add("SMHead", content.Load<Texture2D>("SMHead"));
            textures.Add("SMBody", content.Load<Texture2D>("SMBody"));
            textures.Add("SMGoodShit", content.Load<Texture2D>("Hand"));
            textures.Add("SMUpperArm", content.Load<Texture2D>("UpperArm"));
            textures.Add("SMLowerArm", content.Load<Texture2D>("LowerArm"));
        }

        public static void LoadFonts(ContentManager content)
        {
            fonts.Add("font", content.Load<SpriteFont>("font"));
        }

        public static Texture2D Texture(string key)
        {
            return textures[key];
        }

        public static SpriteFont Font(string key)
        {
            return fonts[key];
        }

        public static SoundEffect SoundEffect(string key)
        {
            return sounds[key];
        }

        public static Song Song(string key)
        {
            return songs[key];
        }

    }
}
