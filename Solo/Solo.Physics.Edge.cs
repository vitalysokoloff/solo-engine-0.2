using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Physics
{
    public class Edge : ICollider
    {
        protected Vector2[] _points;
        protected bool _state;

        public Edge(Vector2[] points)
        {
            _points = points;
            On();
        }

        public Vector2 GetPoint(int n)
        {
            return _points[n];
        }

        public Vector2 GetGlobalPoint(int n)
        {
            return _points[n];
        }

        public int GetPointsLength()
        {
            return _points.Length;
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

        public void OnMove(Vector2 position) { }
        public void OnRotate(float angle) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
