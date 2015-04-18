using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BeaversManager : MonoBehaviour {

	[SerializeField]
	private GameObject m_beaverPrefab;

	[SerializeField]
	private GameObject m_beaverContainer;
	private List<Beaver> m_listBeavers;

	public Action<int> onBeaverKilledListener;

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






	void SmashBeaversHangOnTree(){
		foreach(Beaver beav in m_listBeavers){
			if(beav.getCurrentState() == BeaverState.HangOnTree){

			}
		}
	}




	void CreateBeaver(){
		m_beaverCreated++;
		GameObject plop = Instantiate (m_beaverPrefab);
		plop.transform.SetParent (m_beaverContainer.transform);
		plop.transform.localPosition = Vector3.zero;
		plop.name = "Beaver_"+m_beaverCreated.ToString();
		
		Beaver beav = plop.gameObject.GetComponent<Beaver> ();
		beav.destroyListener += removeBeaver;
		beav.Initialize ();
		
		m_listBeavers.Add (plop.gameObject.GetComponent<Beaver> ());



		
		m_beaverCreated++;
		GameObject plop2 = Instantiate (m_beaverPrefab);
		plop2.transform.SetParent (m_beaverContainer.transform);
		plop2.transform.localPosition = new Vector3 (-20f, 0f, 0f);
		plop2.name = "Beaver_"+m_beaverCreated.ToString();
		
		Beaver beav2 = plop2.gameObject.GetComponent<Beaver> ();
		beav2.destroyListener += removeBeaver;
		beav2.Initialize ();
		
		m_listBeavers.Add (plop.gameObject.GetComponent<Beaver> ());
	}

 	void removeBeaver(Beaver beav){
		beav.destroyListener -= removeBeaver;
		m_listBeavers.Remove (beav);
		m_beaverKilled++;
		if (onBeaverKilledListener != null) {
			onBeaverKilledListener(m_beaverKilled);
		}
	}
}
