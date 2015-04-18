using UnityEngine;
using System.Collections;

public class BrothersManager : MonoBehaviour {

	private BrotherState _state;
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

	private void DoDeath() {
		brother1.Died ();
		brother2.Died ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Beaver") {
			DoDeath();
		}
	}

}
