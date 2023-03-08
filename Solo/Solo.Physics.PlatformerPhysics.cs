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
        protected bool _isColliding;

        // подумать лучше, каак реализовать
        public PlatformerPhysics(GameObject parent, float gravity) : base(parent)
        {
            
        }

        public override void OnCollide(GameObject interacting)
        {
            
        }

        public override void OnNoCollide()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
          
        }
    }
}
