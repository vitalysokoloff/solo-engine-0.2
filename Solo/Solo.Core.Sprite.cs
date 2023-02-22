using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class Sprite : IComponent
    {
        public Texture2D Texture { get; private set; }        

        protected bool _state;
        protected int _framesQty;
        protected int _frameNumber;
        protected Rectangle _sourceRectangle;
        protected Rectangle _drawRectangle;
        protected Vector2 _position;        
        protected Point _size;
        protected Vector2 _pivot;
        protected GameObject _parent;
        protected Timer _timer;
        protected Color _color;
        protected float _angle;

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="texture">Monogame Texture2D</param>
        /// <param name="parent">Нужна ссылка на родительский объект, гейм обджект к которому привязан этот спрайт</param>
        /// <param name="size">Размер текстуры при отображении</param>        /// 
        public Sprite(Texture2D texture, GameObject parent, Point size)
        {
            Init(texture, new Rectangle(0, 0, size.X, size.Y), parent, size);
            _position = Vector2.Zero;
            _drawRectangle = new Rectangle((int)_parent.Position.X, (int)_parent.Position.Y, _size.X, _size.Y);            
            _framesQty = 1;
            Start();
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="texture">Monogame Texture2D</param>
        /// <param name="parent">Нужна ссылка на родительский объект, гейм обджект к которому привязан этот спрайт</param>
        /// <param name="size">Размер текстуры при отображении</param>
        /// <param name="position">Позиция относительно родителя</param>       
        /// <param name="framesQty">Количество кадров</param>
        public Sprite(Texture2D texture, GameObject parent, Point size, Vector2 position, int framesQty)
        {
            Init(texture, new Rectangle(0, 0, size.X, size.Y), parent, size);
            _position = position;
            _drawRectangle = new Rectangle((int)(_parent.Position.X + _position.X), (int)(_parent.Position.Y + _position.Y), _size.X, _size.Y);            
            _framesQty = framesQty;
            Start();
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="texture">Monogame Texture2D</param>
        /// <param name="sourceRectangle">Размеры кадра, область которая вырезается из Texture2D для отображения</param>
        /// <param name="parent">Нужна ссылка на родительский объект, гейм обджект к которому привязан этот спрайт</param>
        /// <param name="size">Размер текстуры при отображении</param>
        /// <param name="position">Позиция относительно родителя</param>
        /// <param name="framesQty">Количество кадров</param>
        public Sprite(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size, Vector2 position, int framesQty)
        {
            Init(texture, sourceRectangle, parent, size);
            _position = position;
            _drawRectangle = new Rectangle((int)(_parent.Position.X + _position.X), (int)(_parent.Position.Y + _position.Y), _size.X, _size.Y);
            _framesQty = framesQty;
            Start();
        }

        private void Init(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size)
        {
            Texture = texture;
            _sourceRectangle = sourceRectangle;
            _parent = parent;
            _size = size;
            _pivot = new Vector2(size.X / 2, size.Y / 2);
            _state = true;
            _frameNumber = 0;
            _timer = Timer.MakeDefault();
            _color = Color.White;
            _angle = 0;

            _parent.MoveEvent += OnMove;
            _parent.RotateEvent += OnRotate;
        }

        public virtual void Start() { }

        public void SetAnimationTimer(int period)
        {
            _timer.Period = period;
            _timer.Reset();
        }

        public void AnimationRun()
        {
            _timer.Start();
        }

        public void AnimationStop()
        {
            _timer.Stop();
        }

        public void AnimationReset()
        {
            _timer.Reset();
        }

        public void FrameMoveRight()
        {
            if (_frameNumber < _framesQty)
                _frameNumber++;
            if (_frameNumber >= _framesQty)
                _frameNumber = 0;

            _sourceRectangle.X = _frameNumber * _size.X;
        }

        public void FrameMoveLeft()
        {
            if (_frameNumber >= 0)
                _frameNumber++;
            if (_frameNumber < 0)
                _frameNumber = _framesQty;

            _sourceRectangle.X = _frameNumber * _size.X;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

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

        public void OnMove(Vector2 position)
        {
            _drawRectangle = new Rectangle((int)(position.X + _position.X), (int)(position.Y + _position.Y), _size.X, _size.Y);
        }

        public void OnRotate(float angle)
        {
            _angle = angle;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, _drawRectangle, _sourceRectangle, _color, _angle, _pivot, SpriteEffects.None, _parent.Layer);
        }

    }

    public class SpriteAnimated : Sprite
    {
        public SpriteAnimated(Texture2D texture, GameObject parent, Point size, Vector2 position, int framesQty) : base(texture, parent, size, position, framesQty) { }
        public SpriteAnimated(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size, Vector2 position, int framesQty) : base(texture, sourceRectangle, parent, size, position, framesQty) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_state)
            {
                if (_timer.Beat(gameTime))
                {
                    FrameMoveRight();
                }
            }
        }

    }

    public class SpriteAnimatedInitially : SpriteAnimated
    {
        public SpriteAnimatedInitially(Texture2D texture, GameObject parent, Point size, Vector2 position, int framesQty) : base(texture, parent, size, position, framesQty) { }
        public SpriteAnimatedInitially(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size, Vector2 position, int framesQty) : base(texture, sourceRectangle, parent, size, position, framesQty) { }

        public override void Start()
        {
            AnimationRun();
        }

    }
}
