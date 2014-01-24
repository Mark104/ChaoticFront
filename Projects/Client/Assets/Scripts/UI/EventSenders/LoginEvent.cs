using UnityEngine;
using System.Collections;

public class LoginEvent : MonoBehaviour {
	
	
	AccountSession AS;
	FrontUIManager UI;
		
	public UILabel username;
	public UILabel password;
	
	void Start()
	{
		
		UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<FrontUIManager>();
		
	}

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<FrontEndGC>().LoginAttempt(username.text,password.text);
	
	}
}
