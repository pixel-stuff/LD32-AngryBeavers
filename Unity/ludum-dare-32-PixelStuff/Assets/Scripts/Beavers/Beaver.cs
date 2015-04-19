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

	[SerializeField]
	private BeaverState m_currentState = BeaverState.Spawning;

	[SerializeField]
	private int m_life = 100;

	private GameObject m_positionTreeToHang; 



	public void Initialize(){
		m_currentState= BeaverState.Running;
		m_beaverSpeedRunning = new Vector3(UnityEngine.Random.Range(this.transform.lossyScale.x/10,this.transform.lossyScale.x/4),0f,0f);
	
	}

	float m_timeBetweenDamage = 1;

	Vector3 m_beaverSpeedRunning = new Vector3 (); 
	Vector3 m_decalageHangOnTree = new Vector3(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f),0f);
	// Update is called once per frame
	void Update () {
		switch (m_currentState) {
		case BeaverState.Running:
			this.transform.localPosition -= m_beaverSpeedRunning;
			
			
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

			if(m_positionTreeToHang != null){
				Vector3 newpos = m_positionTreeToHang.transform.position + m_decalageHangOnTree;
				this.gameObject.transform.position = newpos;
			}


			break;
		case BeaverState.Smashed:
			//TO DO: Afficher Anim écrasé
			Debug.Log ("Ecrasé");
			break;
		case BeaverState.Flying:
			//TO DO: Lancer anim d'envole du beaver <3
			break;
		default:

			break;
			
		}
		if(m_life <= 0){
			Destroy(this.gameObject);
		}
	}

	void OnDestroy(){
		if (destroyListener != null) {
			destroyListener(this);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (collider.gameObject.tag == "Trunk" || collider.gameObject.tag == "SmashTree") {
			Debug.Log ("LANCER ANIMATION DE GERBE DE SANG");
			changeState(BeaverState.Smashed);
			m_life = 0;
			//TO DO: Le cadavre se déplace avec le background
			return;
		}

		if (collider.gameObject.tag == "WeaponTree") {
			if( UnityEngine.Random.Range(0.0f,1.0f) <= 0.1f){
				Debug.Log ("DESTROY BEAVER");
				Destroy(this.gameObject);
			}else{
				changeState(BeaverState.HangOnTree);
				m_positionTreeToHang = collider.gameObject;
				Vector3 m_decalageHangOnTree = new Vector3(UnityEngine.Random.Range(-m_positionTreeToHang.transform.lossyScale.x,m_positionTreeToHang.transform.lossyScale.x),UnityEngine.Random.Range(m_positionTreeToHang.transform.lossyScale.y,m_positionTreeToHang.transform.lossyScale.y),0f);
				Vector3 newpos = m_positionTreeToHang.transform.position + m_decalageHangOnTree;
				this.gameObject.transform.position = newpos;

			}

			
			return;
		}

		
	}

	public void changeState(BeaverState state){

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
