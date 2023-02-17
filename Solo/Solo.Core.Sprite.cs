﻿using Microsoft.Xna.Framework;
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
        protected GameObject _parent;
        // protected Timer - для анимации

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="texture">Monogame Texture2D</param>
        /// <param name="sourceRectangle">Размеры кадра, область которая вырезается из Texture2D для отображения</param>
        /// <param name="parent">Нужна ссылка на родительский объект, гейм обджект к которому привязан этот спрайт</param>
        /// <param name="size">Размер текстуры при отображении</param>        /// 
        public Sprite(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size)
        {
            Init(texture, sourceRectangle, parent, size);
            _position = Vector2.Zero;
            _drawRectangle = new Rectangle((int)_parent.GetPosition().X, (int)_parent.GetPosition().Y, _size.X, _size.Y);
            _framesQty = 1;
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="texture">Monogame Texture2D</param>
        /// <param name="sourceRectangle">Размеры кадра, область которая вырезается из Texture2D для отображения</param>
        /// <param name="parent">Нужна ссылка на родительский объект, гейм обджект к которому привязан этот спрайт</param>
        /// <param name="size">Размер текстуры при отображении</param>
        /// <param name="position">Позиция относительно родителя</param>       
        public Sprite(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size, Vector2 position)
        {
            Init(texture, sourceRectangle, parent, size);
            _position = position;
            _drawRectangle = new Rectangle((int)(_parent.GetPosition().X + _position.X), (int)(_parent.GetPosition().Y + _position.Y), _size.X, _size.Y);
            _framesQty = 1;
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
            _drawRectangle = new Rectangle((int)(_parent.GetPosition().X + _position.X), (int)(_parent.GetPosition().Y + _position.Y), _size.X, _size.Y);
            _framesQty = framesQty;
        }

        private void Init(Texture2D texture, Rectangle sourceRectangle, GameObject parent, Point size)
        {
            Texture = texture;
            _sourceRectangle = sourceRectangle;
            _parent = parent;
            _size = size;
            _state = true;
            _frameNumber = 0;

            _parent.MoveEvent += OnMove;
            
        }

        //добавить что типо AnimationRun, AnimationStop и AnimationReset
        public void FrameMoveRight() { }
        public void FrameMoveLeft() { }
        public void SetPosition() { }
        public void On() { }
        public void Off() { }
        public void GetState() { }
        public void OnMove(Vector2 position)
        {
            _drawRectangle = new Rectangle((int)(position.X + _position.X), (int)(position.Y + _position.Y), _size.X, _size.Y);
        }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }

    }
}
