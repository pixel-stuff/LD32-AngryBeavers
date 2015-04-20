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

	private Vector3 normalPosition;

	public GameObject TreeRepere;

	// Use this for initialization
	void Awake () 
	{
		animator = GetComponent<Animator> ();
		normalPosition = this.transform.position;
	}


	public void changePositionForTree(){

		if (TreeRepere != null) {
			this.transform.position = TreeRepere.transform.position;
			this.transform.Rotate( new Vector3(0,180,0));
		}

	}

	public void backToNormal(){
		this.transform.position = normalPosition;
		this.transform.Rotate( new Vector3(0,180,0));
	}
	public void reset(){
		//animator.SetBool ("NoTree",false);
	}
	public void Run() {
		Debug.Log ("RUN");
		reset ();
		animator.SetBool ("NoTree",true);
		animator.SetBool ("HaveTree",false);
		animator.SetBool ("StartChop",false);
	}

	public void prepareChop() {
		Debug.Log ("Chop");
		reset ();
		animator.SetBool ("NoTree",false);
		animator.SetBool ("StartChop",true);
		animator.SetBool ("HaveTree",false);

	}

	public void HaveTree(){
		animator.SetBool ("NoTree",false);
		animator.SetBool ("StartChop",false);
		animator.SetBool ("HaveTree",true);
	}

	public void chop(){
		animator.SetTrigger("Chop");

	}
	public void Idle() {
		//animator.SetBool ("Running", false);
	}

	public void Died() {
		//animator.SetBool ("Running", false);
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

	public void restart() {

	}
}
