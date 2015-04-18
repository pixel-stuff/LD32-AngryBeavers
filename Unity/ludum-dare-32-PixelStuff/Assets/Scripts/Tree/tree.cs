using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x - 0.1f, 0, 0);
	}
}
