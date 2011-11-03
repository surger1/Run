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
    public class Toggle : MenuControl
    {
        public Toggle(string txt, Vector2 position, String Theme)
        {
            Text = txt;
            Position = position;
            selectable = true;
            ThemeName = Theme;
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

            globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], Text, Position + ContainerPos, toDraw);
        }

        public override void ActivateNow()
        {
            base.ActivateNow();
            pressed = !pressed;
        }
    }


}
