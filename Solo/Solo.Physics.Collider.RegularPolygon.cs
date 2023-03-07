using System;
using Microsoft.Xna.Framework;
using Solo.Core;

namespace Solo.Physics
{
    public class RegularPolygon : Collider
    {
        protected float _radius;
        protected int _verties;

        public RegularPolygon(GameObject parent, Rectangle rectangle) : base(parent, rectangle)
        {
            _verties = rectangle.Height;
            _radius = rectangle.Width;
            _basePoints = new Vector2[_verties];
            _points = new Vector2[_verties];
            _size = new Vector2(_radius * 2, _radius * 2);
            _pivot = new Vector2(_radius, _radius);
            _position = new Vector2(rectangle.X, rectangle.Y);
            _drawRectangle = new Rectangle((int)(GlobalPosition.X), (int)(GlobalPosition.Y), (int)_size.X, (int)_size.Y);
            _sourceRectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
            SetBasePoints();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">verties</param>
        /// <param name="height">radius</param>
        public RegularPolygon(GameObject parent, int x, int y, int width, int height) : base(parent, x, y, width, height)
        {
            _verties = height;
            _radius = width;
            _basePoints = new Vector2[_verties];
            _points = new Vector2[_verties];
            _size = new Vector2(_radius * 2, _radius * 2);
            _pivot = new Vector2(_radius, _radius);
            _position = new Vector2(x, y);
            _drawRectangle = new Rectangle((int)(GlobalPosition.X), (int)(GlobalPosition.Y), (int)_size.X, (int)_size.Y);
            _sourceRectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
            SetBasePoints();
        }

        protected override void SetBasePoints()
        {
            // Используется радиус описанной окружности
            float a = (float)(360 * Math.PI / 180) / _verties; // угол альфа, через который друг от друга находятся вершины правильного многоугольника

            for (int i = 0; i < _verties; i++)
            {
                _basePoints[i] = new Vector2( // получаем координаты вершин x=x0+r*cosA, y=y0+r*sinA 
                    (float)(_radius * Math.Cos(0 + a * i)),
                    (float)(_radius * Math.Sin(0 + a * i))
                    );
                _points[i] = _basePoints[i];
            }
        }

    }
}
