using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Kawagochi
{
    abstract class ParentScreen
    {
        protected abstract Dictionary<string, Button> buttons { get; set; }
        public abstract void Initialize();
        public abstract void Draw();
        public abstract void Update(GameTime gameTime);

        public abstract List<string> getContent();
    }
}
