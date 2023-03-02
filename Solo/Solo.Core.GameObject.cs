using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Physics;

namespace Solo.Core
{
    public class GameObject : IEntity
    {
        public ComponentsDictionary Components { get; }
        public float Layer { get; protected set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        public Vector2 Velocity
        { 
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }

        public float VelocityX
        {
            get
            {
                return _velocity.X;
            }
            set
            {
                _velocity.X = value;
            }
        }

        public float VelocityY
        {
            get
            {
                return _velocity.Y;
            }
            set
            {
                _velocity.Y = value;
            }
        }

        public Vector2 Impulse
        {
            get
            {
                return _impulse;
            }
            set
            {
                _impulse = value;
            }
        }

        public float ImpulseX
        {
            get
            {
                return _impulse.X;
            }
            set
            {
                _impulse.X = value;
            }
        }

        public float ImpulseY
        {
            get
            {
                return _impulse.Y;
            }
            set
            {
                _impulse.Y = value;
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
        protected Vector2 _direction;
        protected bool _debugMode;

        ///  убрать паррен, а на события подписывать в GO менеджере или в энтити типа физикс
        public GameObject(Vector2 position, float layer)
        {
            Components = new ComponentsDictionary();
            _position = position;
            Layer = layer;
            _angle = 0f;
            _direction = Vector2.Zero;
            _debugMode = true;
            _velocity = Vector2.One;
            _impulse = Vector2.Zero;
            SpeedX = 1;
            SpeedY = 1;
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
            _debugMode = status;
        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
            MoveEvent?.Invoke(_position);
        }

        public void Move(Vector2 direction)
        {
            Vector2 delta = direction * _velocity + _impulse;
            _position +=  delta;

            _direction.Y = delta.Y > 0 ? 1 : delta.Y == 0 ? 0 : -1;
            _direction.X = delta.X > 0 ? 1 : delta.X == 0 ? 0 : -1;

            MoveEvent?.Invoke(_position);
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

            if (_debugMode)
            {
                if (Components.Get<Collider>("physical") != null)
                  Components.Get<Collider>("physical").Draw(spriteBatch);
            }
        }
    }
}
