using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
	
	Color tmpCol;

	// Use this for initialization
	void Start () {
		
		tmpCol = renderer.material.GetColor("_Color");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(tmpCol.a <= 0)
		{
			renderer.material.SetColor("_Color",tmpCol);
			this.enabled = false;
			
		}
		else
		{
			tmpCol.a -= 1 * Time.deltaTime;
			renderer.material.SetColor("_Color",tmpCol);
			
		}
	
	}
}
