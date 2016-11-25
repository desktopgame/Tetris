using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// 現在ユーザが移動可能な落下中のピースに関する情報を持つクラスです.
	/// </summary>
	public interface IPieceSelectionModel
	{
		/// <summary>
		/// 行位置.
		/// </summary>
		int Row
		{
			set; get;
		}

		/// <summary>
		/// 列位置.
		/// </summary>
		int Column
		{
			set; get;
		}

		/// <summary>
		/// 現在落下しているピースの形状.
		/// </summary>
		IPiece Piece
		{
			set; get;
		}

		/// <summary>
		/// 選択状態を設定します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="piece"></param>
		void Selection(int row, int column, IPiece piece);

		/// <summary>
		/// 選択状態をクリアします.
		/// </summary>
		void Clear();
	}
}
