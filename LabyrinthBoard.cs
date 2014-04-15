using UnityEngine;
using System.Collections;

namespace labyrinthGenerator
{		
	public enum map : int {wall = 1, empty = 0};

	public class LabyrinthBoard{
		int[,,] board;
		int width, height, size;
		enum sides : int {down = 0, left = 1, up = 2, right = 3};

		public int this[int x, int y, int z]
		{
			get
			{
				return this.board[x,y,z];
			}
		}
		
		public int getWidth()
		{
			return width;
		}
		
		public int getHeight()
		{
			return height;
		}

		public LabyrinthBoard(int x, int y) 
		{
			width = x;
			height = y;
			size = width * height;
			
			board = new int[x,y,4]; 

			CreateBorder(); 
			FillPartRandomly(); 
			FillWholeLabyrinth();
		} 
		
		void CreateBorder() 
		{
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					for (int s = 0; s < 4; s++) {
						fillOnlyBorder(x,y,s);
					}
				}
			}
		}

		void fillOnlyBorder(int x, int y, int s)
		{
			if (isInBorder(x,y,s))
				board[x,y,s] = (int)map.wall;
			else
				board[x,y,s] = (int)map.empty;
		}

		bool isInBorder(int x, int y, int s)
		{
			return ( isInDownBorder(x,y,s) || isInLeftBorder(x,y,s)|| isInUpBorder(x,y,s) || isInRightBorder(x,y,s));
		}

		bool isInDownBorder(int x, int y, int s)
		{
			return y == 0 && s == (int)sides.down;
		}
		
		bool isInLeftBorder(int x, int y, int s)
		{
			return x == 0 && s == (int)sides.left;
		}
		
		bool isInUpBorder(int x, int y, int s)
		{
			return y == (height - 1) && s == (int)sides.up;
		}
		
		bool isInRightBorder(int x, int y, int s)
		{
			return x == (width - 1) && s == (int)sides.right;
		}
		
		void FillPartRandomly() {
			Point rand;
			int tmp;

			for (int i = 1, retries = 0; (i < size * 3 / 10) && (retries < 20) ; retries++) {
				rand = getRandomPoint();
				tmp = Random.Range(0,4);
				if (!isReadyForAnotherWall(rand.width , rand.height)) 
					continue;
				for (int k = 0; k < 4; k++) {
					if (canWallBePlaced(rand.width, rand.height, tmp % 4)){
						board[rand.width, rand.height, tmp % 4] = (int)map.wall;
						Complacement(rand.width, rand.height, tmp % 4);
						i++;
						retries=0;
						k = 4;
					}
					tmp++;
				}
			}
		}
		
		Point getRandomPoint()
		{
			return new Point(Random.Range(0,width-1), Random.Range(0,height-1));
		}

		bool isReadyForAnotherWall(int x, int y) {
			int wallsAround = 0;
			for (int i = 0; i < 4; i++)
				if (board[x,y,i] == (int)map.wall)
					wallsAround++;
			if (wallsAround > 1)
				return false;
			else
				return true;
		}

		bool canWallBePlaced(int width, int height, int side)
		{
			if(board[width, height, side] == (int)map.empty)
				return isOtherSideReadyForWall(width, height, side);
			return false;
		}

		bool isOtherSideReadyForWall(int sx, int sy, int sz) {
			switch (sz){
			case (int)sides.up:
				return isReadyForAnotherWall(sx, sy + 1);
			case (int)sides.down:
				return isReadyForAnotherWall(sx, sy - 1);
			case (int)sides.left:
				return isReadyForAnotherWall(sx - 1, sy);
			case (int)sides.right:
				return isReadyForAnotherWall(sx + 1, sy);
			}
			return false;
		}

		void Complacement(int dx, int dy, int dz) {
			switch(dz){
				case (int)sides.down:
					board[dx,dy - 1, (int)sides.up] = (int)map.wall;
					return;
				case (int)sides.left:
					board[dx - 1, dy, (int)sides.right] = (int)map.wall;
					return;
				case (int)sides.up:
					board[dx, dy + 1, (int)sides.down] = (int)map.wall;
					return;
				case (int)sides.right:
					board[dx + 1, dy, (int)sides.left] = (int)map.wall;
					return;
			}
		}

		void FillWholeLabyrinth() {
			for (int wx = 0; wx < width - 1; wx++) {
				for (int wy = 0; wy < height - 1; wy++) {
					for (int wz = 0; wz < 4; wz++) {
						if (isReadyForAnotherWall(wx, wy) && board[wx,wy,wz] == (int)map.empty && isOtherSideReadyForWall(wx, wy, wz)) {
							board[wx,wy,wz] = (int)map.wall;
							Complacement(wx, wy, wz);
						}
					}
				}
			}
		}
	}
	}
