using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Base : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AnimationFlyWeight anim;
        
        

        public Base()
        {
            Maths.Initialize();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            globals._Graphics = graphics;
            globals._Graphics.PreferMultiSampling = true;
            globals._Graphics.IsFullScreen = false;
            globals._Graphics.PreferredBackBufferHeight = 720;
            globals._Graphics.PreferredBackBufferWidth = 1280;
            globals._Graphics.SynchronizeWithVerticalRetrace = true;
            
            globals.Initialize();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            Camera.Initialize();
            ApocalypseParkour.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            globals._Content = Content;
            globals.loadSprites();

            GraphicsDevice.PresentationParameters.BackBufferFormat = SurfaceFormat.Color;
            GraphicsDevice.RenderState.DepthBufferEnable = true;
            GraphicsDevice.RenderState.DepthBufferWriteEnable = true;
            GraphicsDevice.RenderState.AlphaBlendEnable = true;
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            globals._SpriteBatch = spriteBatch;
            // TODO: use this.Content to load your game content here
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
           // MediaPlayer.Play(Content.Load<Song>("Music/Escape"));
            globals.ShaderRenderTarget = CloneRenderTarget(GraphicsDevice, 1);
            globals.ShaderTexture = new Texture2D(GraphicsDevice, globals.ShaderRenderTarget.Width, globals.ShaderRenderTarget.Height, 1, TextureUsage.None, globals.ShaderRenderTarget.Format);
            globals._GraphicsDevice = GraphicsDevice;
        }
        public static RenderTarget2D CloneRenderTarget(GraphicsDevice device,int numberLevels)
        {
            return new RenderTarget2D(device,
                device.PresentationParameters.BackBufferWidth,
                device.PresentationParameters.BackBufferHeight,
                numberLevels,
                device.DisplayMode.Format,
                device.PresentationParameters.MultiSampleType,
                device.PresentationParameters.MultiSampleQuality
            );
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ApocalypseParkour.updateActive(IsActive);
            ApocalypseParkour.Update(gameTime);
            // TODO: Add your update logic here
            Color blah = new Color(0, 78, 128);
            Vector4 colrrr = blah.ToVector4();

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            ApocalypseParkour.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
