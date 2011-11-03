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
    public class LiveThing : Thing
    {

        #region memberVaribles        
        
        public Vector2 CollisonPoint
        {
            get { return position + globals._Sprites[myAnimationFlyWeight.SpriteName].CollisionPoint; }
        }

        public float CollisonRadius
        {
            get { return globals._Sprites[myAnimationFlyWeight.SpriteName].CollisionRadius; }
        }

        public Vector2 BoundingMask
        {
            get { return new Vector2((float)globals._Sprites[myAnimationFlyWeight.SpriteName].Width * 0.2f, (float)globals._Sprites[myAnimationFlyWeight.SpriteName].Height * 0.375f); }
        }

        

        public Vector2 SpriteCenter
        {
            get { return position + new Vector2((float)globals._Sprites[myAnimationFlyWeight.SpriteName].Width * 0.5f, (float)globals._Sprites[myAnimationFlyWeight.SpriteName].Height * 0.5f); }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        protected Vector2 velocity;

        

        public float Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        protected float direction;

        

        public float Time
        {
            get { return time; }
            set { time = value; }
        }
        protected float time;

        public string StringDirection
        {
            get { return stringDirection; }
            set { stringDirection = value; }
        }
        protected string stringDirection;

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
        protected string action;

        public float xSpeed
        {
            get { return velocity.X; }
            set { velocity.X = value; }
        }

        public float ySpeed
        {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        public bool OnGround = false;
        #endregion

        public LiveThing()
        {
            
        }

        public LiveThing(string spriteName, Vector2 pos, int mid)
        {
            Initialize(spriteName, pos, mid);
        }
        public LiveThing(string spriteName, Vector2 pos, int mid, string startAnim)
        {
            Initialize(spriteName, pos, mid);
            myAnimationFlyWeight.gotoAnim(startAnim,false);
        }

        public virtual void Initialize(string spriteName, Vector2 pos, int mid)
        {
            myAnimationFlyWeight = new AnimationFlyWeight(spriteName);
            position = pos;
            depth = 1.0f;
            time = 1.0f;
            Action = "stand";
            stringDirection = "Right";
            MyID = mid;
            effect = SpriteEffects.None;
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;
            base.Update(gameTime);
        }


    }
}
