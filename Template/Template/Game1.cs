using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D skott_texture;
        List<Skott> skottlista = new List<Skott>();
        Spelare xwing;
        Texture2D xwing_texture;
        KeyboardState kstate = new KeyboardState();
        Texture2D fighter_texture;
        List<Fiende> fiendelista = new List<Fiende>();
        Random random = new Random();

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
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            // Sätter spelrutan till en viss bredd samt höjd^^

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
            xwing_texture = Content.Load<Texture2D>("xwing");
            skott_texture = Content.Load<Texture2D>("skott");
            fighter_texture = Content.Load<Texture2D>("tiefighter");
            xwing = new Spelare(xwing_texture, new Vector2(Window.ClientBounds.Width / 2 - xwing_texture.Width, Window.ClientBounds.Height - xwing_texture.Height), 3);

            for (int i = 0; i < 1; i++)
            {
                fiendelista.Add(new Fiende(fighter_texture, new Vector2(400, 50), 2));
                fiendelista.Add(new Fiende(fighter_texture, new Vector2(600, 50), 1));
                fiendelista.Add(new Fiende(fighter_texture, new Vector2(200, 50), 1));
            }
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

            kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Right))
            {
                xwing.FlyttaPosX(+3);
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                xwing.FlyttaPosX(-3);
            }

            // Knappar för att flytta ^^

        if (kstate.IsKeyDown(Keys.Space) && System.Math.Abs(xwing.SenasteSkott - gameTime.TotalGameTime.Milliseconds) > xwing.FireRate)
            {
                xwing.SenasteSkott = gameTime.TotalGameTime.Milliseconds;
                skottlista.Add(new Skott(skott_texture, new Vector2(xwing.Position.X + 5, xwing.Position.Y + 8)));
                skottlista.Add(new Skott(skott_texture, new Vector2(xwing.Position.X + xwing.Texture.Bounds.Width - 12, xwing.Position.Y + 8)));
            }

            // När man trycker eller håller ner space så ska skotten skjutas med ett visst mellanrum. Skotten ska också skjutas från båda kanonerna på xwing.

                foreach (Skott i in skottlista)
                {
                    bool träff = false;
                    i.FlyttaPosY(2);
                    foreach (Fiende e in fiendelista)
                    {
                        if (i.Box_Runt.Intersects(e.Box_Runt))
                        {
                            träff = true;
                            fiendelista.Remove(e);
                            skottlista.Remove(i);
                            break;
                        }
                    }
                if (träff)
                    break;
                }          

            // Kollar om skott träffar och även att skotten rör sig uppåt.

            foreach (Fiende i in fiendelista)
            {
                i.FlyttaPosY(1);
                if (i.Position.Y >= 720)
                {
                    Exit();
                }
                if (i.Box_Runt.Intersects(xwing.Box_Runt))
                {
                    Exit();
                }
            }

            // Att fienden rör sig mot spelaren och om fiende träffar spelaren så avslutas spelet. Om Fiender åker utanför spelrutan längst ner så avslutas spelet.

            int Rand = random.Next(0, 970);
            int Rand2 = random.Next(0, 970);
            int Rand3 = random.Next(0, 970);
            if (fiendelista.Count < 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    fiendelista.Add(new Fiende(fighter_texture, new Vector2(Rand, 50), 2));
                    fiendelista.Add(new Fiende(fighter_texture, new Vector2(Rand2, 50), 1));
                    fiendelista.Add(new Fiende(fighter_texture, new Vector2(Rand3, 50), 1));
                }
            }

            if (xwing.Position.X <= 0 && (kstate.IsKeyDown(Keys.Left)))

            {
                xwing.Speed = 0;
            }

            else if (kstate.IsKeyDown(Keys.Right))
            {
                xwing.Speed = 3;
            }


            if (xwing.Position.X >= 970)
            {
                if (kstate.IsKeyDown(Keys.Right))
                {
                    xwing.Speed = 0;
                }

                else if (kstate.IsKeyDown(Keys.Left))
                {
                    xwing.Speed = 3;
                }

                // Så att spelaren inte åker utanför spelrutan ^^
                base.Update(gameTime);
            }
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            xwing.Draw(spriteBatch);
            foreach (Skott i in skottlista)
            {
                i.Draw(spriteBatch);
            }

            foreach (Fiende i in fiendelista)
            {
                i.Draw(spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here.

            base.Draw(gameTime);
        }
    }
}