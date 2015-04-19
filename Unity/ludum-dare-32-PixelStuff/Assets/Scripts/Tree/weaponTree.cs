using UnityEngine;
using System.Collections;

public class weaponTree : MonoBehaviour {
	public Sprite finalSprite;
	public Sprite[] TabState;
	public int PVMAX;
	public int PVLastState;
	
	public int CurrentState;
	public int currentPV;
	private int statePV;
	private int nbPVForState;

	public float smashAngle;
	public float secondePrepareSmash;
	public float onTheGroundAngle;
	public float secondeSmash;
	public float iddleAngle;

	public float throwDuration = 2.0f;

	private float currentTimeAnim;

	public bool smashNextTime;
	public bool prepareNextTime;
	public bool throwItNextTime;
	private BoxCollider2D smashBox;
	private BoxCollider2D sharpBox;
	private float throwCurrentTime = 0.0f;

	enum Etat{
		isOnTheFloor,
		idle,
		prepareSmash,
		waitForSmashing,
		isSmashing,
		isThrown
	}
	private Etat currentEtat;

	private float widthBaseSmashBoxCollider;
	private float xBaseSharpBoxCollider;

	// Use this for initialization
	void Start () {
		CurrentState = TabState.Length-1;
		currentPV = PVMAX;
		statePV = (PVMAX - PVLastState);
		nbPVForState = statePV / TabState.Length;
		this.GetComponent<SpriteRenderer>().sprite = TabState[CurrentState];


		smashBox = this.GetComponent<BoxCollider2D> ();
		widthBaseSmashBoxCollider = smashBox.size.x;

		sharpBox = this.GetComponentsInChildren<BoxCollider2D> ()[1];
		xBaseSharpBoxCollider = sharpBox.transform.position.x;

		currentEtat = Etat.isOnTheFloor;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentEtat == Etat.isOnTheFloor) {
			this.GetComponent<FollowingGroundSpeed>().enabled = true;
		}
		if (currentEtat == Etat.waitForSmashing && smashNextTime) {
			Smash ();
			isNomNom ();
		}
		if (currentEtat == Etat.idle && prepareNextTime) {
			prepareSmash ();
		}
		if (currentEtat == Etat.idle && throwItNextTime) {
			currentEtat = Etat.isThrown;
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
		if (currentEtat == Etat.isThrown) {
			// throw the tree :D hell yeah !
			if(throwCurrentTime<throwDuration) {
				/*if(throwCurrentTime<(throwDuration/2.0f)) {
					transform.Translate(0.1f, 0.1f, 0.0f);
				}else{
					transform.Translate(0.1f, -0.1f, 0.0f);
				}*/
				float y=Mathf.Sin ((throwCurrentTime/throwDuration)*Mathf.PI);
				transform.position = new Vector3(transform.position.x+0.01f, y*1.0f, 0.0f);
				print ("throw "+throwCurrentTime+" < "+throwDuration);
				throwCurrentTime += Time.deltaTime;
			}else{
				throwCurrentTime = 0.0f;
				currentEtat = Etat.idle;
				throwItNextTime = false;
			}
		}
	}


	private bool setAngleTo(float angle,float seconde){
		this.transform.RotateAround (this.transform.position, new Vector3 (0, 0, 1), (angle/seconde) * Time.deltaTime);
		currentTimeAnim -= Time.deltaTime;
		return currentTimeAnim < 0f ;
	}

	public void isNomNom(){
		currentPV--;
		setStateForPV ();
	}

	private void setStateForPV ()	{
		if (CurrentState == -1) {
			return;
		}
		if (currentPV <= 0) {
			sharpBox.enabled = false;
			smashBox.enabled = false;
			return;
		}
		if (currentPV <= PVLastState) {
			this.GetComponent<SpriteRenderer>().sprite = finalSprite;
			//CurrentState =-1;
			return;
		}
		int PVStateLost = currentPV - PVLastState;
		int state = (PVStateLost / nbPVForState);
		if (CurrentState != state) {
			this.GetComponent<SpriteRenderer>().sprite = TabState[state];

			float pWidth = TabState[state].rect.width/TabState[TabState.Length-1].rect.width;
			print (pWidth*smashBox.size.x);
			smashBox.size = new Vector2(pWidth*widthBaseSmashBoxCollider, smashBox.size.y);
			sharpBox.transform.position = new Vector3(
				sharpBox.transform.position.x-sharpBox.size.x,
				sharpBox.transform.position.y,
				sharpBox.transform.position.z);

			//smashBox.transform.postion.x-=smashBox.
			CurrentState = state;
		}
	}

	public void Smash(){
		prepareNextTime = false;
		if (currentPV > 0) {
			smashBox.enabled = true;
			sharpBox.enabled = false;
		}
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
		if (currentPV > 0) {
			sharpBox.enabled = true;
			smashBox.enabled = false;
		}
		currentEtat = Etat.idle;
		GameObject.FindGameObjectWithTag ("CameraManager").GetComponent<CameraManager> ().setShaking(true,true,0.2f);
		GameObject.FindGameObjectWithTag ("BeaversManager").GetComponent<BeaversManager> ().SmashBeaversHangOnTree ();
	}

	public void smashASAP(){
		smashNextTime = true;
	}
	public void prepareSmashASAP(){
		prepareNextTime = true;
	}

	public void pick(){
		this.GetComponent<FollowingGroundSpeed>().enabled = false;
		currentEtat = Etat.idle;
	}
}
