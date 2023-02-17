using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class GameObject
    {
        public ComponentsDictionary Components { get; }
        public delegate void MoveDelegate(Vector2 position);
        public event MoveDelegate MoveEvent;

        protected Vector2 _position;

        public void Init() { }
        public virtual void Start() { }
        public Vector2 GetPosition()
        {
            return _position;
        }
        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }
    }
}
