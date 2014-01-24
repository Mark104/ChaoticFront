using UnityEngine;
using uLink;
using System.Collections;

public static class ShipSpawner {

	public static GameObject SpawnShip(byte _ShipID,Vector3 _Pos,PlayerInfo _Info)
	{
		GameObject tmpReturnObj = null;
		
		switch(_ShipID)
		{
		case 0:
			
			tmpReturnObj = uLink.Network.Instantiate(_Info.netPlayer,"Fighters/Bomber",_Pos,Quaternion.identity,0,_Info.teamID,"" + _Info.netPlayer.id);				
			
			
			break;
		case 1:
			
			tmpReturnObj = uLink.Network.Instantiate(_Info.netPlayer,"Fighters/Bomber",_Pos,Quaternion.identity,0,_Info.teamID,"" + _Info.netPlayer.id);
			
			break;
		case 2:
			
			break;
		}
		
		tmpReturnObj.GetComponent<ShipEntity>().shipTeam = _Info.teamID;
		//tmpReturnObj.name = "" + _Info.netPlayer.id;
		
		return new GameObject();
	}
}
