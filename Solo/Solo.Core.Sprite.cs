using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class Sprite : IComponent
    {
        public Texture2D Texture { get; }

        protected bool _state;
        protected int _framesQty;
        protected int _frameNumber;
        protected Rectangle _sourceRectangle;
        protected Rectangle _drawRectangle;
        protected Vector2 _parentPosition;
        protected Vector2 _position;
        // protected Timer - для анимации

        // Сдклай комментарий о том что для чего
        public Sprite(Texture2D texture, Rectangle sourceRectangle, Rectangle drawRectangle, Vector2 parentPosition)
        {

        }

        // Сдклай комментарий о том что для чего
        public Sprite(Texture2D texture, Rectangle sourceRectangle, Rectangle drawRectangle, Vector2 parentPosition, Vector2 ownPosition, int framesQty)
        {

        }

        public void FrameMoveRight() { }
        public void FrameMoveLeft() { }
        public void SetPosition() { }
        public void On() { }
        public void Off() { }
        public void GetState() { }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }

    }
}
