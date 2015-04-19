using UnityEngine;
using System.Collections;

public enum BrotherState {
	Idle,
	MovingIntoScene,
	Running, // The somewhat Idle state
	DropThunk,
	ChoppingWood,
	GrabbingTrunk,
	PrepareAttack,
	Attack,
	Dead
}

public class Brother : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Awake () 
	{
		animator = GetComponent<Animator> ();
	}

	public void Run() {
		Debug.Log ("RUN");
		animator.SetTrigger ("noTree");
	}

	public void prepareChop() {
		Debug.Log ("Chop");
		animator.SetTrigger ("startChop");
	}

	public void Idle() {
		animator.SetBool ("Running", false);
	}

	public void Died() {
		animator.SetBool ("Running", false);
//		animator.SetBool ("PreparingAttack", false);
//		animator.SetTrigger ("Death");
	}

	public void PrepareAttack() {
//		animator.SetBool ("PreparingAttack", true);
	}
	
	public void Attack() {
//		animator.SetBool ("PreparingAttack", false);
//		animator.SetTrigger ("Attack");
	}

	public void DropThunk() {
//		animator.SetTrigger ("DropTrunk");
	}
}
