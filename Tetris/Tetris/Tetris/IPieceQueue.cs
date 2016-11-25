using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// 次に落ちてくるピースを待ち行列で保持するクラスです.
	/// </summary>
	public interface IPieceQueue
	{
		/// <summary>
		/// 最大保持数.
		/// キューのサイズは常にこれをキープします。
		/// </summary>
		int Size {
			get;
		}

		/// <summary>
		/// キューの指定位置のピースを削除せずに返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		IPiece Peek(int index);

		/// <summary>
		/// キューの先頭を削除して返します.
		/// また、その後に末尾にピースが追加されます。
		/// </summary>
		/// <returns></returns>
		IPiece Take();
	}
}
