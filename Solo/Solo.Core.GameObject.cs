using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class GameObject : IEntity
    {
        public ComponentsDictionary Components { get; }
        public float Layer { get; protected set; }      
        
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

        public float Speed { get; set; }

        public delegate void MoveDelegate(Vector2 position);
        public event MoveDelegate MoveEvent;
        public delegate void RotateDelegate(float angle);
        public event RotateDelegate RotateEvent;

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
            _debugMode = false;
            Speed = 1;
            Start();
        }

        protected void Init() { }

        public virtual void Start() { }

        public virtual void OnCollide() { }

        public virtual void OnDebug(bool status)
        {

        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
            MoveEvent?.Invoke(_position);
        }

        public void Move(Vector2 delta)
        {
            _position += delta * Speed;

            _direction = delta;

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
                //if (Components.Get<Collider>("phesics") != null)
                    //Components.Get<Collider>("phesics").Draw(spriteBatch);
            }
        }
    }
}
