using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Kawagochi
{
    class Button
    {

        public string state = "none";

        private Rectangle hitbox;
        private Texture2D texture;
        private Dictionary<string, Color> _colors;
        private MouseState lastState = Mouse.GetState();

        public Button(double x, double y, double scale, Texture2D t)
        {
            hitbox = new Rectangle((int)(x / 100 * Game1.screenWidth), (int)(y / 100 * Game1.screenHeight), (int)(scale * t.Width / 1920 * Game1.screenWidth), (int)(scale * t.Height / 1080 * Game1.screenHeight));
            texture = t;
            _colors = new Dictionary<string, Color>
        {
            { "none", Color.White },
            { "hover", Color.IndianRed },
            { "pressed", Color.LawnGreen },
            {"released", Color.MediumPurple }
        };
        }

        public void Update(MouseState mouseState)
        {
            if (hitbox.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released)
                    state = "pressed";
                else if (mouseState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed && state == "pressed")
                    state = "released";
                else if (mouseState.LeftButton == lastState.LeftButton && state != "pressed") state = "hover";
            }
            else
            {
                state = "none";
            }
            if (state == "released")
            { }
            lastState = mouseState;
        }

        // Make sure Begin is called on s before you call this function
        public void Draw()
        {
            Game1._spriteBatch.Draw(texture, hitbox, _colors[state]);
        }

    }
}
