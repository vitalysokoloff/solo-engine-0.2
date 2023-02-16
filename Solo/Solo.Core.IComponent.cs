using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public interface IComponent
    {
        void On(); // Включает компонент, это касается, например его отображения
        void Off(); // Выключает
        void GetState(); // Узнать Включён или выключен
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
