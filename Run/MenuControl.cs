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
    public class MenuControl
    {
        protected MenuDelegate Activate;
        protected MenuDelegate LeftPress;
        protected MenuDelegate RightPress;
        public bool selected;
        public bool pressed;
        public bool disabled;
        public bool selectable;
        protected Dictionary<String,MenuControl> Controls;
        public Vector2 Position;
        public String Text;
        public String ThemeName;

        virtual public void Update(GameTime gameTime)
        {

        }

        virtual public void Draw(GameTime gameTime, Vector2 ContainerPosition)
        {

        }

        virtual public void ActivateNow()
        {
            if (Activate != null)
            {
                Activate();
                _Keyboard.Update();
            }
        }

        virtual public void LeftActivateNow()
        {
            if (LeftPress != null)
            {
                LeftPress();
            }
        }

        virtual public void RightNow()
        {
            if (RightPress != null)
            {
                RightPress();
            }
        }

        public void SetActivateDelegate(MenuDelegate del)
        {
            Activate = del;
        }

        public void SetLeftDelegate(MenuDelegate left)
        {
            LeftPress = left;
        }

        public void SetRightDelegate(MenuDelegate right)
        {
            RightPress = right;
        }
    }
}
