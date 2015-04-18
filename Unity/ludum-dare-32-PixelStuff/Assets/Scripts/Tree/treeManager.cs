using UnityEngine;
using System.Collections;

public class treeManager : MonoBehaviour {

	private GameObject currentTree;

	private GameObject currentWeapon;
	private Transform spawnArea;

	public GameObject treePrefab;
	public GameObject weaponTreePrefab;

	private bool spawnOneTree;
	// Use this for initialization
	void Start () {
		currentTree = null;
		spawnArea = GameObject.FindGameObjectWithTag ("SpawnArea").transform;
		spawnOneTree = true; // TODO false
	}

	void spawnTree(){
		spawnOneTree = true;
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
		}
		refreshCurrent ();
	}
}
