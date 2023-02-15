using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Storage;

namespace Solo.Core
{
    public class Component : IEntity
    {
        protected Texture2D texture;
        protected Heap cfg;

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
