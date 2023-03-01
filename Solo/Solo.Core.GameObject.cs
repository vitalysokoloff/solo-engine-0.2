using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Physics;

namespace Solo.Core
{
    public class GameObject : IEntity
    {
        public ComponentsDictionary Components { get; }
        public float Layer { get; protected set; }
        public float Speed { get; set; }
        public float Mass { get; set; }

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

        public delegate void PhysicsDelegate(GameObject interacting);
        public event PhysicsDelegate PhysicsEvent;

        protected Vector2 _position;
        protected float _angle;
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
            Speed = 1;
            Start();
        }

        protected void Init() { }

        public virtual void Start() { }

        public void OnCollide(GameObject interacting, string colliderName)
        {
            if (colliderName == "physical")
                PhysicsEvent?.Invoke(interacting);
            else
                OnHit(interacting, colliderName);
        }

        public void OnPhysics(GameObject interacting)
        {
            Move(-Direction);
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

        public void Move(Vector2 force)
        {
            _position += force * Speed;

            _direction = force;

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
