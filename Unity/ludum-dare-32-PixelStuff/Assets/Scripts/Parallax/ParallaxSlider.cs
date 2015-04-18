using UnityEngine;
using System.Collections;

public class ParallaxSlider : MonoBehaviour {

	public GameObject prefabBackground;
	public float speedX = -0.1f;

	private Vector3 speed;
	private ArrayList backgroundList;
	private Vector3 size;
	// Use this for initialization
	void Start () {
		backgroundList = new ArrayList();
		speed.x = speedX;
		backgroundList.Add ((GameObject)Instantiate (prefabBackground));
		backgroundList.Add ((GameObject)Instantiate (prefabBackground));
		backgroundList.Add ((GameObject)Instantiate (prefabBackground));

		foreach (GameObject bg in backgroundList) {
			bg.transform.SetParent(gameObject.transform);
		}
		size = ((GameObject)backgroundList[0]).GetComponent<Renderer>().bounds.size;
		((GameObject)backgroundList[0]).transform.Translate(new Vector3(-size.x, 0.0f, 0.0f));
		((GameObject)backgroundList[1]).transform.Translate(new Vector3(0, 0.0f, 0.0f));
		((GameObject)backgroundList[2]).transform.Translate(new Vector3(size.x, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
		foreach(GameObject slide in backgroundList) {
			slide.transform.Translate(speed);
			if(slide.transform.position.x<=-1.5f*size.x) {
				print ("booyah "+i+" "+slide.ToString()+" "+slide.transform.position);
				slide.transform.Translate(new Vector3(2.5f*size.x, 0.0f, 0.0f));
			}
			i++;
		}
	}
}
