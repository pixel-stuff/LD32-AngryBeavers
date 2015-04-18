using UnityEngine;
using System.Collections;

public enum BrotherState {
	Idle,
	MovingIntoScene,
	Running, // The somewhat Idle state
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
		Debug.Log ("[Brother] Run()");
		animator.SetBool ("Running", true);
	}
}
