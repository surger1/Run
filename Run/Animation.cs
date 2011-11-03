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
    public class Animation
    {
        /// <summary>
        /// Used to initialize the animation class
        /// </summary>
        /// <param name="s">The start index of the animation</param>
        /// <param name="e">The end index of the animation</param>
        /// <param name="l">Wether to loop the animation or not</param>
        /// <param name="n">The name of the animation set</param>
        /// <param name="x">The name of the next animation ("" if none)</param>
        public Animation(int s, int e, bool l, string x)
        {
            start = s;
            end = e;
            loop = l;
            next = x;
        }
        public int start;
        public int end;
        public bool loop;
        public string next;

    }
}
