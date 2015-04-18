using UnityEngine;
using System.Collections;

public enum BrothersState {
	MovingIntoScene,
	Running, // The somewhat Idle state
	ChoppingWood,
	GrabbingTrunk,
	PrepareAttack,
	Attack,
	Dead
}

public class BrothersManager : MonoBehaviour {

	private BrothersState _state;
	public BrothersState state {
		get {
			return _state;
		}
		set {
			if (_state == value)
				return;

			_state = value;

			switch (_state) {
			case BrothersState.MovingIntoScene:
				DoMovingIntoScene();
				break;
			case BrothersState.PrepareAttack:
				DoPrepareAttack();
				break;
			case BrothersState.Attack:
				DoAttack();
				break;
			case BrothersState.ChoppingWood:
				DoChopWood();
				break;
			case BrothersState.GrabbingTrunk:
				DoGrabbingTrunk();
				break;
			case BrothersState.Running:
				DoRunning();
				break;
			}
		}
	}
	
	public Brother brother1;
	public Brother brother2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PrepareAttack() {
		if (state == BrothersState.PrepareAttack) {
			return;
		}

		state = BrothersState.PrepareAttack;
		DoPrepareAttack ();
	}

	public void Attack() {
		if (state == BrothersState.Attack) {
			return;
		}

		state = BrothersState.PrepareAttack;
		DoAttack ();
	}
	
	public void ChopWood() {
		if (state == BrothersState.ChoppingWood || state == BrothersState.GrabbingTrunk) {
			return;
		}
		
		state = BrothersState.ChoppingWood;
		DoChopWood ();
	}
	
	public void Die() {
		if (state == BrothersState.Dead) {
			return;
		}

		state = BrothersState.Dead;
		DoDeath();
	}
	
	private void DoMovingIntoScene() {

		// They start from the left
		brother1.transform.position = new Vector3 (-200, 100);
		brother2.transform.position = new Vector3 (-10, 5);

		state = BrothersState.Running;
	}
	
	private void DoPrepareAttack() {
	}
	
	private void DoAttack() {
		state = BrothersState.Running;
	}

	private void DoChopWood() {
		state = BrothersState.GrabbingTrunk;
	}

	private void DoGrabbingTrunk() {
		state = BrothersState.Running;
	}

	private void DoRunning() {
	}

	private void DoDeath() {
	}
}
