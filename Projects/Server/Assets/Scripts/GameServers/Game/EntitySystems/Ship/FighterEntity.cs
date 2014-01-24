using UnityEngine;
using System.Collections;

public class FighterEntity : ShipEntity {

	void OnTriggerEnter(Collider _Collision)
	{

		if(_Collision.collider.CompareTag("Bullet")) // did we get hit by a bullet?	
		{
			
			//if(shipTeam != _Collision.collider.GetComponent<ClientBullet>().team) // was that bulllet from another team?
			//{
				health -= _Collision.collider.GetComponent<Bullet>().damage;
				
				print ("Lost health " + health);
				
				if(health <= 0)
				{
					print ("This guy be dead chap");
					
					uLink.Network.Destroy(this.gameObject);	
				}
			//}
		
			
			Destroy(_Collision.collider.gameObject);
		}
		
	}

}
