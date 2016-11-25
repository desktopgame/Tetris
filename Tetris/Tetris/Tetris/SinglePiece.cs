using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// １マスのピースを表します.
	/// </summary>
	public class SinglePiece : IPiece
	{
		public int Width
		{
			get { return 1; }
		}

		public int Height
		{
			get { return 1; }
		}
		
		public object RenderingSource
		{
			private set; get;
		}

		
		public SinglePiece(object renderingSource)
		{
			this.RenderingSource = renderingSource;
		}

		public void LeftRotate()
		{
		}

		public void RightRotate()
		{
		}

		public bool IsVisible(int upperLeftRow, int upperLeftColumn, int row, int column)
		{
			return upperLeftRow == row &&
				   upperLeftColumn == column;
		}
	}
}
