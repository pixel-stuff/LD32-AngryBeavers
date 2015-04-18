using UnityEngine;
using System.Collections;

public enum GameState {
	Intro, // Game launched
	Start,
	Over
}

public class GameManager : MonoBehaviour {

	public BrothersManager brothersManager;

	private GameState _state;
	private GameState state {
		get {
			return _state;
		}
		set {
			if (value == _state) 
				return;

			_state = value;

			switch (_state) {
			case GameState.Intro:
				DoIntro ();
				break;
			case GameState.Start:
				DoRestart ();
				break;
			case GameState.Over:
				DoGameOver ();
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
		state = GameState.Intro;
	}
	
	// Update is called once per frame
	void Update () {

		// Handle Keyboard inputs

		if (Input.GetKey (KeyCode.R)) {
			DoRestart();
		}

		if (state == GameState.Start) {
			// While playing
			if (Input.GetKey (KeyCode.S)) {
				DoAttack();
			}
			
			if (Input.GetKey (KeyCode.Space)) {
				DoChopWood();
			}
		}

		if (Input.GetKey (KeyCode.Return)) {
			Submit();
		}
	}
	
	private void Submit() {
		if (state == GameState.Intro) {
			state = GameState.Start;
		}

	}

	private void DoIntro() {

	}

	private void DoRestart() {
		brothersManager.state = BrothersState.MovingIntoScene;
	}

	private void DoAttack() {
		if (brothersManager.state == BrothersState.PrepareAttack) {
			brothersManager.state = BrothersState.Attack;
		} else {
			brothersManager.state = BrothersState.PrepareAttack;
		}
	}

	private void DoChopWood() {
		brothersManager.ChopWood ();
	}

	private void DoGameOver() {
		brothersManager.Die ();
	}

}
