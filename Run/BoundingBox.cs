﻿using System;
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
    class BoundingBox
    {
        public Vector2 position;
        public Vector2 dimensions;
        public BoundingBox(Vector2 pos, Vector2 dim)
        {
            position = pos;
            dimensions = dim;
        }
    }
}
