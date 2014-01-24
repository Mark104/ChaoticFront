using UnityEngine;
using System.Collections;

public class PnlLogin : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(0,-500,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = false;
		
	}
}
