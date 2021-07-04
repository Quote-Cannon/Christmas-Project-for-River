using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Kawagochi
{
    class Gochi
    {
        private Point location;
        private string[] textures;
        private string mood;
        private float scale;
        public double hunger, sleep, fun;
        private Color hungerColor, sleepColor, funColor;
        private MouseState lastState = Mouse.GetState();
        private double feedCD = 0;

        public Gochi(float x, float y, float s, string[] faces)
        {
            location = new Point((int)(x / 100 * Game1.screenWidth), (int)(y / 100 * Game1.screenHeight));
            textures = faces;
            scale = (int)(s / 1920 * Game1.screenWidth);
            Initialise();
        }

        public void Initialise()
        {
            if (Game1.load)
            {
                DateTime loadTime = DateTime.Now;
                string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "/save.txt");
                DateTime timeSaved = DateTime.Parse(input[3] + " " + input[4]);
                hunger = Math.Max(0, double.Parse(input[0]) - (int)((loadTime - timeSaved).TotalMinutes / 30));
                fun = Math.Max(0, double.Parse(input[1]) - (int)((loadTime - timeSaved).TotalMinutes / 15));
                sleep = Math.Min(100, double.Parse(input[2]) + (loadTime - timeSaved).TotalMinutes);
                updateMood();
                return;
            }
            hunger = 25;
            fun = 0;
            sleep = 100;
            updateMood();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            if (ks.IsKeyDown(Keys.D1))
                hunger = 100;
            if (ks.IsKeyDown(Keys.Q))
                hunger = 75;
            if (ks.IsKeyDown(Keys.A))
                hunger = 50;
            if (ks.IsKeyDown(Keys.Z))
                hunger = 25;
            if (ks.IsKeyDown(Keys.D2))
                fun = 100;
            if (ks.IsKeyDown(Keys.W))
                fun = 75;
            if (ks.IsKeyDown(Keys.S))
                fun = 50;
            if (ks.IsKeyDown(Keys.X))
                fun = 25;
            if (ks.IsKeyDown(Keys.D3))
                sleep = 100;
            if (ks.IsKeyDown(Keys.E))
                sleep = 75;
            if (ks.IsKeyDown(Keys.D))
                sleep = 50;
            if (ks.IsKeyDown(Keys.C))
                sleep = 25;
            if (Math.Sqrt((lastState.X - ms.X) ^ 2 + (lastState.Y - ms.Y) ^ 2) > 6)
                fun += 0.5;
            sleep -= gameTime.ElapsedGameTime.TotalSeconds/9;
            if (feedCD > 0)
                feedCD -= gameTime.ElapsedGameTime.TotalSeconds;
            updateMood();
            lastState = ms;
        }

        public void feed()
        {
            if (feedCD <= 0)
            {
                feedCD = 1;
                hunger += 10;
            }
        }

        public void updateMood()
        {
            hunger = Math.Clamp(hunger, 0, 100);
            fun = Math.Clamp(fun, 0, 100);
            sleep = Math.Clamp(sleep, 0, 100);
            hungerColor = new Color((int)Math.Min(510 - hunger * 5.1f, 255), (int)Math.Min(hunger * 5.1f, 255), 0);
            funColor = new Color((int)Math.Min(510 - fun * 5.1f, 255), (int)Math.Min(fun * 5.1f, 255), 0);
            sleepColor = new Color((int)Math.Min(510 - sleep * 5.1f, 255), (int)Math.Min(sleep * 5.1f, 255), 0);
            switch (hunger + sleep + fun)
            {
                case var _ when (hunger + sleep + fun) >= 200 && (hunger + sleep + fun) < 250:
                    mood = textures[0];
                    break;
                case var _ when (hunger + sleep + fun) < 125:
                    mood = textures[2];
                    break;
                case var _ when (hunger + sleep + fun) >= 250:
                    mood = textures[3];
                    break;
                case var _ when (hunger + sleep + fun) >= 125 && (hunger + sleep + fun) < 200:
                    mood = textures[1];
                    break;
            }
        }

        public void Draw()
        {
            Game1._spriteBatch.DrawString(Game1.defaultFont, mood, new Vector2(location.X, location.Y), Color.Black, 0, Vector2.Zero, scale, new SpriteEffects(), 0);
            Game1._spriteBatch.Draw(Game1.empty, new Rectangle((int)(Game1.screenWidth * 0.1), Game1.screenHeight / 10, (int)(3.2f * hunger), 32), hungerColor);
            Game1._spriteBatch.Draw(Game1.empty, new Rectangle((int)(Game1.screenWidth * (0.8 / 3 + 0.1)), Game1.screenHeight / 10, (int)(3.2f * fun), 32), funColor);
            Game1._spriteBatch.Draw(Game1.empty, new Rectangle((int)(Game1.screenWidth * (1.6 / 3 + 0.1)), Game1.screenHeight / 10, (int)(3.2f * sleep), 32), sleepColor);
            Game1._spriteBatch.DrawString(Game1.defaultFont, "Hunger:", new Vector2(Game1.screenWidth * 0.1f, Game1.screenHeight / 15), Color.Black, 0, Vector2.Zero, 0.2f, new SpriteEffects(), 0);
            Game1._spriteBatch.DrawString(Game1.defaultFont, "Fun:", new Vector2(Game1.screenWidth * (0.8f / 3 + 0.1f), Game1.screenHeight / 15), Color.Black, 0, Vector2.Zero, 0.2f, new SpriteEffects(), 0);
            Game1._spriteBatch.DrawString(Game1.defaultFont, "Sleep:", new Vector2(Game1.screenWidth * (1.6f / 3 + 0.1f), Game1.screenHeight / 15), Color.Black, 0, Vector2.Zero, 0.2f, new SpriteEffects(), 0);
            if (feedCD < 1)
            {
                int fuck = 120;
                Game1._spriteBatch.Draw(Game1.borger, new Rectangle(Game1.screenWidth/2-(int)(0.1*Game1.borger.Width * feedCD), Game1.screenHeight/2+(int)(fuck*(feedCD * feedCD) +(fuck*1.5)*feedCD - 0.1 * Game1.borger.Height * feedCD), (int)(Game1.borger.Width*feedCD*0.2), (int)(Game1.borger.Height * feedCD*0.2)), Color.White);
            }
            else
                Game1._spriteBatch.Draw(Game1.borger, new Rectangle(-100, -100, 0, 0), Color.White);
        }

        public List<string> getContent()
        {
            return new List<string> { hunger.ToString(), fun.ToString(), sleep.ToString(), DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString() };
        }
    }
}
