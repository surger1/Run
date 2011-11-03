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
    static class _Keyboard
    {
        // Get input state.
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        static public void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            
        }

        static public bool KeyDown(Keys theKey)
        {
            return keyboardState.IsKeyDown(theKey);
        }

        static public bool KeyPress(Keys theKey)
        {
            if (!lastKeyboardState.IsKeyDown(theKey) && keyboardState.IsKeyDown(theKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool KeyReleased(Keys theKey)
        {
            if (lastKeyboardState.IsKeyDown(theKey) && !keyboardState.IsKeyDown(theKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
}
