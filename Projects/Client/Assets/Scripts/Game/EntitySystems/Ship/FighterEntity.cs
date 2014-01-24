using UnityEngine;
using System.Collections;

public class FighterEntity : ShipEntity {

	void OnTriggerEnter(Collider _Collision)
	{

		if(_Collision.collider.CompareTag("Bullet")) // did we get hit by a bullet?	
		{
			
			Destroy(_Collision.collider.gameObject);
		}
		
	}

}
