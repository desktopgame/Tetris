using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// 行, 列の位置を表すクラス.
	/// </summary>
	public class TetrisPoint
	{
		/// <summary>
		/// 行.
		/// </summary>
		public int Row
		{
			set; get;
		}

		/// <summary>
		/// 列.
		/// </summary>
		public int Column
		{
			set; get;
		}

		public TetrisPoint(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		public override int GetHashCode()
		{
			return Row ^ Column;
		}

		public override bool Equals(object obj)
		{
			TetrisPoint other = obj as TetrisPoint;
			if(other == null)
			{
				return false;
			}
			return other.Row == Row &&
				   other.Column == Column;
		}
	}
}
