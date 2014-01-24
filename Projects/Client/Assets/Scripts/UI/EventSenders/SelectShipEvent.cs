using UnityEngine;
using System.Collections;

public class SelectShipEvent : MonoBehaviour {
	
	public byte shipID;

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().SetSelectedShip(shipID);
	
	}
	
}
