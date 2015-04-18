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
				Intro ();
				break;
			case GameState.Start:
				Start ();
				break;
			case GameState.Over:
				GameOver ();
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
			Restart();
		}

		if (state == GameState.Start) {
			// While playing
			if (Input.GetKey (KeyCode.S)) {
				Attack();
			}
			
			if (Input.GetKey (KeyCode.Space)) {
				ChopWood();
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

	private void Intro() {

	}

	private void Restart() {
		brothersManager.state = BrothersState.MovingIntoScene;
	}

	private void Attack() {
		if (brothersManager.state == BrothersState.PrepareAttack) {
			brothersManager.state = BrothersState.Attack;
		} else {
			brothersManager.state = BrothersState.PrepareAttack;
		}
	}

	private void ChopWood() {
		brothersManager.ChopWood ();
	}

	private void GameOver() {
		brothersManager.Die ();
	}

}
