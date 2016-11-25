using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// IPieceQueueのデフォルト実装です.
	/// ランダムにピースを生成します。
	/// </summary>
	public class PieceQueue : IPieceQueue
	{
		public int Size
		{
			private set; get;
		}

		private List<IPiece> pieceList;
		private Random random;

		public PieceQueue(int size)
		{
			this.pieceList = new List<IPiece>();
			this.random = new Random();
			for(int i=0; i<size; i++)
			{
				pieceList.Add(CreateRandom(CreateRandom()));
			}
		}

		public IPiece Peek(int index)
		{
			return pieceList[index];
		}

		public IPiece Take()
		{
			IPiece head = pieceList[0];
			pieceList.RemoveAt(0);
			pieceList.Add(CreateRandom(CreateRandom()));
			return head;
		}

		private TetrisColorEnum CreateRandom()
		{
			int n = random.Next(4);
			return (TetrisColorEnum)n;
		}

		private IPiece CreateRandom(object o)
		{
			IPiece ret = null;
			int n = random.Next(5);
			switch(n) {
				case 0:
					ret = new RectanglePiece(o);
					break;
				case 1:
					ret = new LinePiece(o);
					break;
				case 2:
					ret = new TPiece(o);
					break;
				case 3:
					ret = new LPiece(o);
					break;
				case 4:
					ret = new ZPiece(o);
					break;
			}

			return ret;
		}
	}
}
