using UnityEngine;
using System.Collections;

public class FrontEndGC : MonoBehaviour {
	
	AccountSession AS;
	FrontUIManager UI;
	MainGridController MGC;
	
	public GameObject BackQuad;
	

	// Use this for initialization
	void Start () {
		
		UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<FrontUIManager>();
		MGC = GameObject.FindGameObjectWithTag("GridController").GetComponent<MainGridController>();
		
		
		GameObject ASObj = GameObject.FindGameObjectWithTag("AccountSession");
		
		if(ASObj == null)
		{
			GameObject tmpObj = new GameObject();
			tmpObj.name = "AccountSessionObj";
			
			AS = tmpObj.AddComponent<AccountSession>();
		}
		else
		{
			AS = ASObj.GetComponent<AccountSession>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Events
	
	public void LogedIn()
	{
		UI.LogedIn();
		BackQuad.AddComponent<Fader>();
		
	}
	
	
	
	
	//UI Event Recievers
	
	public void LoginAttempt(string username,string password)
	{
		print("AttemptingLogin with " + username + " and " + password);
		AS.Login(username,password);
	}
	
	public void AttemptRegister(string username,string password)
	{
		print("AttemptingReg with " + username + " and " + password);
		AS.Register(username,password);
	}
	
	[RPC]
	public void GetHexInfo(uLink.BitStream inputStream)
	{
		
		MGC.SpawnGrid(inputStream);
		
		
		
	}
	
	[RPC]
	public void UpdateTileInfo(uLink.BitStream inputStream)
	{
		MGC.UpdateTile(inputStream);
		
	}

	
}
