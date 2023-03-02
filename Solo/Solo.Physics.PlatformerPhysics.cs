using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class PlatformerPhysics : PhysicsComponent
    {
        protected Vector2 _gravityDirection;
        protected Vector2 _forceDirection;
        protected Vector2 _finalDirection;
        protected float _force;
        protected float _gravity;

        public PlatformerPhysics(GameObject parent, float gravity) : base(parent)
        {
            _gravity = gravity;
            _force = 0;
            _gravityDirection = new Vector2(0, 1);
            _forceDirection = Vector2.Zero;
            _finalDirection = Vector2.Zero;
        }

        public override void OnCollide(GameObject interacting)
        {  
            _forceDirection = _parent.Components.Get<Collider>("physical").GetNormal(interacting.Components.Get<Collider>("physical"));
            _finalDirection = -_forceDirection + _gravityDirection;
            if (_finalDirection.Y < 0)
            {
                _parent.ImpulseY = 0;
                _parent.VelocityY = 0;
            }
            if (_finalDirection.Y > 0)
            {
                _parent.ImpulseY = 0;
            }
            if (_finalDirection.X != 0)
                _parent.ImpulseX = 0;
        }

        public override void OnNoCollide()
        {
            _parent.VelocityY = _gravity;
            _finalDirection = _gravityDirection;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _parent.ImpulseY = _parent.ImpulseY - _parent.ImpulseY * deltaTime;
            _parent.ImpulseX = _parent.ImpulseX - _parent.ImpulseX * deltaTime;
                      
            _parent.Move(_finalDirection);
        }
    }
}
