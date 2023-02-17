using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public interface IComponent
    {
        void Start();
        void On(); // Включает компонент, это касается, например его отображения
        void Off(); // Выключает
        bool GetState(); // Узнать Включён или выключен
        void OnMove(Vector2 position); // Когда GameObject двигается
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
