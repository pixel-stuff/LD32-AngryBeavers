using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeaversManager : MonoBehaviour {

	[SerializeField]
	private GameObject m_beaverPrefab;

	[SerializeField]
	private GameObject m_beaverContainer;
	private List<Beaver> m_listBeavers;

	//[SerializeField]
	//private  m_brothersManager;

	private int m_beaverCreated = 0;
	private int m_beaverKilled = 0;

	// Use this for initialization
	void Start () {
		m_listBeavers = new List<Beaver> ();
		m_beaverContainer.transform.position = GameObject.FindGameObjectWithTag ("SpawnArea").transform.position;

		CreateBeaver ();
	}
	
	// Update is called once per frame
	void Update () {


	
	}











	void CreateBeaver(){
		m_beaverCreated++;
		GameObject plop = Instantiate (m_beaverPrefab);
		plop.transform.SetParent (m_beaverContainer.transform);
		plop.transform.localPosition = Vector3.zero;
		plop.name = "Beaver_"+m_beaverCreated.ToString();
		
		plop.gameObject.GetComponent<Beaver> ().destroyListener += removeBeaver;
		
		m_listBeavers.Add (plop.gameObject.GetComponent<Beaver> ());
	}

 	void removeBeaver(Beaver beav){
		beav.destroyListener -= removeBeaver;
		m_listBeavers.Remove (beav);
		m_beaverKilled++;
	}
}
