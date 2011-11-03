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
    static class _Controller
    {
        // Get input state.
        static GamePadState GamePadState;
        static GamePadState lastGamePadState;
        static PlayerIndex playerIndex;
        public static bool Initialized;
        static bool Connected;
        public static float dead;

        static public void init()
        {
            if (!Initialized)
            {
                bool found = false;
                GamePadState = GamePad.GetState(PlayerIndex.One);
                if (GamePadState.IsConnected && GamePadState.Buttons.A == ButtonState.Pressed && GamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    playerIndex = PlayerIndex.One;
                    found = true;
                }
                GamePadState = GamePad.GetState(PlayerIndex.Two);
                if (GamePadState.IsConnected && GamePadState.Buttons.A == ButtonState.Pressed && GamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    playerIndex = PlayerIndex.Two;
                    found = true;
                }
                GamePadState = GamePad.GetState(PlayerIndex.Three);
                if (GamePadState.IsConnected && GamePadState.Buttons.A == ButtonState.Pressed && GamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    playerIndex = PlayerIndex.Three;
                    found = true;
                }
                GamePadState = GamePad.GetState(PlayerIndex.Four);
                if (GamePadState.IsConnected && GamePadState.Buttons.A == ButtonState.Pressed && GamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    playerIndex = PlayerIndex.Four;
                    found = true;
                }
                if (found)
                {
                    Initialized = true;
                    globals._Sounds["BuildingHit"].Play();
                }
               
            }
            Update();
        }

        static public String GamerName()
        {
            String ret = "Anonymous";
            if (Initialized)
            {
                if (Gamer.SignedInGamers[playerIndex] != null)
                {
                    ret = Gamer.SignedInGamers[playerIndex].Gamertag;
                }
            }
            return ret;
        }

        static public bool Update()
        {
            bool ret = true;
            if (Initialized)
            {
                lastGamePadState = GamePadState;
                GamePadState = GamePad.GetState(playerIndex);
            }
            return ret;
        }

        static public bool ButtonDown(Buttons button)
        {
            return GamePadState.IsButtonDown(button);
        }

        static public bool ButtonPress(Buttons button)
        {
            if (!lastGamePadState.IsButtonDown(button) && GamePadState.IsButtonDown(button))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool ButtonReleased(Buttons button)
        {
            if (lastGamePadState.IsButtonDown(button) && !GamePadState.IsButtonDown(button))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool JoystickAxisPressed(ThumbStick thumbStick,ThumbStickDirection dir)
        {
            bool ret = false;
            if (thumbStick == ThumbStick.Left)
            {
                switch (dir)
                {
                    case ThumbStickDirection.Down:
                        if (GamePadState.ThumbSticks.Left.Y <= -0.5f && lastGamePadState.ThumbSticks.Left.Y > -0.5f)
                        {
                            ret = true;
                        }
                        break;
                    case ThumbStickDirection.Up:
                        if (GamePadState.ThumbSticks.Left.Y >= 0.5f && lastGamePadState.ThumbSticks.Left.Y < 0.5f)
                        {
                            ret = true;
                        }
                        break;
                    case ThumbStickDirection.Left:
                        if (GamePadState.ThumbSticks.Left.X <= -0.5f && lastGamePadState.ThumbSticks.Left.X > -0.5f)
                        {
                            ret = true;
                        }
                        break;
                    case ThumbStickDirection.Right:
                        if (GamePadState.ThumbSticks.Left.X >= 0.5f && lastGamePadState.ThumbSticks.Left.X < 0.5f)
                        {
                            ret = true;
                        }
                        break;
                }
            }
            else
            {
                switch (dir)
                {
                    case ThumbStickDirection.Down:
                        if (GamePadState.ThumbSticks.Right.Y <= -0.5f && lastGamePadState.ThumbSticks.Right.Y > -0.5f)
                        {
                            ret = true;
                        }
                        break;
                    case ThumbStickDirection.Up:
                        if (GamePadState.ThumbSticks.Right.Y >= 0.5f && lastGamePadState.ThumbSticks.Right.Y < 0.5f)
                        {
                            ret = true;
                        }
                        break;
                    case ThumbStickDirection.Left:
                        if (GamePadState.ThumbSticks.Right.X <= -0.5f && lastGamePadState.ThumbSticks.Right.X > -0.5f)
                        {
                            ret = true;
                        }
                        break;
                    case ThumbStickDirection.Right:
                        if (GamePadState.ThumbSticks.Right.X >= 0.5f && lastGamePadState.ThumbSticks.Right.X < 0.5f)
                        {
                            ret = true;
                        }
                        break;
                }
            }
            return ret;
        }

        public static float getThumbStickDirection(ThumbStick thumb)
        {
            if (thumb == ThumbStick.Left)
            {
                return Maths.angle(new Vector2(0, 0), GamePadState.ThumbSticks.Left, true);
            }
            else
            {
                return Maths.angle(new Vector2(0, 0), GamePadState.ThumbSticks.Right, true);
            }
        }

        public static float getThumbStickForce(ThumbStick thumb)
        {
            if (thumb == ThumbStick.Left)
            {
                    return Maths.distance(new Vector2(0, 0), GamePadState.ThumbSticks.Left);
                }
                else
                {
                    return Maths.distance(new Vector2(0, 0), GamePadState.ThumbSticks.Right);
                }
            }
        }

    public enum ThumbStickDirection { Up, Down, Left, Right };

    public enum ThumbStick { Left, Right }


    } 

    
