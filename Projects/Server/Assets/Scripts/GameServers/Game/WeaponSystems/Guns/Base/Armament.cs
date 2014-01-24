using UnityEngine;
using System.Collections;

public class Armament : MonoBehaviour {
	
	ServerFireController SFC;
	
	public int id = 0;
	
	public GameObject projectile;
	
	public int projectileSpeed = 20;

	public float fireRate = 0.2f;
	
	protected byte damage;
	
	float nextFire = 0;
	
		float turretOffset = 1;
	
	public virtual bool Fire () {	
		
		if(nextFire < (float)uLink.Network.time)
		{
			nextFire = (float)uLink.Network.time + fireRate;
		
			GameObject tmpObj =	(GameObject)Instantiate(projectile,transform.position + (transform.forward * turretOffset),Quaternion.Euler(0,transform.eulerAngles.y,0));
			Bullet cb = tmpObj.GetComponent<Bullet>();
			cb.speed = projectileSpeed;
			cb.team = transform.root.GetComponent<ShipEntity>().shipTeam;
			cb.damage = damage;
		
			Physics.IgnoreCollision(transform.root.gameObject.collider,tmpObj.collider);
			
			if(audio != null)
			{
				
				audio.Play();	
			}
			
			
			return true;	
		}
		else
		{
			return false;
		}
		
		return false;
	}
	
	public virtual void ProxyFire (short _Rotation,Vector2 _Position,byte _Team) {
		
		GameObject tmpObj =	(GameObject)Instantiate(projectile,new Vector3(_Position.x,0,_Position.y),Quaternion.Euler(0,_Rotation,0));
		Bullet cb = tmpObj.GetComponent<Bullet>();
		cb.speed = projectileSpeed;
		cb.team = _Team;
		cb.damage = damage;
		
		Physics.IgnoreCollision(transform.root.gameObject.collider,tmpObj.collider);
		
		if(audio != null)
		{
			
			audio.Play();	
		}
			
	}
	
	public virtual void UpdateFacing (Vector3 _Pos) {	
		
			
	}
	
	public virtual string Status () {
			
		return "test";
		
	}

	// Use this for initialization
	public virtual void Start () {
		
		SFC = transform.root.GetComponent<ServerFireController>();
		SFC.LinkGun(this);
		
	}

}
