﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class Camera : IEntity
    {        
        public Vector2 ScreenCenter { get; }  
        
        public float Angle
        {
            get
            {
                return _angle;
            }
        }

        public float Scale
        {
            get
            {
                return _scale;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }

        public Matrix Transform { get; protected set; }
        public float Speed { get; set; }

        protected Vector2 _position;
        protected float _angle;
        protected Vector2 _pivot;
        protected float _scale;
        protected float _viewportHeight;
        protected float _viewportWidth;

        public Camera(GraphicsDevice graphicsDevice)
        {
            _viewportWidth = graphicsDevice.Viewport.Width;
            _viewportHeight = graphicsDevice.Viewport.Height;

            ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _scale = 1f;
            Speed = 1f;
            Start();
        }

        public virtual void Start()
        {

        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public void Move(Vector2 delta)
        {
            _position += delta;
        }

        public void Rotate(float deltaAngle)
        {
            _angle = Tools.CalculateAngle(_angle + deltaAngle);
        }

        public void SetAngle(float newAngle)
        {
            _angle = Tools.CalculateAngle(newAngle);
        }

        public void SetScale(float newScale)
        {
            _angle = newScale;
        }

        public virtual void Update(GameTime gameTime)
        {
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-_position.X, -_position.Y, 0) *
                        Matrix.CreateRotationZ(_angle) *
                        Matrix.CreateTranslation(_pivot.X, _pivot.Y, 0) *
                        Matrix.CreateScale(new Vector3(_scale, _scale, _scale));

            _pivot = ScreenCenter / _scale;           
        }

        /*
        public bool IsInView(Vector2 position, Texture2D texture)
        {

            if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
                return false;

            if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
                return false;
            return true;
        }
        */
    }

    public class ManualCamera : Camera
    {
        public ManualCamera(GraphicsDevice graphicsDevice) : base (graphicsDevice) { }
    }

    public class FocusCamera : Camera
    {
        public GameObject Focus { get; set; }

        public FocusCamera(GraphicsDevice graphicsDevice, GameObject focus) : base(graphicsDevice)
        {
            Focus = focus;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _position.X += (Focus.Position.X - _position.X) * Speed * delta;
            _position.Y += (Focus.Position.Y - _position.Y) * Speed * delta;
        }
    }

    public class PlatformerCamera : FocusCamera
    {
        public PlatformerCamera(GraphicsDevice graphicsDevice, GameObject focus) : base(graphicsDevice, focus) { }
    }
}
