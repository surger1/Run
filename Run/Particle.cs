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
    class Particle
    {
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

        public String SpriteName
        {
            get { return spriteName; }
            set { spriteName = value; }
        }
        String spriteName;

        float Mass;
        int Life;
        int timeAlive;
        public bool killME;
        public bool Fade;

        public Particle(string name,Vector2 pos,Vector2 vel,float mass, int life, bool fade)
        {
            spriteName = name;
            position = pos;
            velocity = vel;
            Mass = mass;
            Life = life;
            timeAlive = 0;
            killME = false;
            Fade = fade;
        }

        public void Update(GameTime gameTime)
        {
            velocity.Y += Maths.Gravity * Mass;
            position += velocity;
            timeAlive += gameTime.ElapsedGameTime.Milliseconds;
            if (timeAlive >= Life && Life > 0)
            {
                killME = true;
            }

        }

 

        public void Draw(GameTime gameTime)
        {
            Color col = Color.White;
            if (Fade)
            {
                float check = (float)((double)timeAlive / (double)Life);

                col = new Color(col.R,col.G,col.B, 1.0f - check);
            }
            globals._Sprites[SpriteName].DrawPoint(gameTime, position, col);
        }

        public void Draw(GameTime gameTime, Vector2 addPos)
        {
            Color col = Color.White;
            if (Fade)
            {
                float check = (float)((double)timeAlive / (double)Life);

                col = new Color(col.R,col.G,col.B, 1.0f - check);
            }
            
            globals._Sprites[SpriteName].DrawPoint(gameTime, position + addPos, col);
        }
    }
}
