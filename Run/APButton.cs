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
    public class APButton : MenuControl
    {
        
        

        public APButton(string txt, Vector2 position, String themeName)
        {
            Text = txt;
            Position = position;
            selectable = true;
            ThemeName = themeName;
        }

        public override void Draw(GameTime gameTime, Vector2 ContainerPos)
        {
            Color toDraw = globals._Themes[ThemeName].Normal;
            if (pressed)
            {
                toDraw = globals._Themes[ThemeName].Pressed;
            }
            if (selected)
            {
                toDraw = globals._Themes[ThemeName].Selected;
            }
            else
            {
                pressed = false;
            }

            globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], Text, Position + ContainerPos, toDraw);
        }

        public override void ActivateNow()
        {
            base.ActivateNow();
            pressed = true;
        }
    }
}
