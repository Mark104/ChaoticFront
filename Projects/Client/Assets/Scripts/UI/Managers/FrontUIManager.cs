using UnityEngine;
using System.Collections;

public class FrontUIManager : MonoBehaviour {
	
	public PnlTop _TopPnl;
	public PnlBot _BotPnl;
	public PnlLogin _PnlLogin;
	public PnlLeft _PnlLeft;
	public PnlRight _PnlRight;
	
	
	
	
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
			
	}
	
	// - Event recievers
	
	public void ShowOptions () {
		
		
		
	}
	
	public void LogedIn() {
		
		_TopPnl.ChangeHideState();
		_BotPnl.ChangeHideState();
		_PnlLogin.ChangeHideState();
		_PnlLeft.ChangeHideState();
		_PnlRight.ChangeHideState();
	}
	
	//--------------------
}
