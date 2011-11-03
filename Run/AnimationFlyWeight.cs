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
    public class AnimationFlyWeight
    {
        #region memberVariables
        private float time;

        public AnimationFlyWeight(String SprName)
        {
            Initialize(SprName);
        }

        public void Initialize(String SprName)
        {
            SpriteName = SprName;
            CurrentAnimation = "default";
            FrameIndex = 0;
            frameSpeed = 0.06f;
        }

        public String CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }
        String currentAnimation;

        public String SpriteName
        {
            get { return spriteName; }
            set { spriteName = value; }
        }
        String spriteName;

        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value; }
        }
        int frameIndex;

        public float FrameSpeed
        {
            get { return frameSpeed; }
            set
            {
                if (value > 0)
                {
                    frameSpeed = value;
                }
                else
                {
                    frameSpeed = 0.001f;
                }
            }
        }
        float frameSpeed;
        #endregion

        /// <summary>
        /// The name of the animation to go to.
        /// </summary>
        /// <param name="next">the name of the animation</param>
        /// <param name="reset">if this is true and the animation</param>
        public void gotoAnim(string next, bool reset)
        {
            if ((next != this.currentAnimation) || reset)
            {
                if (globals._Sprites[SpriteName].AnimationSets.ContainsKey(next))
                {
                    //try
                    //{
                    frameIndex = globals._Sprites[SpriteName].AnimationSets[next].start;
                    currentAnimation = next;
                    // }
                    //catch (Exception ex)
                    // {
                    //     frameIndex = globals._Sprites[SpriteName].AnimationSets["default"].start;
                    //    currentAnimation = "default";
                    // }
                }
                else
                {
                    frameIndex = globals._Sprites[SpriteName].AnimationSets["default"].start;
                    currentAnimation = "default";
                }
            }
        }

        /// <summary>
        /// Advances the current animation and performs the determined action
        /// </summary>
        /// <param name="gameTime">the time spent so far in game</param>
        public void Update(GameTime gameTime, float timeModifier)
        {
           
            float frmSpeed = ((float)(frameSpeed) / timeModifier);
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > frmSpeed)
            {
                time -= frmSpeed;

                // Advance the frame index; looping or clamping as appropriate.
                if (frameIndex == globals._Sprites[SpriteName].AnimationSets[currentAnimation].end)
                {
                    if (globals._Sprites[SpriteName].AnimationSets[currentAnimation].loop)
                    {
                        frameIndex = globals._Sprites[SpriteName].AnimationSets[currentAnimation].start;// % Animation.FrameCount;
                    }
                    else
                    {
                        if (globals._Sprites[SpriteName].AnimationSets[currentAnimation].next != "")
                        {
                            currentAnimation = globals._Sprites[SpriteName].AnimationSets[currentAnimation].next;
                            frameIndex = globals._Sprites[SpriteName].AnimationSets[currentAnimation].start;
                        }
                    }
                }
                else
                {
                    ++frameIndex;
                }
            }
        }

        public void Draw(GameTime gameTime, Vector2 position)
        {
            globals._Sprites[SpriteName].Draw(gameTime, position, frameIndex);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, float scale)
        {
            globals._Sprites[SpriteName].Draw(gameTime, position, spriteEffects, depth, col, frameIndex, scale);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, float scale,float CameraScale)
        {
            globals._Sprites[SpriteName].Draw(gameTime, position, spriteEffects, depth, col, frameIndex, scale,CameraScale);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, float scale, Vector2 stretch, bool UI)
        {
            globals._Sprites[SpriteName].Draw(gameTime, position, spriteEffects, depth, col, frameIndex, scale, stretch, UI);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, bool drawAtOrigin, float scale)
        {
            globals._Sprites[SpriteName].Draw(gameTime, position, spriteEffects, depth, col, frameIndex, drawAtOrigin, scale);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, bool drawAtOrigin, bool drawAsUI, float scale)
        {
            globals._Sprites[SpriteName].Draw(gameTime, position, spriteEffects, depth, col, frameIndex, drawAtOrigin, drawAsUI, scale);
        }

    }
}
