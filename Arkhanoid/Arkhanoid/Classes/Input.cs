using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Arkhanoid.Classes
{
    static class Input
    {
        public static List<GestureSample> Gestures;
        static Input()
        {
            Gestures = new List<GestureSample>();
        }
        public static Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex)
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState(playerIndex);

            if(playerIndex == PlayerIndex.One)
            {
                if (keyboardState.IsKeyDown(Keys.Left)) direction.X -= 1;
                else if (keyboardState.IsKeyDown(Keys.Right)) direction.X += 1;
                else if (keyboardState.IsKeyDown(Keys.A)) direction.X -= 1;
                else if (keyboardState.IsKeyDown(Keys.D)) direction.X += 1;
            }
            return direction;
        }
    }
}
