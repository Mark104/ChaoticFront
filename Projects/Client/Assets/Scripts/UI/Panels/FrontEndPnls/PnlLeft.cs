using UnityEngine;
using System.Collections;

public class PnlLeft : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(-850,0,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = true;
		
	}
}
