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

        public PlatformerPhysics(GameObject parent, float gravity) : base(parent)
        {
            _parent.SpeedY = gravity;
            _gravity = gravity;
            _force = 0;
            _gravityDirection = new Vector2(0, 1);
        }

        public override void OnCollide(GameObject interacting)
        {
            _force = _gravity;
            _parent.Move(-_parent.Direction - _gravityDirection);
            _gravityDirection.Y = 0;
        }

        public override void OnNoCollide()
        {
            _gravityDirection.Y = 1;
            _force = 0;
        }

        public override void Update(GameTime gameTime)
        {
            _parent.SpeedY = _gravity - _force;
            _parent.Move(_gravityDirection);
        }
    }
}
