using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI(){
		
		GUI.Label(new Rect(Screen.width/2, Screen.height/2, 100, 20), "Hello World!");
	}
}
