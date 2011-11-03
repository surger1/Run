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
    class Zombie : LiveThing
    {
        public bool eating;
        new public Vector2 BBox
        {
            get { return new Vector2((float)globals._Sprites[myAnimationFlyWeight.SpriteName].Width / 2, (float)globals._Sprites[myAnimationFlyWeight.SpriteName].Height); }
        }
        
        public Zombie(Vector2 pos,int mid)
        {
            base.Initialize("Zombie", pos, mid);
            myAnimationFlyWeight.gotoAnim("Walk",false);
            myAnimationFlyWeight.FrameSpeed = 0.3f;
            if (Maths.random.Next(2) == 0)
            {
                stringDirection = "Left";
                effect = SpriteEffects.FlipHorizontally;
            }
            falling = false;
        }

        public override void Update(GameTime gameTime)
        {

 	         base.Update(gameTime);
             
             if (stringDirection == "Left")
             {
                 if (!falling)
                 {
                     if (!eating)
                     {
                         velocity.X = -0.16f;
                     }
                     else
                     {
                         velocity.X = 0;
                     }
                 }
                 effect = SpriteEffects.FlipHorizontally;
             }
             else
             {
                 if (!falling)
                 {
                     if (!eating)
                     {
                        velocity.X = 0.16f;
                     }
                     else
                     {
                         velocity.X = 0;
                     }
                 }
                 effect = SpriteEffects.None;
             }
             if (falling)
             {
                 velocity.Y += 1.0f;
                 myAnimationFlyWeight.gotoAnim("Die", false);
                 myAnimationFlyWeight.FrameSpeed = 0.18f;
             }
        }
    }
}
