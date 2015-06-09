using UnityEngine;
using System.Collections;

public class GroundManager : MonoBehaviour {

	Color defColor = Color.white;
	Color activeColor = Color.gray;

	// Reset will reset the entire playground
	void Reset() {
		foreach(Component go in gameObject.GetComponentsInChildren<MeshRenderer>()) {
			((MeshRenderer)go).material.color = defColor;
		}
	}

	// Use this for initialization
	void Start () {
		activeColor.a = 0.5f;
		for (float i=0.0f; i<10.0f; i+=2.0f) {
			for(float j=0.0f; j<10.0f; j+=2.0f) {
				GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
//				plane.GetComponent<MeshRenderer>().material.;
				plane.transform.localScale = new Vector3(0.2f, 1.0f, 0.2f);
				plane.transform.localPosition = new Vector3(
					i,
					0,
					j);
				plane.name = "c"+i+""+j;
				plane.transform.parent = transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mScreenPos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(mScreenPos);
		RaycastHit hit;
		Reset ();
		if (Physics.Raycast (ray, out hit, 100)) {
			MeshRenderer mr = (MeshRenderer)hit.transform.gameObject.GetComponent ("MeshRenderer");
			if (mr.material.color.Equals (defColor))
				mr.material.color = activeColor;
		} else {
			foreach(Component go in gameObject.GetComponentsInChildren<MeshRenderer>()) {
				((MeshRenderer)go).material.color = defColor;
			}
		}
	}
}
