using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerFireController : uLink.MonoBehaviour {
	
	protected Dictionary<byte,Armament> armamentList = new Dictionary<byte, Armament>();
	
	public void LinkGun (Armament _InLink)
	{
		armamentList.Add((byte)_InLink.id,_InLink);
		print ("Linked");
	}
	
	[RPC]
	public void GunFired(byte _ID,short _Rotation,Vector2 _Position)
	{
		//Need to do secruity checks
		
		print ("Shooting gun " + _ID + " with " + armamentList.Count + " as the size");
		if(armamentList[_ID].Fire())
		{
			networkView.RPC("SpawnBullet",uLink.RPCMode.OthersExceptOwner,_ID,_Rotation,_Position);
			
		}
	}
	
}
