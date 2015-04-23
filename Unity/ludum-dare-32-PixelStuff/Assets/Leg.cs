using UnityEngine;
using System.Collections;

public class Leg : MonoBehaviour {
	
	private Animator animator;
	private Vector3 normalPosition;
	
	public GameObject TreeRepere;

	// Use this for initialization
	void Awake () 
	{
		animator = GetComponent<Animator> ();
		normalPosition = this.transform.position;
	}

	public void changePositionForTree(bool mirror, float offsetX){
		
		if (TreeRepere != null) {
			this.transform.position = TreeRepere.transform.position+(new Vector3(offsetX,0.0f,0.0f));
			if(mirror) {
				this.transform.Rotate( new Vector3(0,180,0));
			}
		}
		
	}
	
	public void backToNormal(){
		this.transform.position = normalPosition;
		if (this.transform.localRotation.y != 0) {
			this.transform.Rotate( new Vector3(0,180,0));
		}
	}


	public void reset(){
		//animator.SetBool ("NoTree",false);
	}
	public void Run() {
		Debug.Log ("RUN");
	
		animator.SetBool ("StartChop",false);
		animator.SetBool ("Run",true);
	}
	
	public void prepareChop() {
		Debug.Log ("Chop");
		animator.SetBool ("Run",false);
		animator.SetBool ("StartChop",true);
		
	}

	public void chop(bool isLeft){
		animator.SetTrigger("Chop");
		if (isLeft) {
			transform.localPosition += new Vector3(-0.1f, 0.0f, 0.0f);
		} else {
			transform.localPosition += new Vector3(0.1f, 0.0f, 0.0f);
		}
	}

	public void restart() {
		if (transform.localRotation.y == 1.0f) {
			backToNormal ();
		}
		Run ();
	}

}
