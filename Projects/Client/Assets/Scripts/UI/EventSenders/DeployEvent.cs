using UnityEngine;
using System.Collections;

public class DeployEvent : MonoBehaviour {

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().RequestDeployment();
	
	}
}
