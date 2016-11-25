using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tetris.Input;

namespace Tetris
{
	/// <summary>
	/// テトリスを部品として利用するためのクラスです.
	/// </summary>
	public class TetrisView
	{
		/// <summary>
		/// 画面に表示される盤面です.
		/// </summary>
		public ITetrisModel Model
		{
			set; get;
		}

		/// <summary>
		/// 選択状態を保持するモデルです.
		/// </summary>
		public IPieceSelectionModel SelectionModel
		{
			set; get;
		}

		/// <summary>
		/// 盤面を描画するインターフェイスです.
		/// </summary>
		public ITetrisRenderer Renderer
		{
			set; get;
		}

		/// <summary>
		/// 次に落ちてくる色を管理するキューです.
		/// </summary>
		public IPieceQueue Queue
		{
			set; get;
		}
		
		/// <summary>
		/// このビューの描画開始位置です.
		/// </summary>
		public Vector2 Position
		{
			set; get;
		}

		/// <summary>
		/// １マスあたりの大きさです.
		/// </summary>
		public int TileSize
		{
			set; get;
		}

		private FrameTimer fallTimer;

		public TetrisView(int rowCount, int columnCount) : base()
		{
			this.Model = new TetrisModel(rowCount, columnCount);
			this.SelectionModel = new PieceSelectionModel();
			this.Renderer = new TetrisRenderer();
			this.Queue = new PieceQueue(3);
			this.Position = Vector2.Zero;
			this.TileSize = 32;

			this.fallTimer = new FrameTimer(10);
		}

		public virtual void Initialize()
		{
			this.Model = new TetrisModel(Model.RowCount, Model.ColumnCount);
			this.SelectionModel = new PieceSelectionModel();
			New();
		}

		/// <summary>
		/// 盤面を更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{
			//選択されていない
			if(SelectionModel.Piece == null)
			{
				return;
			}
			//選択されているピースを落下
			if(fallTimer.Update().IsElapsed())
			{
				Fall();
			}
			Control();
		}

		private void Fall()
		{
			//落ちる前の位置を削除
			Model[SelectionModel.Row, SelectionModel.Column] = null;
			//落ちる
			SelectionModel.Row++;
			Model[SelectionModel.Row, SelectionModel.Column] = SelectionModel.Piece;
			//これ以上落下出来ない
			if(!Model.IsPlaceable(SelectionModel.Row + 1, SelectionModel.Column, SelectionModel.Piece))
			{
				Decomposition();
				SelectionModel.Clear();
				New();
				return;
			}
		}
		
		private void New()
		{
			IPiece piece = Queue.Take();
			SelectionModel.Row = piece.Height;
			SelectionModel.Column = (Model.ColumnCount - piece.Width) / 2;
			SelectionModel.Piece = piece;
			if(Model.IsPlaceable(SelectionModel.Row, SelectionModel.Column, piece))
			{
				Model[SelectionModel.Row, SelectionModel.Column] = SelectionModel.Piece;
			} else
			{
				SelectionModel.Clear();
			}
		}

		private void Decomposition()
		{
			int row = SelectionModel.Row;
			int col = SelectionModel.Column;
			IPiece piece = SelectionModel.Piece;
			Model[row, col] = null;
			for(int i = 0; i < piece.Width; i++)
			{
				for(int j = 0; j < piece.Height; j++)
				{
					int realRow = row + j;
					int realColumn = col + i;
					if(!piece.IsVisible(row, col, realRow, realColumn))
					{
						continue;
					}
					Model[realRow, realColumn] = new SinglePiece(piece.RenderingSource);
				}
			}
		}

		private void Control()
		{
			int sRow = SelectionModel.Row;
			int sCol = SelectionModel.Column;
			bool move = false;
			IPiece sPiece = SelectionModel.Piece;
			Detector detector = Detector.GetInstance();
			//キー入力で移動
			if(detector.IsDetect(Handle.LEFT))
			{
				move = true;
				SelectionModel.Column--;
			} else if(detector.IsDetect(Handle.RIGHT))
			{
				move = true;
				SelectionModel.Column++;
			} else if(detector.IsDetect(Handle.DOWN))
			{
				if(Model.IsPlaceable(SelectionModel.Row + 1, SelectionModel.Column, sPiece))
				{
					move = true;
					SelectionModel.Row++;
				}
			} else if(detector.IsDetect(Keys.Enter))
			{
				sPiece.RightRotate();
			}
			//移動を適用
			if(!move)
			{
				return;
			}
			SelectionModel.Row = Math.Min(Model.RowCount - sPiece.Height, Math.Max(0, SelectionModel.Row));
			SelectionModel.Column = Math.Min(Model.ColumnCount - sPiece.Width, Math.Max(0, SelectionModel.Column));
			if(Model.IsPlaceable(SelectionModel.Row, SelectionModel.Column, SelectionModel.Piece))
			{
				Model[sRow, sCol] = null;
				Model[SelectionModel.Row, SelectionModel.Column] = SelectionModel.Piece;
			} else
			{
				SelectionModel.Row = sRow;
				SelectionModel.Column = sCol;
			}
		}

		/// <summary>
		/// 盤面を描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		public virtual void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Draw("Frame", Vector2.Zero, Color.White);
			List<TetrisPoint> list = new List<TetrisPoint>();
			for(int i=0; i<Model.RowCount; i++)
			{
				for(int j=0; j<Model.ColumnCount; j++)
				{
					//指定位置に何もないor既にバッファされているなら次へ
					IPiece piece = Model[i, j];
					if(piece ==null || list.Contains(new TetrisPoint(i, j)))
					{
						continue;
					}
					//指定位置を描画して描画された範囲をバッファ
					TetrisPoint[] alloc;
					Renderer.Draw(renderer, this, i, j, out alloc);
					list.AddRange(alloc);
				}
			}
		}

		/// <summary>
		/// 指定の位置を描画位置へ変換します.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public Vector2 ModelToView(int row, int column)
		{
			return new Vector2(
				(column * TileSize),
				(row * TileSize)
			) + Position;
		}
	}
}
