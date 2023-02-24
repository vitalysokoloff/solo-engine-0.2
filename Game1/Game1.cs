using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solo.Core;
using Solo.Physics;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject gameObject;
        GameObject gameObject2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {

            gameObject = new GameObject(new Vector2(100, 100), 1f);
            gameObject.Components.SetPhysicalCollider(new Rect(gameObject, 0, 0, 50, 50));
            gameObject.Components.Get<Collider>("physical").GenerateTexture(graphics);
            gameObject2 = new GameObject(new Vector2(300, 100), 1f);
            gameObject2.Components.SetPhysicalCollider(new Rect(gameObject2, 0, 0, 50, 50));
            gameObject2.Components.Get<Collider>("physical").GenerateTexture(graphics);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
           
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                gameObject.Move(new Vector2(-1, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                gameObject.Move(new Vector2(1, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                gameObject.Rotate(0.17f);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                gameObject.Rotate(-0.17f);

            if (gameObject.Components.Get<Collider>("physical").Intersects(gameObject2.Components.Get<Collider>("physical")))
            {
                gameObject.Components.Get<Collider>("physical").SetColor(Color.Red);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            gameObject.Draw(spriteBatch);
            gameObject2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
