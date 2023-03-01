using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Core;


namespace Solo.Physics
{
    public class PhysicsComponent : IComponent
    {
        protected bool _state;
        protected GameObject _parent;

        public PhysicsComponent(GameObject parent)
        {
            Init(parent);
            Start();
        }

        private void Init(GameObject parent)
        {
            _parent = parent;
            _parent.PhysicsEvent += OnCollide;
            On();
        }

        public virtual void Start() { }

        public void On()
        {
            _state = true;
        }

        public void Off()
        {
            _state = false;
        }

        public bool GetState()
        {
            return _state;
        }

        public void OnMove(Vector2 delta)
        {

        }

        public virtual void OnCollide(GameObject interacting)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
