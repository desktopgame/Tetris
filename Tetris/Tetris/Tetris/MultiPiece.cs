using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// 複数のピースによって構成されるピースです.
	/// </summary>
	public class MultiPiece : IPiece
	{
		public int Width
		{
			get { return map.GetLength(1); }
		}

		public int Height
		{
			get { return map.GetLength(0); }
		}

		public object RenderingSource
		{
			private set; get;
		}
		private bool[,] map;


		protected MultiPiece(object renderingSource, bool[,] map)
		{
			this.RenderingSource = renderingSource;
			this.map = map;
		}

		public void LeftRotate()
		{
			this.map = RotateAnticlockwise(map);
		}

		public void RightRotate()
		{
			this.map = RotateClockwise(map);
		}

		public bool IsVisible(int upperLeftRow, int upperLeftColumn, int row, int column)
		{
			return map[row - upperLeftRow, column - upperLeftColumn];
		}

		//引用元:http://noriok.hatenadiary.jp/entry/2015/09/01/002613
		static bool[,] RotateClockwise(bool[,] g)
		{
			// 引数の2次元配列 g を時計回りに回転させたものを返す
			int rows = g.GetLength(0);
			int cols = g.GetLength(1);
			var t = new bool[cols, rows];
			for(int i = 0; i < rows; i++)
			{
				for(int j = 0; j < cols; j++)
				{
					t[j, rows - i - 1] = g[i, j];
				}
			}
			return t;
		}

		static bool[,] RotateAnticlockwise(bool[,] g)
		{
			// 引数の2次元配列 g を反時計回りに回転させたものを返す
			int rows = g.GetLength(0);
			int cols = g.GetLength(1);
			var t = new bool[cols, rows];
			for(int i = 0; i < rows; i++)
			{
				for(int j = 0; j < cols; j++)
				{
					t[cols - j - 1, i] = g[i, j];
				}
			}
			return t;
		}
	}

	/// <summary>
	/// □□
	/// □□
	/// </summary>
	public class RectanglePiece : MultiPiece
	{
		public RectanglePiece(object renderingSource) : base(renderingSource, CreateMap())
		{
		}

		private static bool[,] CreateMap()
		{
			return new bool[,] {
				{ true, true},
				{ true, true}
			};
		}
	}

	/// <summary>
	/// □□□□
	/// </summary>
	public class LinePiece : MultiPiece
	{
		public LinePiece(object renderingSource) : base(renderingSource, CreateMap())
		{
		}

		private static bool[,] CreateMap()
		{
			return new bool[,] {
				{ true, true, true, true},
			};
		}
	}

	/// <summary>
	/// □□□
	///  □
	/// </summary>
	public class TPiece : MultiPiece
	{
		public TPiece(object renderingSource) : base(renderingSource, CreateMap())
		{
		}

		private static bool[,] CreateMap()
		{
			return new bool[,] {
				{ true, true, true},
				{ false, true, false},
			};
		}
	}

	/// <summary>
	/// □□□
	/// □
	/// </summary>
	public class LPiece : MultiPiece
	{
		public LPiece(object renderingSource) : base(renderingSource, CreateMap())
		{
		}

		private static bool[,] CreateMap()
		{
			return new bool[,] {
				{ true, true, true},
				{ true, false, false},
			};
		}
	}

	/// <summary>
	///  □□
	/// □□
	/// </summary>
	public class ZPiece : MultiPiece
	{
		public ZPiece(object renderingSource) : base(renderingSource, CreateMap())
		{
		}

		private static bool[,] CreateMap()
		{
			return new bool[,] {
				{ false, true, true},
				{ true, true, false},
			};
		}
	}
}
