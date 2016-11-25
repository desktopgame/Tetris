using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// ITetrisRendererのデフォルト実装です.
	/// </summary>
	public class TetrisRenderer : ITetrisRenderer
	{
		public void Draw(Renderer renderer, TetrisView view, int row, int column, out TetrisPoint[] alloc)
		{
			IPiece piece = view.Model[row, column];
			if(piece == null)
			{
				alloc = new TetrisPoint[0];
				return;
			}
			List<TetrisPoint> list = new List<TetrisPoint>();
			for(int i=0; i<piece.Width; i++)
			{
				for(int j=0; j<piece.Height; j++)
				{
					int realRow = row + j;
					int realColumn = column + i;
					if(!piece.IsVisible(row, column, realRow, realColumn))
					{
						continue;
					}
					Vector2 pos = view.ModelToView(realRow, realColumn);
					list.Add(new TetrisPoint(realRow, realColumn));
					TetrisColorEnum? colorE = piece.RenderingSource as TetrisColorEnum?;
					if(!colorE.HasValue)
					{
						throw new ArgumentException();
					}
					DrawAt(renderer, pos, colorE.Value);
				}
			}
			alloc = list.ToArray();
		}

		protected virtual void DrawAt(Renderer renderer, Vector2 pos, TetrisColorEnum colorE)
		{
			renderer.Draw(colorE.ToString(), pos, Color.White);
		}
	}
}
