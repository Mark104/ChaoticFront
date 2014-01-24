using UnityEngine;
using System.Collections;

public class GridCamController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		transform.Translate((Input.GetAxis("Horizontal")* 4) * Time.deltaTime,0,(Input.GetAxis("Vertical")* 4) * Time.deltaTime);
		
		if(Input.GetMouseButton(1))
		{
			transform.Rotate(0,(Input.GetAxis("Mouse X") * 80) * Time.deltaTime,0);	
			Camera.main.transform.Translate(0,0,(Input.GetAxis("Mouse Y") * 10) * Time.deltaTime,Space.Self);
		}
		
	
	
	}
	
	public void SetGridCamActive()
	{
		this.enabled = true;
		
	}
}
