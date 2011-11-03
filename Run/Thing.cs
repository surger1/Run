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
    public class Thing
    {
        public SpriteEffects effect;
        public bool falling;
        public List<ParticleEmitter> Emitters;
        public Vector2 BBox
        {
            get { return new Vector2((float)globals._Sprites[myAnimationFlyWeight.SpriteName].Width, (float)globals._Sprites[myAnimationFlyWeight.SpriteName].Height); }
        }

        public AnimationFlyWeight MyAnimationFlyWeight
        {
            get { return myAnimationFlyWeight; }
            set { myAnimationFlyWeight = value; }
        }
        protected AnimationFlyWeight myAnimationFlyWeight;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected Vector2 position;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        protected Vector2 velocity;

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        protected float depth;

        public int MyID
        {
            get { return myID; }
            set { myID = value; }
        }
        protected int myID;

        public Thing()
        {
            Initialize("uhoh", new Vector2(0,0), -1);
        }

        public Thing(string spriteName, Vector2 pos, int mid)
        {
            Initialize(spriteName, pos, mid);
        }

        public virtual void Initialize(string spriteName, Vector2 pos, int mid)
        {
            myAnimationFlyWeight = new AnimationFlyWeight(spriteName);
            position = pos;
            depth = 1.0f;
            MyID = mid;
            effect = SpriteEffects.None;
            Emitters = new List<ParticleEmitter>();
        }

        public virtual void AddEmitter(Vector2 pos, Vector2 reg, int amount, int freq, int em, string part, bool act)
        {
            Emitters.Add(new ParticleEmitter(pos, reg, amount, freq, em, part, act));
        }

        public virtual void Draw(GameTime gameTime)
        {
            
            foreach (ParticleEmitter p in Emitters)
            {
                p.Draw(gameTime);
            }
            myAnimationFlyWeight.Draw(gameTime, position, effect, 1.0f, Color.White, 1.0f, 1.0f);
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity;
            myAnimationFlyWeight.Update(gameTime, 1.0f);
            foreach (ParticleEmitter p in Emitters)
            {
                p.Update(gameTime,position);
            }
        }
    }
}
