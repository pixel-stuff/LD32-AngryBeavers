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
	public ScoreGUI scoreGUI;

	public int score;

	public GameState _state = GameState.Over;
	private AudioManager m_audioManager;
	private int chopAltern = 0;
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
			//	DoIntro ();
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

		brothersManager.brothersDiedAction += BrothersDied;

		m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	void Awake() {
		state = GameState.Intro;
		beaversManager.onBeaverKilledListener += OnBeaverKilled;

	}
	
	// Update is called once per frame
	void Update () {


		// While playing

		if (state == GameState.InGame) {

			if (Input.touchCount == 1) {    
				// touch on screen
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					DoPrepareSmash();
				}
				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					DoSmash();
				}
			}

			if (Input.touchCount == 2) {    
				StopAtNextTree();
			}

			if (Input.touchCount == 1) {   
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
				if(chopAltern++ %2 ==0){
					ChopTreeLeft();
				}else{
					ChopTreeRight();
				}
				}
			}






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
		
		// Handle Keyboard inputs
		if (state == GameState.Over) {
			if(Input.GetKeyDown (KeyCode.R)) {
				MenuRestartBtnClicked();
			}
		}

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
		if(GameObject.FindGameObjectWithTag("Player") != null)
		GameObject.FindGameObjectWithTag("Player").GetComponent<BrothersManager>().ChopLeft();
	}
	private void ChopTreeRight(){
		if(GameObject.FindGameObjectWithTag("Player") != null)
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
		//brothersManager.state = BrotherState.Dead;
		BrothersDied ();
	}

	void OnBeaverKilled(int totalBeaverKilled) {
		score++;
	}

	public void StartIngame(){
		Debug.Log ("START INGAME");
		brothersManager.gameObject.SetActive (true);
		brothersManager.gameObject.GetComponent<BrothersManager> ().stopAtNextTree = true;
		beaversManager.gameObject.SetActive (true);
		parallaxManager.gameObject.SetActive (true);
		treeMan.gameObject.SetActive (true);
		restartGameScript.gameObject.SetActive (false);
		introScript.gameObject.SetActive (false);
		state = GameState.InGame;
		m_audioManager.startAudioBackground ();
		beaversManager.Restart ();
		brothersManager.restart ();
		treeMan.restart ();
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

		score = 0;
	}

	public void BrothersDied() {
		Debug.Log ("START GAMEOVER");
		brothersManager.gameObject.SetActive (false);
		beaversManager.gameObject.SetActive (false);
		parallaxManager.gameObject.SetActive (false);
		if (treeMan.gameObject.GetComponent<treeManager> ().currentWeapon != null) {
			treeMan.gameObject.GetComponent<treeManager> ().currentWeapon.SetActive (false);
		}
		treeMan.gameObject.SetActive (false);
		restartGameScript.gameObject.SetActive (true);
		introScript.gameObject.SetActive (false);
		scoreGUI.beaverKilled = score;
		state = GameState.Over;
		m_audioManager.stopAudioBackground ();
	}
}
