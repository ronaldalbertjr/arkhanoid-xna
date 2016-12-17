using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkhanoid.Classes
{
    class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;

        public virtual void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height); }
        }
        public static bool CheckCollision(GameObject player, Ball bola)
        {
            if (player.Bounds.Intersects(bola.Bounds))
                return true;
            else
                return false;
        }
    }
}
