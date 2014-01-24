using UnityEngine;
using uLink;
using System.Collections;
using System.Collections.Generic;

public class GameController : uLink.MonoBehaviour {
	
	// Map to hold all the players, key is network id
	Dictionary<int,PlayerInfo> currentPlayers = new Dictionary<int, PlayerInfo>(); 

	
	public DeploymentPoint[] deployPoints;

	
	
	
	// Use this for initialization
	void Start () {
		
		Application.targetFrameRate = 60;

		uLink.Network.InitializeServer(64, 7100);
	}
	
	void Update () {
		
		foreach(DeploymentPoint _DeployPt in deployPoints)
		{
			_DeployPt.SpawnShips();
		}
	}
	
	public void Initalize() // Lets start this server! Called typically, by the accountsession
	{
		
		
	}
	
	public void SpawnShip(byte _ShipID,PlayerInfo _Info,Vector2 _Pos)
	{
		
		
		
	}
	// CAL,LLBACKS
	
	void uLink_OnServerInitialized()
	{
		Debug.Log("Server successfully started on port " + uLink.Network.listenPort);
	}
	
	// Update is called once per frame
	void uLink_OnPlayerDisconnected(uLink.NetworkPlayer player)
	{
		
		uLink.Network.DestroyPlayerObjects(player);
		uLink.Network.RemoveRPCs(player);
		
		// this is not really necessery unless you are removing NetworkViews without calling uLink.Network.Destroy
		uLink.Network.RemoveInstantiates(player);
		
		currentPlayers.Remove(player.id); // Remove from listing
	}

	void uLink_OnPlayerConnected(uLink.NetworkPlayer player)
	{
		currentPlayers.Add(player.id,new PlayerInfo(player,PlayerInfo.PlayerStates.SELECTION,0)); // Add to listing
		networkView.RPC("SetPlayerState",player,(byte)PlayerInfo.PlayerStates.SELECTION);
	}
	
	[RPC]
	void DeployRequest(byte _SpawnPt,byte _SelectedShip,uLink.NetworkMessageInfo _Info)
	{
		print ("Got deploy request with spawnpoint" +_SpawnPt);
		if(_SpawnPt - 1 < deployPoints.Length)
		{
			print ("Got deploy request");
			if (deployPoints[_SpawnPt - 1].CanPlayerSpawnHere(currentPlayers[_Info.sender.id]))
			{
				print ("He can spawn here, aading " + _SelectedShip + " to buffer");
				deployPoints[_SpawnPt - 1].AddBufferedShip(_SelectedShip,currentPlayers[_Info.sender.id]);
				
			}
		}
	}
	
}
