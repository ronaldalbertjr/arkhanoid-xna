using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkhanoid.Classes
{
    class Player:GameObject
    {
        public Vector2 Velocity;
        public int blocksDestroyed = 0;
        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            if (Position.X <= 0) Position.X = 0;
            else if(Position.X + Texture.Width >= Game1.ScreenWidth)
            {
                Position.X = Game1.ScreenWidth - Texture.Width;
            }
        }
    }
}
