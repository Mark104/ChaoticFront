using UnityEngine;
using UnityEditor;
using System.Collections;

public class HexSpawner : EditorWindow  {

	int xGridSize = 7;
	int yGridSize = 11;
	

	[MenuItem ("Window/HexSpawner")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		HexSpawner window = (HexSpawner)EditorWindow.GetWindow (typeof (HexSpawner));
	}
	
	void OnGUI () {
		
		
		GUILayout.Label ("HexSpawner", EditorStyles.boldLabel);
		
		if(GUILayout.Button("SpawnHexGrid"))
		{
			GameObject parentObject = new GameObject();
			parentObject.AddComponent<MainGridController>();
			parentObject.name = "GridParent";
			
			int offset = 1;
			
			for(int x = 0; x < xGridSize; x++)
			{
				
				for(int y = 0;y < yGridSize; y++)
				{
					GameObject tmpObj = Instantiate(Resources.Load("hex")) as GameObject;
					tmpObj.transform.position = new Vector3(2*(x+0)/1.1547007242866666f,0,(y *2 + offset) + 1);
					tmpObj.name = "" + x + "" + y;
					
					tmpObj.transform.parent = parentObject.transform;
				}
				
				if(offset == 1)
				{
					offset = 0;
				}
				else
				{
					offset = 1;
				}
			}
		}
	}
}
