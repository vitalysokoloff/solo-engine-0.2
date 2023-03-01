using Solo.Core;

namespace Solo.Physics
{
    public class SimplePhysics : PhysicsComponent
    {
        public SimplePhysics(GameObject parent) : base(parent) { }

        public override void OnCollide(GameObject interacting)
        {
            _parent.Move(-_parent.Direction);
        }
    }
}
