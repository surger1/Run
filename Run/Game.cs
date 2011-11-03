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
using System.IO;

namespace Run
{
    static class ApocalypseParkour
    {
        static List<Level> levels;
        static int currentLevel;
        static bool DeadDisplay;
        static bool pause;
        static int fps = 0;
        static float total = 0;
        static string sFPS = "";
        static int milliSecondsPast;
        static bool LastCheck;
        static AnimationFlyWeight Bars;
        static int LevelUpSelection = 0;
        static int ParkAdd = 0;
        static int JumpAdd = 0;
        static int AccelAdd = 0;
        static int SpeedAdd = 0;
        static Color AccelCol = Color.White;
        static Color JumpCol = Color.White;
        static Color SpeedCol = Color.White;
        static Color ParkCol = Color.White;
        static int bonus;
        static int distance;
        static int totalUpgradeCost;
        const int JumpCost = 150;
        const int AccelCost = 225;
        const int ParkCost = 175;
        const int SpeedCost = 200;
        static int Required;
        static int level;
        static bool hats;
        static bool cannon;
        static List<String> Difficulties;
        public static string CurrentMenu;
        public static int Difficulty;
        public static bool leisureMode;
        public static bool MultiHazard;
        public static bool DoubleJumpEnabled;
        public static bool GodMode;
        public static bool Horde;
        static bool IsActive;
        static bool LastActive;
        static string timeStamper = "";


        static public void Save()
        {
            if (StorageContainer.TitleLocation != null)
            {
                string fullpath = StorageContainer.TitleLocation + "Run.xml";
                FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
            }
        }

        static public void Initialize()
        {
            CurrentMenu = "Start";
            Bars = new AnimationFlyWeight("Bars");
            levels = new List<Level>();
            
            DeadDisplay = false;
            pause = false;
            Difficulties = new List<string>();
            Difficulties.Add("My Little Apocalypse");
            Difficulties.Add("Doomsday");
            Difficulties.Add("Seas will boil, Stars will fall in dispare");
            Difficulties.Add("A booming voice will speak the words to end existence");
            Difficulty = 1;
            Building.LoadPatterns();
            //Camera.shake(100000, 500.0f, 0.01f);
        }

        static public void InitController()
        {
            _Controller.init();
        }

        static public void updateActive(bool up)
        {
            LastActive = IsActive;
            IsActive = up;
        }

        static public void ToggleGodMode()
        {
            GodMode = !GodMode;
        }

        static public void ToggleDoubleJump()
        {
            DoubleJumpEnabled = !DoubleJumpEnabled;
        }

        static public void ToggleMultiHazard()
        {
            MultiHazard = !MultiHazard;
        }

        static public void ToggleLeisureMode()
        {
            leisureMode = !leisureMode;
        }

        static public void ToggleHorde()
        {
            Horde = !Horde;
        }

        static public void UnPause()
        {
            pause = false;
            CurrentMenu = "";
            globals._Menus["Pause"].Active = false;
        }

        static public void ToggleGravity()
        {
            if (Maths.Gravity < 0.8f)
            {
                Maths.Gravity = 1.0f;
            }
            else
            {
                Maths.Gravity = 0.5f;
            }

        }

        static public void IncreaseDifficulty()
        {
            Difficulty++;
            if(Difficulty >= Difficulties.Count)
            {
                Difficulty = 0;
            }

            globals._Menus["Options"].UpdateControlText("Difficulty", Difficulties[Difficulty]);
        }

        static public void DecreaseDifficulty()
        {
            Difficulty--;
            if (Difficulty < 0)
            {
                Difficulty = Difficulties.Count - 1;
            }

            globals._Menus["Options"].UpdateControlText("Difficulty", Difficulties[Difficulty]);
        }

        static public void StartGame()
        {
            levels.Clear();
            currentLevel = 0;
            level = 1;
            AddLeveL(new Vector2(100, -3000));
            CurrentMenu = "";
            globals._Menus["Start"].Active = false;
        }

        static public void RestartLevel()
        {
            AddLeveL(new Vector2(100, -3000));
            currentLevel++;
            levels.RemoveAt(currentLevel - 1);
            currentLevel--;
            DeadDisplay = false;
            Camera.BackColor = Color.SkyBlue;
            CurrentMenu = "";
            globals._Menus["Dead"].Deactivate();
        }

        static public void Update(GameTime gameTime)
        {
            
            Camera.Update(gameTime);
            _Keyboard.Update();
            bool badUpdate = _Controller.Update();
            

            if (!DeadDisplay && CurrentMenu == "")
            {
                if (levels.Count > 0 && levels[currentLevel].LevelCompleted())
                {
                    if (bonus > 0)
                    {
                        if (_Keyboard.KeyPress(Keys.Enter) || _Controller.ButtonPress(Buttons.Start))
                        {
                            Player.DistanceExperience += bonus + distance;
                            bonus = 0;
                            distance = 0;
                        }
                        if(distance > 0)
                        {
                            Player.DistanceExperience += 1;
                            distance--;
                        }
                        else
                        {
                            Player.DistanceExperience += 1;
                            bonus--;
                        }
                    }
                    else
                    {


                        if (_Keyboard.KeyPress(Keys.Down) || _Controller.JoystickAxisPressed(ThumbStick.Left,ThumbStickDirection.Down))
                        {
                            if (LevelUpSelection < 3)
                            {
                                LevelUpSelection++;
                            }
                            else
                            {
                                LevelUpSelection = 0;
                            }
                        }

                        if (_Keyboard.KeyPress(Keys.Up) || _Controller.JoystickAxisPressed(ThumbStick.Left,ThumbStickDirection.Up))
                        {
                            if (LevelUpSelection > 0)
                            {
                                LevelUpSelection--;
                            }
                            else
                            {
                                LevelUpSelection = 3;
                            }
                        }

                        if (_Keyboard.KeyPress(Keys.Left) || _Controller.JoystickAxisPressed(ThumbStick.Left, ThumbStickDirection.Left))
                        {
                            switch (LevelUpSelection)
                            {
                                case 0:
                                    if (JumpAdd > 0)
                                    {
                                        Player.DistanceExperience += (JumpAdd + Player.JumpLevel) * 100;
                                        JumpAdd--;
                                    }
                                    break;
                                case 1:
                                    if (ParkAdd > 0)
                                    {
                                        Player.DistanceExperience += (ParkAdd + Player.ParkLevel) * 125;
                                        ParkAdd--;
                                    }
                                    break;
                                case 2:
                                    if (AccelAdd > 0)
                                    {
                                        Player.DistanceExperience += (AccelAdd + Player.AccelLevel) * 200;
                                        AccelAdd--;
                                    }
                                    break;
                                case 3:
                                    if (SpeedAdd > 0)
                                    {
                                        Player.DistanceExperience += (SpeedAdd + Player.SpeedLevel) * 150;
                                        SpeedAdd--;
                                    }
                                    break;

                            }
                        }

                        if (_Keyboard.KeyPress(Keys.Right) || _Controller.JoystickAxisPressed(ThumbStick.Left, ThumbStickDirection.Right))
                        {
                            switch (LevelUpSelection)
                            {
                                case 0:
                                    if (JumpAdd + Player.JumpLevel < 4 && (JumpAdd + Player.JumpLevel + 1) * 100 < Player.DistanceExperience)
                                    {
                                        JumpAdd++;
                                        Player.DistanceExperience -= (JumpAdd + Player.JumpLevel) * 100;
                                    }
                                    break;
                                case 1:
                                    if (ParkAdd + Player.ParkLevel < 4 && (ParkAdd + Player.ParkLevel + 1) * 125 < Player.DistanceExperience)
                                    {
                                        ParkAdd++;
                                        Player.DistanceExperience -= (ParkAdd + Player.ParkLevel) * 125;
                                    }
                                    break;
                                case 2:
                                    if (AccelAdd + Player.AccelLevel < 4 && (AccelAdd + Player.AccelLevel + 1) * 200 < Player.DistanceExperience)
                                    {
                                        AccelAdd++;
                                        Player.DistanceExperience -= (AccelAdd + Player.AccelLevel) * 200;
                                    }
                                    break;
                                case 3:
                                    if (SpeedAdd + Player.SpeedLevel < 4 && (SpeedAdd + Player.SpeedLevel + 1) * 150 < Player.DistanceExperience)
                                    {
                                        SpeedAdd++;
                                        Player.DistanceExperience -= (SpeedAdd + Player.SpeedLevel) * 150;
                                    }
                                    break;

                            }
                        }

                        AccelCol = Color.White;
                        JumpCol = Color.White;
                        SpeedCol = Color.White;
                        ParkCol = Color.White;
                        switch (LevelUpSelection)
                        {
                            case 0:
                                JumpCol = Color.Yellow;
                                if (Player.JumpLevel + JumpAdd + 1 <= 4)
                                {
                                    Required = (Player.JumpLevel + JumpAdd + 1) * 100;
                                }
                                else
                                {
                                    Required = -1;
                                }
                                break;
                            case 1:
                                ParkCol = Color.Yellow;
                                if (Player.ParkLevel + ParkAdd + 1 <= 4)
                                {
                                    Required = (Player.ParkLevel + ParkAdd + 1) * 125;
                                }
                                else
                                {
                                    Required = -1;
                                }
                                break;
                            case 2:
                                AccelCol = Color.Yellow;
                                if (Player.AccelLevel + AccelAdd + 1 <= 4)
                                {
                                    Required = (Player.AccelLevel + AccelAdd + 1) * 200;
                                }
                                else
                                {
                                    Required = -1;
                                }
                                break;
                            case 3:
                                SpeedCol = Color.Yellow;
                                if (Player.SpeedLevel + SpeedAdd + 1 <= 4)
                                {
                                    Required = (Player.SpeedLevel + SpeedAdd + 1) * 150;
                                }
                                else
                                {
                                    Required = -1;
                                }
                                break;

                        }
                        if (_Keyboard.KeyPress(Keys.Enter) || _Controller.ButtonPress(Buttons.Start))
                        {
                            Player.JumpLevel += JumpAdd;
                            Player.ParkLevel += ParkAdd;
                            Player.AccelLevel += AccelAdd;
                            Player.SpeedLevel += SpeedAdd;
                            JumpAdd = 0;
                            ParkAdd = 0;
                            AccelAdd = 0;
                            SpeedAdd = 0;
                            level++;
                            if (level >= 6)
                            {
                                CurrentMenu = "Credits";
                                globals._Menus["CreditsBack"].Active = true;
                                globals._Menus["Credits"].Active = true;
                                globals._Menus["Credits"].selected = true;
                                Camera.flash(1000, Color.Black, true, false);
                            }
                            else
                            {
                                
                                AddLeveL(new Vector2(100, -3000));
                            }
                            levels.RemoveAt(0);
                            
                        }
                    }
                }
                else if ((!IsActive && LastActive))// || (badUpdate && _Controller.Initialized))
                {
                    pause = true;
                    CurrentMenu = "Pause";
                    globals._Menus["Pause"].Active = true;
                    globals._Menus["Pause"].selected = true;

                }
                else if (_Keyboard.KeyPress(Keys.Enter) || _Controller.ButtonPress(Buttons.Start))
                {
                    pause = !pause;
                    if (pause)
                    {
                        CurrentMenu = "Pause";
                        globals._Menus["Pause"].Active = true;
                        globals._Menus["Pause"].selected = true;
                        _Keyboard.Update();
                    }
                    else
                    {
                        CurrentMenu = "";
                    }
                }

                if (!pause && levels.Count > 0)
                {
                    levels[currentLevel].Update(gameTime);
                    if (levels[currentLevel].IsDead())
                    {
                        DeadDisplay = true;
                        CurrentMenu = "Dead";
                        globals._Menus["Dead"].Active = true;
                        globals._Menus["Dead"].selected = true;
                    }
                }
                else
                {
                    
                }
            }
            /*else
            {
                Camera.BackColor = Color.Black;
                if (_Keyboard.KeyPress(Keys.Enter) || _Controller.ButtonPress(Buttons.Start) && CurrentMenu == "")
                {
                    AddLeveL(new Vector2(100, -3000));
                    currentLevel++;
                    levels.RemoveAt(currentLevel - 1);
                    currentLevel--;
                    DeadDisplay = false;
                    Camera.BackColor = Color.SkyBlue;
                }

            }*/

            if (CurrentMenu != "")
            {
                foreach (Menu m in globals._Menus.Values)
                {
                    m.Update(gameTime);
                }
            }

            

        }

        static public void Draw(GameTime gameTime)
        {

            DateTime start = DateTime.Now;
            RenderTarget2D temp = (RenderTarget2D)globals._GraphicsDevice.GetRenderTarget(0);
            globals._GraphicsDevice.SetRenderTarget(0, globals.ShaderRenderTarget);

            globals._GraphicsDevice.Clear(Camera.BackColor);
            globals._SpriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                              SpriteSortMode.Immediate,
                              SaveStateMode.None);
            






            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            total += elapsed;
            
            if (total >= 1)
            {

                sFPS = fps.ToString();

                fps = 0;

                total = 0;

            }
            fps += 1;
            if (!DeadDisplay && CurrentMenu != "Start")
            {
                if (levels.Count > 0)
                {
                    levels[currentLevel].Draw(gameTime);
                }
            }
            
            





            globals._SpriteBatch.End();


            globals._GraphicsDevice.SetRenderTarget(0, temp);
            globals.ShaderTexture = globals.ShaderRenderTarget.GetTexture();

            // Use Immediate mode and our effect to draw the scene
            // again, using our pixel shader.
            
            globals._SpriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate,
                SaveStateMode.None);
            globals._Effects["Glare"].Begin();
            globals._Effects["Glare"].CurrentTechnique.Passes[0].Begin();
            
            globals._SpriteBatch.Draw(globals.ShaderTexture, Vector2.Zero, Color.White);
            
            globals._Effects["Glare"].CurrentTechnique.Passes[0].End();
            globals._Effects["Glare"].End();

            globals._SpriteBatch.End();
            globals._SpriteBatch.Begin();
            globals.DrawDefaultSprite(gameTime, new Vector2(-640 + Camera.Position.X, 1400), "BlackBar");
            globals.DrawRectangle(gameTime, new Vector2(-640 + Camera.Position.X, 1592), new Vector2(1280, 1000), Color.Black, false);
            

            globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], sFPS, new Vector2(90, 620), Color.White);
            if (CurrentMenu != "")
            {
                foreach (Menu m in globals._Menus.Values)
                {
                    m.Draw(gameTime);
                }
            }
            if (pause)
            {
                //globals.DrawRectangle(gameTime, Camera.Position - new Vector2(globals._Graphics.GraphicsDevice.Viewport.Width / 2, globals._Graphics.GraphicsDevice.Viewport.Height / 2), new Vector2(globals._Graphics.GraphicsDevice.Viewport.Width, globals._Graphics.GraphicsDevice.Viewport.Height), new Color(0, 0, 0, 200),false);
                //globals._SpriteBatch.DrawString(globals._SpriteFonts["M04B"], "PAUSE", new Vector2(globals._Graphics.GraphicsDevice.Viewport.Width / 2 - (60.0f * 3f), globals._Graphics.GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
                //globals._FontSprites["LargeGradient"].Draw(gameTime, "PAUSE", new Vector2(globals._Graphics.GraphicsDevice.Viewport.Width / 2, globals._Graphics.GraphicsDevice.Viewport.Height / 2), Color.White, HorizontalJustification.Center, VerticalJustification.Center, true);
            }

            if (levels.Count > 0 && levels[currentLevel].seriouslyDone)
            {
                globals.DrawRectangle(gameTime, Camera.Position - new Vector2(globals._Graphics.GraphicsDevice.Viewport.Width / 2, globals._Graphics.GraphicsDevice.Viewport.Height / 2), new Vector2(globals._Graphics.GraphicsDevice.Viewport.Width, globals._Graphics.GraphicsDevice.Viewport.Height), Color.Black, false);
                
            }
            if (levels.Count > 0 && levels[currentLevel].LevelCompleted())
            {
                globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], (((float)levels[currentLevel].goal / (float)(levels[currentLevel].PlayingLevel.TotalHours)) / 1000.0).ToString().Substring(0, 4) + "\\kmph", new Vector2(100, 100), Color.White);
                Bars.gotoAnim((Player.JumpLevel + JumpAdd).ToString(), false);
                Bars.Draw(gameTime, new Vector2(Camera.Position.X, Camera.Position.Y + 0), SpriteEffects.None, 1.0f, Color.White, 1.0f);
                globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Jump", new Vector2(640 - 140, 360), JumpCol);
                Bars.gotoAnim((Player.ParkLevel + ParkAdd).ToString(), false);
                Bars.Draw(gameTime, new Vector2(Camera.Position.X, Camera.Position.Y + 16), SpriteEffects.None, 1.0f, Color.White, 1.0f);
                globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Parkour", new Vector2(640 - 170, 360+16), ParkCol);
                Bars.gotoAnim((Player.AccelLevel + AccelAdd).ToString(), false);
                Bars.Draw(gameTime, new Vector2(Camera.Position.X, Camera.Position.Y + 32), SpriteEffects.None, 1.0f, Color.White, 1.0f);
                globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Acceleration", new Vector2(640 - 220, 360+32), AccelCol);
                Bars.gotoAnim((Player.SpeedLevel + SpeedAdd).ToString(), false);
                Bars.Draw(gameTime, new Vector2(Camera.Position.X, Camera.Position.Y + 48), SpriteEffects.None, 1.0f, Color.White, 1.0f);
                globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Speed", new Vector2(640 - 150, 360+48), SpeedCol);
                
                if (bonus > 0 || distance > 0)
                {
                    if (distance > 0)
                    {
                        globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Travelled:  " + Player.DistanceExperience.ToString() + " + " + distance.ToString() + " Distance", new Vector2(640, 360 + 72), Color.White);
                    }
                    else
                    {
                        globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Travelled:" + Player.DistanceExperience.ToString() + " + " + bonus.ToString() + " Speed Bonus", new Vector2(640, 360 + 72), Color.White);
                    }

                    
                }
                else
                {
                    globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Travelled:" + Player.DistanceExperience.ToString(), new Vector2(640, 360 + 72), Color.White);
                    if (Required >= 0)
                    {
                        globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "Required: " + Required.ToString(), new Vector2(640, 360 + 95), Color.White);
                    }
                    else
                    {
                        globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], "MAXED", new Vector2(640, 360 + 95), Color.White);
                    }
                }
                
            }

            //globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], timeStamper, new Vector2(340, 100), Color.White);
            Camera.DrawFlash(gameTime);
            globals._SpriteBatch.End();
            DateTime end = DateTime.Now;
            timeStamper = ((end - start).Milliseconds).ToString() ;
        }

        static public void DoBonus()
        {
            bonus = (int)(((float)levels[currentLevel].goal * (((float)levels[currentLevel].goal / (float)(levels[currentLevel].PlayingLevel.TotalHours)) / 100000.0)) * (0.8f + (Difficulty * 0.2f)) );
            distance = levels[currentLevel].goal;
        }

        static public void AddLeveL(Vector2 startPos)
        {
            levels.Add(new Level(level * 150,level));
        }

        static public void OptionsActivate()
        {
            globals._Menus["Options"].selected = true;
            globals._Menus["Options"].Active = true;
            globals._Menus["Start"].selected = false;
            _Keyboard.Update();
        }
        static public void OptionsCancel()
        {
            globals._Menus["Options"].selected = false;
            globals._Menus["Start"].selected = true;
            _Keyboard.Update();
        }

        static public void ModsActivate()
        {
            globals._Menus["Mods"].selected = true;
            globals._Menus["Mods"].Active = true;
            globals._Menus["Start"].selected = false;
            _Keyboard.Update();
        }
        static public void ModsCancel()
        {
            globals._Menus["Mods"].selected = false;
            globals._Menus["Start"].selected = true;
            _Keyboard.Update();
        }


        static public void EndAttempt()
        {
            globals._Menus["Pause"].selected = false;
            globals._Menus["Confirm"].Active = true;
            globals._Menus["Confirm"].selected = true;
        }
        static public void EndCurrentGame()
        {
            globals._Menus["Pause"].Deactivate();
            globals._Menus["Confirm"].Deactivate();
            globals._Menus["Dead"].Deactivate();
            globals._Menus["Credits"].Deactivate();
            globals._Menus["CreditsBack"].Deactivate();
            globals._Menus["Start"].selected = true;
            globals._Menus["Start"].Active = true;
            Camera.flash(1000, Color.Black, true, false);
            CurrentMenu = "Start";
            levels.Clear();
            pause = false;
            DeadDisplay = false;
        }
        static public void EndDeny()
        {
            globals._Menus["Pause"].selected = true;
            globals._Menus["Confirm"].Active = false;
            globals._Menus["Confirm"].selected = false;
            
        }
        static public void HatsToggle()
        {
            hats = !hats;
        }
        static public void CannonToggle()
        {
            cannon = !cannon;
        }
    }
}
