using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Kawagochi
{
    class GameScreen : ParentScreen
    {
        protected override Dictionary<string, Button> buttons { get; set; }
        private Gochi gochi;
        Random rnd = new Random();
        public GameScreen()
        {
            Initialize();
        }

        public override void Initialize()
        {
            List<string[]> faces = new List<string[]>();
            faces.Add(new string[] { ":^)", ":^|", ":^(", ":^D" });
            faces.Add(new string[] { "^_^", "-_-", "v_v", "^v^" });
            faces.Add(new string[] { ":)", ":|", ":(", ":D" });
            faces.Add(new string[] { "8)", "8|", "8(", "8D" });
            faces.Add(new string[] { "x)", "x|", "x(", "xD" });
            faces.Add(new string[] { ".w.", "~_~", "T_T", "\\0o0/" });
            buttons = new Dictionary<string, Button>();
            buttons.Add("exit", new Button(90, 94, 1, Game1.exit));
            buttons.Add("borger", new Button(47, 80, 0.2, Game1.borger));
            gochi = new Gochi(35, 25, 3, faces[rnd.Next(faces.Count)]);
        }

        public override void Draw()
        {
            Game1._spriteBatch.Draw(Game1.background, new Vector2(0, 0), Color.White);
            foreach (KeyValuePair<string, Button> kv in buttons)
                kv.Value.Draw();
            gochi.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, Button> kv in buttons)
                kv.Value.Update(Mouse.GetState());
            if (buttons["exit"].state == "released")
                Game1.kill = true;
            if (buttons["borger"].state == "released")
                gochi.feed();
            gochi.Update(gameTime);
        }

        public override List<string> getContent()
        {
            List<string> output = gochi.getContent();
            return output;
        }
    }
}
