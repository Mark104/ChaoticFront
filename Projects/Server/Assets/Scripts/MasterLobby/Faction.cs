using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Faction {
	
	public short factionID;
	
	public struct TilePos {
		
		public byte xPos;
		public byte yPos;
		
		public TilePos (byte _XPos,byte _YPos)
		{
			xPos = _XPos;
			yPos = _YPos;
		}
		
	}
	
	List<TilePos> frontierHexs = new List<TilePos>();
	
	BattleController BC;
	
	short currentAttacks = 0;
	
	public Faction(BattleController _BC)
	{
		BC = _BC;
	
		//BC.CheckTileForAttack(1);
	}
	
	public void AddFrontierHex(byte _XPos,byte _YPos)
	{
		TilePos tmpPos = new TilePos();
		tmpPos.xPos = _XPos;
		tmpPos.yPos = _YPos;
		
		frontierHexs.Add(tmpPos);
	}
	
	public void CalculateNextMove()
	{
		List<int> randomValuesUsed = new List<int>();
		do
		{
			int rndVal = Random.Range(0,frontierHexs.Count);
			
			if(randomValuesUsed.Count != 0)
			{
				if(!randomValuesUsed.Contains(rndVal))
				{
					randomValuesUsed.Add(rndVal);
					BC.CheckTileForAttack(frontierHexs[rndVal].xPos,frontierHexs[rndVal].yPos,factionID);
				
					currentAttacks++;
				}
			}
			else
			{
				randomValuesUsed.Add(rndVal);
				BC.CheckTileForAttack(frontierHexs[rndVal].xPos,frontierHexs[rndVal].yPos,factionID);
				
				currentAttacks++;
				
			}
			
		}while(currentAttacks < 3);
	}
	
}
