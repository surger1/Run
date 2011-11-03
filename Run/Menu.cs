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
    public delegate void MenuDelegate();

    public class Menu : MenuControl
    {
        
        AnimationFlyWeight BackGround;
        public Color BackColor;
        public Vector2 Dimensions;
        public Vector2 BeginDestination;
        public Vector2 EndDestination;
        public float Speed;
        public MenuDelegate Cancel;
        public MenuDelegate ReachDestination;
        public MenuDelegate UnReachDestination;
        public MenuDelegate UpdateMethod;
        public bool Active;

        int Selection;

        public Menu(String backName,Color backColor,Vector2 position,Vector2 dimesions)
        {
            Controls = new Dictionary<string, MenuControl>();
            if (backName != "")
            {
                BackGround = new AnimationFlyWeight(backName);
            }
            else
            {
                BackGround = new AnimationFlyWeight("Rectangle");
            }
            Position = position;
            Dimensions = dimesions;
            Selection = 0;
            BeginDestination = position;
            EndDestination = position;
            Active = false;
            selected = false;
            BackColor = backColor;
        }

        public void ToggleActivation()
        {
            Active = !Active;
        }

        public void AddCancel(MenuDelegate can)
        {
            Cancel = can;
        }

        public void AddReach(MenuDelegate reach)
        {
            ReachDestination = reach;
        }

        public void AddUnreach(MenuDelegate unReach)
        {
            UnReachDestination = unReach;
        }

        public void AddUpdateMethod(MenuDelegate init)
        {
            UpdateMethod = init;
        }

        public void UponReaching()
        {
            if (ReachDestination != null)
            {
                ReachDestination();
            }
        }

        public void UponUnReaching()
        {
            UnReachDestination();
        }

        public void ZipEffects(Vector2 begin, Vector2 end, float speed)
        {
            BeginDestination = begin;
            EndDestination = end;
            Speed = speed;
        }

        override public void Update(GameTime gameTime)
        {
            if (Active && selected)
            {
                BackGround.Update(gameTime, 1.0f);
                if (Controls.Count > 0)
                {
                    if (_Keyboard.KeyPress(Keys.Up) || _Controller.JoystickAxisPressed(ThumbStick.Left,ThumbStickDirection.Up))
                    {
                        Selection--;
                        if (Selection < 0)
                        {
                            Selection = Controls.Count - 1;
                        }
                        while (!Controls.ElementAt(Selection).Value.selectable)
                        {
                            Selection--;
                            if (Selection < 0)
                            {
                                Selection = Controls.Count - 1;
                            }
                        }
                    }

                    if (_Keyboard.KeyPress(Keys.Down) || _Controller.JoystickAxisPressed(ThumbStick.Left,ThumbStickDirection.Down))
                    {
                        Selection++;
                        if (Selection >= Controls.Count)
                        {
                            Selection = 0;
                        }
                        while (!Controls.ElementAt(Selection).Value.selectable)
                        {
                            Selection++;
                            if (Selection >= Controls.Count)
                            {
                                Selection = 0;
                            }
                        }
                    }

                    if (_Keyboard.KeyPress(Keys.Enter) || _Controller.ButtonPress(Buttons.Start))
                    {
                        if (Controls.Count > 0)
                        {
                            Controls.ElementAt(Selection).Value.ActivateNow();
                        }
                    }

                    if (_Keyboard.KeyPress(Keys.Escape) || _Controller.ButtonPress(Buttons.B))
                    {
                        CancelMenu();
                    }
                }

                if (UpdateMethod != null)
                {
                    UpdateMethod();
                }

                for (int i = 0; i < Controls.Count; ++i)
                {
                    if (i == Selection)
                    {
                        Controls.ElementAt(i).Value.selected = true;
                    }
                    else
                    {
                        Controls.ElementAt(i).Value.selected = false;
                    }
                    Controls.ElementAt(i).Value.Update(gameTime);
                }
            }
            if (selected)
            {
                if (Position != EndDestination)
                {
                    float move = Speed;
                    if(Maths.distance(Position,EndDestination) < Speed)
                    {
                        move = Maths.distance(Position, EndDestination);
                        UponReaching();
                    }
                    Position += Maths.pointOnACircle(move, Maths.angle(Position, EndDestination));
                }
            }
            else
            {
                if (Position != BeginDestination)
                {
                    float move = Speed;
                    if (Maths.distance(Position, BeginDestination) < Speed)
                    {
                        move = Maths.distance(Position, BeginDestination);
                    }
                    Position += Maths.pointOnACircle(move, Maths.angle(Position, BeginDestination));
                }
            }
        }

        public void AddButton(String buttonTxt, Vector2 position, string ThemName)
        {
            Controls.Add(buttonTxt,new APButton(buttonTxt,position,ThemName));
        }

        public void AddToggle(String buttonTxt, Vector2 position, string ThemName)
        {
            Controls.Add(buttonTxt, new Toggle(buttonTxt, position, ThemName));
        }

        public void AddText(string name, String buttonTxt, Vector2 position, string ThemName)
        {
            Controls.Add(name, new APText(buttonTxt, position, ThemName));
        }

        public void AddFontSpriteText(string name, String buttonTxt, Vector2 position, String FontName, string ThemName)
        {
            Controls.Add(name, new APSpriteFont(buttonTxt, position, FontName, ThemName));
        }

        public void AddActiveFunctionalityToControl(String name, MenuDelegate del)
        {
            Controls[name].SetActivateDelegate(del);
        }

        public void AddLeftActiveFunctionalityToControl(String name, MenuDelegate del)
        {
            Controls[name].SetLeftDelegate(del);
        }

        public void AddRightActiveFunctionalityToControl(String name, MenuDelegate del)
        {
            Controls[name].SetRightDelegate(del);
        }

        public void Deactivate()
        {
            Active = false;
            selected = false;
            Selection = 0;
        }

        public void UpdateControlText(String name, string newText)
        {
            Controls[name].Text = newText;
        }

        public void CancelMenu()
        {
            if (Cancel != null)
            {
                Cancel();
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (Active)
            {
                globals.DrawRectangle(gameTime, Position, Dimensions, BackColor, true);
                BackGround.Draw(gameTime, Position, SpriteEffects.None, 1.0f, Color.White, false, true, 1.0f);
                foreach (MenuControl c in Controls.Values)
                {
                    c.Draw(gameTime, Position);
                }
            }
        }
    }
}
