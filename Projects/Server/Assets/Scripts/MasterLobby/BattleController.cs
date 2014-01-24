using UnityEngine;
using uLink;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleController : uLink.MonoBehaviour {
	
	byte xGridSize = 7;
	byte yGridSize = 11;
	
	byte currentIncrementedKey = 0;
	
	public struct TilePos {
		
		public byte xPos;
		public byte yPos;
		
		public TilePos (byte _XPos,byte _YPos)
		{
			xPos = _XPos;
			yPos = _YPos;
		}
		
	}
	
	struct AttackInformation
	{
		public TilePos attacker;
		public TilePos attackee;
		public byte serverKey;
		
		public AttackInformation(TilePos _Attacker,TilePos _Attackee,byte _ServerKey)
		{
			attacker = _Attacker;
			attackee = _Attackee;
			serverKey = _ServerKey;
			
		}
		
		
		
	}
	
	List<AttackInformation> battles = new List<AttackInformation>();
	
	private uLink.BitStream currentTIleData;

	enum WarStates
	{
		STARTING,
		PLAYING,
		FINISHED
	} WarStates currentWarState;
	
	
	struct Tile {
		
		public short owner;
		public byte ownerStrength; // Values below 101 and above 0 is red, values over 100 and below 201 is blue
		public bool isAttacked;
	}
	
	Faction redFaction;
	Faction blueFaction;
	
	Tile [,] hexGrid;// = new Tile[xGridSize,yGridSize];

	void Start () {
		
		hexGrid = new Tile[xGridSize,yGridSize];
		
		redFaction = new Faction(this);
		redFaction.factionID = 1;
		blueFaction = new Faction(this);
		blueFaction.factionID = 2;
		
		ChangeWarState(WarStates.STARTING); // First Lets start the game
		
		Func<int, int> func1 = x => x + 1;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Time.time > 10)
		{
			if(currentWarState == WarStates.STARTING)
			{
				ChangeWarState(WarStates.PLAYING);
			}
		}
	
	}
	
	void ChangeWarState(WarStates _NewState)
	{
		currentWarState = _NewState;
		
		switch(_NewState)
		{
		case WarStates.STARTING:
			
			//First lets set the tiles to their natural owners
			
			for(int x = 0; x < xGridSize;x++)
			{
			 	for(int y = 0; y < yGridSize;y++)
				{
					
					hexGrid[x,y] = new Tile();
					hexGrid[x,y].ownerStrength = 100;
					hexGrid[x,y].isAttacked = false;
					
					if( y < 5)
					{
						hexGrid[x,y].owner = redFaction.factionID; // Below 5 in the grid is red
						
					}
					else
					{
						hexGrid[x,y].owner = blueFaction.factionID; // Above 5 is the grid is blue

					}
				}
			}
			
			redFaction.AddFrontierHex((byte)1,(byte)5);
			redFaction.AddFrontierHex((byte)2,(byte)4);
			redFaction.AddFrontierHex((byte)4,(byte)4);
			redFaction.AddFrontierHex((byte)5,(byte)4);
			
			blueFaction.AddFrontierHex((byte)1,(byte)6);
			blueFaction.AddFrontierHex((byte)2,(byte)5);
			blueFaction.AddFrontierHex((byte)4,(byte)5);
			blueFaction.AddFrontierHex((byte)5,(byte)5);
		
			
			//manual tile alterations
			hexGrid[0,0].owner = 0;
			hexGrid[1,5].owner = 1;
			hexGrid[1,0].owner = 0;
			hexGrid[5,0].owner = 0;
			hexGrid[6,0].owner = 0;
			hexGrid[0,9].owner = 0;
			hexGrid[0,10].owner = 0;
			hexGrid[1,10].owner = 0;
			hexGrid[2,10].owner = 0;
			hexGrid[3,5].owner = 0;
			hexGrid[4,10].owner = 0;
			hexGrid[5,10].owner = 0;
			hexGrid[6,9].owner = 0;
			hexGrid[6,10].owner = 0;
			hexGrid[1,2].owner = 0;
			hexGrid[1,8].owner = 0;
			hexGrid[5,2].owner = 0;
			hexGrid[5,8].owner = 0;
			hexGrid[6,4].owner = 0;
			hexGrid[0,5].owner = 0;
			
			redFaction.CalculateNextMove();
			blueFaction.CalculateNextMove();
			
			
			
			break;
		case WarStates.PLAYING:
			
			
			break;
		case WarStates.FINISHED:
			
			break;
		}
		
		
	}
	
	public void BattleFinished(byte _XPos,byte _yPos, byte strengthChange)
	{
		int i = 0;
		
		foreach(AttackInformation info in battles)
		{
			if(info.attackee.xPos == _XPos)
				if(info.attackee.yPos == _yPos)
				{
					print ("Current Strength " + hexGrid[_XPos,_yPos].ownerStrength + " take away " + strengthChange);
					hexGrid[_XPos,_yPos].ownerStrength = strengthChange;
				print ("Current Strength " + hexGrid[_XPos,_yPos].ownerStrength + " take away " + strengthChange);
					if(hexGrid[_XPos,_yPos].ownerStrength > (byte)200)
					{
						hexGrid[_XPos,_yPos].ownerStrength = 200;
					}
					else if (hexGrid[_XPos,_yPos].ownerStrength == 0)
					{
						
						hexGrid[_XPos,_yPos].ownerStrength = 10;
						hexGrid[_XPos,_yPos].owner = hexGrid[info.attacker.xPos,info.attacker.yPos].owner;
						UpdateTileOwnership(_XPos,_yPos);
						print ("System taken, clients updated");
					}
				

				break;
					
				}
			i++;
		}
		
		print (battles.Count);
		battles.RemoveAt(i);
		print (battles.Count);
		
	}
	
	private void UpdateTileOwnership(byte _Xpos,byte _YPos)
	{
		uLink.BitStream updateData = new uLink.BitStream(true);
		
		updateData.WriteByte(_Xpos);
		updateData.WriteByte(_YPos);
	
		updateData.WriteByte((byte)hexGrid[_Xpos,_YPos].owner);
		
		networkView.RPC("UpdateTileInfo",uLink.RPCMode.Others,updateData);
	}
	
	public List<TilePos> GetAdjacentTiles(byte _XPos,byte _YPos) // Check surrounding tiles for attacks or if hostile
	{
		List<TilePos> usableTiles = new List<TilePos>();
		
		bool isOdd;
		
		print ("Tile checked is " + _XPos + "," + _YPos);
		
		if((_XPos % 2) != 0)
		{
			isOdd = true;
		}
		else
		{
			isOdd = false;
		}
			
		if(_XPos != 0)
		{
			if(isOdd)
			{
				if(_YPos != 0)
				{
					if(hexGrid[_XPos -1,_YPos -1].owner != 0)
						usableTiles.Add(new TilePos((byte)(_XPos -1),(byte)(_YPos -1)));
				}
			}
			else
			{
				if(_YPos != yGridSize -1)
				{
					if(hexGrid[_XPos -1,_YPos + 1].owner != 0)
						usableTiles.Add(new TilePos((byte)(_XPos -1),(byte)(_YPos + 1)));
				}
			}
			
			if(hexGrid[_XPos -1,_YPos].owner != 0)
				usableTiles.Add(new TilePos((byte)(_XPos -1),(byte)(_YPos)));
		}
		
		if(_XPos != xGridSize-1)
		{
			if(_YPos != 0)
			{
				if(isOdd)
				{
					if(hexGrid[_XPos +1,_YPos - 1].owner != 0)
						usableTiles.Add(new TilePos((byte)(_XPos +1),(byte)(_YPos - 1)));
				}
				else
				{
					if(_YPos != yGridSize -1)
					{
						if(hexGrid[_XPos +1,_YPos + 1].owner != 0)
							usableTiles.Add(new TilePos((byte)(_XPos +1),(byte)(_YPos + 1)));
					}
				}

				if(hexGrid[_XPos,_YPos - 1].owner != 0)
					usableTiles.Add(new TilePos((byte)(_XPos),(byte)(_YPos - 1)));

			}
						
			if(hexGrid[_XPos +1,_YPos].owner != 0)
					usableTiles.Add(new TilePos((byte)(_XPos +1),(byte)(_YPos)));
			
		}
		
		if(_YPos != yGridSize -1)
		{
			if(hexGrid[_XPos,_YPos + 1].owner != 0)
					usableTiles.Add(new TilePos((byte)(_XPos),(byte)(_YPos + 1)));
		}
		return usableTiles;
	}
	
	public bool CheckTileForAttack(byte _XPos,byte _YPos,int _FactionID) // Check surrounding tiles for attacks or if hostile
	{
		List<TilePos> fetchedTiles = GetAdjacentTiles(_XPos,_YPos);
		List<TilePos> attackableTiles = new List<TilePos>();
		
		foreach(TilePos tilePos in fetchedTiles)
		{
			if(hexGrid[tilePos.xPos,tilePos.yPos].owner != _FactionID && !hexGrid[tilePos.xPos,tilePos.yPos].isAttacked)
			{
				attackableTiles.Add(tilePos);
			}
			
		}
		
		
		if(attackableTiles.Count == 0)
		{
			return false;
		}
		else
		{
			int randomValue = UnityEngine.Random.Range(0,attackableTiles.Count);

			TilePos tileToAttack = new TilePos(attackableTiles[randomValue].xPos,attackableTiles[randomValue].yPos);
			
			battles.Add(new AttackInformation(new TilePos(_XPos,_YPos),tileToAttack,currentIncrementedKey));
			
			if(currentIncrementedKey != 254)
			{
				currentIncrementedKey++;
			}
			else
			{
				currentIncrementedKey = 0;
			}
			
			hexGrid[tileToAttack.xPos,tileToAttack.yPos].isAttacked = true;
			
			string factionName;
			if(_FactionID == 1)
			{
				factionName = "Red";
			}
			else
			{
				factionName = "Blue";	
			}
			
			print (factionName + " attacking tile" + tileToAttack.xPos + "," + tileToAttack.yPos);
			return true;
		}
		
	}
	
	public uLink.BitStream GetTileData()
	{
		currentTIleData = new uLink.BitStream(true);
		
		currentTIleData.WriteByte(xGridSize);
		currentTIleData.WriteByte(yGridSize);
		
		for(int x = 0;x < xGridSize;x++)
			for(int y = 0;y < yGridSize;y++)
			{
				currentTIleData.WriteByte((byte)hexGrid[x,y].owner);
				
			}
		
		currentTIleData.WriteByte((byte)battles.Count);
		
		foreach(AttackInformation info in battles)
		{
			currentTIleData.WriteByte(info.attacker.xPos);
			currentTIleData.WriteByte(info.attacker.yPos);
			
			currentTIleData.WriteByte(info.attackee.xPos);
			currentTIleData.WriteByte(info.attackee.yPos);
			
			currentTIleData.WriteByte(info.serverKey);
		}
		
		return currentTIleData;
		
	}
	
}
