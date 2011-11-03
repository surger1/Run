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
    class Sprite
    {
        //enum onEnd { loop, freeze, change }
        #region memberVariables

        public int Frames
        {
            get { return frames; }
        }
        int frames;

        public int Width
        {
            get { return width; }
        }
        int width;

        public int Height
        {
            get { return height; }
        }
        int height;

        public Vector2 CollisionPoint
        {
            get { return collisionPoint; }
        }
        Vector2 collisionPoint;

        public Vector2 CenterPoint
        {
            get { return centerPoint; }
            set { centerPoint = value; }
        }
        Vector2 centerPoint;

        public float CollisionRadius
        {
            get { return collisionRadius; }
        }
        float collisionRadius;

        public string Path
        {
            get { return path; }
        }
        string path;

        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;

        public Dictionary<String, Animation> AnimationSets
        {
            get { return animationSets; }
        }
        Dictionary<String, Animation> animationSets;

        #endregion

        /// <summary>
        /// Adds an animation set to the animation
        /// </summary>
        /// <param name="begin">the beginning cell of the animation</param>
        /// <param name="end">the end cell of the animation</param>
        /// <param name="loop">wether the animation loops or not</param>
        /// <param name="name">name of the animation</param>
        /// <param name="next">the name of the animation to skip to</param>
        public void addSet(string name, int begin, int end, bool loop, string next)
        {
            animationSets.Add(name, new Animation(begin, end, loop, next));
        }
        /// <summary>
        /// the sprite sheet to load in
        /// </summary>
        /// <param name="p">the path of the sprite sheet</param>
        /// <param name="w">the width of a sprite cell</param>
        /// <param name="h">the height of a sprite cell</param>
        public Sprite(string p, int w, int h)
        {
            this.path = p;
            this.width = w;
            this.height = h;
            this.animationSets = new Dictionary<String, Animation>();
            this.animationSets.Clear();
            collisionRadius = (float)(w) / 4.0f;//,h / 5);
            collisionPoint = new Vector2(w - (w / 2), h - (w / 2));
            centerPoint = new Vector2(w / 2, h / 2);
        }
        /// <summary>
        /// Loads the sprite sheet into memory
        /// </summary>
        public void LoadContent()
        {
            this.texture = globals._Content.Load<Texture2D>(path);
            this.frames = (this.texture.Width / this.width) * (this.texture.Height / this.Height);
            this.addSet("default", 0, 0, false, "");
        }


        public void Draw(GameTime gameTime, Vector2 position, int frameIndex)
        {

            // Calculate the source rectangle of the current frame.
            int xx = (((frameIndex % (texture.Width / width))) * width);
            int yy = ((frameIndex - (frameIndex % (texture.Width / width))) / (texture.Width / width)) * height;
            Rectangle source = new Rectangle(xx, yy, width, height);
            // Draw the current frame.
            Vector2 Origin = new Vector2(0, 0);

            Camera.Draw(texture, position, source, Origin);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, int frameIndex, float Scale, Vector2 stretch,bool UI)
        {

            // Calculate the source rectangle of the current frame.
            int xx = (((frameIndex % (texture.Width / width))) * width);
            int yy = ((frameIndex - (frameIndex % (texture.Width / width))) / (texture.Width / width)) * height;
            Rectangle source = new Rectangle(xx, yy, width * (int)stretch.X, height * (int)stretch.Y);
            // Draw the current frame.
            Vector2 Origin = new Vector2(0, 0);

            

            if (UI)
            {
                globals._SpriteBatch.Draw(texture, position, source, col, 0.0f, Origin, 1.0f, spriteEffects, depth);
            }
            else
            {
                Camera.Draw(texture, position, source, col, 0.0f, Origin, Scale, spriteEffects, depth);
            }
        }
        /// <summary>
        /// Draw the things to the screen
        /// </summary>
        /// <param name="gameTime">required</param>
        /// <param name="spriteBatch">required</param>
        /// <param name="position">the position the sprite should be drawn on the screen</param>
        /// <param name="spriteEffects">required</param>
        /// <param name="depth">the z positon of the sprite</param>
        /// <param name="col">the colour the sprite should be drawn in</param>
        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, int frameIndex, float Scale)
        {

            // Calculate the source rectangle of the current frame.
            int xx = (((frameIndex % (texture.Width / width))) * width);
            int yy = ((frameIndex - (frameIndex % (texture.Width / width))) / (texture.Width / width)) * height;
            Rectangle source = new Rectangle(xx, yy, width, height);
            // Draw the current frame.
            Vector2 Origin = new Vector2(0, 0);

            Camera.Draw(texture, position, source, col, 0.0f, Origin, Scale, spriteEffects, depth);
        }

        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, int frameIndex, float Scale,float cameraScale)
        {

            // Calculate the source rectangle of the current frame.
            int xx = (((frameIndex % (texture.Width / width))) * width);
            int yy = ((frameIndex - (frameIndex % (texture.Width / width))) / (texture.Width / width)) * height;
            Rectangle source = new Rectangle(xx, yy, width, height);
            // Draw the current frame.
            Vector2 Origin = new Vector2(0, 0);

            Camera.Draw(texture, position, source, col, 0.0f, Origin, Scale, spriteEffects, depth, cameraScale);
        }
        /// <summary>
        /// Draw the things to the screen
        /// </summary>
        /// <param name="gameTime">required</param>
        /// <param name="spriteBatch">required</param>
        /// <param name="position">the position the sprite should be drawn on the screen</param>
        /// <param name="spriteEffects">required</param>
        /// <param name="depth">the z positon of the sprite</param>
        /// <param name="col">the colour the sprite should be drawn in</param>
        /// <param name="drawAtCenter">describes wether the origin is the top left or the center</param>
        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, int frameIndex, bool drawAtCenter, float Scale)
        {

            // Calculate the source rectangle of the current frame.
            int xx = (((frameIndex % (texture.Width / width))) * width);
            int yy = ((frameIndex - (frameIndex % (texture.Width / width))) / (texture.Width / width)) * height;
            Rectangle source = new Rectangle(xx, yy, width, height);
            // Draw the current frame.
            Vector2 Origin;
            if (drawAtCenter)
            {
                Origin = centerPoint;
            }
            else
            {
                Origin = new Vector2(0, 0);
            }
            Camera.Draw(texture, position, source, col, 0.0f, Origin, 1.0f, spriteEffects, depth);
        }

        /// <summary>
        /// Draw the things to the screen
        /// </summary>
        /// <param name="gameTime">required</param>
        /// <param name="spriteBatch">required</param>
        /// <param name="position">the position the sprite should be drawn on the screen</param>
        /// <param name="spriteEffects">required</param>
        /// <param name="depth">the z positon of the sprite</param>
        /// <param name="col">the colour the sprite should be drawn in</param>
        /// <param name="drawAtCenter">describes wether the origin is the top left or the center</param>
        /// <param name="drawAsUI">if true the image will be drawn to the screen ignoring the camera</param>
        public void Draw(GameTime gameTime, Vector2 position, SpriteEffects spriteEffects, float depth, Color col, int frameIndex, bool drawAtCenter, bool drawAsUI, float Scale)
        {

            // Calculate the source rectangle of the current frame.
            int xx = (((frameIndex % (texture.Width / width))) * width);
            int yy = ((frameIndex - (frameIndex % (texture.Width / width))) / (texture.Width / width)) * height;
            Rectangle source = new Rectangle(xx, yy, width, height);
            // Draw the current frame.
            Vector2 Origin;
            if (drawAtCenter)
            {
                Origin = centerPoint;
            }
            else
            {
                Origin = new Vector2(0, 0);
            }

            if (drawAsUI)
            {
                globals._SpriteBatch.Draw(texture, position, source, col, 0.0f, Origin, 1.0f, spriteEffects, depth);
            }
            else
            {
                Camera.Draw(texture, position, source, col, 0.0f, Origin, 1.0f, spriteEffects, depth);
            }
        }
        public void DrawPoint(GameTime gameTime, Vector2 position, Color col)
        {
            Vector2 Origin = new Vector2(0, 0);
            Camera.Draw(texture, position, col);
        }
    }
}
