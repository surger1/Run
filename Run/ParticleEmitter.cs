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
    public class ParticleEmitter
    {
        List<Particle> Particles;

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

        public Vector2 Region
        {
            get { return region; }
            set { region = value; }
        }
        protected Vector2 region;

        public int AmountEmitted;
        public int Frequency;
        public int NumberOFEmissions;
        public string ParticleType;
        public int SinceLastEmission;
        public int emissions;
        public bool Active;

        public ParticleEmitter(Vector2 pos, Vector2 reg, int amount, int freq,int em,string part,bool act)
        {
            position = pos;
            region = reg;
            Particles = new List<Particle>();
            AmountEmitted = amount;
            Frequency = freq;
            NumberOFEmissions = em;
            ParticleType = part;
            SinceLastEmission = 0;
            emissions = 0;
            Active = act;
            position = pos;
        }

        public virtual void Update(GameTime gameTime,Vector2 pos)
        {
            position += velocity;
            SinceLastEmission += gameTime.ElapsedGameTime.Milliseconds;
            if (SinceLastEmission >= Frequency && emissions < NumberOFEmissions && Active)
            {
                switch(ParticleType)
                {
                    case "GlassFragment":
                        for (int i = 0; i < AmountEmitted; ++i)
                        {
                            Emit("GlassFragment",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, new Vector2(((float)Maths.random.NextDouble() * 10.0f) - 5.0f, ((float)Maths.random.NextDouble() * 10.0f) - 10.0f), 1.0f,-1,false);
                        
                        }
                        SinceLastEmission = 0;
                         emissions++;
                        break;
                    case "BlockFragment":
                        for (int i = 0; i < AmountEmitted; ++i)
                        {
                            Emit("BlockFragment",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, new Vector2(((float)Maths.random.NextDouble() * 10.0f) - 5.0f, ((float)Maths.random.NextDouble() * 10.0f) - 10.0f), 1.0f,-1,false);
                        
                        }
                        SinceLastEmission = 0;
                        emissions++;
                    break;
                    case "SmallSmoke":
                        for (int i = 0; i < AmountEmitted; ++i)
                        {
                            Emit("SmallSmoke",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, Vector2.Zero, 0.0f, 3000, true);

                        }
                        SinceLastEmission = 0;
                        emissions++;
                    break;
                    case "JetStream":
                        for (int i = 0; i < AmountEmitted; ++i)
                        {
                            Emit("JetStream",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, Vector2.Zero, 0.0f, 1000, true);

                        }
                        SinceLastEmission = 0;
                        emissions++;
                    break;
                    case "DustCloud":
                        for (int i = 0; i < AmountEmitted; ++i)
                        {
                            Emit("DustCloud",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, new Vector2(((float)Maths.random.NextDouble() * 10.0f) - 5.0f, ((float)Maths.random.NextDouble() * 10.0f) - 10.0f), 0.5f, 300, true);

                        }
                        SinceLastEmission = 0;
                        emissions++;
                    break;
                    case "MeteorSmoke":
                        for (int i = 0; i < AmountEmitted; ++i)
                        {
                            Emit("MeteorSmoke",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, Vector2.Zero, -0.5f, 700, true);

                        }
                        SinceLastEmission = 0;
                        emissions++;
                    break;
                    case "Flamming":
                    for (int i = 0; i < AmountEmitted; ++i)
                    {
                        Emit("Fire",position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, Vector2.Zero, 0.0f, 300, true);

                    }
                    SinceLastEmission = 0;
                    emissions++;
                    break;
                    case "Fire":
                    for (int i = 0; i < AmountEmitted; ++i)
                    {
                        Emit("Fire", position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, Vector2.Zero, -0.1f, 600, true);

                    }
                    SinceLastEmission = 0;
                    emissions++;
                    break;
                    case "JetBlast":
                    for (int i = 0; i < AmountEmitted; ++i)
                    {
                        Emit("JetBlast", position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y)) + pos, Vector2.Zero, 0.0f, 300, true);

                    }
                    SinceLastEmission = 0;
                    emissions++;
                    break;
                    case "Absorb":
                    for (int i = 0; i < AmountEmitted; ++i)
                    {
                        Vector2 poss = position + new Vector2(Maths.random.Next((int)region.X), Maths.random.Next((int)region.Y));
                        Emit("Absorb", poss , Maths.pointOnACircle(3.0f,Maths.angle(poss,position + (region / 2),false)) , 0.0f, 300, true);

                    }
                    SinceLastEmission = 0;
                    emissions++;
                    break;
                }
            }
            for (int i = 0; i < Particles.Count; ++i)
            {
                Particles[i].Update(gameTime);
                if (Particles[i].killME)
                {
                    Particles.RemoveAt(i);
                }
            }
            
        }

        public void Emit(string partType,Vector2 pos, Vector2 vel, float mass, int life,bool fade)
        {
            Particles.Add(new Particle(partType, pos, vel, mass, life, fade));
        }

        public virtual void Draw(GameTime gameTime)
        {
            for (int i = 0;i  < Particles.Count;++i)
            {
                Particles[i].Draw(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime,Vector2 addPos)
        {
            for (int i = 0; i < Particles.Count; ++i)
            {
                Particles[i].Draw(gameTime,addPos);
            }
        }

        public void Activate()
        {
            Active = true;
        }
    }
}
