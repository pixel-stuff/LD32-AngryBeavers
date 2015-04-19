using UnityEngine;
using System.Collections;

public class BrothersManager : MonoBehaviour {

	private BrotherState _state;

	public bool stopAtNextTree;
	public bool Chop;
	public BrotherState state {
		get {
			return _state;
		}
		set {

			Debug.Log("[BrotherManager] Changing from state: " + _state.ToString() + ", to: " + value.ToString());

			if (_state == value)
				return;

			_state = value;

			switch (_state) {
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
			}
		}
	}

	public Brother brother1;
	public Brother brother2;
	
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
		brother1.Run ();
		brother2.Run ();
	}

	private void DoDropTunk() {
		brother1.DropThunk ();
		brother2.DropThunk ();

		state = BrotherState.ChoppingWood;


	}

	private void DoDeath() {
		brother1.Died ();
		brother2.Died ();
	}

	void DoStopParallax(){
		Debug.Log("Pause enable");
		GameObject.FindGameObjectWithTag ("ParallaxManager").GetComponent<ParallaxManager> ().isPaused = true;
	}

	void DoRestartParallax(){
		Debug.Log("Pause disable");
		GameObject.FindGameObjectWithTag ("ParallaxManager").GetComponent<ParallaxManager> ().isPaused = false;
	}

	void ChopLeft(){
		GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().ChopLeft ();
	}
	void ChopRight(){
		GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().ChopRight ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Beaver") {
			DoDeath();
		}
		if (col.gameObject.tag == "Tree" && stopAtNextTree) {
			Debug.Log("COLLISION AUTHORISE TREE");
			DoStopParallax();
			GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager> ().trowWeaponOnGround ();
			DoDropTunk();
			Chop=true;
			stopAtNextTree = false;
		}

		if (col.gameObject.tag == "GripTree") {
			Debug.Log("COLLISION new arme");
			//todo desativer le script de mouvement
		}
	}

	void Update(){
		if (Chop) {
			Debug.Log ("Chop");
			ChopLeft ();
			ChopRight ();
		}
	}

}
