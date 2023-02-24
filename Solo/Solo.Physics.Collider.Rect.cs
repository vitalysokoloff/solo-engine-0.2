﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Core;

namespace Solo.Physics
{
    public class Rect : Collider
    {
        /// <summary>
        /// the pivot is always in the middle of the shape
        /// </summary>
        public Rect(GameObject parent, Rectangle rectangle) : base(parent, rectangle)
        {
            _basePoints = new Vector2[4];
            Points = new Vector2[4];
            _size = new Vector2(rectangle.Width, rectangle.Height);
            _pivot = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            _position = new Vector2(rectangle.X, rectangle.Y);
            _drawRectangle = new Rectangle((int)(GlobalPosition.X), (int)(GlobalPosition.Y), (int)_size.X, (int)_size.Y);
            _sourceRectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
            SetBasePoints();
        }

        /// <summary>
        /// the pivot is always in the middle of the shape
        /// </summary>
        public Rect(GameObject parent, int x, int y, int width, int height) : base(parent, x, y, width, height)
        {
            _basePoints = new Vector2[4];
            Points = new Vector2[4];
            _size = new Vector2(width, height);
            _pivot = new Vector2(width / 2, height / 2);
            _position = new Vector2(x, y);
            _drawRectangle = new Rectangle((int)(GlobalPosition.X), (int)(GlobalPosition.Y), (int)_size.X + 1, (int)_size.Y + 1);
            _sourceRectangle = new Rectangle(0, 0, (int)_size.X + 1, (int)_size.Y + 1);
            SetBasePoints();
        }

        protected override void SetBasePoints()
        {
            _basePoints[0] = GlobalPosition;
            _basePoints[1] = GlobalPosition + new Vector2(_size.X, 0);
            _basePoints[2] = GlobalPosition + new Vector2(_size.X, _size.Y);
            _basePoints[3] = GlobalPosition + new Vector2(0, _size.Y);
            Points[0] = _basePoints[0];
            Points[1] = _basePoints[1];
            Points[2] = _basePoints[2];
            Points[3] = _basePoints[3];
        }
    }

}