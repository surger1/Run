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
    public static class Camera
    {
        public static Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        static Vector2 position;

        public static float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        static float rotation;

        public static float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        static float scale;

        static float boundary;

        static public Color BackColor;

        public static void Initialize()
        {
            position = new Vector2(0, 0);
            scale = 1.0f;
            boundary = 75.0f;
            BackColor = Color.SkyBlue;
        }

        static int shakeTime;
        static int timeShaking;
        static float shakeSeverity;
        static float shakeSpeed;
        static float shakeProgress;
        static Vector2 ShakeAdd;

        static int flashLength;
        static int flashingFor;
        static Color flashColor;
        static bool flashFade;
        static bool flashFadeOut;
        static float flashAlpha;

        public static void shake(int ShakeTime, float ShakeSeverity, float ShakeSpeed)
        {
            timeShaking = 0;
            shakeProgress = 0;
            shakeSpeed = ShakeSpeed;
            shakeSeverity = ShakeSeverity;
            shakeTime = ShakeTime;
        }

        public static void flash(int flashLen,Color flashCol,bool fade,bool fadeOut)
        {
            flashFade = fade;
            flashColor = flashCol;
            flashLength = flashLen;
            flashingFor = 0;
            flashFadeOut = fadeOut;
            flashAlpha = (float)flashColor.A;
        }
        

        private static Vector2 ApplyTransformations(Vector2 pos)
        {
            Vector2 adjustedPosition = new Vector2(position.X + (globals._Graphics.GraphicsDevice.Viewport.Width / -2.0f / scale), position.Y + (globals._Graphics.GraphicsDevice.Viewport.Height / -2.0f / scale));
            Vector2 finalPosition = (pos - adjustedPosition) + ShakeAdd;// - (position / 2 * scale)) * scale); 
            // you can apply scaling and rotation here also
            //.....
            //--------------------------------------------
            return new Vector2((float)Math.Round((float)finalPosition.X), (float)Math.Round((float)finalPosition.Y));
        }

        public static void Draw(Texture2D texture, Vector2 pos, Rectangle? sourceRectangle, Color color, float rot, Vector2 origin, float scal, SpriteEffects effects, float layerDepth)
        {
            //scale -= 0.0001f;
            //rotation += 0.01f;
            Vector2 drawPosition = ApplyTransformations(pos);


            globals._SpriteBatch.Draw(texture, drawPosition, sourceRectangle, color, rot, origin, scal, effects, layerDepth);
        }

        public static void Draw(Texture2D texture, Vector2 pos, Rectangle? sourceRectangle, Color color, float rot, Vector2 origin, float scal, SpriteEffects effects, float layerDepth, float cameraScale)
        {
            //scale -= 0.0001f;
            //rotation += 0.01f;
            scale = cameraScale;
            Vector2 drawPosition = ApplyTransformations(pos);
            scale = 1.0f;

            globals._SpriteBatch.Draw(texture, drawPosition, sourceRectangle, color, rot, origin, scal, effects, layerDepth);
        }

        public static void Draw(Texture2D texture, Vector2 pos, Rectangle? sourceRectangle, Vector2 origin )
        {
            globals._SpriteBatch.Draw(texture, pos, sourceRectangle, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);
        }

        public static void Update(GameTime gameTime)
        {
            if (timeShaking < shakeTime)
            {
                timeShaking += gameTime.ElapsedGameTime.Milliseconds;
                ShakeAdd = new Vector2(shakeSeverity *(float)Math.Sin(shakeProgress),0.0f);
                shakeProgress += shakeSpeed;
            }
            else
            {
                ShakeAdd = new Vector2();
            }

            if (flashingFor < flashLength)
            {
                flashingFor += gameTime.ElapsedGameTime.Milliseconds;
                if (flashFade)
                {
                    if (!flashFadeOut)
                    {
                        flashColor = new Color(flashColor, (byte)(flashAlpha - (flashAlpha * (double)((double)flashingFor / (double)flashLength))));
                    }
                    else
                    {
                        flashColor = new Color(flashColor, (byte)(flashAlpha * (double)((double)flashingFor / (double)flashLength)));
                    }
                }
            }
        }

        public static void DrawFlash(GameTime gameTime)
        {
            if (flashingFor < flashLength)
            {
                globals.DrawRectangle(gameTime, Position - new Vector2(10000,10000), new Vector2(40000,40000), flashColor, false);
            }
        }

        public static void Draw(Texture2D texture, Vector2 pos, Color color)
        {
            //scale -= 0.0001f;
            //rotation += 0.01f;
            Vector2 drawPosition = ApplyTransformations(pos);


            globals._SpriteBatch.Draw(texture, drawPosition, color);
        }
    }
}
