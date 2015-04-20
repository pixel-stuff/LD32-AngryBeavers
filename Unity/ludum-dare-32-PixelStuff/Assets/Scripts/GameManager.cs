using UnityEngine;
using System.Collections;

public enum GameState {
	Intro, // Game launched
	InGame,
	Over
}

public class GameManager : MonoBehaviour {

	public BrothersManager brothersManager;
	public BeaversManager beaversManager;
	public ParallaxManager parallaxManager;
	public treeManager treeMan;


	public RestartGameScript restartGameScript;
	public IntroScript introScript;

	public int score;

	public GameState _state = GameState.Over;
	private GameState state {
		get {
			return _state;
		}
		set {
			
			//Debug.Log("[GameManager] Changing from state: " + _state.ToString() + ", to: " + value.ToString());

			if (value == _state) 
				return;

			_state = value;

			switch (_state) {
			case GameState.Intro:
				DoIntro ();
				break;
			case GameState.InGame:
				DoRestart ();
				break;
			case GameState.Over:
				DoGameOver ();
				break;
			}
		}
	}

	void Start(){
		//parallax, beavermanager, brotherManager, TreeManager
		brothersManager.gameObject.SetActive (false);
		beaversManager.gameObject.SetActive (false);
		parallaxManager.gameObject.SetActive (false);
		treeMan.gameObject.SetActive (false);

		restartGameScript.onKeyDown += MenuRestartBtnClicked;
		introScript.onKeyDown += StartIngame;

		restartGameScript.gameObject.SetActive (false);
		introScript.gameObject.SetActive (true);
	}

	void Awake() {
		state = GameState.Intro;
		beaversManager.onBeaverKilledListener += OnBeaverKilled;

	}
	
	// Update is called once per frame
	void Update () {

		// Handle Keyboard inputs



		// While playing

		if (state == GameState.InGame) {


			if (Input.GetKeyDown (KeyCode.S)) {
				DoAttack();
			}
			
			if (Input.GetKeyDown (KeyCode.Space)) {
				DoChopWood();
			}

			if(Input.GetKeyDown (KeyCode.UpArrow)){
				DoPrepareSmash();
			}

			if(Input.GetKeyDown (KeyCode.LeftArrow)){
				ChopTreeLeft();
			}
			if(Input.GetKeyDown (KeyCode.RightArrow)){
				ChopTreeRight();
			}

			if(Input.GetKeyDown (KeyCode.UpArrow)){
				DoPrepareSmash();
			}

			if (Input.GetKeyDown (KeyCode.D)) {

				state = GameState.Over;
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				StopAtNextTree();

			}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				DoSmash();
			}
		}

		/*if (Input.GetKey (KeyCode.Return)) {
			if (state == GameState.Intro) {
				state = GameState.InGame;
			}
		}*/

	}

	private void StopAtNextTree(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<BrothersManager> ().StopAtNextTree();
	}

	private void DoPrepareSmash(){
		GameObject.FindGameObjectWithTag("TreeManager").GetComponent<treeManager>().PrepareSmash();
	}

	private void DoSmash(){
		GameObject.FindGameObjectWithTag("TreeManager").GetComponent<treeManager>().Smash();
	}

	private void ChopTreeLeft(){
		Debug.Log ("LEFT");
		GameObject.FindGameObjectWithTag("Player").GetComponent<BrothersManager>().ChopLeft();
	}
	private void ChopTreeRight(){
		GameObject.FindGameObjectWithTag("Player").GetComponent<BrothersManager>().ChopRight();
	}

	private void DoIntro() {
		state = GameState.InGame;
	}

	private void DoRestart() {
		brothersManager.state = BrotherState.MovingIntoScene;
	}

	private void DoAttack() {
		if (brothersManager.state == BrotherState.PrepareAttack) {
			brothersManager.state = BrotherState.Attack;
		} else {
			brothersManager.state = BrotherState.PrepareAttack;
		}
	}

	private void DoChopWood() {
		brothersManager.state = BrotherState.DropThunk;
	}

	private void DoGameOver() {
		/*brothersManager.state = BrotherState.Dead;
		menuManager.Reset ();*/
	}

	void OnBeaverKilled(int totalBeaverKilled) {
		score++;
	}

	public void StartIngame(){
		Debug.Log ("START INGAME");
		brothersManager.gameObject.SetActive (true);
		beaversManager.gameObject.SetActive (true);
		parallaxManager.gameObject.SetActive (true);
		treeMan.gameObject.SetActive (true);
		restartGameScript.gameObject.SetActive (false);
		introScript.gameObject.SetActive (false);
		state = GameState.InGame;
	}

	public void MenuRestartBtnClicked() {
		Debug.Log ("START INTRO");
		state = GameState.Intro;
			brothersManager.gameObject.SetActive (false);
		beaversManager.gameObject.SetActive (false);
		parallaxManager.gameObject.SetActive (false);
		treeMan.gameObject.SetActive (false);
		restartGameScript.gameObject.SetActive (false);
		introScript.gameObject.SetActive (true);
	}

	public void BrothersDied() {
		Debug.Log ("START GAMEOVER");
		brothersManager.gameObject.SetActive (false);
		beaversManager.gameObject.SetActive (false);
		parallaxManager.gameObject.SetActive (false);
		treeMan.gameObject.SetActive (false);
		restartGameScript.gameObject.SetActive (true);
		introScript.gameObject.SetActive (false);
		state = GameState.Over;

	}
}
