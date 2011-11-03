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
    public class Player : LiveThing
    {
        protected ulong jumpTimer;
        protected ulong ledgeTimer;
        protected const ulong ledgeTime = 100;
        public bool parkour;
        public bool ledgeGrab;
        public bool OnMissle;
        public static float accel = 0.1f;
        public static float topSpeed = 15.0f;
        public static ulong jumpStrength = 100;
        public static float ledgeSpeed = 7.0f;
        public static int AccelLevel = 0;
        public static int SpeedLevel = 0;
        public static int JumpLevel = 0;
        public static int ParkLevel = 0;
        public static float baseAccel = 0.1f;
        public static float baseSpeed = 15.0f;
        public static ulong baseJump = 100;
        public static float basePark = 7.0f;
        public static float AddAccel = 0.025f;
        public static float AddSpeed = 0.8f;
        public static ulong AddJump = 12;
        public static float AddPark = 4.0f;
        public bool LoseControl;
        public static int DistanceExperience = 0;
        public bool playedThisStep;
        public bool OnMetal;

        bool doubleJumped;
        

        public Player(string spriteName, Vector2 pos, int mid)
        {
            base.Initialize(spriteName, pos, mid);
            jumpTimer = 501;
            ledgeTimer = 50;
            ledgeGrab = false;
            parkour = true;
            OnMissle = false;
            LoseControl = false;
            accel = baseAccel + (AccelLevel * AddAccel);
            topSpeed = baseSpeed + (SpeedLevel * AddSpeed);
            jumpStrength = baseJump + ((ulong)JumpLevel * AddJump);
            ledgeSpeed = basePark + (ParkLevel * AddPark);
        }

        public override void Update(GameTime gameTime)
        {
            myAnimationFlyWeight.Update(gameTime, 1.0f);
            if (!OnMissle)
            {
                position += velocity;
                
            }
            else
            {
                doubleJumped = false;
            }
            jumpTimer += (ulong)gameTime.ElapsedGameTime.Milliseconds;
            ledgeTimer += (ulong)gameTime.ElapsedGameTime.Milliseconds;

            if ((_Keyboard.KeyPress(Keys.Space) || _Controller.ButtonPress(Buttons.A) ) && !doubleJumped && !OnGround && !LoseControl && ApocalypseParkour.DoubleJumpEnabled)
            {
                jumpTimer = 0;
                doubleJumped = true;
            }

            if ((_Keyboard.KeyDown(Keys.Right) || LoseControl || (_Controller.getThumbStickDirection(ThumbStick.Left) < Math.PI / 2.0f || _Controller.getThumbStickDirection(ThumbStick.Left) > Math.PI * 1.5f) && _Controller.getThumbStickForce(ThumbStick.Left) > 0.25f) && OnGround)
            {
                if (velocity.X < 0)
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
                doubleJumped = false;
               
            }

            if ((_Keyboard.KeyDown(Keys.Left) || (_Controller.getThumbStickDirection(ThumbStick.Left) >= Math.PI / 2.0f && _Controller.getThumbStickDirection(ThumbStick.Left) <= Math.PI * 1.5f) && _Controller.getThumbStickForce(ThumbStick.Left) > 0.25f)  && OnGround)
            {

                if (velocity.X > 0)
                {
                    velocity.X -= accel + 0.4f;
                }
                else
                {
                    velocity.X -= accel;
                    stringDirection = "Left";
                }
                if (velocity.X < topSpeed * -1.0f)
                {
                    velocity.X = topSpeed * -1.0f;
                }
                
            }

            if ((_Keyboard.KeyPress(Keys.Space) || _Controller.ButtonPress(Buttons.A)) && (OnGround || OnMissle) && !LoseControl)
            {
                if (jumpTimer > jumpStrength + 35)
                {
                    jumpTimer = 0;
                }
                if (OnMissle)
                {
                    OnMissle = false;
                    Velocity = new Vector2(20.0f, Velocity.Y);
                    
                }
            }

            if (!ledgeGrab)
            {
                ledgeTimer = 0;
            }

            if (ledgeGrab)
            {
                float num = 0.0f;
                if(stringDirection == "Right")
                {

                    num = ledgeSpeed;
                }
                else
                {
                    num = ledgeSpeed * -1.0f;
                }
                Velocity = new Vector2(num, -10.0f);
                doubleJumped = false;
            }

            if ((_Keyboard.KeyDown(Keys.Space) || _Controller.ButtonDown(Buttons.A)) && (jumpTimer < jumpStrength))
            {
                velocity.Y = -15.0f;
            }

            if (_Keyboard.KeyReleased(Keys.Space) || _Controller.ButtonReleased(Buttons.A))
            {
                jumpTimer = 500;
            }
            


            if (!_Keyboard.KeyDown(Keys.Right) && !_Keyboard.KeyDown(Keys.Left) && _Controller.getThumbStickForce(ThumbStick.Left) < 0.25f && OnGround && !LoseControl)
            {
                if (Math.Abs(velocity.X) < 0.3)
                {
                    velocity.X = 0;
                }
                if (velocity.X > 0)
                {
                    velocity.X -= 0.3f;
                }

                if (velocity.X < 0)
                {
                    velocity.X += 0.3f;
                }
            }

            if (velocity.X != 0 && OnGround)
            {
                Action = "Run";
            }
            else if(OnGround && velocity.X == 0.0f)
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

            if (!ledgeGrab)
            {
                velocity.Y += Maths.Gravity;
            }
            else
            {
                if (ledgeTimer >= ledgeTime)
                {
                    ledgeGrab = false;
                }
            }
            if (!LoseControl)
            {
                Camera.Position = new Vector2(position.X + 300, position.Y - 100);
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
        }
    }
}
