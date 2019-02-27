

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;



namespace StatePatternExample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class StateExample : Game
    {
       private  GraphicsDeviceManager graphics;
       private SpriteBatch spriteBatch;


       private GameAgent agent;

       private List<GameObject> gameObjects;


        public StateExample()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Rectangle objectPosition;

            Rectangle viewPortBounds;

            int viewWidth = GraphicsDevice.Viewport.Width;
            int viewHeight = GraphicsDevice.Viewport.Height;

            viewPortBounds = new Rectangle(0, 0, viewWidth, viewHeight);


            // define position and size of agent
            objectPosition = new Rectangle(400, 200, 30, 20);

            agent = new GameAgent(objectPosition, viewPortBounds, Color.Blue);

            // create the game objects and add to the list
            GameObject tempObject;

            gameObjects = new List<GameObject>();

            // define position and size of food object
            objectPosition = new Rectangle(600, 50, 80, 60);


            tempObject = new GameObject(objectPosition, Color.White);

            gameObjects.Add(tempObject);

            // water object
            objectPosition = new Rectangle(600, 300, 80, 80);

            tempObject = new GameObject(objectPosition, Color.White);

            gameObjects.Add(tempObject);

            // bed object
            objectPosition = new Rectangle(60, 200, 80, 80);

            tempObject = new GameObject(objectPosition, Color.White);

            gameObjects.Add(tempObject);


            agent.GameObjects = gameObjects;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load in an image
            agent.CharacterImage = Content.Load<Texture2D>("images\\NPC");

            gameObjects[0].CharacterImage = Content.Load<Texture2D>("images\\burger");

            gameObjects[1].CharacterImage = Content.Load<Texture2D>("images\\waterglass");

            gameObjects[2].CharacterImage = Content.Load<Texture2D>("images\\bed");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

        
            agent.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            foreach (GameObject g in gameObjects)
            {
                g.Draw(spriteBatch);
            }

            agent.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
