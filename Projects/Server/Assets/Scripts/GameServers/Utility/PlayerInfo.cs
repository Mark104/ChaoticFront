using UnityEngine;
using uLink;
using System.Collections;

public class PlayerInfo {
	
	public enum PlayerStates
	{
		SELECTION,
		WAITINGTOSPAWN,
		PLAYING,
		END
	}
	
	public uLink.NetworkPlayer netPlayer;
	public PlayerStates playerState;
	public byte teamID;
	
	public PlayerInfo(uLink.NetworkPlayer _NetPlayer,PlayerStates _PlayerState,byte _TeamID)
	{
		netPlayer = _NetPlayer;
		playerState = _PlayerState;
		teamID = _TeamID;
	}
}
