using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tetris.Input;

namespace Tetris
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Renderer renderer;
		private Number number;
		private TetrisView tetrisView;
		private int score;

		public Game1()
		{
			this.graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			this.tetrisView = new TetrisView(14, 9);
			tetrisView.Position = new Vector2(32, 0);
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
			this.spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			this.renderer = new Renderer(Content, spriteBatch);
			this.number = new Number();
			renderer.LoadTexture("Blue");
			renderer.LoadTexture("Red");
			renderer.LoadTexture("Green");
			renderer.LoadTexture("Yellow");
			renderer.LoadTexture("Frame");
			renderer.LoadTexture("Start");
			renderer.LoadTexture("Next");
			renderer.LoadTexture("Score");
			renderer.LoadTexture("Number");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
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
			// Allows the game to exit
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here
			if(Detector.GetInstance().IsDetect(Keys.Space))
			{
				this.score = 0;
				tetrisView.Initialize();
			}
			tetrisView.Update(gameTime);
			score += (tetrisView.Model.RemoveLines() * 10);
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			renderer.Begin();
			renderer.Draw("Frame", Vector2.Zero, Color.White);
			tetrisView.Draw(gameTime, renderer);
			//NEXT
			Vector2 nextScale = new Vector2(1, 1);
			Vector2 nextImagePosition = new Vector2(352, 0);
			Vector2 nextPosition = nextImagePosition + new Vector2(166 + 32, 31 + 20);
			IPiece piece = tetrisView.Queue.Peek(0);
			renderer.Draw("Next", nextImagePosition, Color.White);
			for(int i=0; i<piece.Width; i++)
			{
				for(int j=0; j<piece.Height; j++)
				{
					Vector2 offset = new Vector2(i * 32, j * 32);
					if(!piece.IsVisible(0, 0, j, i))
					{
						continue;
					}
					renderer.Draw(piece.RenderingSource.ToString(), nextPosition + offset, Color.White);
				}
			}
			//SCORE
			Vector2 scoreScale = new Vector2(1, 1);
			Vector2 scoreImagePosition = new Vector2(352, 200);
			Vector2 scoreNumPosition = scoreImagePosition + new Vector2(31, 91);
			renderer.Draw("Score", scoreImagePosition, Color.White);
			number.DrawHorizontal(renderer, scoreNumPosition, score);
			//PRES SPACE
			if(tetrisView.SelectionModel.Piece == null)
			{
				Vector2 size = new Vector2(346, 60);
				Vector2 screenSize = new Vector2(800, 480);
				Vector2 position = ((screenSize - size) / 2) + new Vector2(0, 100);
				renderer.Draw("Start", position, Color.White);
			}
			renderer.End();
			base.Draw(gameTime);
		}
	}
}
