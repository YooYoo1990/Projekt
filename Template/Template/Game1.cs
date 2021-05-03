using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D xwing;
        Vector2 xwingpos = new Vector2(340, 300);
        //KOmentar

        List<Skott> skott = new List<Skott>();
        KeyboardState pastKey;

        public Game1()
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
            // TODO: Add your initialization logic here

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
            xwing = Content.Load<Texture2D>("xwing");

            // TODO: use this.Content to load your game content here 
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
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Right))
                xwingpos.X += 5;
            if (kstate.IsKeyDown(Keys.Left))
                xwingpos.X -= 5;
            if (xwingpos.X < 0)
                xwingpos.X = 0;
            if (kstate.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
                Skjuta();

            pastKey = Keyboard.GetState();
            UpdateSkott();




            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void UpdateSkott()
        {
            foreach(Skott skott in skott)
            {
                skott.position += skott.hastighet;
                if (Vector2.Distance(skott.position, xwingpos) > 500)
                    skott.synliga = false;
            }
            for (int i = 0; i < skott.Count; i++)
            {
                if (!skott[i].synliga)
                    skott.RemoveAt(i);
                i--;
            }
        }

        public void Skjuta()
        {
            Skott nyttSkott = new Skott(Content.Load<Texture2D>("pixel"));
            nyttSkott.position = xwingpos + nyttSkott.hastighet * 5;
            nyttSkott.synliga = true;

            if (skott.Count() < 20)
                skott.Add(nyttSkott);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(xwing, xwingpos, Color.White);
            foreach (Skott skott in skott)
                skott.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here.

            base.Draw(gameTime);
        }
    }
}
