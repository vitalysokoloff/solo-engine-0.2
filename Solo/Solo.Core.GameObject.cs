using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Physics;

namespace Solo.Core
{
    public class GameObject : IEntity
    {
        public ComponentsDictionary Components { get; }
        public bool DebugMode { get; set; }
        public float Layer { get; protected set; }

        public Vector2 Resist
        {
            get
            {
                return _resist;
            }
        }

        public Vector2 Velocity
        { 
            get
            {
                return _velocity;
            }
        }

        public Vector2 Impilse
        {
            get
            {
                return _impulse;
            }
        }

        public float Angle
        {
            get
            {
                return _angle;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }

        public Vector2 Direction
        {
            get
            {
                return _direction;
            }
        }

        public delegate void MoveDelegate(Vector2 position);
        public event MoveDelegate MoveEvent;

        public delegate void RotateDelegate(float angle);
        public event RotateDelegate RotateEvent;

        public delegate void CollideDelegate(GameObject interacting);
        public event CollideDelegate OnCollideEvent;

        public delegate void NoCollideDelegate();
        public event NoCollideDelegate OnNoCollideEvent;

        protected Vector2 _position;
        protected float _angle;
        protected Vector2 _velocity;
        protected Vector2 _impulse;
        protected Vector2 _resist;
        protected Vector2 _direction;

        ///  убрать паррен, а на события подписывать в GO менеджере или в энтити типа физикс
        public GameObject(Vector2 position, float layer)
        {
            Components = new ComponentsDictionary();
            DebugMode = true;
            _position = position;
            Layer = layer;
            _angle = 0f;
            _direction = Vector2.Zero;
            _velocity = Vector2.Zero;
            _impulse = Vector2.Zero;
            _resist = new Vector2(1, 1);

            Start();
        }

        protected void Init() { }

        public virtual void Start() { }

        public void OnCollide(GameObject interacting, string colliderName)
        {
            if (colliderName == "physical")
                OnCollideEvent?.Invoke(interacting);
            else
                OnHit(interacting, colliderName);
        }

        public void OnNoCollide()
        {
            OnNoCollideEvent?.Invoke();
        }

        public virtual void OnHit(GameObject interacting, string colliderName) { }

        public virtual void OnDebug(bool status)
        {
            DebugMode = status;
        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
            MoveEvent?.Invoke(_position);
        }

        public void SetImpulse(Vector2 impulse)
        {
            _impulse = impulse;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void CalcVelosity(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _velocity = _impulse - Impilse * deltaTime * _resist;
            _position += _velocity * _direction;
            MoveEvent?.Invoke(_position);
        }
        
        public void Move(Vector2 direction)
        {
            _position += _velocity * direction;
            MoveEvent?.Invoke(_position);
        }

        public void Stop()
        {
            _velocity = Vector2.Zero;
        }

        public void StopX()
        {
            _velocity.X = 0;
        }

        public void StopY()
        {
            _velocity.Y = 0;
        }

        public void Rotate(float deltaAngle)
        {
            _angle = Tools.CalculateAngle(_angle + deltaAngle);

            RotateEvent?.Invoke(_angle);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Components.Get<Sprite>("main") != null)
                Components.Get<Sprite>("main").Update(gameTime);
            if (Components.Get<PhysicsComponent>("physics") != null)
                Components.Get<PhysicsComponent>("physics").Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Components.Get<Sprite>("main") != null)
                Components.Get<Sprite>("main").Draw(spriteBatch);

            if (DebugMode)
            {
                if (Components.Get<Collider>("physical") != null)
                  Components.Get<Collider>("physical").Draw(spriteBatch);
            }
        }
    }
}
