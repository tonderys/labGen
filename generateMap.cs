using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using labyrinthGenerator;

public class Wall
{
	public GameObject wall;
	public int x,y;
	public Wall(GameObject wall, int x, int y)
	{
		this.wall = wall;
		this.x = x;
		this.y = y;
	}
}

public class GenerateMap : MonoBehaviour {

	public List<Wall> walls = new List<Wall>();

	// Use this for initialization
	void Start () {
		LabyrinthMaze lab = new LabyrinthMaze(30,30);

		for (int x = 0; x < 90; x++){
			for (int y = 0; y < 90; y++){
				if (lab[x,y] == 1) 
					walls.Add(new Wall(GameObject.CreatePrimitive(PrimitiveType.Cube), x, y));
//				if (lab[x,y] < 0)
//					walls.Add(new Wall(GameObject.CreatePrimitive(PrimitiveType.Sphere), x, y));
			}
		}
		foreach (Wall w in walls){
			w.wall.transform.position = new Vector3(w.x, w.y, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
