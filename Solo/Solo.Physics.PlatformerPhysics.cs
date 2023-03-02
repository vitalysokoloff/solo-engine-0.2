using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class PlatformerPhysics : PhysicsComponent
    {
        protected Vector2 _gravityDirection;
        protected bool _isOnCollide;
        protected float _force;
        protected float _gravity;
        protected Vector2 _lastDirection;

        public PlatformerPhysics(GameObject parent, float gravity) : base(parent)
        {
            _parent.SpeedY = gravity;
            _lastDirection = _parent.Direction;
            _gravity = gravity;
            _force = 0;
            _gravityDirection = new Vector2(0, 1);
        }

        public override void OnCollide(GameObject interacting)
        {
            

        }

        public override void OnNoCollide()
        {
            _parent.VelocityY = _gravity;
            _parent.Move(_gravityDirection);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _parent.ImpulseY = _parent.ImpulseY - _parent.ImpulseY * deltaTime;
        }
    }
}
