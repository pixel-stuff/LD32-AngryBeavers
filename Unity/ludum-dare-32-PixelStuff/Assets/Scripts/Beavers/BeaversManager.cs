using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeaversManager : MonoBehaviour {

	[SerializeField]
	private GameObject m_beaverPrefab;

	[SerializeField]
	private GameObject m_beaverContainer;
	private List<Beaver> m_listBeavers;

	[SerializeField]
	private BrothersManager m_brothersManager;

	// Use this for initialization
	void Start () {
		m_beaverContainer.transform.position = GameObject.FindGameObjectWithTag ("SpawnArea").transform.position;


		GameObject plop = Instantiate (m_beaverPrefab);
		plop.transform.SetParent (m_beaverContainer.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
