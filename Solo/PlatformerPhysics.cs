using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class PlatformerPhysics : PhysicsComponent
    {
        protected Vector2 _gravityDirection;

        public PlatformerPhysics(GameObject parent) : base(parent)
        {
            _parent.SpeedY = 1;
            _gravityDirection = new Vector2(0, 1);
        }

        public override void OnCollide(GameObject interacting)
        {
            _parent.Move(-_parent.Direction - _gravityDirection);
        }

        public override void Update(GameTime gameTime)
        {
            _parent.Move(_gravityDirection);
        }
    }
}
