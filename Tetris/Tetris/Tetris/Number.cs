using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Tetris;

namespace Tetris
{
	/// <summary>
	/// 数字を描画するためのヘルパークラスです.
	/// </summary>
	public class Number
	{
		private Texture2D fontTexture;
		private Vector2 CharacterSize;


		public Number()
		{
		}

		/// <summary>
		/// テクスチャを取得します.
		/// </summary>
		/// <param name="renderer"></param>
		private void CheckTexture(Renderer renderer)
		{
			if(fontTexture != null)
			{
				return;
			}
			this.fontTexture = renderer.GetTexture("Number");
			this.CharacterSize = new Vector2(fontTexture.Width / 10, fontTexture.Height);
		}

		/// <summary>
		/// 指定の文字数を描画するのに必要な大きさを返します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public Vector2 MeasureSize(Renderer renderer, int length)
		{
			CheckTexture(renderer);
			return CharacterSize * length;
		}

		/// <summary>
		/// 文字数で計測します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="nStr"></param>
		/// <returns></returns>
		public Vector2 MeasureSize(Renderer renderer, string nStr)
		{
			return MeasureSize(renderer, nStr.Length);
		}

		/// <summary>
		/// 指定の設定で指定の添え字を描画します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="rotate"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="effect"></param>
		/// <param name="layerDepth"></param>
		/// <param name="index"></param>
		/// <exception cref="ArgumentException">引数が10以上のとき</exception>
		public void Draw(Renderer renderer, Vector2 position, Color color, float rotate, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth, int index)
		{
			if(index >= 10)
			{
				throw new ArgumentException();
			}
			CheckTexture(renderer);
			Rectangle area = new Rectangle(
				(int)(index * CharacterSize.X),
				0,
				(int)CharacterSize.X,
				(int)CharacterSize.Y
			);
			renderer.Draw(
				"Number",
				position,
				area,
				color,
				rotate,
				origin,
				scale,
				effect,
				layerDepth
			);
		}

		/// <summary>
		/// デフォルトの設定で添え字を描画します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="position"></param>
		/// <param name="index"></param>
		public void Draw(Renderer renderer, Vector2 position, int index)
		{
			Draw(renderer, position, Color.White, 0f, Vector2.Zero, new Vector2(1,1), SpriteEffects.None, 0f, index);
		}

		/// <summary>
		/// 指定の数字を一文字ずつ横方向に位置をずらして描画します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="rotate"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="effect"></param>
		/// <param name="layerDepth"></param>
		/// <param name="number"></param>
		public void DrawHorizontal(Renderer renderer, Vector2 position, Color color, float rotate, Vector2 origin, float scale, SpriteEffects effect, float layerDepth, int number)
		{
			CheckTexture(renderer);
			string numberStr = number.ToString();
			Vector2 basePoint = position;
			for(int i = 0; i < numberStr.Length; i++)
			{
				int numberAt = int.Parse(numberStr.Substring(i, 1));
				Draw(renderer, basePoint, color, rotate, origin, new Vector2(scale, scale), effect, layerDepth, numberAt);
				basePoint.X += CharacterSize.X;
			}
		}

		/// <summary>
		/// デフォルトの設定で描画します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="position"></param>
		/// <param name="number"></param>
		public void DrawHorizontal(Renderer renderer, Vector2 position, int number)
		{
			DrawHorizontal(renderer, position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f, number);
		}

		/// <summary>
		/// 1文字あたりの大きさを返します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <returns></returns>
		public Vector2 GetCharacterSize(Renderer renderer)
		{
			CheckTexture(renderer);
			return CharacterSize;
		}
	}
}
