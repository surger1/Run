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
    class Building : Thing
    {
        public static List<List<String>> Patterns;
        public static List<String> LargeToppers;
        public List<BoundingBox> boundingBoxes;
        public List<ParticleEmitter> Emitters;
        public List<LiveThing> Birds;
        public List<LiveThing> Coins;
        public List<Thing> Toppers;
        public static RenderTarget2D BuildingTarget;
        public static List<Dictionary<string,Texture2D>> BuildingTextures;
        public static List<Texture2D> HighwayTextures;
        public bool peopleSpawned;
        public bool peopleSpawnAfter;
        

        public List<Zombie> zombies;
        public LiveThing Meteor;
        public bool launched;
        public bool crumbly;
        public bool crumbling;
        public Vector2 drawExtra;
        public LiveThing HorseMan;
        public LiveThing Missle;
        public Thing UFO;
        public bool Blasted;
        public bool ride;
        public int width;
        public int height;
        public bool windowsBroken;
        public bool buildingCracked;
        public bool invulnerable;
        public bool MeteorCrashed;
        public bool hitByHorseMan;
        public bool freeWay;
        public bool UpdateNeeded;
        public string CurrentState;
        public int selectedTex;

        public const int UFOFlash = 500;
        public const int UFOLast = 250;
        public bool BeginLaserSequence;
        public bool LaserSecondPhase;
        public int LaserSequenceProgress;

        bool disturbed;

        public int Width
        {
            get { return width * 32; }

        }

        public int Height
        {
            get { return height * 32; }

        }

        public Building()
        {
            boundingBoxes = new List<BoundingBox>();
            zombies = new List<Zombie>();
            crumbly = false;
            crumbling = false;
            Blasted = false;
            Emitters = new List<ParticleEmitter>() ;

        }

        public static void LoadPatterns()
        {
            Patterns = new List<List<string>>();
            LargeToppers = new List<string>();
            BuildingTextures = new List<Dictionary<string, Texture2D>>();
            HighwayTextures = new List<Texture2D>();

            List<string> a = new List<string>();
            a.Add("BBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            a.Add("BWWMMWWBWWMMWWBWWMMWWBWWMMWWB");
            a.Add("BWWMMWWBWWMMWWBWWMMWWBWWMMWWB");
       


            List<string> b = new List<string>();
            b.Add("BBBBBBBBBBBBBBBBBBBBBBBBB");
            b.Add("BWMWBWMWBWMWBWMWBWMWBWMWB");
            b.Add("BWMWBWMWBWMWBWMWBWMWBWMWB");
            b.Add("BWMWBWMWBWMWBWMWBWMWBWMWB");

            List<string> c = new List<string>();
            c.Add("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            c.Add("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            c.Add("BWWBWWBWWBWWBWWBWWBWWBWWBWWBWWB");
            c.Add("BWWBWWBWWBWWBWWBWWBWWBWWBWWBWWB");
            c.Add("BWWBWWBWWBWWBWWBWWBWWBWWBWWBWWB");


            List<string> d = new List<string>();
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("WWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMM");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");
            d.Add("MMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWWMMMMWWWW");


            Patterns.Add(a);
            Patterns.Add(b);
            Patterns.Add(c);
            Patterns.Add(d);


            LargeToppers.Add("Tower");
            LargeToppers.Add("WaterTower");
            int PatternNumber = 0;
            int pat = 0;
            foreach (List<String> s in Patterns)
            {
                
                int patHeight = 64;
                int min = s[0].Length;

                    List<AnimationFlyWeight> Animations = new List<AnimationFlyWeight>();
                    BuildingTextures.Add(new Dictionary<string, Texture2D>());
                    

                    int patWidth = s[0].Length;

                    
                    //boundingBoxes.Add(new BoundingBox(new Vector2(0, 0), new Vector2((float)(width * 32), (float)(height * 32))));
                    int patWidthPro = 0;
                    int patHeightPro = 0;



                    for (int y = 0; y < patHeight; ++y)
                    {
                        patWidthPro = 0;
                        int GlassRatio = 0;
                        int BlockRatio = 0;
                        for (int x = 0; x < patWidth; ++x)
                        {
                            Animations.Add(new AnimationFlyWeight("BuildingBlocks"));
                           /* if (y == 0 || x == 0 || x == patWidth - 1)
                            {
                                Animations[Animations.Count - 1].gotoAnim("Block", false);
                            }
                            else
                            {*/
                              

                                if (patHeightPro < Patterns[pat].Count && patWidthPro < Patterns[pat][0].Length)
                                {
                                    switch (Patterns[pat][patHeightPro][patWidthPro])
                                    {
                                        case 'W':
                                            Animations[Animations.Count - 1].gotoAnim("Window", false);
                                            GlassRatio += 0;
                                            break;
                                        case 'M':
                                            Animations[Animations.Count - 1].gotoAnim("Mindow", false);
                                            GlassRatio += 0;
                                            break;
                                        case 'B':
                                            Animations[Animations.Count - 1].gotoAnim("Block", false);
                                            BlockRatio += 0;
                                            break;
                                    }
                                    patWidthPro++;
                                }
                                else
                                {
                                    Animations[Animations.Count - 1].gotoAnim("Block", false);
                                    
                                }
                                if (patWidthPro >= Patterns[pat][0].Length)
                                {

                                    patWidthPro = 0;
                                }
                            //}


                        }
                        patWidthPro = 0;
                        patHeightPro++;
                        if (patHeightPro >= Patterns[pat].Count)
                        {
                            patHeightPro = 0;
                        }
                    }
                    
                    for (int i = 0; i < Animations.Count(); ++i)
                    {
                        if (((i - (i % patWidth)) / patWidth) > 0)
                        {
                            if (Animations[i - patWidth].CurrentAnimation[0] == Animations[i].CurrentAnimation[0])
                            {
                                Animations[i - patWidth].gotoAnim(Animations[i - patWidth].CurrentAnimation + "Down", false);
                                Animations[i].gotoAnim(Animations[i].CurrentAnimation + "Up", false);
                            }

                        }

                        if ((i % patWidth) - 1 >= 0)
                        {
                            if (Animations[i - 1].CurrentAnimation[0] == Animations[i].CurrentAnimation[0])
                            {
                                Animations[i - 1].gotoAnim(Animations[i - 1].CurrentAnimation + "Right", false);
                                Animations[i].gotoAnim(Animations[i].CurrentAnimation + "Left", false);
                            }
                        }
                    }

                    #region Normal
                    BuildingTarget = new RenderTarget2D(globals._GraphicsDevice, patWidth * 32, patHeight * 32, 1, SurfaceFormat.Color);
                    RenderTarget2D temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
                    DepthStencilBuffer oldStencil = globals._GraphicsDevice.DepthStencilBuffer;
                    globals._GraphicsDevice.SetRenderTarget(0, BuildingTarget);
                    DepthStencilBuffer stencil = new DepthStencilBuffer(globals._GraphicsDevice, patWidth * 32, patHeight * 32, globals._GraphicsDevice.DepthStencilBuffer.Format);
                    globals._GraphicsDevice.DepthStencilBuffer = stencil;
                    globals._GraphicsDevice.Clear(Color.TransparentBlack);
                    globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                                      SpriteSortMode.Immediate,
                                      SaveStateMode.None);
                    GameTime gameTime = new GameTime();
                    for (int i = 0; i < Animations.Count; ++i)
                    {
                        //myAnimationFlyWeight.gotoAnim(Animations[i], false);
                        Animations[i].Draw(gameTime, new Vector2((float)((i % patWidth) * 32.0), (float)(((float)(i - (i % patWidth)) / (float)(patWidth)) * 32.0)));

                    }
                    globals._SpriteBatch.End();
                    globals._GraphicsDevice.SetRenderTarget(0, temp);
                    globals._GraphicsDevice.DepthStencilBuffer = oldStencil;
                    Texture2D BuildingTex = BuildingTarget.GetTexture();


                    Color[] pixels = new Color[BuildingTex.Width * BuildingTex.Height];
                    BuildingTarget.GetTexture().GetData<Color>(pixels);


                    for (int i = 0; i < pixels.Length; i++)
                    {
                        if (pixels[i] != new Color(0,0,0,0))
                            pixels[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B, 255);
                    }

                    BuildingTex.SetData<Color>(pixels);

                    BuildingTextures[PatternNumber].Add("Normal",BuildingTex);
                    //BuildingTarget.Dispose();
#endregion
                    #region Windows Broken
                    BuildingTarget = new RenderTarget2D(globals._GraphicsDevice, patWidth * 32, patHeight * 32, 1, SurfaceFormat.Color);
                    temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
                    oldStencil = globals._GraphicsDevice.DepthStencilBuffer;
                    globals._GraphicsDevice.SetRenderTarget(0, BuildingTarget);
                    stencil = new DepthStencilBuffer(globals._GraphicsDevice, patWidth * 32, patHeight * 32, globals._GraphicsDevice.DepthStencilBuffer.Format);
                    globals._GraphicsDevice.DepthStencilBuffer = stencil;
                    globals._GraphicsDevice.Clear(Color.TransparentBlack);
                    globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                                      SpriteSortMode.Immediate,
                                      SaveStateMode.None);
                    for (int i = 0; i < Animations.Count; ++i)
                    {
                        if(Animations[i].CurrentAnimation.Contains("indow"))
                        {
                            Animations[i].gotoAnim(Animations[i].CurrentAnimation + "Broke", false);

                        }
                        Animations[i].Draw(gameTime, new Vector2((float)((i % patWidth) * 32.0), (float)(((float)(i - (i % patWidth)) / (float)(patWidth)) * 32.0)));
                        
                    }
                    globals._SpriteBatch.End();
                    globals._GraphicsDevice.SetRenderTarget(0, temp);
                    globals._GraphicsDevice.DepthStencilBuffer = oldStencil;
                    BuildingTex = BuildingTarget.GetTexture();


                    pixels = new Color[BuildingTex.Width * BuildingTex.Height];
                    BuildingTarget.GetTexture().GetData<Color>(pixels);


                    for (int i = 0; i < pixels.Length; i++)
                    {
                        if (pixels[i] != new Color(0, 0, 0, 0))
                            pixels[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B, 255);
                    }

                    BuildingTex.SetData<Color>(pixels);

                    BuildingTextures[PatternNumber].Add("WindowsBroken", BuildingTex);
                    //BuildingTarget.Dispose();
#endregion
                    #region AllBroken
                    BuildingTarget = new RenderTarget2D(globals._GraphicsDevice, patWidth * 32, patHeight * 32, 1, SurfaceFormat.Color);
                    temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
                    oldStencil = globals._GraphicsDevice.DepthStencilBuffer;
                    globals._GraphicsDevice.SetRenderTarget(0, BuildingTarget);
                    stencil = new DepthStencilBuffer(globals._GraphicsDevice, patWidth * 32, patHeight * 32, globals._GraphicsDevice.DepthStencilBuffer.Format);
                    globals._GraphicsDevice.DepthStencilBuffer = stencil;
                    globals._GraphicsDevice.Clear(Color.TransparentBlack);
                    globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                                      SpriteSortMode.Immediate,
                                      SaveStateMode.None);
                    for (int i = 0; i < Animations.Count; ++i)
                    {
                        if (Animations[i].CurrentAnimation.Contains("Block"))
                        {
                            Animations[i].gotoAnim(Animations[i].CurrentAnimation + "Broke", false);

                        }
                        Animations[i].Draw(gameTime, new Vector2((float)((i % patWidth) * 32.0), (float)(((float)(i - (i % patWidth)) / (float)(patWidth)) * 32.0)));

                    }
                    globals._SpriteBatch.End();
                    globals._GraphicsDevice.SetRenderTarget(0, temp);
                    globals._GraphicsDevice.DepthStencilBuffer = oldStencil;
                    BuildingTex = BuildingTarget.GetTexture();


                    pixels = new Color[BuildingTex.Width * BuildingTex.Height];
                    BuildingTarget.GetTexture().GetData<Color>(pixels);


                    for (int i = 0; i < pixels.Length; i++)
                    {
                        if (pixels[i] != new Color(0, 0, 0, 0))
                            pixels[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B, 255);
                    }

                    BuildingTex.SetData<Color>(pixels);

                    BuildingTextures[PatternNumber].Add("AllBroken", BuildingTex);
                    //BuildingTarget.Dispose();
                    #endregion
                    #region Blow Out Center All
                    BuildingTarget = new RenderTarget2D(globals._GraphicsDevice, patWidth * 32, patHeight * 32, 1, SurfaceFormat.Color);
                    temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
                    oldStencil = globals._GraphicsDevice.DepthStencilBuffer;
                    globals._GraphicsDevice.SetRenderTarget(0, BuildingTarget);
                    stencil = new DepthStencilBuffer(globals._GraphicsDevice, patWidth * 32, patHeight * 32, globals._GraphicsDevice.DepthStencilBuffer.Format);
                    globals._GraphicsDevice.DepthStencilBuffer = stencil;
                    globals._GraphicsDevice.Clear(Color.TransparentBlack);
                    globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                                      SpriteSortMode.Immediate,
                                      SaveStateMode.None);
                    int xx = (patWidth / 2) - 6;
                    int xWidth = 11;
                    if (patWidth % 2 == 1)
                    {
                        xx = (patWidth / 2) - 5;
                        xWidth = 10;
                    }

                    for (int i = xx; i < Animations.Count; i += patWidth)
                    {
                        for (int ii = 0; ii <= xWidth; ii++)
                        {
                            if (ii == 0)
                            {
                                Animations[i + ii].gotoAnim("DestroyedLeft", false);
                            }
                            else if (ii == xWidth)
                            {
                                Animations[i + ii].gotoAnim("DestroyedRight", false);
                            }
                            else
                            {
                                Animations[i + ii].gotoAnim("Nothing", false);
                            }
                        }
                    }

                    for (int i = 0; i < Animations.Count; ++i)
                    {
                        Animations[i].Draw(gameTime, new Vector2((float)((i % patWidth) * 32.0), (float)(((float)(i - (i % patWidth)) / (float)(patWidth)) * 32.0)));
                    }
                    globals._SpriteBatch.End();
                    globals._GraphicsDevice.SetRenderTarget(0, temp);
                    globals._GraphicsDevice.DepthStencilBuffer = oldStencil;
                    BuildingTex = BuildingTarget.GetTexture();


                    pixels = new Color[BuildingTex.Width * BuildingTex.Height];
                    BuildingTarget.GetTexture().GetData<Color>(pixels);


                    for (int i = 0; i < pixels.Length; i++)
                    {
                        if (pixels[i] != new Color(0, 0, 0, 0))
                            pixels[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B, 255);
                    }

                    BuildingTex.SetData<Color>(pixels);

                    BuildingTextures[PatternNumber].Add("CenterBlowOutAllBroken", BuildingTex);
                    //BuildingTarget.Dispose();
                    #endregion
                    #region Blow Out Center Windows
                    BuildingTarget = new RenderTarget2D(globals._GraphicsDevice, patWidth * 32, patHeight * 32, 1, SurfaceFormat.Color);
                    temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
                    oldStencil = globals._GraphicsDevice.DepthStencilBuffer;
                    globals._GraphicsDevice.SetRenderTarget(0, BuildingTarget);
                    stencil = new DepthStencilBuffer(globals._GraphicsDevice, patWidth * 32, patHeight * 32, globals._GraphicsDevice.DepthStencilBuffer.Format);
                    globals._GraphicsDevice.DepthStencilBuffer = stencil;
                    globals._GraphicsDevice.Clear(Color.TransparentBlack);
                    globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                                      SpriteSortMode.Immediate,
                                      SaveStateMode.None);


                    for (int i = 0; i < Animations.Count; ++i)
                    {
                        if (Animations[i].CurrentAnimation.Contains("Block"))
                        {
                            Animations[i].gotoAnim(Animations[i].CurrentAnimation.Replace("Broke", ""), false);

                        }
                        Animations[i].Draw(gameTime, new Vector2((float)((i % patWidth) * 32.0), (float)(((float)(i - (i % patWidth)) / (float)(patWidth)) * 32.0)));
                    }
                    globals._SpriteBatch.End();
                    globals._GraphicsDevice.SetRenderTarget(0, temp);
                    globals._GraphicsDevice.DepthStencilBuffer = oldStencil;
                    BuildingTex = BuildingTarget.GetTexture();


                    pixels = new Color[BuildingTex.Width * BuildingTex.Height];
                    BuildingTarget.GetTexture().GetData<Color>(pixels);


                    for (int i = 0; i < pixels.Length; i++)
                    {
                        if (pixels[i] != new Color(0, 0, 0, 0))
                            pixels[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B, 255);
                    }

                    BuildingTex.SetData<Color>(pixels);

                    BuildingTextures[PatternNumber].Add("CenterBlowOutWindowsBroken", BuildingTex);
                    BuildingTarget.Dispose();
                    #endregion
                    PatternNumber++;
                    pat++;
            }
            for (int l = 10; l < 33; ++l)
            {
                List<AnimationFlyWeight> Animations = new List<AnimationFlyWeight>();
                int patWidth = l;
                int patHeight = 32;

                for (int y = 0; y < patHeight; ++y)
                {

                    for (int x = 0; x < patWidth; ++x)
                    {
                        Animations.Add(new AnimationFlyWeight("BuildingBlocks"));
                        if (y == 0)
                        {
                            Animations[Animations.Count - 1].gotoAnim("Guide", false);
                        }
                        else if (y == 1 || y == 2 || y == 3)
                        {
                            Animations[Animations.Count - 1].gotoAnim("Free", false);
                        }
                        else if (y == 4 || y == 5)
                        {
                            Animations[Animations.Count - 1].gotoAnim("Tree", false);
                        }
                        else
                        {
                            if (x % 7 == 0)
                            {
                                Animations[Animations.Count - 1].gotoAnim("Tree", false);
                            }
                            else
                            {
                                Animations[Animations.Count - 1].gotoAnim("Nothing", false);
                            }
                        }
                    }
                }
                for (int i = 0; i < Animations.Count(); ++i)
                {
                    if (Animations[i].CurrentAnimation != "Nothing")
                    {
                        if (((i - (i % patWidth)) / patWidth) > 0)
                        {
                            if (Animations[i - patWidth].CurrentAnimation[0] == Animations[i].CurrentAnimation[0])
                            {
                                Animations[i - patWidth].gotoAnim(Animations[i - patWidth].CurrentAnimation + "Down", false);
                                Animations[i].gotoAnim(Animations[i].CurrentAnimation + "Up", false);
                            }

                        }

                        if ((i % patWidth) - 1 >= 0)
                        {
                            if (Animations[i - 1].CurrentAnimation[0] == Animations[i].CurrentAnimation[0])
                            {
                                Animations[i - 1].gotoAnim(Animations[i - 1].CurrentAnimation + "Right", false);
                                Animations[i].gotoAnim(Animations[i].CurrentAnimation + "Left", false);
                            }
                        }
                    }
                }
                BuildingTarget = new RenderTarget2D(globals._GraphicsDevice, patWidth * 32, patHeight * 32, 1, SurfaceFormat.Color);
                RenderTarget2D temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
                DepthStencilBuffer oldStencil = globals._GraphicsDevice.DepthStencilBuffer;
                globals._GraphicsDevice.SetRenderTarget(0, BuildingTarget);
                DepthStencilBuffer stencil = new DepthStencilBuffer(globals._GraphicsDevice, patWidth * 32, patHeight * 32, globals._GraphicsDevice.DepthStencilBuffer.Format);
                globals._GraphicsDevice.DepthStencilBuffer = stencil;
                globals._GraphicsDevice.Clear(Color.TransparentBlack);
                globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                                  SpriteSortMode.Immediate,
                                  SaveStateMode.None);
                GameTime gameTime = new GameTime();
                for (int i = 0; i < Animations.Count; ++i)
                {
                    //myAnimationFlyWeight.gotoAnim(Animations[i], false);
                    Animations[i].Draw(gameTime, new Vector2((float)((i % patWidth) * 32.0), (float)(((float)(i - (i % patWidth)) / (float)(patWidth)) * 32.0)));

                }
                globals._SpriteBatch.End();
                globals._GraphicsDevice.SetRenderTarget(0, temp);
                globals._GraphicsDevice.DepthStencilBuffer = oldStencil;
                Texture2D BuildingTex = BuildingTarget.GetTexture();


                Color[] pixels = new Color[BuildingTex.Width * BuildingTex.Height];
                BuildingTarget.GetTexture().GetData<Color>(pixels);


                for (int i = 0; i < pixels.Length; i++)
                {
                    if (pixels[i] != new Color(0, 0, 0, 0))
                        pixels[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B, 255);
                }

                BuildingTex.SetData<Color>(pixels);

                HighwayTextures.Add(BuildingTex);
                BuildingTarget.Dispose();
            }
        }

        public Building(string spriteName, Vector2 pos, int mid)
        {
            boundingBoxes = new List<BoundingBox>();
            base.Initialize(spriteName, pos, mid);
            zombies = new List<Zombie>();
        }

        public Building(Vector2 pos, int mid)
        {
            boundingBoxes = new List<BoundingBox>();
            zombies = new List<Zombie>();
            Birds = new List<LiveThing>();
            Coins = new List<LiveThing>();
            Emitters = new List<ParticleEmitter>();
            Toppers = new List<Thing>();

            if (Maths.random.Next(30) == 1 && mid != 1)
            {
                base.Initialize("Crane",new Vector2(pos.X,pos.Y), mid);
                boundingBoxes.Add(new BoundingBox(new Vector2(0, 0), new Vector2(1070.0f, 64.0f)));
                invulnerable = true;

            }
            else
            {
                base.Initialize("BuildingBlocks", pos, mid);
                GenerateLook(mid);
            }

        }

        public void GenerateLook(int mid)
        {
            if (mid == 0)
            {
                selectedTex = Maths.random.Next(BuildingTextures.Count);
                width = BuildingTextures[selectedTex]["Normal"].Width / 32;
                height = BuildingTextures[selectedTex]["Normal"].Height / 32;
                Emitters.Add(new ParticleEmitter(position, new Vector2(width * 32.0f, height * 9.0f), 300, 0, 1, "GlassFragment", false));
                Emitters.Add(new ParticleEmitter(position, new Vector2(width * 32.0f, height * 9.0f), 100, 0, 1, "BlockFragment", false));
                Emitters.Add(new ParticleEmitter(position, new Vector2(100, 100), 250, 0, 1, "BlockFragment", false));
                Emitters.Add(new ParticleEmitter(Vector2.Zero, new Vector2(width * 32.0f, height * 9.0f), 75, 400, 10000, "DustCloud", false));
                Emitters.Add(new ParticleEmitter(position, new Vector2(100, 100), 250, 0, 1, "DustCloud", false));
                Emitters.Add(new ParticleEmitter(Vector2.Zero, new Vector2(width * 32.0f, height * 9.0f), 50, 100, 10000, "BlockFragment", false));
                boundingBoxes.Add(new BoundingBox(new Vector2(0, 0), new Vector2((float)(width * 32), (float)(height * 32))));
                CurrentState = "Normal";
                if (Maths.random.Next(2) == 0)
                {

                    int t = Maths.random.Next(LargeToppers.Count);
                    Toppers.Add(new Thing(LargeToppers[t], new Vector2(Maths.random.Next((width * 32) - globals._Sprites[LargeToppers[t]].Width), 0.0f), 0));
                }
                
            }
            else
            {

                selectedTex = Maths.random.Next(HighwayTextures.Count);
                width = HighwayTextures[selectedTex].Width / 32;
                height = HighwayTextures[selectedTex].Height / 32;
                boundingBoxes.Add(new BoundingBox(new Vector2(0, 32), new Vector2((float)(width * 32), (float)(3 * 32))));
                Emitters.Add(new ParticleEmitter(position, new Vector2(width * 32.0f, height * 9.0f), 300, 0, 1, "GlassFragment", false));
                Emitters.Add(new ParticleEmitter(position, new Vector2(width * 32.0f, height * 9.0f), 100, 0, 1, "BlockFragment", false));
                Emitters.Add(new ParticleEmitter(position, new Vector2(100, 100), 250, 0, 1, "BlockFragment", false));
                Emitters.Add(new ParticleEmitter(Vector2.Zero, new Vector2(width * 32.0f, height * 9.0f), 400, 100, 10000, "DustCloud", false));
                Emitters.Add(new ParticleEmitter(position, new Vector2(100, 100), 250, 0, 1, "DustCloud", false));
                Emitters.Add(new ParticleEmitter(Vector2.Zero, new Vector2(width * 32.0f, height * 9.0f), 50, 100, 10000, "BlockFragment", false));
                invulnerable = true;
                freeWay = true;
                for(int i = 0; i < width;i += 10)
                {
                    Toppers.Add(new Thing("LightPost", new Vector2(i * 32,globals._Sprites["LightPost"].Height + 100), 0));
                }
                for (int i = 0; i < width - 2; i += Maths.random.Next(2) + 1)
                {
                    Toppers.Add(new Thing("Vehicle" + (Maths.random.Next(3)).ToString(), new Vector2(i * 32, 0), 0));
                    i += 2;
                }
            }
        }

        public void DrawBefore(GameTime gameTime)
        {
            int yTopperAdjust = 0;
            if (freeWay)
            {
                yTopperAdjust = 32;
            }

            foreach (Thing t in Toppers)
            {
                t.MyAnimationFlyWeight.Draw(gameTime, new Vector2(t.Position.X + position.X, position.Y - globals._Sprites[t.MyAnimationFlyWeight.SpriteName].Height + yTopperAdjust) + drawExtra, SpriteEffects.None, 0.1f, Color.White, 1.0f);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!invulnerable || freeWay)
            {
                if (!freeWay)
                {
                    foreach (BoundingBox bb in boundingBoxes)
                    {
                        if (bb.position.Y >= 0)
                        {
                            globals.DrawRectangle(gameTime, bb.position + drawExtra + position, bb.dimensions, new Color(0.1f, 0.1f, 0.1f), false);
                            
                        }
                    }
                }
                else
                {
                    Camera.Draw(HighwayTextures[selectedTex], position + drawExtra, Color.White);
                }                
            }
            else
            {
                myAnimationFlyWeight.Draw(gameTime, position + new Vector2(-22,-155), SpriteEffects.None, 1.0f, Color.White, 1.0f);
            }
            if (CurrentState != null)
            {
                Camera.Draw(BuildingTextures[selectedTex][CurrentState], position + drawExtra, Color.White);
            }
            foreach (Thing t in zombies)
            {
                t.Draw(gameTime);
            }
            if (Meteor != null)
            {
                foreach (ParticleEmitter p in Meteor.Emitters)
                {
                    p.Draw(gameTime);
                }
                Meteor.MyAnimationFlyWeight.Draw(gameTime,Meteor.Position + drawExtra,SpriteEffects.None,1.0f,Color.White,1.0f);
                
            }
            if (HorseMan != null)
            {
                HorseMan.Draw(gameTime);
            }
            if (Missle != null  && Blasted)
            {
                Missle.Draw(gameTime);
            }
            
            foreach (ParticleEmitter pe in Emitters)
            {
                pe.Draw(gameTime);
            }
            foreach (LiveThing t in Birds)
            {
                t.Draw(gameTime);
            }
            if (UFO != null)
            {
                if (LaserSecondPhase && LaserSequenceProgress < UFOFlash + UFOLast)
                {
                    globals.DrawSpriteStretched("Shot", gameTime, new Vector2(UFO.Position.X + 100, UFO.Position.Y + 200), new Vector2(1, 10000), Color.White, false);
                }
                UFO.Draw(gameTime);
                
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            
            foreach (LiveThing lt in Coins)
            {
                lt.Update(gameTime);
            }
            foreach (Zombie t in zombies)
            {
                bool fall = true;
                foreach (BoundingBox b in boundingBoxes)
                {
                    if (Maths.regionInRegion(new Vector4(t.Position + new Vector2(25, 49), t.BBox.Y, 10), new Vector4(b.position + Position, b.dimensions.Y, b.dimensions.X)))
                    {
                        fall = false;
                    }
                }
                if (fall)
                {
                    t.falling = true;
                }
                t.Update(gameTime);
            }
            if (UFO != null)
            {
                UFO.Update(gameTime);
            }
                
            
            if (Meteor != null)
            {
                Meteor.Update(gameTime);
                if (Meteor.Position.Y + 100 >= Position.Y || (Meteor.Velocity.Y == 0.0f && Meteor.MyAnimationFlyWeight.CurrentAnimation == "Land"))
                {
                    Meteor.Velocity = new Vector2();
                    Meteor.Position = new Vector2(Meteor.Position.X, Position.Y - 100);
                    Meteor.MyAnimationFlyWeight.gotoAnim("Land", false);
                    Emitters[2].Position = new Vector2(Meteor.Position.X + 50, Meteor.Position.Y + 50) ;
                    Emitters[2].Activate();
                    Emitters[4].Position = new Vector2(Meteor.Position.X + 50, Meteor.Position.Y + 50);
                    Emitters[4].Activate();
                    BreakWindows(true);
                    Disturb();
                    if (!MeteorCrashed)
                    {
                        if (!ApocalypseParkour.GodMode)
                        {
                            boundingBoxes.Add(new BoundingBox(Meteor.Position - position + new Vector2(63, 60), new Vector2(64, 64)));
                        }
                        globals._Sounds["MeteorImpact"].Play();
                    }
                    MeteorCrashed = true;
                }
                
            }

            if (HorseMan != null)
            {
                HorseMan.Update(gameTime);
            }
            if (Missle != null)
            {
                Missle.Update(gameTime);
            }

            if (crumbling)
            {
                if (drawExtra.X == 5.0f)
                {
                    drawExtra.X = -5.0f;
                }
                else
                {
                    drawExtra.X = 5.0f;
                }
                Velocity += new Vector2(0, 0.02f);

                foreach (Zombie t in zombies)
                {
                    t.Velocity += new Vector2(0, 0.02f);
                }
            }
            base.Update(gameTime);
            for (int i = 0;i<Emitters.Count();++i)
            {
                if (i == 3 || i == 5)
                {
                    Emitters[i].Update(gameTime, position);
                }
                else
                {
                    Emitters[i].Update(gameTime, Vector2.Zero);
                }
            }
            foreach (LiveThing t in Birds)
            {
                t.Update(gameTime);
            }
            if (BeginLaserSequence && LaserSequenceProgress < UFOFlash + UFOLast)
            {
                LaserSequenceProgress += gameTime.ElapsedGameTime.Milliseconds;
                if (LaserSequenceProgress >= UFOFlash && !LaserSecondPhase)
                {
                    Camera.flash(UFOLast, new Color(Color.LightCyan, 50), false, false);
                    LaserSecondPhase = true;
                    BreakWindows(true);
                    if (!ApocalypseParkour.GodMode)
                    {
                        BlowOutCenter();
                    }
                    Camera.shake(UFOLast, 32.0f, 5.0f);
                    globals._Sounds["LaserFire"].Play();
                }
                if (LaserSecondPhase && LaserSequenceProgress >= UFOFlash + UFOLast)
                {
                    Camera.flash(50, Color.LightCyan, true, false);
                    
                }
            }
        }

        public void BlowOutCenter()
        {
            int xx = (width / 2) - 6;
            int xWidth = 11;
            if (width % 2 == 1)
            {
                xx = (width / 2) - 5;
                xWidth = 10;
            }
            
            /*for (int i = xx; i < Animations.Count; i += width)
            {
                for (int ii = 0; ii <= xWidth; ii++)
                {
                    if (ii == 0)
                    {
                        Animations[i + ii].gotoAnim(Animations[i + ii].CurrentAnimation + "DestroyedLeft", false);
                    }
                    else if (ii == xWidth)
                    {
                        Animations[i + ii].gotoAnim(Animations[i + ii].CurrentAnimation + "DestroyedRight", false);
                    }
                    else
                    {
                        Animations[i + ii].gotoAnim("Nothing", false);
                    }
                }
            }*/
            if (buildingCracked)
            {
                CurrentState = "CenterBlowOutAllBroken";
            }
            else
            {
                CurrentState = "CenterBlowOutWindowsBroken";
            }
            boundingBoxes.RemoveAt(0);// Add(new BoundingBox(new Vector2(0, 0), new Vector2((float)(width * 32), (float)(height * 32))));

            boundingBoxes.Add(new BoundingBox(new Vector2(0, 0), new Vector2((float)(xx * 32), (float)(height * 32))));
            boundingBoxes.Add(new BoundingBox(new Vector2(((xx + xWidth + 1) * 32), 0), new Vector2((float)((width - (xx + xWidth + 1)) * 32), (float)(height * 32))));
        }

        public bool ZombieCheck(Vector4 player, float playerVelocity,bool canKill,bool justCheck)
        {
            bool ret = false;
            foreach (Zombie t in zombies)
            {
                if (Maths.regionInRegion(new Vector4(t.Position + new Vector2(25, 49), 50, 10),player) && !t.falling)
                {
                    ret = true;
                    if (canKill)
                    {
                        if (!justCheck)
                        {
                            t.falling = true;
                            if (!ApocalypseParkour.GodMode)
                            {
                                t.Velocity += new Vector2(playerVelocity * 0.6f, Math.Abs(playerVelocity) * -0.4f);
                            }
                            else
                            {
                                t.Velocity += new Vector2(playerVelocity * 2.0f, Math.Abs(playerVelocity) * -1.0f);
                            }
                        }
                    }
                    else if(!justCheck)
                    {
                        if (t.eating)
                        {
                            ret = false;
                        }
                        else
                        {

                            t.eating = true;

                        }
                    }
                    
                }
            }
            return ret;
        }

        public bool HorseManCheck(Vector4 player, float playerVelocity)
        {
            bool ret = false;
            if(HorseMan != null && !hitByHorseMan)
            {
                if (Maths.regionInRegion(new Vector4(HorseMan.Position, 32, 64), player))
                {
                    ret = true;
                    //hitByHorseMan = true;
                }
            }
            return ret;
        }

        public void AddMeteor()
        {
            if (!invulnerable)
            {
                Meteor = new LiveThing("Meteor", new Vector2(position.X + Maths.random.Next((int)Width / 3) - 1320 + ((int)Width / 3), position.Y - 2000), 0);
                
            }
        }

        public void LaunchMeteor()
        {
            if (Meteor != null && !launched)
            {
                launched = true;
                Meteor.Velocity = new Vector2(115.0f, 175.0f);
                globals._Sounds["MeteorFall"].Play();
                Meteor.AddEmitter(new Vector2(48, 48), new Vector2(24, 24), 1, 50, 5000, "MeteorSmoke", true);
            }
        }

        public void AddHorseMan()
        {
            if (!invulnerable)
            {
                HorseMan = new LiveThing("Horse", new Vector2(position.X + 1000, position.Y - 70), 0);
            }
        }

        public void RideOn()
        {
            if (HorseMan != null && !ride)
            {
                ride = true;
                HorseMan.Velocity = new Vector2(-10.0f,0.0f);
                HorseMan.MyAnimationFlyWeight.gotoAnim("Run", false);
                HorseMan.MyAnimationFlyWeight.FrameSpeed = 0.12f;
            }
        }

        public void AddMissle()
        {
            Missle = new LiveThing("Missle", new Vector2(position.X  - 2000 - Maths.random.Next(1000), position.Y), 0);
        }

        public void LaunchMissle()
        {
            if (Missle != null && !Blasted)
            {
                Blasted = true;
                Missle.Velocity = new Vector2(20.0f, 0.0f);
            }
        }

        public void Crumble()
        {
            if (crumbly && !crumbling)
            {
                crumbling = true;
                Emitters[3].Activate();
                Emitters[5].Activate();
            }
            Disturb();
        }

        public void MakeCrumbly(bool explode)
        {
            if (!crumbly && !invulnerable)
            {
                crumbly = true;
                BreakWindows(explode);
                CrackBuilding(explode);

                
            }
            Disturb();
        }

        public void BreakWindows(bool explode)
        {
            if (explode)
            {
                Emitters[0].Activate();
            }
            if (!windowsBroken)
            {
                windowsBroken = true;
                if(!LaserSecondPhase && !buildingCracked)
                {
                    CurrentState = "WindowsBroken";
                }
                if (explode)
                {
                    globals._Sounds["GlassShatter"].Play();
                }
            }
        }

        public void CrackBuilding(bool explode)
        {
            if (explode)
            {
                Emitters[1].Activate();
            }
            if (!buildingCracked)
            {
                if (LaserSecondPhase)
                {
                    CurrentState = "CenterBlowOutAllBroken";
                }
                else
                {
                    CurrentState = "AllBroken";
                }
            }
        }

        public void Flock()
        {
            if (!disturbed)
            {
                float add = 0;
                while (add < width * 32)
                {
                    Birds.Add(new LiveThing("Bird", new Vector2(position.X + add, position.Y - 10), 0));
                    add += Maths.random.Next(32) + 16;
                }
            }
        }

        public void Disturb()
        {
            if (!disturbed)
            {
                foreach (LiveThing lt in Birds)
                {
                    lt.Velocity = new Vector2(Maths.random.Next(-5, 5), ((float)(Maths.random.NextDouble()) * -9.0f) - 2.0f);
                    lt.MyAnimationFlyWeight.gotoAnim("Fly", false);
                }
                
            }
            disturbed = true;
        }

        public void Infest()
        {
            if (!invulnerable)
            {
                int num = Maths.random.Next(5) + 1;
                for (int i = 0; i < num; ++i)
                {
                    zombies.Add(new Zombie(new Vector2(Position.X + Maths.random.Next((int)width * 32), Position.Y - 50), 0));
                }
            }
        }

        public void Invade()
        {
            if (!invulnerable)
            {
                UFO = new Thing("UFO", new Vector2(position.X + (width * 16) - (globals._Sprites["UFO"].Width / 2), position.Y - 700), 0);
            }
        }

        public void BeginFirinMahLazor()
        {
            if (!BeginLaserSequence && UFO != null)
            {
                globals._Sounds["LaserCharge"].Play();
                BeginLaserSequence = true;
                Camera.flash(UFOFlash, Color.LightCyan, true, true);
                Disturb();
                UFO.AddEmitter(new Vector2(UFO.Position.X + 91, UFO.Position.Y + 114), new Vector2(410, 300), 2, 10, 40, "Absorb", true);

            }
        }
    }
}
