using UnityEngine;
using System.Collections;

public class RegisterEvent : MonoBehaviour {

	public UILabel username;
	public UILabel password;

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<FrontEndGC>().AttemptRegister(username.text,password.text);
	
	}
}
