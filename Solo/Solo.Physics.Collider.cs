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
                if (_parent != null)
                    return _position + _parent.Position;
                else
                    return _position;
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
            if (_parent != null)
            {
                _parent.MoveEvent += OnMove;
                _parent.RotateEvent += OnRotate;
            }
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
            return GJK.CheckCollision(this, collider).Answer;
        }

        public virtual GJK.Resault GetResault(Collider collider)
        {
            return GJK.CheckCollision(this, collider);
        }
        
        public virtual Vector2 GetNormal(Collider collider)
        {
            // Когда EPA  реализуешь?

            int length = collider.GetPointsLength();
            Edge[] edges = new Edge[length];

            for (int i = 0; i < length - 1; i++)
                edges[i] = new Edge(collider.GetGlobalPoint(i), collider.GetGlobalPoint(i + 1));
            edges[length - 1] = new Edge(collider.GetGlobalPoint(length - 1), collider.GetGlobalPoint(0));

            int pointer = 0;
            
            for (int i = 0; i < edges.Length; i++)
            {  
                float length1 = (float)Math.Sqrt(Math.Pow(edges[pointer].Middle.X - GlobalPosition.X, 2) + Math.Pow(edges[pointer].Middle.Y - GlobalPosition.Y, 2));
                float length2 = (float)Math.Sqrt(Math.Pow(edges[i].Middle.X - GlobalPosition.X, 2) + Math.Pow(edges[i].Middle.Y - GlobalPosition.Y, 2));                

                if (length2 < length1)
                    pointer = i;
            }
            

            Console.WriteLine("[> " + pointer + " [/mp: " + edges[pointer].Middle + "/ap: " + edges[pointer].A + "/bp: " + edges[pointer].B + "]");
            Console.WriteLine("/gp:" + GlobalPosition + "/cgp: " + collider.GlobalPosition + " <]");
            return Tools.EdgeToNormal(edges[pointer].A, edges[pointer].B);

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

        protected class Edge
        {
            public Vector2 A { get; }
            public Vector2 B { get; }
            public Vector2 Middle { get; }

            public Edge(Vector2 a, Vector2 b)
            {
                A = a;
                B = b;
                Middle = new Vector2(((A.X + B.X) / 2), ((A.Y + B.Y) / 2));                
            }
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, _drawRectangle, _sourceRectangle, _color, _angle, _pivot, SpriteEffects.None, _parent.Layer);
            }
        }
    }
}
