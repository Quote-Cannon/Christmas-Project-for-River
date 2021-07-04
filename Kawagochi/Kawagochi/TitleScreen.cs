using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Kawagochi
{
    class TitleScreen : ParentScreen
    {
        protected override Dictionary<string, Button> buttons { get; set; }
        public TitleScreen()
        {
            Initialize();
        }
        public override void Initialize()
        {
            buttons = new Dictionary<string, Button>();
            if (File.Exists(Directory.GetCurrentDirectory() + "/save.txt"))
            {
                buttons.Add("newgame", new Button(110, 110, 0, Game1.empty));
                buttons.Add("continue", new Button(40, 69, 2, Game1.continuegame));
            }
            else
            {
                buttons.Add("newgame", new Button(40, 69, 2, Game1.newgame));
                buttons.Add("continue", new Button(110, 110, 0, Game1.empty));
            }
            buttons.Add("exit", new Button(40, 83, 2, Game1.exit));
        }
        public override void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, Button> kv in buttons)
                kv.Value.Update(Mouse.GetState());
            if (buttons["newgame"].state == "released")
            {
                Game1.load = false;
                Game1.switchScreen("main");
            }
            if (buttons["continue"].state == "released")
            {
                Game1.load = true;
                Game1.switchScreen("main");
            }
            if (buttons["exit"].state == "released")
                Game1.kill = true;
        }
        public override void Draw()
        {
            Game1._spriteBatch.Draw(Game1.background, new Vector2(0, 0), Color.White);
            foreach (KeyValuePair<string, Button> kv in buttons)
                kv.Value.Draw();
            Game1._spriteBatch.Draw(Game1.title, new Rectangle((int)(Game1.screenWidth*0.1), (int)(Game1.screenHeight * 0.1), (int)(Game1.screenWidth * 0.8), (int)(Game1.screenHeight * 0.5)), Color.White);
        }

        public override List<string> getContent()
        {
            return new List<string>();
        }
    }
}