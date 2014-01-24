using UnityEngine;
using uLink;
using System.Collections;
using System.Collections.Generic;

public class MainGridController : uLink.MonoBehaviour {
	
	byte xGridSize;
	byte yGridSize;
	
	Tile[,] mainTileGrid;
	Vector3 centrePos;
	
	public struct TilePos {
		
		public byte xPos;
		public byte yPos;
		
		public TilePos (byte _XPos,byte _YPos)
		{
			xPos = _XPos;
			yPos = _YPos;
		}
		
	}
	
	
	
	
	public void UpdateTile(uLink.BitStream inputStream)
	{
		uLink.BitStream readerStream = inputStream.ReadBitStream();
		
		mainTileGrid[readerStream.ReadByte(),readerStream.ReadByte()].SetInfo(readerStream.ReadByte());
		
	}
	
	public void SpawnGrid(uLink.BitStream inputStream)
	{
		uLink.BitStream readerStream = inputStream.ReadBitStream();
		
		xGridSize = readerStream.ReadByte();
		yGridSize = readerStream.ReadByte();
		
		print ("Spawning grid of size " + xGridSize + "," + yGridSize);
		
		mainTileGrid = new Tile[xGridSize,yGridSize];
		
		centrePos = new Vector3(xGridSize -2,0,yGridSize);
		
		Camera.main.transform.root.position = centrePos;
		Camera.main.transform.root.GetComponent<GridCamController>().SetGridCamActive();
		transform.position = centrePos;
		
		int offset = 1;
		
		for(int x = 0; x < xGridSize; x++)
		{
			
			for(int y = 0;y < yGridSize; y++)
			{
				GameObject tmpObj = Instantiate(Resources.Load("hex")) as GameObject;
				tmpObj.transform.position = new Vector3(2*(x+0)/1.1547007242866666f,0,(y *2 + offset) + 1);
				tmpObj.name = "" + x + "" + y;
				
				tmpObj.transform.parent = this.transform;
				
				mainTileGrid[x,y] = tmpObj.GetComponent<Tile>();
				
				mainTileGrid[x,y].SetInfo(readerStream.ReadByte());
				
			}
			
			if(offset == 1)
			{
				offset = 0;
			}
			else
			{
				offset = 1;
			}
		}
		
		int numberofBattles = readerStream.ReadByte();
		
		for(int i = 0; i < numberofBattles; i++)
		{
			byte attackerXPos = readerStream.ReadByte();
			byte attackerYPos = readerStream.ReadByte();
			
			byte attackeeXPos = readerStream.ReadByte();
			byte attackeeYPos = readerStream.ReadByte();
			
			byte serverKey = readerStream.ReadByte();
			mainTileGrid[attackeeXPos,attackeeYPos].SetAsBattle(serverKey);
		}
		
	}

	// Use this for initialization
	void Start () {
		

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
