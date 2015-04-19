using UnityEngine;
using System.Collections;

public class treeManager : MonoBehaviour {

	public GameObject currentTree;

	public GameObject currentWeapon;
	private Transform spawnArea;

	public GameObject treePrefab;
	public GameObject weaponTreePrefab;

	private bool spawnOneTree;

	public int spawnBeaverMin;
	public int spawnBeaverMax;
	public int nbBeaverUntilSpawnTree;
	public int lastDeadBeaverNumber;

	// Use this for initialization
	void Start () {
		currentTree = null;
		currentWeapon = null;
		spawnArea = GameObject.FindGameObjectWithTag ("SpawnArea").transform;
		spawnOneTree = true; // TODO false
		nbBeaverUntilSpawnTree = getRandNbBeaver();
		lastDeadBeaverNumber = 0;
	}

	bool canSpawnTree ()
	{
		return currentTree ? false : true;
	}

	void refreshCurrent ()
	{
		if (currentTree) {
			if (currentTree.transform.position.x < -spawnArea.transform.position.x) {
				Destroy (currentTree);
				currentTree = null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawnTree () && spawnOneTree) {
			currentTree = Instantiate(treePrefab);
			currentTree.transform.position = spawnArea.position;
			currentTree.transform.SetParent (this.transform);
			spawnOneTree = false;

			nbBeaverUntilSpawnTree = getRandNbBeaver();

			lastDeadBeaverNumber = GameObject.FindGameObjectWithTag("BeaversManager").GetComponent<BeaversManager>().getBeaverKilledTotal();
		}

		if (nbBeaverUntilSpawnTree < GameObject.FindGameObjectWithTag("BeaversManager").GetComponent<BeaversManager>().getBeaverKilledTotal()-lastDeadBeaverNumber)
			spawnOneTree = true;

		refreshCurrent ();
		playerPickUpWeapon ();
	}

	public void trowWeaponOnGround(){
		if (currentWeapon != null) {
			currentWeapon.GetComponent<weaponTree>().throwItNextTime = true;
		}
	}

	public GameObject playerPickUpWeapon(){
		if (currentTree !=null && currentTree.GetComponent<tree> ().isOnFloor()) {
			if (currentWeapon){
				currentWeapon.GetComponent<weaponTree>().removeHangBeaver();
				Destroy(currentWeapon);
			}
			currentWeapon = Instantiate(weaponTreePrefab);
			currentWeapon.transform.position = new Vector3(currentTree.GetComponent<tree>().trunk.transform.position.x,
			                                               currentTree.GetComponent<tree>().trunk.transform.position.y,
			                                               currentTree.GetComponent<tree>().trunk.transform.position.z-0.1f);
			currentWeapon.transform.eulerAngles = new Vector3(0,0,0);
		}
		return currentWeapon;

	}


	public void ChopLeft(){
		Debug.Log ("LEFT");
		if (currentTree != null) {
			Debug.Log ("doleft");
			currentTree.GetComponent<tree> ().chopLeft ();
		}
	}

	public void ChopRight(){
		if(currentTree != null)
		currentTree.GetComponent<tree> ().chopRight ();
	}

	public void pickWeapon(){
		if(currentWeapon != null)
		currentWeapon.GetComponent<weaponTree> ().pick ();
	}

	public void PrepareSmash(){
		if (currentWeapon != null) {
			if( !currentWeapon.GetComponent<weaponTree>().prepareNextTime){
				currentWeapon.GetComponent<weaponTree>().prepareSmashASAP();
			}
		}
	}

	public void Smash(){
		if (currentWeapon != null) {
			if (!currentWeapon.GetComponent<weaponTree> ().smashNextTime) {
				currentWeapon.GetComponent<weaponTree> ().smashASAP ();
			}
		}
	}

	private int getRandNbBeaver() {
		return (int) Mathf.Floor(Random.Range (spawnBeaverMin, spawnBeaverMax+1));
	}
}
