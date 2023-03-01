using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Core;

namespace Solo.Physics
{
    public class SimplePhysicsComponent : PhysicsComponent
    {
        public SimplePhysicsComponent(GameObject parent) : base(parent) { }

        public override void OnCollide(GameObject interacting)
        {
            _parent.Move(-_parent.Direction);
        }
    }
}
