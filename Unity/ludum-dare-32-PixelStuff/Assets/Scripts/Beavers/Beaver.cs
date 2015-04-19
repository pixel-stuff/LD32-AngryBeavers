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

	private GameObject m_positionTreeToHang; 

	[SerializeField]
	private Animation m_BeaversAnimations;

	[SerializeField]
	private EndFlyingAnimation m_endAnimationScript;

	private treeManager m_treeManager;

	float m_timeBetweenDamage = 1;

	Vector3 m_beaverSpeedRunning = new Vector3 (); 
	Vector3 m_decalageHangOnTree = new Vector3(UnityEngine.Random.Range(-1f,1f),UnityEngine.Random.Range(-1f,1f),0f);



	public void Initialize(treeManager treeManager){
		m_treeManager = treeManager;
		m_currentState= BeaverState.Running;
		m_beaverSpeedRunning = new Vector3(UnityEngine.Random.Range(this.transform.lossyScale.x/20,this.transform.lossyScale.x/10),0f,0f);
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
			}

			if(m_positionTreeToHang != null){
				Vector3 newpos = m_positionTreeToHang.transform.position + m_decalageHangOnTree;
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
				m_positionTreeToHang = collider.gameObject;
				Vector3 m_decalageHangOnTree = new Vector3(UnityEngine.Random.Range(-m_positionTreeToHang.transform.lossyScale.x/4,m_positionTreeToHang.transform.lossyScale.x/4),UnityEngine.Random.Range(m_positionTreeToHang.transform.lossyScale.y/4,m_positionTreeToHang.transform.lossyScale.y/4),0f);
				Vector3 newpos = m_positionTreeToHang.transform.position + m_decalageHangOnTree;
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
				m_timeSmashStateBegin = Time.time;
				break;
			case BeaverState.Flying:
				m_ejectSprite.SetActive (true);
				m_BeaversAnimations.Play();
				m_timeFlyStateBegin = Time.time;

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
