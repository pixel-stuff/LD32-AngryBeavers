using UnityEngine;
using System.Collections;
using System;

public enum BeaverState{
	Spawning, 
	Running, 
	HangOnTree,
	Smashed,
	Flying
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

	private int m_life = 100;



	public void Initialize(){
		m_currentState= BeaverState.HangOnTree;
	}

	float m_timeBetweenDamage = 1;
	// Update is called once per frame
	void Update () {
		switch (m_currentState) {
		case BeaverState.Running:
			this.transform.localPosition -= new Vector3 (0.05f, 0f, 0f);
			
			
			if (this.transform.localPosition.x <= -20f) {
				Destroy (this.gameObject);
			}

			break;
		case BeaverState.HangOnTree:
			m_timeBetweenDamage += Time.deltaTime;
			if(m_timeBetweenDamage >= 1.0f){
				m_life =  m_life - 15;
				m_timeBetweenDamage = Time.deltaTime;

			}

			if(m_life <= 0){
				Destroy(this.gameObject);
			}


			break;
		case BeaverState.Smashed:

			break;
		case BeaverState.Flying:

			break;
		default:

			break;
			
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
			changeState(BeaverState.Smashed);
			return;
		}

		if (collider.gameObject.tag == "WeaponTree") {
			if( UnityEngine.Random.Range(0.0f,1.0f) <= 0.1f){
				Debug.Log ("DESTROY BEAVER");
				Destroy(this.gameObject);
			}else{
				Debug.Log ("BEAVER HANG ON TREE");
				changeState(BeaverState.HangOnTree);
			}

			
			return;
		}

		
	}

	private void changeState(BeaverState state){

		m_currentState = state;
		resetImage ();
		switch (m_currentState) {
			case BeaverState.Running:
				m_idleSprite.SetActive (true);
				break;
			case BeaverState.HangOnTree:
				m_eatSprite.SetActive (true);
				break;
			case BeaverState.Smashed:
				m_idleSprite.SetActive (true);
				break;
			case BeaverState.Flying:
				m_ejectSprite.SetActive (true);
				break;
			default:
				m_idleSprite.SetActive(true);
				break;

		}
	}

	private void resetImage(){
		m_idleSprite.SetActive (false);
		m_eatSprite.SetActive (false);
		m_ejectSprite.SetActive (false);
	}

	public BeaverState getCurrentState(){
		return m_currentState;
	}
}
