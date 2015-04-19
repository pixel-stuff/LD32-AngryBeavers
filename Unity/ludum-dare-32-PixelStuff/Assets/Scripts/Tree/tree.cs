using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {
	enum TreeState{
		UP,
		CHOPED,
		FALLEN,
		ONFLOOR,
		PICKED
	};
	public GameObject rightChop;
	public GameObject leftChop;
	public GameObject trunk;
	public GameObject stump;
	public float SecondeAnimation;
	// Use this for initialization

	private TreeState state;
	private Transform fallenTransform;

	private float remaningSeconde;
	private float RotationZIteration;

	private bool isShake=true;

	void Start () {
		state = TreeState.UP;

	}
	
	// Update is called once per frame
	void Update () {

		//test 
		if (this.gameObject.transform.position.x < 0) {
			chopLeft();
			chopRight();
		}
		//!test

		if ( state == TreeState.UP && isChoped()) {
			state = TreeState.CHOPED;
			calculFallPosition();
			remaningSeconde = SecondeAnimation;
		}
		
		if (state == TreeState.CHOPED && remaningSeconde > 0) {
			remaningSeconde -= Time.deltaTime;
			fall ();
		}

		if (remaningSeconde < 0) {
			trunk.GetComponent<BoxCollider2D>().enabled = false;
			if(isShake){
			GameObject.FindGameObjectWithTag ("CameraManager").GetComponent<CameraManager> ().setShaking(true,true,0.2f);
				isShake = false;
				state=TreeState.ONFLOOR;
				trunk.GetComponent<SpriteRenderer>().enabled=false;
			}else{
			state = TreeState.FALLEN;
			}
		}
	}

	bool isChoped(){
		return rightChop.GetComponent<ChopOnTree> ().isCompletlyChop() && leftChop.GetComponent<ChopOnTree> ().isCompletlyChop();
		//return true;
	}

	void chopLeft(){
		leftChop.GetComponent<ChopOnTree> ().triggerChop ();
	}
	void chopRight(){
		rightChop.GetComponent<ChopOnTree> ().triggerChop ();
	}

	void calculFallPosition(){
	
		RotationZIteration = (-90f) / SecondeAnimation;
	
	}

	void fall(){
		//trunk.transform.position = new Vector3 (trunk.transform.position.x + PositionXIteration * Time.deltaTime, trunk.transform.position.y + PositionYIteration * Time.deltaTime, 0);
		trunk.transform.RotateAround (stump.transform.position, new Vector3 (0, 0, 1), RotationZIteration * Time.deltaTime);
	}

	public bool isFallen(){
		return state == TreeState.FALLEN;
	}
	public bool isOnFloor(){
		return state == TreeState.ONFLOOR;
	}
}
