using System;
using NUnit.Framework;
using labyrinthGenerator;

public class LabyrinthTest
{
	[Test]
	public void extremelySmallLabyrinths ()
	{
		LabyrinthMaze lab;
		int[,] test;

		lab= new LabyrinthMaze(0,0);
		test = new int[0,0];
		Assert.That(lab.isEqualTo(test,0,0));

		lab = new LabyrinthMaze(1,1);
		test = new int[3,3]{
			{1	,1	,1},
			{1	,-1	,1},
			{1	,1	,1}};
		Assert.That(lab.isEqualTo(test,3,3));

		lab = new LabyrinthMaze(2,2);
		test = new int[6,6]{
			{1	,1	,1	,1	,1	,1},
			{1	,-1	,-1	,-1	,-1	,1},
			{1	,-1	,-1	,-1	,-1	,1},
			{1	,-1	,-1	,-1	,-1	,1},
			{1	,-1	,-1	,-1	,-1	,1},
			{1	,1	,1	,1	,1	,1},};
		
		Assert.That(lab.isEqualTo(test,6,6));
	}
	
	[Test]
	public void growingHundredLabyrinths ()
	{
		LabyrinthMaze lab;
		for (int i = 0; i < 100; i++){
			lab = new LabyrinthMaze(i,i);
			Assert.AreEqual(lab.mazeHeight, i*3);
			Assert.AreEqual(lab.mazeWidth, i*3);
		}
	}

	[Test]
	public void bigLabyrinth ()
	{
		LabyrinthMaze lab = new LabyrinthMaze(500,500);
		Assert.AreEqual(lab.mazeHeight, 1500);
		Assert.AreEqual(lab.mazeWidth, 1500);
	}
}
