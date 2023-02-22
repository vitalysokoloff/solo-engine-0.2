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
        
        public Vector2[] Points { get; protected set; } // массив точек для отрисовки фигуры
        public Texture2D Texture { get; protected set; }

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
        public Collider(Rectangle rectangle)
        {
            _angle = 0f;
        }

        /// <summary>
        /// the pivot is always in the middle of the shape
        /// </summary>
        public Collider(int x, int y, int width, int height)
        {
            _angle = 0f;
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

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
            SetBasePoints(); // Установка точек относительно новых координат позишн
        }

        public void OnMove(Vector2 position)
        {
            _drawRectangle = new Rectangle((int)(position.X + _position.X), (int)(position.Y + _position.Y), _size.X, _size.Y);
        }



        public virtual void SetAngle(float angle)
        {
            AngleRotation = angle;
            RotatePoints();
        }

        public virtual void SetAngle(int angle)
        {
            SetAngle((float)(angle * Math.PI / 180));
        }

        public void Rotate(int deltaAngle)
        {
            float oldAngle = AngleRotation;
            float newAngle = AngleRotation + (float)(deltaAngle * Math.PI / 180);
            float delta = newAngle - oldAngle;
            float a360 = (float)(360 * Math.PI / 180);
            if (delta < 0)
            {
                AngleRotation = a360 - delta;
            }
            if (newAngle > a360)
                AngleRotation = delta;
            if (newAngle >= 0 && newAngle <= a360)
                AngleRotation = newAngle;
            RotatePoints();
        }

        public virtual bool Intersects(Collider shape)
        {
            return GJK.CheckCollision(this, shape);
        }

        public virtual void SetTexture(GraphicsDeviceManager graphics, Color color)
        {
            Texture = new Texture2D(graphics.GraphicsDevice, (int)Size.X + 1, (int)Size.Y + 1);
            Color[] data = new Color[Texture.Width * Texture.Height];
            Texture.SetData(data);
            Vector2 point0 = GlobalPosition; // для приведения к новой системе координат в верхней левой точке
            for (int i = 0; i < Points.Length - 1; i++)
                Tools.DrawLine(Texture, color, Points[i] - point0, Points[i + 1] - point0);
            Tools.DrawLine(Texture, color, Points[Points.Length - 1] - point0, Points[0] - point0);
        }

        protected void RotatePoints()
        {
            for (int i = 0; i < Points.Length; i++)
                Points[i] = new Vector2(
                (float)((_basePoints[i].X - _pivot.X) * Math.Cos(AngleRotation) - (_basePoints[i].Y - _pivot.Y) * Math.Sin(AngleRotation) + _pivot.X),
                (float)((_basePoints[i].X - _pivot.X) * Math.Sin(AngleRotation) + (_basePoints[i].Y - _pivot.Y) * Math.Cos(AngleRotation) + _pivot.Y)
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
