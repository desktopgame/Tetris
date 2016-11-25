using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// テトリスのグリッド上に配置されるピースです.
	/// </summary>
	public interface IPiece
	{
		/// <summary>
		/// 空白部分を含む完全に覆いつくす横幅.
		/// </summary>
		int Width
		{
			get;
		}

		/// <summary>
		/// 空白部分を含む完全に覆いつくす縦幅.
		/// </summary>
		int Height
		{
			get;
		}

		/// <summary>
		/// 描画に必要なオブジェクトを提供します.
		/// このインスタンスはITetrisRendererの実装クラスによって解釈されます。
		/// </summary>
		object RenderingSource
		{
			get;
		}

		/// <summary>
		/// 左方向へ回転.
		/// </summary>
		void LeftRotate();

		/// <summary>
		/// 右方向へ回転.
		/// </summary>
		void RightRotate();

		/// <summary>
		/// 指定位置が空白部分でないかどうかを返します.
		/// </summary>
		/// <param name="upperLeftRow"></param>
		/// <param name="upperLeftColumn"></param>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		bool IsVisible(int upperLeftRow, int upperLeftColumn, int row, int column);
	}
}
