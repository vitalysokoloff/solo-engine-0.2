using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public interface IComponent
    {
        void On();
        void Off();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
