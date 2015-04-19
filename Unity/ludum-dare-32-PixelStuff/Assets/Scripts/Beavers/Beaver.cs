using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	
	[SerializeField]
	private GameObject m_smashSprite;

	public Action<Beaver> destroyListener;

	[SerializeField]
	private BeaverState m_currentState = BeaverState.Spawning;

	[SerializeField]
	private int m_life = 100;


	[SerializeField]
	private Animation m_BeaversAnimations;

	[SerializeField]
	private EndFlyingAnimation m_endAnimationScript;

	private treeManager m_treeManager;

	float m_timeBetweenDamage = 1;

	Vector3 m_beaverSpeedRunning = new Vector3 (); 
	Vector3 m_decalageHangOnTree = new Vector3 (); 
	GameObject m_treeToHang;


	public void Initialize(treeManager treeManager){
		m_treeManager = treeManager;
		m_currentState= BeaverState.Running;
		m_beaverSpeedRunning = new Vector3(UnityEngine.Random.Range(this.transform.lossyScale.x/40,this.transform.lossyScale.x/20),0f,0f);
		m_decalageHangOnTree = new Vector3 (UnityEngine.Random.Range (-1.0f, 0.8f), UnityEngine.Random.Range (-0.5f, 0.5f), 0f);

		m_endAnimationScript.endFlyingAnimationAction += endFlyingAnimationListener;
	}

	void Start(){
		AudioManager audioMan = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
		// TODO : Changer l'audio à la création du castor
		//audioMan.Play("Bwaaa");
		

	}
		
	private float m_timeFlyStateBegin;
	private float m_timeSmashStateBegin;


	// Update is called once per frame
	void Update () {
		AudioManager audioMan = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

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
				m_treeManager.currentWeapon.GetComponent<weaponTree>().isNomNom();
				audioMan.Play("beaver_eat");
			}

			if(m_treeToHang != null){
				Vector3 newpos = m_treeToHang.transform.position + m_decalageHangOnTree;
				this.gameObject.transform.position = newpos;
			}

			
			if(m_life <= 0){
				Destroy(this.gameObject);
				//changeState (BeaverState.Flying);

			}

			break;
		case BeaverState.Smashed:
			//TO DO: Afficher Anim écrasé
			//Debug.Log ("Ecrasé");
			audioMan.Play("beaver_death");
			if (this.transform.position.x <= -20f) {
				Destroy (this.gameObject);
			}
			if(Time.time - m_timeSmashStateBegin >= 5){
				Destroy (this.gameObject);
			}
			break;
		case BeaverState.Flying:
			//TO DO: Lancer anim d'envole du beaver <3


			if(Time.time - m_timeFlyStateBegin >= 5){
				Destroy (this.gameObject);
			}
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

		if ((collider.gameObject.tag == "Trunk" || collider.gameObject.tag == "SmashTree") && m_currentState != BeaverState.HangOnTree) {
			changeState(BeaverState.Smashed);
			m_life = 0;
			this.gameObject.GetComponent<FollowingGroundSpeed>().enabled = true;
			return;
		}

		if (collider.gameObject.tag == "WeaponTree") {
			if( UnityEngine.Random.Range(0.0f,1.0f) <= 0.1f){
				changeState(BeaverState.Smashed);
				m_life = 0;
				this.gameObject.GetComponent<FollowingGroundSpeed>().enabled = true;
			}else{
				changeState(BeaverState.HangOnTree);
				m_treeToHang = collider.gameObject;
				Vector3 newpos = m_treeToHang.transform.position + m_decalageHangOnTree;
				this.gameObject.transform.position = newpos;

			}

			
			return;
		}

		
	}

	private void endFlyingAnimationListener(){
		Destroy (this.gameObject);
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
				m_smashSprite.SetActive (true);
			Debug.Log("ISSAMSH");
			m_smashSprite.GetComponentInChildren<ParticleSystem>().Play();
				m_timeSmashStateBegin = Time.time;
				this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				break;
			case BeaverState.Flying:
				m_ejectSprite.SetActive (true);
				m_BeaversAnimations.Play();
				m_timeFlyStateBegin = Time.time;
				this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

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
