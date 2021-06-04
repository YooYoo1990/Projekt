using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Spelare
    {
        Vector2 position;
        Texture2D texture;
        int fireRate = 250;
        int senasteskott = 0;
        Rectangle Box_runt;
        int speed = 3;

        public Spelare(Texture2D newTexture, Vector2 newPosition, int newSpeed)
        {
            position = newPosition;
            texture = newTexture;
            speed = newSpeed;
            Box_runt = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Rectangle Box_Runt
        {
            set { Box_runt = value; }
            get { return Box_runt; }
        }

        public int FireRate
        {
            set { fireRate = value; }
            get { return fireRate; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public int SenasteSkott
        {
            set { senasteskott = value; }
            get { return senasteskott; }
        }

        public void FlyttaPosX(int direction)
        {
            position.X += this.speed * direction;
            Box_runt.Location = position.ToPoint();
        }

        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }

        public Texture2D Texture
        {
            set { texture = value; }
            get { return texture; }
        }

        public int Speed
        {
            set { speed = value; }
            get { return speed; }
        }

    }
}
