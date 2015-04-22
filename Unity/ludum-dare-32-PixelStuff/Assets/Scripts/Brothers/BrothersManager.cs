using UnityEngine;
using System.Collections;
using System;

public class BrothersManager : MonoBehaviour {

	private BrotherState _state = BrotherState.Running;


	private bool lok;
	public bool stopAtNextTree;
	public bool AuthoriseStop;
	public Action brothersDiedAction;
	public bool Chop;

	private AudioManager m_audioManager;

	public BrotherState state {
		get {
			return _state;
		}
		set {

			Debug.Log("[BrotherManager] Changing from state: " + _state.ToString() + ", to: " + value.ToString());

			if (_state == value)
				return;

			_state = value;

			/*switch (_state) {
			case BrotherState.MovingIntoScene:
				DoMovingIntoScene();
				break;
			case BrotherState.PrepareAttack:
				DoPrepareAttack();
				break;
			case BrotherState.Attack:
				DoAttack();
				break;
			case BrotherState.ChoppingWood:
				DoChopWood();
				break;
			case BrotherState.GrabbingTrunk:
				DoGrabbingTrunk();
				break;
			case BrotherState.Running:
				DoRunning();
				break;
			case BrotherState.Dead:
				DoDeath();
				break;
			case BrotherState.DropThunk:
				DoDropTunk();
				break;
			}*/
		}
	}
	
	public Brother brother1;
	public Brother brother2;

	public Leg leg1;
	public Leg leg2;

	public GameObject armBrother1;
	
	private void DoMovingIntoScene() {
		state = BrotherState.Running;
	}

	private void DoIdle() {
		brother1.Idle ();
		brother2.Idle ();
	}
	
	private void DoPrepareAttack() {
		brother1.PrepareAttack ();
		brother2.PrepareAttack ();
	}
	
	private void DoAttack() {
		brother1.Attack ();
		brother2.Attack ();

		state = BrotherState.Running;
	}

	private void DoChopWood() {
		state = BrotherState.GrabbingTrunk;
	}

	private void DoGrabbingTrunk() {
		state = BrotherState.Running;
	}

	private void DoRunning() {
		m_audioManager.startAudioBrothersRunning ();
		brother1.Run ();
		leg1.Run ();
		brother2.Run ();
		leg2.Run ();
	}

	private void DoHaveTree() {
		m_audioManager.startAudioBrothersRunning ();
		brother1.HaveTree ();
		leg1.Run ();
		brother2.HaveTree ();
		leg2.Run ();
	}

	private void DoDropTunk() {
		m_audioManager.stopAudioBrothersRunning ();
		brother1.DropThunk ();
		brother2.DropThunk ();

		state = BrotherState.ChoppingWood;
	}

	private void DoDeath() {
		m_audioManager.stopAudioBrothersRunning ();
		brother1.Died ();
		brother2.Died ();
		if (brothersDiedAction != null) {
			brothersDiedAction ();
		}
	}

	void DoStopParallax(){
		Debug.Log("Pause enable");
		GameObject.FindGameObjectWithTag ("ParallaxManager").GetComponent<ParallaxManager> ().isPaused = true;
	}

	void DoRestartParallax(){
		Debug.Log("Pause disable");
		GameObject.FindGameObjectWithTag ("ParallaxManager").GetComponent<ParallaxManager> ().isPaused = false;
	}

	public void ChopLeft(){
		if (Chop) {
			Debug.Log ("LEFT");
			GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().ChopLeft ();
			brother2.chop();
			//leg2.chop();
		}
	}
	public void ChopRight(){
		if (Chop) {
			GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().ChopRight ();
			brother1.chop();
		}
	}


	public void prepareChop(){
		brother1.prepareChop ();
		brother1.changePositionForTree ();
		brother2.prepareChop ();
		leg1.prepareChop ();
		leg1.changePositionForTree ();
		leg2.prepareChop ();
	}
	public void StopAtNextTree(){
		if (AuthoriseStop) {
			stopAtNextTree = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Beaver" && col.gameObject.GetComponent<Beaver>().getCurrentState() == BeaverState.Running) {
			DoDeath();
		}
		if (col.gameObject.tag == "CanChopTree") {
			Debug.Log("COLLISION ZONE ESPACE");
			AuthoriseStop = true;
		}

		if (col.gameObject.tag == "Tree") {
			if(stopAtNextTree){
			Debug.Log("COLLISION AUTHORISE TREE");
			
			DoStopParallax();
			GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().trowWeaponOnGround ();
			DoDropTunk();
			Chop=true;
			stopAtNextTree = false;
				prepareChop();
			}
			AuthoriseStop = false;


		}

		if (col.gameObject.tag == "GripTree") {
			Debug.Log("COLLISION new arme");
			GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().pickWeapon();
			DoHaveTree();
		}
	}

	public void restart() {
		brother1.restart ();
		leg1.restart ();
		brother2.restart ();
		leg2.restart ();
		GameObject.FindGameObjectWithTag ("ParallaxManager").GetComponent<ParallaxManager> ().isPaused = false;
		state = BrotherState.Running;
	}
	
	public void treeIsChop(){
		Chop = false;
		brother1.backToNormal ();
		leg1.backToNormal ();
		DoRunning();
	}
	void Start() {
		m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	void Update(){
		if(!lok){
			DoRunning();
			lok = true;
		}

		/*if (Chop) {
			ChopLeft ();
			ChopRight ();
		}*/
	}

}
