using UnityEngine;
using System.Collections;

public class spriteshadows : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().receiveShadows = true;
		gameObject.GetComponent<SpriteRenderer> ().castShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
