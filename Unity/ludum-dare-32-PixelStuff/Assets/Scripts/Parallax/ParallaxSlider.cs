using UnityEngine;
using System.Collections;

public class ParallaxSlider : MonoBehaviour {

	public GameObject prefabBackground;
	public float speedX = -0.1f;
	public float posZ = 0.0f;

	private Vector3 speed;
	private ArrayList backgroundList;
	private Vector3 size;
	// Use this for initialization
	void Start () {
		backgroundList = new ArrayList();
		speed.x = speedX;
		backgroundList.Add (((GameObject)Instantiate (prefabBackground)));
		backgroundList.Add (((GameObject)Instantiate (prefabBackground)));
		backgroundList.Add (((GameObject)Instantiate (prefabBackground)));
		size = ((GameObject)backgroundList[0]).GetComponent<Renderer>().bounds.size;
		print (size);
		foreach (GameObject bg in backgroundList) {
			bg.transform.SetParent(gameObject.transform);
		}
		((GameObject)backgroundList[0]).transform.position = new Vector3(-size.x, 0.0f, posZ);
		((GameObject)backgroundList[1]).transform.position = new Vector3(0.0f, 0.0f, posZ);
		((GameObject)backgroundList[2]).transform.position = new Vector3(size.x, 0.0f, posZ);
		Camera.main.transform.position = new Vector3 (0.0f, 0.0f, -1.0f);
	}

	// Update is called once per frame
	void Update () {
		foreach(GameObject slide in backgroundList) {
			slide.transform.Translate(speed);
			if(slide.transform.position.x<=-1.5f*size.x) {
				slide.transform.Translate(new Vector3(3.0f*size.x, 0.0f, 0.0f));
			}
		}
	}
}
