using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeploymentPoint : MonoBehaviour {
	
	
	public struct ShipSpawnInfo 
	{
		public byte shipID;
		public Vector2 pos;
		public PlayerInfo player;
		
		public ShipSpawnInfo(byte _ShipID,Vector2 _Pos,PlayerInfo _Player)
		{
			shipID = _ShipID;
			pos = _Pos;
			player = _Player;
			
		}
	}
	
	public int pointID;
	public int owner;
	
	float lastSpawn = 0;
	
	
	public List<ShipSpawnInfo> bufferedSpawns = new List<ShipSpawnInfo>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
	
	}
	
	public void AddBufferedShip(byte _ShipID,PlayerInfo _Player)
	{
		Vector3 spawnPos = transform.position;
		Vector2 finalPos = Random.insideUnitCircle * 5;
		
		finalPos.x += spawnPos.x;
		finalPos.y += spawnPos.z;
		
		ShipSpawnInfo tmpInfo = new ShipSpawnInfo(_ShipID,finalPos,_Player);
		
		bufferedSpawns.Add(tmpInfo);

		
	}
	
	public bool SpawnShips()
	{
		if (Time.time < lastSpawn + 2)
		{
			lastSpawn = Time.time;
			
			foreach(ShipSpawnInfo _Info in bufferedSpawns)
			{
				print ("Spawning a buffered ship with ID " + _Info.shipID);
				Vector3 tmpPos = transform.position;
				GameObject tmpShip = ShipSpawner.SpawnShip(_Info.shipID,tmpPos,_Info.player);
				tmpShip.name = "" + _Info.player.netPlayer.id;
			}
			
			bufferedSpawns.Clear();
			return true;
		}
		else
		{
			return false;
		}
		
	}
	
	public bool CanPlayerSpawnHere(PlayerInfo _Info)
	{
		
		if(_Info.teamID == owner)
		{
			return true;
			
		}
		else
		{
		
			return false;
		}
	}
}
