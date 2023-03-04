using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Core;

namespace Solo.Physics
{
    public class Collider : ICollider
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
            On();
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

        public Vector2 GetGlobalPoint(int n)
        {
            return _points[n] + GlobalPosition;
        }

        public Vector2 GetPoint(int n)
        {
            return _points[n];
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

        public virtual bool Intersects(ICollider collider)
        {
            return GJK.CheckCollision(this, collider);
        }

        // Проверь по нормалям что выходит, а то херня какая-то
        public virtual Vector2 GetNormal(ICollider collider)
        {
            Vector2 tmp = Vector2.Zero;
            Vector2 n = Vector2.Zero;
            Edge edge;
            Vector2[] points;

            for ( int i = 0; i < collider.GetPointsLength() - 1; i++)
            {
                points = new Vector2[] { collider.GetGlobalPoint(i), collider.GetGlobalPoint(i + 1) };
                edge = new Edge(points);
                if (Intersects(edge))
                {
                    Vector2 a = points[1] - points[0]; // получаем вектор в новой системе координат
                    tmp = Tools.VectorToNormal(a); // получаем нормаль, поворот на -90
                    n.X = tmp.X != 0 ? tmp.X : 0;
                    n.Y = tmp.Y != 0 ? tmp.Y : 0;
                }
            }

            points = new Vector2[] { collider.GetGlobalPoint(collider.GetPointsLength() - 1), collider.GetGlobalPoint(0) };
            edge = new Edge(points);
            if (Intersects(edge))
            {
                Vector2 a = points[1] - points[0]; // получаем вектор в новой системе координат
                tmp = Tools.VectorToNormal(a); // получаем нормаль, поворот на -90
                n.X = tmp.X != 0 ? tmp.X : 0;
                n.Y = tmp.Y != 0 ? tmp.Y : 0;
                
            }

            return n;

        }

        public virtual void GenerateTexture(GraphicsDeviceManager graphics)
        {
            Texture = new Texture2D(graphics.GraphicsDevice, (int)Size.X + 2, (int)Size.Y + 2);
            Color[] data = new Color[Texture.Width * Texture.Height];
            Texture.SetData(data);
            for (int i = 0; i < _points.Length - 1; i++)
                Tools.DrawLine(Texture, _color, _points[i] + _pivot, _points[i + 1] + _pivot);
            Tools.DrawLine(Texture, _color, _points[_points.Length - 1] + _pivot, _points[0] + _pivot);
        }

        protected void RotatePoints()
        {
            for (int i = 0; i < _points.Length; i++)
                _points[i] = new Vector2(
                (float)((_basePoints[i].X ) * Math.Cos(_angle) - (_basePoints[i].Y) * Math.Sin(_angle)),
                (float)((_basePoints[i].X ) * Math.Sin(_angle) + (_basePoints[i].Y) * Math.Cos(_angle))
                );
        }

        protected virtual void SetBasePoints() { }

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
