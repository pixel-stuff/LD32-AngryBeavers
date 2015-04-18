using UnityEngine;
using System.Collections;
using System;

public class Beaver : MonoBehaviour {

	[SerializeField]
	private GameObject m_idleSprite;

	[SerializeField]
	private GameObject m_eatSprite;

	[SerializeField]
	private GameObject m_ejectSprite;


	public Action<Beaver> destroyListener;

	// Use this for initialization
	void Start () {
		//Destroy (this.gameObject);
	}

	public void Initialize(){

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition -= new Vector3 (0.05f, 0f, 0f);

		if (this.transform.localPosition.x <= -20f) {
			Destroy (this.gameObject);
		}
	}

	void OnDestroy(){
		if (destroyListener != null) {
			destroyListener(this);
		}
	}
}
