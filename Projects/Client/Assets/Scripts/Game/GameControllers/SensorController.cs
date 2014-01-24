using UnityEngine;
using System;
using System.Collections;

public class SensorController : MonoBehaviour {
	
	public Camera sensorCam;
	
	public GameController GC;
	
	public enum SensorMode
	{
		OVERVIEW,
		FOLLOW
	}SensorMode currentSensorMode;
		
		
	public void SetSensorMode(SensorMode _SensorMode)
	{
		currentSensorMode = _SensorMode;
		
		if(_SensorMode == SensorMode.OVERVIEW)
		{
			
			
		}else if (_SensorMode == SensorMode.FOLLOW)
		{
			
			
		}
		
	}
	
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentSensorMode == SensorMode.OVERVIEW)
		{
			Ray ray = sensorCam.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
			
			RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit))
			{
				if(hit.collider.CompareTag("SpawnPoint"))
				{
					if(Input.GetMouseButtonDown(0))
					{
						GC.SetSpawnPoint(Convert.ToByte(hit.collider.name));
					}
				}
			}
			
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		}
	}
}
