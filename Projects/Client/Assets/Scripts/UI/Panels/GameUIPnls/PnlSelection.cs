using UnityEngine;
using System.Collections;

public class PnlSelection : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(0,-300,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = true;
		
	}
}
