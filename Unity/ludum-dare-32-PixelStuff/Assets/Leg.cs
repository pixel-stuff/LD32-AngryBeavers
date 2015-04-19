using UnityEngine;
using System.Collections;

public class Leg : MonoBehaviour {
	
	private Animator animator;
	
	// Use this for initialization
	void Awake () 
	{
		animator = GetComponent<Animator> ();
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

}
