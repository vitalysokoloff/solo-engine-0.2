using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class GameObject
    {
        public ComponentsDictionary Components { get; }
        public float Layer { get; protected set; }
        public Vector2 Position { get; protected set; }
        public float Angle
        {
            get
            {
                return _angle;
            }
        }

        public delegate void MoveDelegate(Vector2 position);
        public event MoveDelegate MoveEvent;
        public delegate void RotateDelegate(float angle);
        public event RotateDelegate RotateEvent;

        protected Scene _parent;
        protected float _angle;

        public GameObject(Scene parent, Vector2 position, float layer)
        {
            Components = new ComponentsDictionary();
            _parent = parent;
            Position = position;
            Layer = layer;
            _angle = 0f;
        }

        public void Init() { }

        public virtual void Start() { }

        public virtual void OnCollide() { }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
            MoveEvent?.Invoke(Position);
        }

        public void Move(Vector2 delta)
        {
            Position += delta;
            MoveEvent?.Invoke(Position);
        }

        public void Rotate(float deltaAngle)
        {
            _angle = CalculateAngle(_angle + deltaAngle);

            RotateEvent?.Invoke(_angle);
        }

        private float CalculateAngle(float sum)
        {
            if (sum == Tools.DegreesToRadians(360))
                sum = 0;

            if (sum > 6.283f)
                sum = CalculateAngle(sum - 6.283f);
            if (sum < 0f)
                sum = CalculateAngle(6.283f + sum);

            return sum;
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
