using UnityEngine;
using System.Collections;
using uLink;

public class JoinServer : uLink.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		
		uLink.Network.Connect("192.168.0.2",7100);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	private void uLink_OnConnectedToServer()
	{
		print ("Connected");	
		
	}
	
	
}
