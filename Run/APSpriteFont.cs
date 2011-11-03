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
    class APSpriteFont : MenuControl
    {
        public String FontName;
        

        public APSpriteFont(string txt, Vector2 position, String fn, String theme)
        {
            Text = txt;
            Position = position;
            selectable = false;
            FontName = fn;
            ThemeName = theme;
        }

        public override void Draw(GameTime gameTime, Vector2 ContainerPos)
        {
            globals._FontSprites[FontName].Draw(gameTime, Text, Position + ContainerPos, globals._Themes[ThemeName].Normal, HorizontalJustification.Center, VerticalJustification.Center, true);
        }
    }
}
