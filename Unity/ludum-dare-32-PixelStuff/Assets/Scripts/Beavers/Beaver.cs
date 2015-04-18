using UnityEngine;
using System.Collections;
using System;

public enum BeaverState{
	Spawning, 
	Running, 
	HangOnTree,
	Smashed
}


public class Beaver : MonoBehaviour {

	[SerializeField]
	private GameObject m_idleSprite;

	[SerializeField]
	private GameObject m_eatSprite;

	[SerializeField]
	private GameObject m_ejectSprite;


	public Action<Beaver> destroyListener;

	private BeaverState m_currentState = BeaverState.Spawning;



	public void Initialize(){
		m_currentState= BeaverState.Running;
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

	void OnTriggerEnter2D(Collider2D collider){
		Beaver other = collider.gameObject.GetComponent<Beaver> ();
		if (collider.gameObject.tag == "Trunk" || collider.gameObject.tag == "SmashTree") {
			Debug.Log ("LANCER ANIMATION DE GERBE DE SANG");
			return;
		}

		if (collider.gameObject.tag == "WeaponTree") {
			if( UnityEngine.Random.Range(0.0f,1.0f) <= 0.1f){
				Debug.Log ("DESTROY BEAVER");

			}else{
				Debug.Log ("BEAVER HANG ON TREE");
			}

			
			return;
		}
		Debug.Log (this.name + " feel " + other.gameObject.name + "in state " +  other.getCurrentState());

		
	}

	public BeaverState getCurrentState(){
		return m_currentState;
	}
}
