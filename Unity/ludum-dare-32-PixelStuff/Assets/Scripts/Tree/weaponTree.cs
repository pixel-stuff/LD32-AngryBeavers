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

	public float currentTimeAnim;

	public bool smashNextTime;
	public bool prepareNextTime;
	public bool throwItNextTime;
	private BoxCollider2D smashBox;
	private BoxCollider2D sharpBox;
	private float throwCurrentTime = 0.0f;


	public float xPickUp;
	public float yPickUp;
	public enum Etat{
		isOnTheFloor,
		idle,
		prepareSmash,
		waitForSmashing,
		isSmashing,
		isThrown
	}
	public Etat currentEtat;

	private float[] m_widthSmashBoxCollider;
	private float[] m_xOffSmashBoxCollider;
	private float[] m_xSharpBoxCollider;

	public float widthBaseSmashBoxCollider;
	private float previousXSharpBoxCollider;
	public float xBaseSharpBoxCollider;
	private float m_lastHalfWidth;

	// Use this for initialization
	void Start () {
		CurrentState = TabState.Length-1;
		currentPV = PVMAX;
		statePV = (PVMAX - PVLastState);
		nbPVForState = statePV / TabState.Length;
		this.GetComponent<SpriteRenderer>().sprite = TabState[CurrentState];

		// Ground truth values
		m_widthSmashBoxCollider = new float[]{1.6f, 2.45f, 3.14f, 3.82f, 4.4f};
		m_xOffSmashBoxCollider = new float[]{-0.2f, -0.25f, -0.25f, -0.25f, -0.25f};
		m_xSharpBoxCollider = new float[]{0.8f, 1.25f, 1.55f, 1.88f, 2.2f};

		smashBox = this.GetComponent<BoxCollider2D> ();
		sharpBox = this.GetComponentsInChildren<BoxCollider2D> ()[1];

		smashBox.size = new Vector2(m_widthSmashBoxCollider[CurrentState+1], smashBox.size.y);
		smashBox.offset = new Vector2(m_xOffSmashBoxCollider[CurrentState+1], smashBox.offset.y);
		sharpBox.transform.localPosition = new Vector3(
			m_xSharpBoxCollider[CurrentState+1],
			sharpBox.transform.localPosition.y,
			sharpBox.transform.localPosition.z);

		widthBaseSmashBoxCollider = smashBox.size.x;
		xBaseSharpBoxCollider = sharpBox.transform.localPosition.x;
		previousXSharpBoxCollider = xBaseSharpBoxCollider;

		currentEtat = Etat.isOnTheFloor;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentEtat == Etat.idle && smashBox != null && sharpBox !=null ) {
			smashBox.size = new Vector2(m_widthSmashBoxCollider[CurrentState+1], smashBox.size.y);
			smashBox.offset = new Vector2(m_xOffSmashBoxCollider[CurrentState+1], smashBox.offset.y);
			sharpBox.transform.localPosition = new Vector3(
				m_xSharpBoxCollider[CurrentState+1],
				sharpBox.transform.localPosition.y,
				sharpBox.transform.localPosition.z);

			if (this.GetComponent<SpriteRenderer>().sprite != finalSprite) {
				xPickUp = this.transform.position.x - (m_lastHalfWidth-TabState[CurrentState].rect.width/2)/100;
				this.transform.position = new Vector3(xPickUp,
			    	                                  this.transform.position.y,				                                 
			        	                              this.transform.position.z);
				m_lastHalfWidth = TabState[CurrentState].rect.width/2;
			} else {
				xPickUp = this.transform.position.x - (m_lastHalfWidth-finalSprite.rect.width/2)/100;
				this.transform.position = new Vector3(xPickUp,
				                                      this.transform.position.y,				                                 
				                                      this.transform.position.z);
				m_lastHalfWidth = finalSprite.rect.width/2;
			}

		}
		if (currentEtat == Etat.isOnTheFloor) {
			this.GetComponent<FollowingGroundSpeed>().enabled = true;
		}
		if (currentEtat == Etat.waitForSmashing && smashNextTime) {
			Smash ();
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
				this.transform.position = new Vector3(xPickUp,yPickUp,this.transform.position.z);
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

				float y=Mathf.Sin ((throwCurrentTime/throwDuration)*(Mathf.PI+0.2f)) + yPickUp ;
				transform.position = new Vector3(transform.position.x+0.01f, y, 0.0f);
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
		this.transform.RotateAround (new Vector3(this.transform.position.x - TabState[CurrentState].rect.width/200 ,this.transform.position.y,this.transform.position.z), new Vector3 (0, 0, 1), (angle/seconde) * Time.deltaTime);
		currentTimeAnim -= Time.deltaTime;
		return currentTimeAnim < 0.0f ;
	}

	public void isNomNom(){
		currentPV--;
		setStateForPV ();
	}

	private void setStateForPV ()	{

		if (currentPV <= 0) {
			GameObject.FindGameObjectWithTag("BeaversManager").GetComponent<BeaversManager>().destroyBeaversOnTree();
			Destroy(this.gameObject);
			Destroy(this);
		}
		if (CurrentState == -1) {
			return;
		}
		/*if (currentPV <= 0) {
			sharpBox.enabled = false;
			smashBox.enabled = false;
			return;
		}*/
		if (currentPV <= PVLastState) {
			this.GetComponent<SpriteRenderer>().sprite = finalSprite;
			//CurrentState =-1;
			return;
		}
		int PVStateLost = currentPV - PVLastState;
		int state = PVStateLost / nbPVForState;
		if (CurrentState != state) {
			this.GetComponent<SpriteRenderer>().sprite = TabState[state];
			/*
			float pWidth = TabState[state].rect.width/TabState[TabState.Length-1].rect.width;
			smashBox.size = new Vector2(pWidth*widthBaseSmashBoxCollider, smashBox.size.y);

			print ("SB.x = "+smashBox.size.x);
			sharpBox.transform.Translate( new Vector3(
				//sharpBox.transform.position.x+sharpBox.size.x,
				2*xBaseSharpBoxCollider*pWidth-xBaseSharpBoxCollider,
				//-Mathf.Abs(widthBaseSmashBoxCollider-smashBox.size.x+2*smashBox.offset.x),
				0,
				0));
			previousXSharpBoxCollider = smashBox.size.x;
			//smashBox.transform.postion.x-=smashBox.*/
			CurrentState = state;
		}
	}

	public void Smash(){
		smashNextTime = false;
		prepareNextTime = false;
		smashBox.enabled = true;
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
		GameObject.FindGameObjectWithTag ("CameraManager").GetComponent<CameraManager> ().setShaking(true,true,0.2f);
		GameObject.FindGameObjectWithTag ("BeaversManager").GetComponent<BeaversManager> ().SmashBeaversHangOnTree ();
	}

	public void smashASAP(){
		if (currentEtat != Etat.isThrown) {
			smashNextTime = true;
		}
	}
	public void prepareSmashASAP(){
		if (currentEtat != Etat.isThrown) {
			prepareNextTime = true;
		}
	}

	public void pick(){
		Debug.Log ("PICCCCKKKKKK");
		this.GetComponent<FollowingGroundSpeed>().enabled = false;
		GameObject.FindGameObjectWithTag ("GripTree").GetComponent<BoxCollider2D> ().enabled = false;
		xPickUp = this.transform.position.x;
		this.transform.position = new Vector3(this.transform.position.x,yPickUp,this.transform.position.z);
		currentEtat = Etat.idle;
		m_lastHalfWidth = TabState[CurrentState].rect.width/2;
	}
}
