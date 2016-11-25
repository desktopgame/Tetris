using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// テクスチャを保持し、描画処理を担当するクラス.
	/// </summary>
	public class Renderer
	{
		private ContentManager contentManager;
		private SpriteBatch spriteBatch;
		private Dictionary<string, Texture2D> textureDictionary;

		public Renderer(ContentManager contentManager, SpriteBatch spriteBatch)
		{
			this.contentManager = contentManager;
			this.spriteBatch = spriteBatch;
			this.textureDictionary = new Dictionary<string, Texture2D>();
		}
		/// <summary>
		/// 指定のパスのコンテンツを読み込んで辞書へ登録します.
		/// </summary>
		/// <param name="path"></param>
		public void LoadTexture(string path)
		{
			if(textureDictionary.ContainsKey(path))
			{
				return;
			}
			Texture2D texture = contentManager.Load<Texture2D>(path);
			textureDictionary[path] = texture;
		}

		/// <summary>
		/// 辞書へ登録されたコンテンツを削除します.
		/// </summary>
		/// <param name="path"></param>
		public void UnloadTexture(string path)
		{
			if(!textureDictionary.ContainsKey(path))
			{
				return;
			}
			textureDictionary.Remove(path);
		}

		/// <summary>
		/// 辞書へ登録されたコンテンツを返します.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public Texture2D GetTexture(string path)
		{
			return textureDictionary[path];
		}

		/// <summary>
		/// 描画処理を開始します.
		/// </summary>
		public void Begin()
		{
			spriteBatch.Begin();
		}

		/// <summary>
		/// 指定のパスの画像を描画します.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="pos"></param>
		/// <param name="color"></param>
		public void Draw(string path, Vector2 pos, Color color)
		{
			spriteBatch.Draw(textureDictionary[path], pos, color);
		}

		/// <summary>
		/// 倍率を指定して描画します.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="pos"></param>
		/// <param name="scale"></param>
		/// <param name="color"></param>
		public void Draw(string path, Vector2 pos, Vector2 scale, Color color)
		{
			spriteBatch.Draw(
				textureDictionary[path],
				pos,
				null,
				color,
				0f,
				Vector2.Zero,
				scale,
				SpriteEffects.None,
				0f
			);
		}

		/// <summary>
		/// 全てのパラメータを指定して描画します.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="dst"></param>
		/// <param name="src"></param>
		/// <param name="color"></param>
		/// <param name="rotate"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="effects"></param>
		/// <param name="layerDepth"></param>
		public void Draw(string path, Vector2 dst, Rectangle? src, Color color, float rotate, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			spriteBatch.Draw(
				textureDictionary[path],
				dst,
				src,
				color,
				rotate,
				origin,
				scale,
				effects,
				layerDepth
			);
		}

		/// <summary>
		/// 描画処理を終了します.
		/// </summary>
		public void End()
		{
			spriteBatch.End();
		}
	}
}
