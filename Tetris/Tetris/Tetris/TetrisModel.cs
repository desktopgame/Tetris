using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// ITetrisModelのデフォルト実装です.
	/// </summary>
	public class TetrisModel : ITetrisModel
	{
		public int RowCount
		{
			private set; get;
		}

		public int ColumnCount
		{
			private set; get;
		}

		public IPiece this[int row, int column]
		{
			get { return table[row, column]; }
			set { table[row, column] = value; }
		}


		private IPiece[,] table;
		

		public TetrisModel(int rowCount, int columnCount)
		{
			this.RowCount = rowCount;
			this.ColumnCount = columnCount;
			this.table = new IPiece[rowCount, columnCount];
		}

		public bool CanTurnLeft(int row, int column, IPiece piece)
		{
			piece.LeftRotate();
			try
			{
				return IsSafeLocation(row, column, piece);
			} finally
			{
				piece.RightRotate();
			}
		}

		public bool CanTurnRight(int row, int column, IPiece piece)
		{
			piece.RightRotate();
			try
			{
				return IsSafeLocation(row, column, piece);
			} finally
			{
				piece.LeftRotate();
			}
		}

		public bool IsPlaceable(int row, int column, IPiece piece)
		{
			List<TetrisPoint> list = GetPointList(row, column, piece);
			//壁を越えているか検査
			if(IsOverRange(list))
			{
				return false;
			}
			//全てのマスに対して
			for(int i=0; i<RowCount; i++)
			{
				for(int j=0; j<ColumnCount; j++)
				{
					//そのマスが空or今検査しているピース自身なら無視
					IPiece at = this[i, j];
					if(at == null || at.Equals(piece))
					{
						continue;
					}
					//そこにピースがあるので無理
					if(i == row && j == column)
					{
						return false;
					}
					//そのピースの全ての占有範囲に対して
					for(int otherWidth=0; otherWidth<at.Width; otherWidth++)
					{
						for(int otherHeight=0; otherHeight<at.Height; otherHeight++)
						{
							int otherRealRow = otherHeight + i;
							int otherRealColumn = otherWidth + j;
							if(!at.IsVisible(i, j, otherRealRow, otherRealColumn))
							{
								continue;
							}
							if(IsOverRange(otherRealRow, otherRealColumn))
							{
								return false;
							}
							if(list.Contains(new TetrisPoint(otherRealRow, otherRealColumn)))
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}

		public int RemoveLines()
		{
			int lines = 0;
			for(int i = 0; i < RowCount; i++)
			{
				if(getPieceCountAtRow(i) != ColumnCount)
				{
					continue;
				}
				//行の削除
				lines++;
				for(int j = 0; j < ColumnCount; j++)
				{
					this[i, j] = null;
				}
				//行の落下
				for(int j = i; j > 0; j--)
				{
					for(int i2 = 0; i2 < ColumnCount; i2++)
					{
						if(this[j - 1, i2] == null)
						{
							continue;
						}
						this[j, i2] = this[j - 1, i2];
						this[j - 1, i2] = null;
					//	move(j - 1, i2, j, i2);
					}
				}
			}
			return lines;
		}

		private int getPieceCountAtRow(int row)
		{
			int count = 0;
			for(int i = 0; i < ColumnCount; i++)
			{
				if(this[row, i] != null)
				{
					count++;
				}
			}
			return count;
		}

		private int getPieceCountAtColumn(int column)
		{
			int count = 0;
			for(int i = 0; i < RowCount; i++)
			{
				if(this[i, column] != null)
				{
					count++;
				}
			}
			return count;
		}

		/// <summary>
		/// 指定のピースが指定の位置において占有している範囲を返します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="piece"></param>
		/// <returns></returns>
		private List<TetrisPoint> GetPointList(int row, int column, IPiece piece)
		{
			List<TetrisPoint> list = new List<TetrisPoint>();
			if(piece == null)
			{
				return list;
			}
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
					list.Add(new TetrisPoint(realRow, realColumn));
				}
			}
			return list;
		}

		/// <summary>
		/// 指定の位置に指定のピースが存在出来るか同課を返します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <param name="piece"></param>
		/// <returns></returns>
		private bool IsSafeLocation(int row, int column, IPiece piece)
		{
			List<TetrisPoint> list = GetPointList(row, column, piece);
			//壁を越えているか検査
			if(IsOverRange(list))
			{
				return false;
			}
			//全てのマスに対して
			for(int i=0; i<RowCount; i++)
			{
				for(int j=0; j<ColumnCount; j++)
				{
					//そのマスが空or今検査しているピース自身なら無視
					IPiece at = this[i, j];
					if(at == null || at.Equals(piece))
					{
						continue;
					}
					//そのマスがもともと今検査しているピースの占有範囲内なら無視
					bool self = false;
					foreach(TetrisPoint elem in list)
					{
						if(elem.Row == i && elem.Column == j)
						{
							self = true;
							break;
						}
					}
					if(self)
					{
						continue;
					}
					//そのピースの全ての占有範囲に対して
					for(int otherWidth=0; otherWidth<at.Width; otherWidth++)
					{
						for(int otherHeight=0; otherHeight<at.Height; otherHeight++)
						{
							int otherRealRow = otherHeight + i;
							int otherRealColumn = otherWidth + j;
							if(!at.IsVisible(i, j, otherRealRow, otherRealColumn))
							{
								continue;
							}
							//他のピースの占有範囲と重複するなら
							foreach(TetrisPoint elem in list)
							{
								if(elem.Row == otherRealRow &&
								   elem.Column == otherRealColumn)
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		/// <summary>
		/// 壁を越えているならtrue.
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		private bool IsOverRange(List<TetrisPoint> list)
		{
			foreach(TetrisPoint elem in list)
			{
				if(IsOverRange(elem.Row, elem.Column))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 壁を越えているならtrue.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		private bool IsOverRange(int row, int column)
		{
			if(row < 0 ||
			   column < 0 ||
			   row >= RowCount ||
			   column >= ColumnCount)
			{
				return true;
			}
			return false;
		}
		
	}
}
