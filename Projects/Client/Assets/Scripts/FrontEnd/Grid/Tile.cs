using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	byte owner;
	short serverKey = -1;
	
	public void SetInfo(byte _Owner)
	{
		if(_Owner == 0)
		{
			Destroy(this.gameObject);
			return;
		}else if (_Owner == 1)
		{
			transform.Find("Outline").renderer.material = Resources.Load("Materials/RedOutline") as Material;
		}else if (_Owner == 2)
		{
			transform.Find("Outline").renderer.material = Resources.Load("Materials/BlueOutline") as Material;
		}
		
	}
	
	public void SetAsBattle(byte _ServerKey)
	{
		print ("SetAsBattle");
		serverKey = _ServerKey;
		
	}
	

}
