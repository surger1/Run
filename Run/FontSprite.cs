using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Run
{
    public class FontSprite
    {
        public string theString;
        public Vector2 Location;
        public Color color;
        public AnimationFlyWeight myAnim;
        public HorizontalJustification hJustify;
        public VerticalJustification vJustify;

        public FontSprite(string fontName)
        {
            myAnim = new AnimationFlyWeight(fontName);
        }

        public void Draw(GameTime gameTime,string s, Vector2 location, Color col, HorizontalJustification hj, VerticalJustification vj,bool ui)
        {
            Vector2 loc = location;

            switch (hj)
            {
                case HorizontalJustification.Center:
                    loc.X -= (((s.Length + 1)* globals._Sprites[myAnim.SpriteName].Width) / 2) + ((globals._Sprites[myAnim.SpriteName].Width) / 2);
                    break;
                case HorizontalJustification.Right:
                    loc.X -= s.Length * globals._Sprites[myAnim.SpriteName].Width;
                    break;
            }

            switch (vj)
            {
                case VerticalJustification.Center:
                    loc.Y -= globals._Sprites[myAnim.SpriteName].Height / 2;
                    break;
                case VerticalJustification.Top:
                    loc.Y -= globals._Sprites[myAnim.SpriteName].Height;
                    break;
            }

            int i = 0;
            foreach (char c in s)
            {
                i++;
                myAnim.gotoAnim(c.ToString(), false);
                myAnim.Draw(gameTime, new Vector2(loc.X + (globals._Sprites[myAnim.SpriteName].Width * i), loc.Y), SpriteEffects.None, 1.0f, col,false,ui, 1.0f); 
            }
        }
    }

    

    public enum HorizontalJustification
    {
        Left,
        Right,
        Center
    }

    public enum VerticalJustification
    {
        Top,
        Bottom,
        Center
    }
}
