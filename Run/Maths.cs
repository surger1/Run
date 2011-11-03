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
    public static class Maths
    {
        public static Random random;
        public static float Gravity = 1.0f;
        public const int PixelsInAMeter = 150;

        public static void Initialize()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Used to find the distance between two points
        /// </summary>
        /// <param name="a">The first point</param>
        /// <param name="b">The second point</param>
        /// <returns>Returns the distance as a float</returns>
        public static float distance(Vector2 a, Vector2 b)
        {
            float dis = (float)Math.Sqrt(sqr(a.X - b.X) + sqr(a.Y - b.Y));
            return dis;
        }

        /// <summary>
        /// Used to find the square of a number
        /// </summary>
        /// <param name="num">The number to be squared</param>
        /// <returns>The square of the number</returns>
        public static float sqr(float num)
        {
            return num * num;
        }

        public static float slope(Vector2 a, Vector2 b)
        {
            return ((b.Y - a.Y) / (b.X - a.X));
            //y = mx + b
            //b = (m * a.X - a.Y) * (-1);
        }

        public static float angle(Vector2 a, Vector2 b, bool flip)
        {
            if (flip)
            {
                a.Y *= -1.0f;
                b.Y *= -1.0f;
            }

            float angl = (float)(Math.Atan2((double)b.Y - (double)a.Y, (double)b.X - (double)a.X));
            return angl;
        }

        public static float angle(Vector2 a, Vector2 b)
        {
            

            float angl = (float)(Math.Atan2((double)b.Y - (double)a.Y, (double)b.X - (double)a.X));
            
            return angl;
        }

        public static Vector2 pointOnACircle(float distance, float angle)
        {
            Vector2 ret = new Vector2();
            ret.X = (float)((double)distance * Math.Cos((double)angle));
            ret.Y = (float)((double)distance * Math.Sin((double)angle));
            return ret;
        }

        public static bool RaySphereIntersect(Vector2 p1, Vector2 p2, Vector2 c, float r)
        {
            Vector2 dir = p2 - p1;
            Vector2 diff = c - p1;
            float t = DotProduct(diff, dir) / DotProduct(dir, dir);
            if (t < 0.0f)
                t = 0.0f;
            if (t > 1.0f)
                t = 1.0f;
            Vector2 closest = p1 + t * dir;
            Vector2 d = c - closest;
            float distsqr = DotProduct(d, d);
            return distsqr <= r * r;
        }

        public static float DotProduct(Vector2 vec1, Vector2 vec2)
        {
            return (vec1.X * vec2.X) + (vec1.Y * vec2.Y);
        }

        public static float clampAngle(float ang)
        {
            float amnt = ang;
            while (amnt > (float)(Math.PI * 2.0))
            {
                amnt -= (float)(Math.PI * 2.0);
            }

            while (amnt < 0.0f)
            {
                amnt += (float)(Math.PI * 2.0);
            }
            return amnt;
        }

        public static float randomFloat(float ceiling)
        {
            return (float)(random.NextDouble()) * ceiling;
        }

        public static bool pointInRegion(Vector2 point, Vector4 rect)
        {
            if (point.X > rect.X && point.X < rect.X + rect.W && point.Y > rect.Y && point.Y < rect.Y + rect.Z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool regionXCompare(Vector4 rectA, Vector4 rectB)
        {
            if((rectA.X >= rectB.X && rectA.X <= rectB.X + rectB.W) || (rectA.X + rectA.W >= rectB.X && rectA.X + rectA.W <= rectB.X + rectB.W))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool regionYCompare(Rectangle rectA, Rectangle rectB)
        {
            if ((rectA.Y >= rectB.Y && rectA.Y <= rectB.Y + rectB.Height) || (rectA.Y + rectA.Height >= rectB.Y && rectA.Y + rectA.Height <= rectB.Y + rectB.Height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool regionInRegion(Vector4 rectA, Vector4 rectB)
        {
            if(pointInRegion(new Vector2(rectA.X,rectA.Y),rectB))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectA.X + rectA.W, rectA.Y), rectB))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectA.X, rectA.Y + rectA.Z), rectB))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectA.X + rectA.W, rectA.Y + rectA.Z), rectB))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectB.X, rectB.Y), rectA))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectB.X + rectB.W, rectB.Y), rectA))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectB.X, rectB.Y + rectB.Z), rectA))
            {
                return true;
            }
            if (pointInRegion(new Vector2(rectB.X + rectB.W, rectB.Y + rectB.Z), rectA))
            {
                return true;
            }
            return false;
        }
    }
}
