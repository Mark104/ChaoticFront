using UnityEngine;
using System.Collections;

public class PnlTop : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(0,100,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = true;
		
	}
}
