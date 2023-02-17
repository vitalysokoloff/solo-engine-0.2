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

        }

        private void Init(Texture2D texture, Rectangle sourceRectangle, GameObject parent)
        {
            Texture = texture;
            _sourceRectangle = sourceRectangle;
            _parent = parent;
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
        public void OnMove() { }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }

    }
}
