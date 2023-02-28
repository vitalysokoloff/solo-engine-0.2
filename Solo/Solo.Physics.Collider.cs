using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Core;

namespace Solo.Physics
{
    public abstract class Collider : IComponent
    {
        public Vector2 GlobalPosition
        {
            get
            {
                return _position + _parent.Position;
            }
        } 

        public float Angle
        {
            get
            {
                return _angle;
            }
        }

        public Vector2 Size
        {
            get
            {
                return _size;
            }
        }
        
        public Texture2D Texture { get; protected set; }

        protected Vector2[] _points;
        protected bool _state;
        protected Vector2 _position;
        protected Vector2 _size;
        protected GameObject _parent;
        protected float _angle;
        protected Rectangle _drawRectangle; 
        protected Rectangle _sourceRectangle;
        protected Vector2[] _basePoints;
        protected Vector2 _pivot;
        protected Color _color = Color.White;

        /// <summary>
        /// the pivot is always in the middle of the shape
        /// </summary>
        public Collider(GameObject parent, Rectangle rectangle)
        {
            Init(parent);
        }

        /// <summary>
        /// the pivot is always in the middle of the shape
        /// </summary>
        public Collider(GameObject parent, int x, int y, int width, int height)
        {
            Init(parent);
        }

        private void Init(GameObject parent)
        {
            _angle = 0f;
            _parent = parent;
            parent.MoveEvent += OnMove;
            parent.RotateEvent += OnRotate;
        }

        public virtual void Start() { }

        public void On()
        {
            _state = true;
        }

        public void Off()
        {
            _state = false;
        }

        public bool GetState()
        {
            return _state;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public Vector2 GetPoint(int n)
        {
            return _points[n] + GlobalPosition;
        }

        public int GetPointsLength()
        {
            return _points.Length;
        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
            SetBasePoints(); // Установка точек относительно новых координат позишн
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public void OnMove(Vector2 position)
        {
            _drawRectangle.X = (int)GlobalPosition.X;
            _drawRectangle.Y = (int)GlobalPosition.Y;
        }

        public void OnRotate(float angle)
        {
            _angle = angle;
            RotatePoints();
        }

        public virtual bool Intersects(Collider collider)
        {
            return GJK.CheckCollision(this, collider);
        }

        public virtual void GenerateTexture(GraphicsDeviceManager graphics)
        {
            Texture = new Texture2D(graphics.GraphicsDevice, (int)Size.X + 2, (int)Size.Y + 2);
            Color[] data = new Color[Texture.Width * Texture.Height];
            Texture.SetData(data);
            for (int i = 0; i < _points.Length - 1; i++)
                Tools.DrawLine(Texture, _color, _points[i], _points[i + 1]);
            Tools.DrawLine(Texture, _color, _points[_points.Length - 1], _points[0]);
        }

        protected void RotatePoints()
        {
            for (int i = 0; i < _points.Length; i++)
                _points[i] = new Vector2(
                (float)((_basePoints[i].X - _pivot.X) * Math.Cos(_angle) - (_basePoints[i].Y - _pivot.Y) * Math.Sin(_angle) + _pivot.X),
                (float)((_basePoints[i].X - _pivot.X) * Math.Sin(_angle) + (_basePoints[i].Y - _pivot.Y) * Math.Cos(_angle) + _pivot.Y)
                );
        }

        protected abstract void SetBasePoints();

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, _drawRectangle, _sourceRectangle, _color, _angle, _pivot, SpriteEffects.None, _parent.Layer);
        }
    }
}
