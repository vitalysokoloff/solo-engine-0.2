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

        public delegate void MoveDelegate(Vector2 position);
        public event MoveDelegate MoveEvent;
        public delegate void RotateDelegate(float angle);
        public event RotateDelegate RotateEvent;

        protected Vector2 _position;
        protected Scene _parent;
        protected float _angle;

        public GameObject(Scene parent, Vector2 position, float layer)
        {
            Components = new ComponentsDictionary();
            _parent = parent;
            _position = position;
            Layer = layer;
            _angle = 0f;
            Start();
        }

        protected void Init() { }

        public virtual void Start() { }

        public virtual void OnCollide() { }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
            MoveEvent?.Invoke(_position);
        }

        public void Move(Vector2 delta)
        {
            _position += delta;
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
        }
    }
}
