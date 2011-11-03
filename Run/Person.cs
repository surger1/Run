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
    public class Person : LiveThing
    {
        protected ulong jumpTimer;
        public  float accel = 0.06f;
        public  float topSpeed = 9.0f;
        public  ulong jumpStrength = 60;
        public  float baseAccel = 0.06f;
        public  float baseSpeed = 12.0f;
        public  ulong baseJump = 60;
        public  float AddAccel = 0.025f;
        public  float AddSpeed = 0.8f;
        public  ulong AddJump = 12;
        public int terminalVelocity ;//= 25 + Maths.random.Next(3) + Maths.random.Next(3) + Maths.random.Next(3);

        public bool playedThisStep;
        public bool OnMetal;
        public bool OnEdge;
        public bool dead;
        public bool minJump;

        public Person(Vector2 pos, int mid)
        {
            base.Initialize("Sean", pos, mid);
            jumpTimer = 501;
            int skill = Maths.random.Next(3) + Maths.random.Next(3) + Maths.random.Next(3);
            terminalVelocity = 28 + skill;
            accel = baseAccel + (skill * AddAccel);
            topSpeed = baseSpeed + (skill * AddSpeed);
            jumpStrength = baseJump + ((ulong)skill * AddJump);
            OnEdge = false;

        }

        public void checkTerminal()
        {
            if (Velocity.Y > terminalVelocity)
            {
                dead = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            myAnimationFlyWeight.Update(gameTime, 1.0f);

            jumpTimer += (ulong)gameTime.ElapsedGameTime.Milliseconds;

            if (OnGround)
            {
                if (dead)
                {
                    
                    if (velocity.X > 1.0f)
                    {
                        velocity.X -= 1.0f;
                    }
                    else if( velocity.X < -1.0f)
                    {
                        velocity.X += 1.0f;
                    }
                    else
                    {
                        velocity.X = 0;
                    }
                }
                else if (velocity.X < 0)
                {
                    velocity.X += accel + 0.4f;
                }
                else
                {
                    velocity.X += accel;
                    stringDirection = "Right";
                }
                if (velocity.X > topSpeed)
                {
                    velocity.X = topSpeed;
                }
            }

            if (OnGround)
            {
                jumpTimer = 500;
            }

            if (OnEdge && (OnGround) && !dead)
            {
                if (jumpTimer > jumpStrength + 35)
                {
                    jumpTimer = 0;
                    velocity.Y = -15.0f;
                }
            }


            if (!OnGround && (jumpTimer < jumpStrength) && !dead)
            {
                velocity.Y = -15.0f;
                if (minJump)
                {
                    jumpTimer = jumpStrength;
                    minJump = false;
                }
            }


            if (dead)
            {
                Action = "Dead";
            }
            else if (velocity.X != 0 && OnGround)
            {
                Action = "Run";
            }
            else if (OnGround && velocity.X == 0.0f)
            {
                Action = "Stand";
            }
            else if (velocity.Y < 0.0f)
            {
                Action = "Jump";
            }
            else if (!OnGround && velocity.Y > 2.0f)
            {
                Action = "Fall";
            }
            


            switch (Action)
            {
                case "Dead":
                    myAnimationFlyWeight.gotoAnim(Action, false);
                    break;
                case "Run":
                    myAnimationFlyWeight.gotoAnim(Action, false);
                    myAnimationFlyWeight.FrameSpeed = 0.06f - ((Math.Abs(Velocity.X)) * 0.0025f);
                    break;
                case "Jump":
                    myAnimationFlyWeight.gotoAnim(Action, false);
                    myAnimationFlyWeight.FrameSpeed = 0.08f;
                    break;
                default:
                    myAnimationFlyWeight.gotoAnim(Action, false);
                    break;
            }

            if (stringDirection == "Left")
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effect = SpriteEffects.None;
            }

            if (Velocity.X != 0 && OnGround && (MyAnimationFlyWeight.FrameIndex == 12 || MyAnimationFlyWeight.FrameIndex == 17) && !playedThisStep)
            {
                if (!OnMetal)
                {
                    globals._Sounds["Footstep"].Play(0.2f, 0.0f - 0.25f + ((float)(Maths.random.NextDouble() / 1.0) * 0.5f), 0.0f);
                }
                else
                {
                    globals._Sounds["MetalFootstep"].Play(0.2f, 0.0f - 0.25f + ((float)(Maths.random.NextDouble() / 1.0) * 0.5f), 0.0f);
                }
                playedThisStep = true;
            }
            if (MyAnimationFlyWeight.FrameIndex != 12 && MyAnimationFlyWeight.FrameIndex != 17)
            {
                playedThisStep = false;
            }
            
            position += Velocity;
            velocity.Y += Maths.Gravity;
        }
    }
}
