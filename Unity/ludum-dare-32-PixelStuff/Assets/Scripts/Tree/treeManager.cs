using UnityEngine;
using System.Collections;

public class treeManager : MonoBehaviour {

	private GameObject currentTree;

	private GameObject currentWeapon;
	private Transform spawnArea;

	public GameObject treePrefab;
	public GameObject weaponTreePrefab;


	// Use this for initialization
	void Start () {
		currentTree = null;
		spawnArea = GameObject.FindGameObjectWithTag ("SpawnArea").transform;
	}

	bool spawnTree ()
	{
		return currentTree ? false : true;
	}

	void refreshCurrent ()
	{
		if(currentTree.transform.position.x < -spawnArea.transform.position.x){
			Destroy(currentTree);
			currentTree = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTree ()) {
			currentTree = Instantiate(treePrefab);
			currentTree.transform.position = spawnArea.position;
			currentTree.transform.SetParent (this.transform);
		}
		refreshCurrent ();
	}
}
