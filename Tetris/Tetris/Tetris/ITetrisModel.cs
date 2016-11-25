using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// テトリスの実行モデル.
	/// </summary>
	public interface ITetrisModel
	{
		/// <summary>
		/// 行数.
		/// </summary>
		int RowCount
		{
			get;
		}

		/// <summary>
		/// 列数.
		/// </summary>
		int ColumnCount
		{
			get;
		}

		/// <summary>
		/// 指定位置のピース.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		IPiece this[int row, int column]
		{
			set; get;
		}

		/// <summary>
		/// 指定のピースが左方向へ回転できるかどうかを返します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="piece"></param>
		/// <returns></returns>
		bool CanTurnLeft(int row, int column, IPiece piece);

		/// <summary>
		/// 指定のピースが右方向へ回転できるかどうかを返します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="piece"></param>
		/// <returns></returns>
		bool CanTurnRight(int row, int column, IPiece piece);

		/// <summary>
		/// 指定位置にピースを置けるかどうかを返します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="piece"></param>
		/// <returns></returns>
		bool IsPlaceable(int row, int column, IPiece piece);

		/// <summary>
		/// 揃っている行を全て削除して削除した行数を返します.
		/// </summary>
		/// <returns></returns>
		int RemoveLines();
	}
}
