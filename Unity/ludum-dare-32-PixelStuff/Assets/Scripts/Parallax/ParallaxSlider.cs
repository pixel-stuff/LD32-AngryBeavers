using UnityEngine;
using System.Collections;

public class ParallaxSlider : MonoBehaviour {

	public GameObject prefabBackground;

	private ArrayList backgroundList;
	// Use this for initialization
	void Start () {
		backgroundList = new ArrayList();
		GameObject instance = null;
		instance = (GameObject)Instantiate (prefabBackground);
		backgroundList.Add (instance);
		instance = (GameObject)Instantiate (prefabBackground);
		backgroundList.Add (instance);
		instance = (GameObject)Instantiate (prefabBackground);
		backgroundList.Add (instance);

		foreach (GameObject bg in backgroundList) {
			bg.transform.SetParent(gameObject.transform);
		}
		Vector3 size = ((GameObject)backgroundList[0]).GetComponent<Renderer>().bounds.size;
		Debug.Log (size);
		((GameObject)backgroundList[0]).transform.Translate(new Vector3(-size.x, 0.0f, 0.0f));
		((GameObject)backgroundList[1]).transform.Translate(new Vector3(0, 0.0f, 0.0f));
		((GameObject)backgroundList[2]).transform.Translate(new Vector3(size.x, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
