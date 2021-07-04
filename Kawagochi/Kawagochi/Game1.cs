using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Kawagochi
{
    public class Game1 : Game
    {
        static GraphicsDeviceManager _graphics;
        public static int screenWidth = 1920, screenHeight = 1080;
        public static SpriteBatch _spriteBatch;
        static ParentScreen currentScreen;
        public static Texture2D test, empty, newgame, continuegame, settings, exit, background, title, borger;
        public static SpriteFont defaultFont;
        public static bool kill = false, load;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            test = Content.Load<Texture2D>("franknife");
            empty = Content.Load<Texture2D>("pixel");
            newgame = Content.Load<Texture2D>("newgame");
            continuegame = Content.Load<Texture2D>("continue");
            settings = Content.Load<Texture2D>("settings");
            exit = Content.Load<Texture2D>("exit");
            background = Content.Load<Texture2D>("background");
            defaultFont = Content.Load<SpriteFont>("defaultFont");
            title = Content.Load<Texture2D>("title");
            borger = Content.Load<Texture2D>("borger");
        }

        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here
            currentScreen = new TitleScreen();
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ToggleFullScreen();
            _graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (kill)
            {
                List<string> content = currentScreen.getContent();
                if (content.Count != 0)
                    File.WriteAllLines(Directory.GetCurrentDirectory() + "/save.txt", content);
                Exit();
            }
            // TODO: Add your update logic here
            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            base.Draw(gameTime);
            currentScreen.Draw();
            _spriteBatch.End();
        }

        public static void switchScreen(string index)
        {
            switch (index)
            {
                case "title":
                    currentScreen = new TitleScreen();
                    break;
                case "main":
                    currentScreen = new GameScreen();
                    break;
                default:
                    throw new System.NullReferenceException();
            }
        }
    }
}
