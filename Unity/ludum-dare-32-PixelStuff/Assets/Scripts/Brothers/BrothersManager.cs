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

	public void PrepareAttack() {
		if (state == BrotherState.PrepareAttack) {
			return;
		}

		state = BrotherState.PrepareAttack;
		DoPrepareAttack ();
	}

	public void Attack() {
		if (state == BrotherState.Attack) {
			return;
		}

		state = BrotherState.PrepareAttack;
		DoAttack ();
	}
	
	public void ChopWood() {
		if (state == BrotherState.ChoppingWood || state == BrotherState.GrabbingTrunk) {
			return;
		}
		
		state = BrotherState.ChoppingWood;
		DoChopWood ();
	}
	
	public void Die() {
		if (state == BrotherState.Dead) {
			return;
		}

		state = BrotherState.Dead;
		DoDeath();
	}

	private void DoIdle() {

	}
	
	private void DoPrepareAttack() {
	}
	
	private void DoAttack() {
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
	}
}
