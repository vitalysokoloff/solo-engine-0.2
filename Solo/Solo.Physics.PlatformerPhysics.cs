using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class PlatformerPhysics : PhysicsComponent
    {     
        protected float _gravity;
        protected float _force;
        protected bool _onFloor;

        public PlatformerPhysics(GameObject parent) : base(parent)
        {
            _gravity = -2;
            _parent.SetResist(new Vector2(4f, 2f));
            _onFloor = false;
            _parent.SetDirectionY(1);
        }

        public override void OnCollide(GameObject interacting)
        {
            
            Vector2 noramal = _parent.Components.Get<Collider>("physical").GetNormal(interacting.Components.Get<Collider>("physical"));

            if (noramal.Y != 0)
            {
                _parent.StopY();

                if (noramal.Y < 0)
                {
                    _onFloor = true;
                }
                else
                {
                    _onFloor = false;
                }
            }

            _parent.Move(noramal - _parent.Direction);
        }

        public override void OnNoCollide()
        {
            _onFloor = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!_onFloor)
            {                
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_parent.Velocity.Y > -_gravity)
                    _parent.SetDirectionY(-1);
                else
                {
                    _parent.SetDirectionY(1);
                }
                _force = _gravity * deltaTime;
                _parent.SetImpulseY(_parent.Impulse.Y -_force);
            }

            _parent.CalcVelosity(gameTime);
        }
    }
}
