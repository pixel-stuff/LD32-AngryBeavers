using UnityEngine;
using System.Collections;

public enum BrothersState {
	Idle,
	MovingIntoScene,
	CuttingLumber,
	GrabbingTrunk,
	PrepareAttack,
	Attack,
	Dying,
	Died
}

public class BrothersManager : MonoBehaviour {
	
	public BrothersState state = BrothersState.Idle;

	private Brother brother1;
	private Brother brother2;

	// Use this for initialization
	void Start () {
		state = BrothersState.MovingIntoScene;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
