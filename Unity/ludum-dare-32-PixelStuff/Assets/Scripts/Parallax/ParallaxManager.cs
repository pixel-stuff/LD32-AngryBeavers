using UnityEngine;
using System.Collections;

public class ParallaxManager : MonoBehaviour {

	public GameObject refGroundMaster;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		print (getGroundSpeedf ());
	}

	// getGroundSpeed
	public float getGroundSpeedf() {
		return refGroundMaster.GetComponent<ParallaxSlider>().getSpeed().x;
	}
}
