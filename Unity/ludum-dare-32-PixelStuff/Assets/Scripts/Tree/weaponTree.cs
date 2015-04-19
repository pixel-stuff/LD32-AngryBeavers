using UnityEngine;
using System.Collections;

public class weaponTree : MonoBehaviour {
	public Sprite finalSprite;
	public Sprite[] TabState;
	public int PVMAX;
	public int PVLastState;
	
	private int CurrentState;
	private int currentPV;
	private int statePV;
	private int nbPVForState;

	public float smashAngle;
	public float secondePrepareSmash;
	public float onTheGroundAngle;
	public float secondeSmash;
	public float iddleAngle;

	private float currentTimeAnim;

	public bool smashNextTime;
	public bool prepareNextTime;
	private BoxCollider2D smashBox;
	private BoxCollider2D sharpBox;

	enum Etat{
		idle,
		prepareSmash,
		waitForSmashing,
		isSmashing
	}
	private Etat currentEtat;

	// Use this for initialization
	void Start () {
		CurrentState = 0;
		currentPV = PVMAX;
		statePV = (PVMAX - PVLastState);
		nbPVForState = statePV / TabState.Length;
		this.GetComponent<SpriteRenderer>().sprite = TabState[CurrentState];

		smashBox = this.GetComponent<BoxCollider2D> ();

		sharpBox = this.GetComponentsInChildren<BoxCollider2D> ()[1];

		currentEtat = Etat.idle;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentEtat == Etat.waitForSmashing && smashNextTime) {
			Smash ();
			isNomNom ();
		}
		if (currentEtat == Etat.idle && prepareNextTime) {
			prepareSmash ();
		}

	if (currentEtat == Etat.isSmashing) {
			if (setAngleTo(onTheGroundAngle -smashAngle,secondeSmash)){
				this.transform.localEulerAngles = new Vector3(0,0,onTheGroundAngle) ;
				smashHitTheGround();

			}
	}
	if (currentEtat == Etat.prepareSmash) {
			if(setAngleTo(smashAngle,secondePrepareSmash)){
				this.transform.localEulerAngles = new Vector3(0,0,smashAngle) ;
				currentEtat = Etat.waitForSmashing;
			}
		}
	}


	private bool setAngleTo(float angle,float seconde){
		this.transform.RotateAround (this.transform.position, new Vector3 (0, 0, 1), (angle/seconde) * Time.deltaTime);
		currentTimeAnim -= Time.deltaTime;
		return currentTimeAnim < 0f ;
	}

	public void isNomNom(){
		currentPV -=30;
		setStateForPV ();
	}

	private void setStateForPV ()	{
		if (CurrentState == -1)
			return;
		if (currentPV <= PVLastState) {
			this.GetComponent<SpriteRenderer>().sprite = finalSprite;
			CurrentState =-1;
			return;
		}
		int PVStateLost = currentPV - PVLastState;
		int state = PVStateLost / nbPVForState;
		if (CurrentState != state) {
			this.GetComponent<SpriteRenderer>().sprite = TabState[state];
			CurrentState = state;
		}
	}

	public void Smash(){
		prepareNextTime = false;

		smashBox.enabled= true;
		sharpBox.enabled = false;
		currentEtat = Etat.isSmashing;
		currentTimeAnim = secondeSmash;
		prepareNextTime = false;
	}

	public void prepareSmash(){
		smashBox.enabled = false;
		currentTimeAnim = secondePrepareSmash;
		currentEtat = Etat.prepareSmash;
	}

	public void smashHitTheGround(){
		smashNextTime = false;
		sharpBox.enabled = true;
		smashBox.enabled = false;
		currentEtat = Etat.idle;
	}

	public void smashASAP(){
		smashNextTime = true;
	}
	public void prepareSmashASAP(){
		prepareNextTime = true;
	}

}
