using UnityEngine;
using uLink;
using uLobby;
using System.Collections.Generic;
using System.Collections;

public class MasterLobbyController : uLink.MonoBehaviour {
	
	BattleController BC;
	
	void Awake () {
		
		BC = GetComponent<BattleController>();
		
	}

	// Use this for initialization
	void Start () {
		
		Application.runInBackground = true;
		
		Lobby.AddListener(this);
	
		uLobby.LobbyConnectionError handle = Lobby.ConnectAsServer("ec2-54-229-103-211.eu-west-1.compute.amazonaws.com",7050);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void uLobby_OnConnected()
	{
		print ("Connected to masterserver");
	
		ServerRegistry.AddServer("192.168.0.2",6050);//,playerCount,(int)currentGameState);
		//ServerRegistry.AddServer(6050,ServerName,playerCount,(int)currentGameState);//,playerCount,(int)currentGameState);
	
	}
	
	void uLink_OnPlayerConnected(uLink.NetworkPlayer player)
	{
		uLink.BitStream stream = BC.GetTileData();
		

		networkView.RPC("GetHexInfo",player,stream);
	}

}
