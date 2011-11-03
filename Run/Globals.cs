using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Run
{
    static class globals
    {
        public static ContentManager _Content;
        public static SpriteBatch _SpriteBatch;
        public static GraphicsDeviceManager _Graphics;
        public static GraphicsDevice _GraphicsDevice;
        public static Dictionary<String, Sprite> _Sprites;
        public static Dictionary<String, SpriteFont> _SpriteFonts;
        public static Dictionary<String, FontSprite> _FontSprites;
        public static Dictionary<String, Menu> _Menus;
        public static List<Building> _Buildings;
        public static Dictionary<String, Effect> _Effects;
        public static Dictionary<String, MenuTheme> _Themes;
        public static RenderTarget2D ShaderRenderTarget;
        public static Texture2D ShaderTexture;
        public static Dictionary<String, SoundEffect> _Sounds;
        public static Dictionary<String, SoundEffectInstance> _SoundInstances;

        public static void Initialize()
        {


        }



        public static void Draw(GameTime gameTime)
        {

        }

        public static void Update(GameTime gameTime)
        {

        }

        public static Level CurrentLevel;

        public static void loadSprites()
        {
            

            _Menus = new Dictionary<string, Menu>();
            _Sprites = new Dictionary<string, Sprite>();
            _SpriteFonts = new Dictionary<string, SpriteFont>();
            _FontSprites = new Dictionary<string, FontSprite>();
            _Sounds = new Dictionary<string, SoundEffect>();
            _SoundInstances = new Dictionary<string, SoundEffectInstance>();
            _Effects = new Dictionary<string,Effect>();
            _Themes = new Dictionary<string, MenuTheme>();

            //Effects
            _Effects.Add("Glare", _Content.Load<Effect>("Glare"));

            //Sprite Fonts
            _SpriteFonts.Add("FIPPS", _Content.Load<SpriteFont>("Sprites/FIPPS"));
            _SpriteFonts.Add("M04B", _Content.Load<SpriteFont>("Sprites/mo4b"));
            _FontSprites.Add("LargeGradient", new FontSprite("LargeGradientFont"));

            //Load Sprites
            _Sprites.Add("Sean", new Sprite("Sprites/Sean", 50, 50));
            _Sprites.Add("Building0", new Sprite("Sprites/Buidling", 600, 600));
            _Sprites.Add("Building1", new Sprite("Sprites/Buidling2", 800, 1200));
            _Sprites.Add("Building2", new Sprite("Sprites/Buidling3", 1000, 1000));
            _Sprites.Add("Zombie", new Sprite("Sprites/Zombie", 50, 50));
            _Sprites.Add("Meteor", new Sprite("Sprites/Meteor", 192, 192));
            _Sprites.Add("Horse", new Sprite("Sprites/Horse", 112, 70));
            _Sprites.Add("Missle", new Sprite("Sprites/Missle", 128, 12));
            _Sprites.Add("BuildingBlocks", new Sprite("Sprites/BuildingBlocks", 32, 32));
            _Sprites.Add("GlassFragment", new Sprite("Sprites/GlassFragment", 54, 57));
            _Sprites.Add("BlockFragment", new Sprite("Sprites/BuildingFragment", 4, 4));
            _Sprites.Add("Bird", new Sprite("Sprites/Bird", 16, 16));
            _Sprites.Add("Rectangle", new Sprite("Sprites/Rectangle", 1, 1));
            _Sprites.Add("Toonie", new Sprite("Sprites/Toonie", 16, 16));
            _Sprites.Add("Loonie", new Sprite("Sprites/Loonie", 16, 16));
            _Sprites.Add("LargeGradientFont", new Sprite("Sprites/LargeGradientFont", 60, 60));
            _Sprites.Add("UFO", new Sprite("Sprites/UFO", 600, 312));
            _Sprites.Add("Shot", new Sprite("Sprites/Shot", 400, 1));
            _Sprites.Add("Crane", new Sprite("Sprites/Crane", 1092, 1050));
            _Sprites.Add("Tower", new Sprite("Sprites/Tower", 164, 548));
            _Sprites.Add("WaterTower", new Sprite("Sprites/WaterTower", 313, 341));
            _Sprites.Add("BlackBar", new Sprite("Sprites/BlackBar", 1280, 192));
            _Sprites.Add("Sky1", new Sprite("Sprites/Sky", 1, 720));
            _Sprites.Add("Sky2", new Sprite("Sprites/Sky2", 1, 720));
            _Sprites.Add("Sky3", new Sprite("Sprites/Sky3", 1, 720));
            _Sprites.Add("Sky4", new Sprite("Sprites/Sky4", 1, 720));
            _Sprites.Add("Sky5", new Sprite("Sprites/Sky5", 1, 720));
            _Sprites.Add("Cloud0", new Sprite("Sprites/Cloud", 400, 200));
            _Sprites.Add("Cloud1", new Sprite("Sprites/Cloud2", 400, 200));
            _Sprites.Add("DoomWall", new Sprite("Sprites/Doom", 100,720));
            _Sprites.Add("StartBackground", new Sprite("Sprites/StartBackground", 1280, 720));
            _Sprites.Add("DeadScreen", new Sprite("Sprites/DeadScreen", 1280, 720));
            _Sprites.Add("LightPost", new Sprite("Sprites/LightPost", 50, 388));
            _Sprites.Add("Vehicle0", new Sprite("Sprites/vehicle0", 80, 47));
            _Sprites.Add("Vehicle1", new Sprite("Sprites/vehicle1", 101, 41));
            _Sprites.Add("Vehicle2", new Sprite("Sprites/vehicle2", 78, 33));
            addAlphabetSprite("LargeGradientFont");
            _Sprites.Add("CityScape", new Sprite("Sprites/CityScape", 2700, 945));
            _Sprites.Add("Bars", new Sprite("Sprites/Bars", 128, 16));
            _Sprites.Add("AttackShip", new Sprite("Sprites/attackship", 1230, 390));
            _Sprites.Add("PassengerPlane", new Sprite("Sprites/Plane", 150, 83));
            _Sprites.Add("SmallSmoke", new Sprite("Sprites/SmallSmoke", 24, 24));
            _Sprites.Add("FighterJet", new Sprite("Sprites/Jet", 384, 72));
            _Sprites.Add("JetStream", new Sprite("Sprites/JetStream", 48, 24));
            _Sprites.Add("DustCloud", new Sprite("Sprites/DustCloud", 48, 48));
            _Sprites.Add("MeteorSmoke", new Sprite("Sprites/MeteorSmoke", 64, 64));
            _Sprites.Add("Fire", new Sprite("Sprites/Fire", 8, 8));
            _Sprites.Add("JetBlast", new Sprite("Sprites/JetBlast", 24, 24));
            _Sprites.Add("Absorb", new Sprite("Sprites/Absorb", 11, 11));

            _Sprites["Loonie"].addSet("Spin", 0, 7, true, "");
            _Sprites["Toonie"].addSet("Spin", 0, 7, true, "");

            _Sprites["Bars"].addSet("0", 0, 0, true, "");
            _Sprites["Bars"].addSet("1", 1, 1, true, "");
            _Sprites["Bars"].addSet("2", 2, 2, true, "");
            _Sprites["Bars"].addSet("3", 3, 3, true, "");
            _Sprites["Bars"].addSet("4", 4, 4, true, "");


            _Sprites["DoomWall"].addSet("Doom", 0, 3, true, "");


            _Sprites["Bird"].addSet("Perch", 0, 0, true, "");
            _Sprites["Bird"].addSet("Fly", 1, 4, true, "");

            _Sprites["Sean"].addSet("Run", 12, 21, true, "");
            _Sprites["Sean"].addSet("Jump", 33, 38, false, "");
            _Sprites["Sean"].addSet("Fall", 38, 38, false, "");
            _Sprites["Sean"].addSet("Dead", 88, 88, false, "");

            _Sprites["Zombie"].addSet("Stand", 0, 3, true, "");
            _Sprites["Zombie"].addSet("Walk", 6, 11, true, "");
            _Sprites["Zombie"].addSet("Die", 12, 15, false, "");

            _Sprites["Meteor"].addSet("Fall", 0, 0, false, "");
            _Sprites["Meteor"].addSet("Land", 1, 1, false, "");

            _Sprites["Horse"].addSet("Run", 0, 5, true, "");

            _Sprites["BuildingBlocks"].addSet("BlockRightDown", 0, 0, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftRightDown", 1, 1, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftDown", 2, 2, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockDown", 3, 3, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockRight", 4, 4, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightDown", 5, 5, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpRightDown", 6, 6, false, "");
            _Sprites["BuildingBlocks"].addSet("Block", 7, 7, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftDown", 8, 8, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpDown", 9, 9, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftRight", 10, 10, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightDown2", 11, 11, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpRight", 12, 12, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRight", 13, 13, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeft", 14, 14, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUp", 15, 15, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeft", 16, 16, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightDown3", 17, 17, false, "");

            _Sprites["BuildingBlocks"].addSet("WindowRightDown", 18, 18, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftRightDown", 19, 19, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftDown", 20, 20, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowDown", 21, 21, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowRight", 22, 22, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightDown1", 23, 23, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpRightDown", 24, 24, false, "");
            _Sprites["BuildingBlocks"].addSet("Window", 25, 25, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftDown", 26, 26, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpDown", 27, 27, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftRight", 28, 28, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightDown", 29, 29, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpRight", 30, 30, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRight", 31, 31, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeft", 32, 32, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUp", 33, 33, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeft", 34, 34, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightDown3", 35, 35, false, "");

            _Sprites["BuildingBlocks"].addSet("MindowRightDown", 18, 18, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftRightDown", 19, 19, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftDown", 20, 20, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowDown", 21, 21, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowRight", 22, 22, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightDown1", 23, 23, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpRightDown", 24, 24, false, "");
            _Sprites["BuildingBlocks"].addSet("Mindow", 25, 25, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftDown", 26, 26, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpDown", 27, 27, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftRight", 28, 28, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightDown", 29, 29, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpRight", 30, 30, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRight", 31, 31, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeft", 32, 32, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUp", 33, 33, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeft", 34, 34, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightDown3", 35, 35, false, "");

            _Sprites["BuildingBlocks"].addSet("WindowRightDownBroke", 36, 36, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftRightDownBroke", 37, 37, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftDownBroke", 38, 38, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowDownBroke", 39, 39, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowRightBroke", 40, 40, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightDow191Broke", 41, 41, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpRightDownBroke", 42, 42, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowBroke", 43, 43, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftDownBroke", 44, 44, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpDownBroke", 45, 45, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftRightBroke", 46, 46, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightDownBroke", 47, 47, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpRightBroke", 48, 48, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightBroke", 49, 49, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftBroke", 50, 50, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpBroke", 51, 51, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowLeftBroke", 52, 52, false, "");
            _Sprites["BuildingBlocks"].addSet("WindowUpLeftRightDow213Broke", 53, 53, false, "");

            _Sprites["BuildingBlocks"].addSet("MindowRightDownBroke", 36, 36, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftRightDownBroke", 37, 37, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftDownBroke", 38, 38, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowDownBroke", 39, 39, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowRightBroke", 40, 40, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightDow191Broke", 41, 41, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpRightDownBroke", 42, 42, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowBroke", 43, 43, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftDownBroke", 44, 44, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpDownBroke", 45, 45, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftRightBroke", 46, 46, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightDownBroke", 47, 47, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpRightBroke", 48, 48, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightBroke", 49, 49, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftBroke", 50, 50, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpBroke", 51, 51, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowLeftBroke", 52, 52, false, "");
            _Sprites["BuildingBlocks"].addSet("MindowUpLeftRightDow213Broke", 53, 53, false, "");

            _Sprites["BuildingBlocks"].addSet("BlockRightDownBroke", 54, 54, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftRightDownBroke", 55, 55, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftDownBroke", 56, 56, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockDownBroke", 57, 57, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockRightBroke", 58, 58, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightDow1209roke", 59, 59, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpRightDownBroke", 60, 60, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockBroke", 61, 61, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftDownBroke", 62, 62, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpDownBroke", 63, 63, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftRightBroke", 64, 64, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightDownBroke", 65, 65, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpRightBroke", 66, 66, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightBroke", 67, 67, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftBroke", 68, 68, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpBroke", 69, 69, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockLeftBroke", 70, 70, false, "");
            _Sprites["BuildingBlocks"].addSet("BlockUpLeftRightDownBroke23", 71, 71, false, "");

            _Sprites["BuildingBlocks"].addSet("DestroyedLeft", 72, 72, false, "");
            _Sprites["BuildingBlocks"].addSet("DestroyedRight", 73, 73, false, "");
            _Sprites["BuildingBlocks"].addSet("GuideRight", 74, 74, false, "");
            _Sprites["BuildingBlocks"].addSet("Guide", 75, 75, false, "");
            _Sprites["BuildingBlocks"].addSet("GuideLeftRight", 75, 75, false, "");
            _Sprites["BuildingBlocks"].addSet("GuideLeft", 76, 76, false, "");
            _Sprites["BuildingBlocks"].addSet("Nothing", 77, 77, false, "");

            _Sprites["BuildingBlocks"].addSet("FreeRightDown", 78, 78, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeLeftRightDown", 79, 79, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeLeftDown", 80, 80, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeDown", 81, 81, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeRight", 82, 82, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpLeftRightDown", 83, 83, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpRightDown", 84, 84, false, "");
            _Sprites["BuildingBlocks"].addSet("Free", 85, 85, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpLeftDown", 86, 86, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpDown", 87, 87, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeLeftRight", 88, 88, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpLeftRightDow12", 89, 89, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpRight", 90, 90, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpLeftRight", 91, 91, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpLeft", 92, 92, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUp", 93, 93, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeLeft", 94, 94, false, "");
            _Sprites["BuildingBlocks"].addSet("FreeUpLeftRightDow01", 95, 95, false, "");

            _Sprites["BuildingBlocks"].addSet("TreeRightDown", 78, 78, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeLeftRightDown", 79, 79, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeLeftDown", 80, 80, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeDown", 81, 81, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeRight", 82, 82, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpLeftRightDown", 83, 83, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpRightDown", 84, 84, false, "");
            _Sprites["BuildingBlocks"].addSet("Tree", 85, 85, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpLeftDown", 86, 86, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpDown", 87, 87, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeLeftRight", 88, 88, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpLeftRightDow12", 89, 89, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpRight", 90, 90, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpLeftRight", 91, 91, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpLeft", 92, 92, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUp", 93, 93, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeLeft", 94, 94, false, "");
            _Sprites["BuildingBlocks"].addSet("TreeUpLeftRightDow01", 95, 95, false, "");

            //Initialize
            foreach (Sprite s in _Sprites.Values)
            {
                s.LoadContent();
            }

            //Make Themes
            _Themes.Add("MainMenu", new MenuTheme(Color.Yellow, Color.Green, Color.White, Color.DarkGray,"Gradient"));

            //Make Menus
            _Menus.Add("Start", new Menu("StartBackground", Color.Black, Vector2.Zero, new Vector2(1280, 720)));
            _Menus["Start"].AddButton("Story Mode", new Vector2(550, 400), "MainMenu");
            _Menus["Start"].AddButton("Options", new Vector2(550, 430), "MainMenu");
            _Menus["Start"].AddButton("Mods", new Vector2(550, 460), "MainMenu");
            _Menus["Start"].AddActiveFunctionalityToControl("Story Mode", new MenuDelegate(ApocalypseParkour.StartGame));
            _Menus["Start"].AddActiveFunctionalityToControl("Options", new MenuDelegate(ApocalypseParkour.OptionsActivate));
            _Menus["Start"].AddActiveFunctionalityToControl("Mods", new MenuDelegate(ApocalypseParkour.ModsActivate));
            _Menus["Start"].AddUpdateMethod(new MenuDelegate(ApocalypseParkour.InitController));
            _Menus["Start"].Active = true;
            _Menus["Start"].selected = true;

            _Menus.Add("Options", new Menu("", new Color(0,128,128,196), new Vector2(0, -480), new Vector2(1280, 480)));
            _Menus["Options"].ZipEffects(new Vector2(0, -480), new Vector2(0, 240), 60.0f);
            _Menus["Options"].AddButton("Difficulty:", new Vector2(200, 200), "MainMenu");
            _Menus["Options"].AddText("Difficulty", "Doomsday", new Vector2(350, 200), "MainMenu");
            _Menus["Options"].AddActiveFunctionalityToControl("Difficulty:", new MenuDelegate(ApocalypseParkour.IncreaseDifficulty));
            _Menus["Options"].AddRightActiveFunctionalityToControl("Difficulty:", new MenuDelegate(ApocalypseParkour.IncreaseDifficulty));
            _Menus["Options"].AddLeftActiveFunctionalityToControl("Difficulty:", new MenuDelegate(ApocalypseParkour.DecreaseDifficulty));
            _Menus["Options"].AddButton("BUGGLE BUGGLE", new Vector2(550, 230), "MainMenu");
            _Menus["Options"].AddCancel(ApocalypseParkour.OptionsCancel);

            _Menus.Add("Mods", new Menu("", new Color(0, 128, 128, 196), new Vector2(0, 1200), new Vector2(1280, 480)));
            _Menus["Mods"].ZipEffects(new Vector2(0, 1200), new Vector2(0, 240), 60.0f);
            _Menus["Mods"].AddToggle("Hats", new Vector2(550, 200), "MainMenu");
            _Menus["Mods"].AddToggle("Cannon", new Vector2(550, 230), "MainMenu");
            _Menus["Mods"].AddToggle("Gravity", new Vector2(550, 260), "MainMenu");
            _Menus["Mods"].AddToggle("Leisure Mode", new Vector2(550, 290), "MainMenu");
            _Menus["Mods"].AddToggle("Extreme Mode", new Vector2(550, 320), "MainMenu");
            _Menus["Mods"].AddToggle("Double Jump", new Vector2(550, 350), "MainMenu");
            _Menus["Mods"].AddToggle("God Mode", new Vector2(550, 380), "MainMenu");
            _Menus["Mods"].AddToggle("Horde", new Vector2(550, 410), "MainMenu");
            _Menus["Mods"].AddActiveFunctionalityToControl("Gravity", new MenuDelegate(ApocalypseParkour.ToggleGravity));
            _Menus["Mods"].AddActiveFunctionalityToControl("Leisure Mode", new MenuDelegate(ApocalypseParkour.ToggleLeisureMode));
            _Menus["Mods"].AddActiveFunctionalityToControl("Extreme Mode", new MenuDelegate(ApocalypseParkour.ToggleMultiHazard));
            _Menus["Mods"].AddActiveFunctionalityToControl("Double Jump", new MenuDelegate(ApocalypseParkour.ToggleDoubleJump));
            _Menus["Mods"].AddActiveFunctionalityToControl("God Mode", new MenuDelegate(ApocalypseParkour.ToggleGodMode));
            _Menus["Mods"].AddActiveFunctionalityToControl("Horde", new MenuDelegate(ApocalypseParkour.ToggleHorde));
            _Menus["Mods"].AddCancel(ApocalypseParkour.ModsCancel);

            _Menus.Add("Pause", new Menu("", new Color(0, 0, 0, 56), new Vector2(0, 0), new Vector2(1280, 720)));
            _Menus["Pause"].AddButton("Continue", new Vector2(200, 200), "MainMenu");
            _Menus["Pause"].AddButton("End", new Vector2(200, 230), "MainMenu");
            _Menus["Pause"].AddFontSpriteText("Pause", "Pause", new Vector2(550, 200),"LargeGradient", "MainMenu");
            _Menus["Pause"].AddActiveFunctionalityToControl("Continue", new MenuDelegate(ApocalypseParkour.UnPause));
            _Menus["Pause"].AddActiveFunctionalityToControl("End", new MenuDelegate(ApocalypseParkour.EndAttempt));

            _Menus.Add("Confirm", new Menu("", new Color(0, 0, 0, 255), new Vector2(550, 430), new Vector2(0, 0)));
            _Menus["Confirm"].AddButton("No", new Vector2(50, 50), "MainMenu");
            _Menus["Confirm"].AddButton("Yes", new Vector2(50, 80), "MainMenu");
            _Menus["Confirm"].AddText("Sure", "Are you sure?", new Vector2(350, 200), "MainMenu");
            _Menus["Confirm"].AddActiveFunctionalityToControl("No", new MenuDelegate(ApocalypseParkour.EndDeny));
            _Menus["Confirm"].AddActiveFunctionalityToControl("Yes", new MenuDelegate(ApocalypseParkour.EndCurrentGame));

            _Menus.Add("Dead", new Menu("DeadScreen", new Color(0, 0, 0, 255), new Vector2(0, 0), new Vector2(0, 0)));
            _Menus["Dead"].AddButton("Yes", new Vector2(50, 50), "MainMenu");
            _Menus["Dead"].AddButton("No", new Vector2(50, 80), "MainMenu");
            _Menus["Dead"].AddActiveFunctionalityToControl("No", new MenuDelegate(ApocalypseParkour.EndCurrentGame));
            _Menus["Dead"].AddActiveFunctionalityToControl("Yes", new MenuDelegate(ApocalypseParkour.RestartLevel));


            _Menus.Add("CreditsBack", new Menu("", new Color(0, 0, 0, 255), new Vector2(0, 0), new Vector2(0, 0)));

            _Menus.Add("Credits", new Menu("", new Color(0, 0, 0, 0), new Vector2(0, 900), new Vector2(0, 0)));
            _Menus["Credits"].AddFontSpriteText("Programmer", "Programmer", new Vector2(640, 50), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Shane Brandon", "Shane Brandon", new Vector2(640, 100), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Artist", "Artist", new Vector2(640, 200), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Travis Brandon", "Travis Brandon", new Vector2(640, 250), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Sound", "Sound", new Vector2(640, 350), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Stephen Hummel", "Stephen Hummel", new Vector2(640, 400), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Special Thanks", "Special Thanks", new Vector2(640, 500), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Rosemary Brandon", "Rosemary Brandon", new Vector2(640, 550), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Kyle Guillette", "Kyle Guillette", new Vector2(640, 600), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Iain Lawrence", "Iain Lawrence", new Vector2(640, 650), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Damon Farquar", "Damon Farquar", new Vector2(640, 700), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Chris Brousseau", "Chris Brousseau", new Vector2(640, 750), "LargeGradient", "MainMenu");
            _Menus["Credits"].AddFontSpriteText("Thanks For Playing", "Thanks For Playing", new Vector2(640, 1250), "LargeGradient", "MainMenu");
            _Menus["Credits"].ZipEffects(new Vector2(0, 900), new Vector2(0, -780), 0.5f);
            //Load Sounds
            _Sounds.Add("GlassShatter",_Content.Load<SoundEffect>("Sound/GlassShatter"));
            _Sounds.Add("BuildingHit", _Content.Load<SoundEffect>("Sound/BuildingHit"));
            _Sounds.Add("LaserCharge", _Content.Load<SoundEffect>("Sound/LaserCharge"));
            _Sounds.Add("LaserFire", _Content.Load<SoundEffect>("Sound/LaserFire"));
            _Sounds.Add("Footstep", _Content.Load<SoundEffect>("Sound/ConcreteFootstep"));
            _Sounds.Add("MetalFootstep", _Content.Load<SoundEffect>("Sound/MetalStep3"));
            _Sounds.Add("JetEngine", _Content.Load<SoundEffect>("Sound/JetEngine"));
            _Sounds.Add("MeteorImpact", _Content.Load<SoundEffect>("Sound/MeteorImpact"));
            _Sounds.Add("MeteorFall", _Content.Load<SoundEffect>("Sound/bombfall"));

            //Load Instances
            _SoundInstances.Add("JetEngine", _Sounds["JetEngine"].CreateInstance());

            
        }

        public static void playerSignIn(int playerId, string name)
        {

        }

        public static void addAlphabetSprite(string n)
        {
            _Sprites[n].addSet("a", 0, 0, false, "");
            _Sprites[n].addSet("b", 1, 1, false, "");
            _Sprites[n].addSet("c", 2, 2, false, "");
            _Sprites[n].addSet("d", 3, 3, false, "");
            _Sprites[n].addSet("e", 4, 4, false, "");
            _Sprites[n].addSet("f", 5, 5, false, "");
            _Sprites[n].addSet("g", 6, 6, false, "");
            _Sprites[n].addSet("h", 7, 7, false, "");
            _Sprites[n].addSet("i", 8, 8, false, "");
            _Sprites[n].addSet("j", 9, 9, false, "");
            _Sprites[n].addSet("k", 10, 10, false, "");
            _Sprites[n].addSet("l", 11, 11, false, "");
            _Sprites[n].addSet("m", 12, 12, false, "");
            _Sprites[n].addSet("n", 13, 13, false, "");
            _Sprites[n].addSet("o", 14, 14, false, "");
            _Sprites[n].addSet("p", 15, 15, false, "");
            _Sprites[n].addSet("q", 16, 16, false, "");
            _Sprites[n].addSet("r", 17, 17, false, "");
            _Sprites[n].addSet("s", 18, 18, false, "");
            _Sprites[n].addSet("t", 19, 19, false, "");
            _Sprites[n].addSet("u", 20, 20, false, "");
            _Sprites[n].addSet("v", 21, 21, false, "");
            _Sprites[n].addSet("w", 22, 22, false, "");
            _Sprites[n].addSet("x", 23, 23, false, "");
            _Sprites[n].addSet("y", 24, 24, false, "");
            _Sprites[n].addSet("z", 25, 25, false, "");
            _Sprites[n].addSet("A", 26, 26, false, "");
            _Sprites[n].addSet("B", 27, 27, false, "");
            _Sprites[n].addSet("C", 28, 28, false, "");
            _Sprites[n].addSet("D", 29, 29, false, "");
            _Sprites[n].addSet("E", 30, 30, false, "");
            _Sprites[n].addSet("F", 31, 31, false, "");
            _Sprites[n].addSet("G", 32, 32, false, "");
            _Sprites[n].addSet("H", 33, 33, false, "");
            _Sprites[n].addSet("I", 34, 34, false, "");
            _Sprites[n].addSet("J", 35, 35, false, "");
            _Sprites[n].addSet("K", 36, 36, false, "");
            _Sprites[n].addSet("L", 37, 37, false, "");
            _Sprites[n].addSet("M", 38, 38, false, "");
            _Sprites[n].addSet("N", 39, 39, false, "");
            _Sprites[n].addSet("O", 40, 40, false, "");
            _Sprites[n].addSet("P", 41, 41, false, "");
            _Sprites[n].addSet("Q", 42, 42, false, "");
            _Sprites[n].addSet("R", 43, 43, false, "");
            _Sprites[n].addSet("S", 44, 44, false, "");
            _Sprites[n].addSet("T", 45, 45, false, "");
            _Sprites[n].addSet("U", 46, 46, false, "");
            _Sprites[n].addSet("V", 47, 47, false, "");
            _Sprites[n].addSet("W", 48, 48, false, "");
            _Sprites[n].addSet("X", 49, 49, false, "");
            _Sprites[n].addSet("Y", 50, 50, false, "");
            _Sprites[n].addSet("Z", 51, 51, false, "");
            _Sprites[n].addSet("1", 52, 52, false, "");
            _Sprites[n].addSet("2", 53, 53, false, "");
            _Sprites[n].addSet("3", 54, 54, false, "");
            _Sprites[n].addSet("4", 55, 55, false, "");
            _Sprites[n].addSet("5", 56, 56, false, "");
            _Sprites[n].addSet("6", 57, 57, false, "");
            _Sprites[n].addSet("7", 58, 58, false, "");
            _Sprites[n].addSet("8", 59, 59, false, "");
            _Sprites[n].addSet("9", 60, 60, false, "");
            _Sprites[n].addSet("0", 61, 61, false, "");
            _Sprites[n].addSet("!", 62, 62, false, "");
            _Sprites[n].addSet("?", 63, 63, false, "");
            _Sprites[n].addSet(".", 64, 64, false, "");
            _Sprites[n].addSet(" ", 65, 65, false, "");
        }


        public static void DrawRectangle(GameTime gameTime, Vector2 pos, Vector2 size, Color color, bool asUI)
        {
            globals._Sprites["Rectangle"].Draw(gameTime, pos, SpriteEffects.None, 1.0f, color, 0, 1.0f, size, asUI);
        }

        public static void DrawSpriteStretched(String name, GameTime gameTime, Vector2 pos, Vector2 size, Color color, bool asUI)
        {
            globals._Sprites[name].Draw(gameTime, pos, SpriteEffects.None, 1.0f, color, 0, 1.0f, size, asUI);
        }

        public static void DrawDefaultSprite(GameTime gameTime,Vector2 pos, string SpriteName,Color col)
        {
            globals._Sprites[SpriteName].Draw(gameTime, pos, SpriteEffects.None, 1.0f, col, 0, 1.0f);
        }

        public static void DrawDefaultSprite(GameTime gameTime, Vector2 pos, string SpriteName)
        {
            globals._Sprites[SpriteName].Draw(gameTime, pos, SpriteEffects.None, 1.0f, Color.White, 0, 1.0f);
        }
    }
}
