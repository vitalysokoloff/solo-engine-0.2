using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class SimplePhysics : PhysicsComponent
    {
        public SimplePhysics(GameObject parent) : base(parent) { }

        public override void OnCollide(GameObject interacting)
        {
            _parent.Move(-_parent.Components.Get<Collider>("physical").GetNormal(interacting.Components.Get<Collider>("physical")) - _parent.Direction);
        }
    }
}
