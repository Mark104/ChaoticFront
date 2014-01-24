using UnityEngine;
using System.Collections;

public class GameUIManager : MonoBehaviour {
	
	public PnlSelection pnlSelection;
	
	
	public void ShowSelection()
	{
		print ("Yo");
		pnlSelection.ChangeHideState();
		
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
