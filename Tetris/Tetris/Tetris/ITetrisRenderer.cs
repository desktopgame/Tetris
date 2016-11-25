using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// ピースの描画実装です.
	/// </summary>
	public interface ITetrisRenderer
	{
		/// <summary>
		/// 指定位置のピースを描画します.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="view"></param>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="alloc"></param>
		void Draw(Renderer renderer, TetrisView view, int row, int column, out TetrisPoint[] alloc);
	}
}
