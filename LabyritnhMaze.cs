using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace labyrinthGenerator
{
	public class LabyrinthMaze
	{
		int[,] maze;
		LabyrinthBoard labyrinthBoard;
		public int mazeWidth, mazeHeight,boardWidth, boardHeight;
		const int wall = (int)map.wall, empty = (int)map.empty;

		public LabyrinthMaze(int width, int height)
		{
			boardWidth = width;
			boardHeight = height;
			mazeWidth = width * 3;
			mazeHeight = height * 3;
			maze = new int[mazeWidth,mazeHeight];

			labyrinthBoard = new LabyrinthBoard(width, height);

			FillWithWalls();
			UpdateMaze();
			floodLabyrinth(new Point(1,1));
		}

		public int this[int x, int y]
		{
			get
			{
				return this.maze[x,y];
			}
		}

		public bool isEqualTo(int[,] givenMaze, int len1, int len2)
		{
			if (this.mazeWidth != len1 || this.mazeHeight != len2)
				return false;
			for (int x = 0; x < len1; x++){
				for (int y = 0; y < len2; y++){
					if (this[x,y] != givenMaze[x,y]) return false;
				}
			}
			return true;
		}

		void FillWithWalls() 
		{
			for (int i = 0; i < mazeWidth; i++)
				for (int j = 0; j < mazeHeight; j++)
					maze[i,j] = wall;
		}
		
		void UpdateMaze() 
		{
			bool left = false, right = false, up = false, down = false;
			
			for (int x = 0; x < boardWidth; x++) {
				for (int y = 0; y < boardHeight; y++) {
					for (int w = 0; w < 4; w++) {
						
						maze[(3 * x) + 1,(3 * y) + 1] = empty;
						
						if (labyrinthBoard[x,y,w] == empty && w == 0) {
							maze[(3 * x) + 1,(3 * y) + 0] = empty;
							down = true;
						}
						if (labyrinthBoard[x,y,w] == empty && w == 1) {
							maze[(3 * x) + 0,(3 * y) + 1] = empty;
							left = true;
						}
						if (labyrinthBoard[x,y,w] == empty && w == 2) {
							maze[(3 * x) + 1,(3 * y) + 2] = empty;
							up = true;
						}
						if (labyrinthBoard[x,y,w] == empty && w == 3) {
							maze[(3 * x) + 2,(3 * y) + 1] = empty;
							right = true;
						}
					}
					if (left && up)
						maze[(3 * x) + 0,(3 * y) + 2] = empty;
					if (right && up)
						maze[(3 * x) + 2,(3 * y) + 2] = empty;
					if (right && down)
						maze[(3 * x) + 2,(3 * y) + 0] = empty;
					if (left && down)
						maze[(3 * x) + 0,(3 * y) + 0] = empty;
					left = right = up = down = false;
				}
			}
		}

		public void floodLabyrinth(Point playerPosition)
		{
			if (mazeWidth < 3 || mazeHeight < 3) return;
			floodSegment(playerPosition, -1);
		}

		void floodSegment(Point start, int waterMark)
		{
			Queue<Point> actual = new Queue<Point>();
			bool isLabyrinthFlooded = false;
			Point p;

			actual.Enqueue(new Point(start.width, start.height));

			while(!isLabyrinthFlooded){
				isLabyrinthFlooded = true;
				while(actual.Count > 0){
					p = actual.Dequeue();
					if(maze[p.width, p.height] == 0){
						maze[p.width, p.height] = waterMark;
						if (maze[p.width - 1, p.height] == 0){ 
							actual.Enqueue(new Point(p.width - 1, p.height));
							isLabyrinthFlooded = false;
						}
						if (maze[p.width + 1, p.height] == 0){
							actual.Enqueue(new Point(p.width + 1, p.height));
							isLabyrinthFlooded = false;
						}
						if (maze[p.width, p.height - 1] == 0){
							actual.Enqueue(new Point(p.width, p.height - 1));
							isLabyrinthFlooded = false;
						}
						if (maze[p.width, p.height + 1] == 0){ 
							actual.Enqueue(new Point(p.width, p.height + 1));
							isLabyrinthFlooded = false;
						}
					}
				}
			}
		}

		Point getPointWithValueFromLabyrinth(int searchedValue)
		{
			for (int x = 0; x < mazeWidth; x++){
				for (int y = 0; y < mazeHeight; y++){
					if(maze[x,y] == searchedValue) return new Point(x,y);
				}
			}
			return new Point(-1,-1);
		}

		Point VoidSearch(double xmin, double xmax, double ymin, double ymax) 
		{	
			if (xmin < 0 || xmax < 0 || ymin < 0 || ymax < 0 || xmin > 1
			    || xmax > 1 || ymin > 1 || ymax > 1) {
				xmin = 0.0;
				xmax = 1.0;
				ymin = 0.0;
				ymax = 1.0;
			}
			
			int tmpX, tmpY;
			int Xmin = (int) (mazeWidth * xmin), Xmax = (int) (mazeWidth * xmax), Ymin = (int) (mazeHeight * ymin), Ymax = (int) (mazeHeight * ymax);
			while (true) {
				tmpX = Xmin + Random.Range(0,Xmax-Xmin);
				tmpY = Ymin + Random.Range(0,Ymax-Ymin);
				if (maze[tmpX,tmpY] == 0) {
					Point tmp = new Point(tmpX, tmpY);
					return tmp;
				}
			}
		}

		public string printLab()
		{
			string buffer = "";
			for (int x = 0; x < mazeWidth; x++){
				for (int y = 0; y < mazeHeight; y++){
					buffer += maze[x,y] + "\t";
				}
				buffer += "\n";
			}
			return buffer;
		}
	}
}
