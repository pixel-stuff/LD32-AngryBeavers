using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

	public int beaverKilled = 0;
	public Font myFont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI(){
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.white;

		GUI.Label(new Rect(Screen.width/2-20, Screen.height/2-100, 200, 40),""+beaverKilled,myStyle);
	}
}
