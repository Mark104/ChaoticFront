using UnityEngine;
using uLink;
using System.Collections;

public class GameController : uLink.MonoBehaviour {
	
	public Camera GameCam;
	
	GameUIManager UI;
	SensorController SC;
	
	byte spawnPt;
	
	enum SelectedShip{
		NONE,	
		INTERCEPTOR,
		BOMBER,
		SCOUT,
		ASSAULT,
		FLAK,
		SHIELD,
		FLAGSHIP,
		DREADNAUGHT,
		CARRIER
	}SelectedShip currentSelectedShip;
	
	
	enum PlayerStates
	{
		SELECTION,
		WAITINGTOSPAWN,
		PLAYING,
		END
	}PlayerStates currentPlayerState;
	
	
	enum GameView
	{
		SENSOR,
		GAME
	}GameView currentGameView;
	
	void Start()
	{
		UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
		SC = GameObject.FindGameObjectWithTag("SensorController").GetComponent<SensorController>();
		SC.GC = this;
	}
	
	public void SetSelectedShip (byte _ShipID)
	{
		
		currentSelectedShip = (SelectedShip)(_ShipID + (byte)1);
		
		print (currentSelectedShip + " currently selected");
	}
	
	public void SetSpawnPoint(byte _SpawnPt)
	{
		print(_SpawnPt + " spawn point set");
		spawnPt = _SpawnPt;
		
	}
	
	public void RequestDeployment()
	{
		if(spawnPt != 0)
		{
			print ("Requesting deployment at " + spawnPt + " with ship " + currentSelectedShip);
			networkView.RPC("DeployRequest",uLink.RPCMode.Server,spawnPt,(byte)currentSelectedShip);
		}
	}
	
	public void ShipSpawned()
	{
		
		
		EnableGameCam();
	}
	
	public void EnableGameCam()
	{
		SC.sensorCam.gameObject.SetActive(false);;
		GameCam.gameObject.SetActive(true);
	}
	
	public void EnableSensorCam()
	{
		GameCam.enabled = false;
		SC.sensorCam.enabled = true;
	}
	
	void SetGameView(GameView _GameVIew)
	{
		if(_GameVIew == GameView.SENSOR)
		{
			currentGameView = GameView.SENSOR;
		}else if (_GameVIew == GameView.GAME)
		{
			
			
		}
		
	}
	
	[RPC]
	void SetPlayerState(byte state)
	{
		PlayerStates tmpPlyrState = (PlayerStates)state;
		currentPlayerState = tmpPlyrState;
		print ("State set to " + currentPlayerState);
		
		if(tmpPlyrState == PlayerStates.SELECTION)
		{
			currentSelectedShip = SelectedShip.NONE;
			SetGameView(GameView.SENSOR);
			UI.ShowSelection();
			SC.SetSensorMode(SensorController.SensorMode.OVERVIEW);
			
		}else if(tmpPlyrState == PlayerStates.WAITINGTOSPAWN)
		{
			
			
			
		}
		
	}
	
}
