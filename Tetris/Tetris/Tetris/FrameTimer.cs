using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// フレーム単位で時間を計測するタイマーです.
	/// </summary>
	public class FrameTimer
	{
		private int offset;
		private int limit;

		public FrameTimer(int limit)
		{
			this.offset = 0;
			this.limit = limit;
		}

		/// <summary>
		/// オフセットを0に戻します.
		/// </summary>
		public void Clear()
		{
			this.offset = 0;
		}

		/// <summary>
		/// オフセットを加算します.
		/// </summary>
		/// <returns></returns>
		public FrameTimer Update()
		{
			if(offset++ >= limit)
			{
				this.offset = 0;
			}
			return this;
		}

		/// <summary>
		/// オフセットが0なら(指定した時間が経過したなら)true.
		/// </summary>
		/// <returns></returns>
		public bool IsElapsed()
		{
			return offset == 0;
		}
	}
}
