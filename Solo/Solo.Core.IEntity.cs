using Microsoft.Xna.Framework;

namespace Solo.Core
{
    public interface IEntity
    {
        void Start();
        void Update(GameTime gameTime);
    }
}
