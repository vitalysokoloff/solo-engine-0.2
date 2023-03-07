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
            _gravity = gravity;
            _force = _parent.ResistX;
            _gravityDirection = new Vector2(0, 1);
            _isColliding = false;
            _forceDirection = Vector2.Zero;
        }

        public override void OnCollide(GameObject interacting)
        {
            _force = _parent.ResistX;
            _forceDirection = _parent.Components.Get<Collider>("physical").GetNormal(interacting.Components.Get<Collider>("physical"));
            _parent.Move(-_forceDirection - _parent.Direction);
            _isColliding = true;

            if (_isColliding && _forceDirection.X != 0)
                _parent.ImpulseX = 0;
            if (_isColliding && _forceDirection.Y != 0)
                _parent.ImpulseY = 0;
        }

        public override void OnNoCollide()
        {
            _isColliding = false;
            _force = 1; 
        }

        public override void Update(GameTime gameTime)
        {
            

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _parent.ImpulseY = _parent.ImpulseY - _parent.ImpulseY * deltaTime * _parent.ResistY;
            _parent.ImpulseX = _parent.ImpulseX - _parent.ImpulseX * deltaTime * _force;

            _parent.Move(_gravityDirection);
        }
    }
}
