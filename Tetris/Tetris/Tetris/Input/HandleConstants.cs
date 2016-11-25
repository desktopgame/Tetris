using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tetris.Input
{
	/// <summary>
	/// キーボードの入力とパッドの入力を紐づけるハンドルを定義する定数クラスです.
	/// </summary>
	public static class HandleConstants
	{
		/// <summary>
		/// 決定キー.
		/// </summary>
		public static readonly Handle ENTER = new Handle(
			(index, mouse, key) => (key.IsKeyDown(Keys.Z) || key.IsKeyDown(Keys.Space)),
			(index, gamePad) => gamePad.Buttons.A == ButtonState.Pressed
		);

		/// <summary>
		/// キャンセルキー.
		/// </summary>
		public static readonly Handle CANCEL = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.X),
			(index, gamePad) => gamePad.Buttons.B == ButtonState.Pressed
		);

		/// <summary>
		/// 抽出キー.
		/// </summary>
		public static readonly Handle EXTRACT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.C),
			(index, gamePad) => gamePad.Buttons.X == ButtonState.Pressed
		);

		/// <summary>
		/// 上キー.
		/// </summary>
		public static readonly Handle TOP = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Up),
			(index, gamePad) =>
				gamePad.ThumbSticks.Left.Y > 0
		);

		/// <summary>
		/// 左キー.
		/// </summary>
		public static readonly Handle LEFT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Left),
			(index, gamePad) =>
				gamePad.ThumbSticks.Left.X < 0
		);

		/// <summary>
		/// 下キー.
		/// </summary>
		public static readonly Handle BOTTOM = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Down),
			(index, gamePad) =>
				gamePad.ThumbSticks.Left.Y < 0
		);

		/// <summary>
		/// 右キー.
		/// </summary>
		public static readonly Handle RIGHT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Right),
			(index, gamePad) =>
				gamePad.ThumbSticks.Left.X > 0
		);

		/// <summary>
		/// ポーズキー.
		/// </summary>
		public static readonly Handle PAUSE = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Q),
			(index, gamePad) => gamePad.Buttons.Start == ButtonState.Pressed
		);

		/// <summary>
		/// 終了キー.
		/// </summary>
		public static readonly Handle EXIT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Escape),
			(index, gamePad) => gamePad.Buttons.Back == ButtonState.Pressed
		);

		/// <summary>
		/// Aキー.
		/// </summary>
		public static readonly Handle A = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.A),
			(index, gamePad) => gamePad.Buttons.B == ButtonState.Pressed
		);

		/// <summary>
		/// Bキー.
		/// </summary>
		public static readonly Handle B = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.B),
			(index, gamePad) => gamePad.Buttons.B == ButtonState.Pressed
		);

		#region 隠しコマンド用
		private static Dictionary<string, Keys> converter = null;
		private static Dictionary<Keys, Handle> di = null;

		private static void Init()
		{
			if(converter != null && di != null)
			{
				return;
			}
			HandleConstants.converter = new Dictionary<string, Keys>();
			HandleConstants.di = new Dictionary<Keys, Handle>();
			Keys[] keys = (Keys[])Enum.GetValues(typeof(Keys));
			for(int i = 0; i < keys.Length; i++)
			{
				Keys key = keys[i];
				string s = Enum.GetName(typeof(Keys), key);
				char c = s[0];
				if(s.Length != 1 || !char.IsLetter(c))
				{
					continue;
				}
				converter[c.ToString().ToUpper()] = key;
				di[key] = new Handle(
					(index, mouse, keyS) => keyS.IsKeyDown(key),
					(index, gamePad) => false
				);
			}
		}

		/// <summary>
		/// キーコードを列挙へ変換します.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static Keys CharForKey(char c)
		{
			Init();
			return converter[c.ToString().ToUpper()];
		}

		/// <summary>
		/// 指定のキー入力を許容するハンドルを返します.
		/// ただしここで返されるハンドルはパッドには対応していません。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static Handle HandleForKey(Keys key)
		{
			Init();
			return di[key];
		}
		#endregion
	}
}
