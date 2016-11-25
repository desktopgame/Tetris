using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// IPieceSelectionModelのデフォルト実装です.
	/// </summary>
	public class PieceSelectionModel : IPieceSelectionModel
	{
		public int Row
		{
			set; get;
		}

		public int Column
		{
			set; get;
		}

		public IPiece Piece
		{
			set; get;
		}
		
		
		public PieceSelectionModel()
		{
			Clear();
		}

		public void Selection(int row, int column, IPiece piece)
		{
			this.Row = row;
			this.Column = column;
			this.Piece = piece;
		}

		public void Clear()
		{
			this.Row = -1;
			this.Column = -1;
			this.Piece = null;
		}
	}
}
