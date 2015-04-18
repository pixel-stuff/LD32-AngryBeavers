using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		Camera.main.orthographicSize = 2.3f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
