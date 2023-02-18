using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Core
{
    public class Camera : IEntity
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Pivot;
        public float Scale;
        public Vector2 ScreenCenter { get; protected set; }
        public Matrix Transform { get; protected set; }
        public float MoveSpeed;

        protected float _viewportHeight;
        protected float _viewportWidth;

        public Camera(GraphicsDevice graphicsDevice)
        {
            _viewportWidth = graphicsDevice.Viewport.Width;
            _viewportHeight = graphicsDevice.Viewport.Height;

            ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            Scale = 1;
            MoveSpeed = 1f;
        }

        public void Update(GameTime gameTime)
        {
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Pivot.X, Pivot.Y, 0) *
                        Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

            Pivot = ScreenCenter / Scale;
        }

    }
}
