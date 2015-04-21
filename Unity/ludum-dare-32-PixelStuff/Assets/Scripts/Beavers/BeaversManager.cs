using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BeaversManagerState{
	GameStarting,
	LettingPlayerCutDowntree,
	Assault
	
}

public class BeaversManager : MonoBehaviour {

	[SerializeField]
	private GameObject m_beaverPrefab;

	[SerializeField]
	private GameObject m_beaverContainer;
	private List<Beaver> m_listBeavers;

	private treeManager m_treeManager;

	public Action<int> onBeaverKilledListener;

	private BeaversManagerState m_currentState;

	private int m_beaverCreated = 0;
	private int m_beaverKilledTotal = 0;

	// Use this for initialization
	void Start () {
		m_listBeavers = new List<Beaver> ();
		m_beaverContainer.transform.position = GameObject.FindGameObjectWithTag ("SpawnArea").transform.position;

		m_currentState = BeaversManagerState.GameStarting;
		
		m_timeStateStarted[(int)m_currentState] = Time.time;

		m_treeManager = GameObject.FindGameObjectWithTag ("TreeManager").GetComponent<treeManager>();
	}


	private float m_lastCreated;
	public float[] m_frequenceCreationBeaver = new float[]{
		0, //GameStarting
		0.333f,	//LettingPlayerCutDowntree
		2f	//Assault
	};

	private float[] m_timeStateStarted = new float[]{
		0,
		0f,	//LettingPlayerCutDowntree
		0f	//Assault
	};
	private float[] m_timeToStayInState = new float[]{
		8f,
		8f,	//LettingPlayerCutDowntree
		10f		//Assault
	};
	// Update is called once per frame
	void Update () {
		if (m_beaverCreated - m_beaverKilledTotal >= 50) {
			m_currentState = BeaversManagerState.LettingPlayerCutDowntree;
			return;
		}

		if (m_currentState == BeaversManagerState.GameStarting) {
			if (Time.time - m_timeStateStarted [(int)m_currentState] > m_timeToStayInState [(int)m_currentState]) {
				int cur = (int)m_currentState;
				cur++;
				m_currentState = (BeaversManagerState)cur;
				m_timeStateStarted[(int)m_currentState] = Time.time;
				return;
			}
		}

		
		if(Time.time - m_lastCreated >= 1/m_frequenceCreationBeaver[(int)m_currentState]){
			CreateBeaver();
			m_lastCreated = Time.time;
		}


		//Change State lorsque temps écoulé
		if (Time.time - m_timeStateStarted [(int)m_currentState] > m_timeToStayInState [(int)m_currentState]) {
			int cur = (int)m_currentState;
			cur = (cur+1)%3;
			if(cur == 0){
				cur = 1;

			}
			m_currentState = (BeaversManagerState)cur;
			m_timeStateStarted[(int)m_currentState] = Time.time;
		}
	}

	public void SmashBeaversHangOnTree(){
		foreach(Beaver beav in m_listBeavers){
			if(beav.getCurrentState() == BeaverState.HangOnTree){
				beav.changeState(BeaverState.Flying);
			}
		}
	}

	public void destroyBeaversOnTree(){
		foreach (Beaver beav in m_listBeavers) {
			if (beav.getCurrentState () == BeaverState.HangOnTree) {
				beav.transform.position = new Vector3(
					beav.transform.position.x,
					GameObject.FindGameObjectWithTag ("SpawnArea").transform.position.y,
					beav.transform.position.z
					);
				beav.changeState(BeaverState.Running);
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
		beav.Initialize (m_treeManager);

		
		m_listBeavers.Add (plop.gameObject.GetComponent<Beaver> ());
	}


	public void letThePlayerCuttingDownTree(){
		m_currentState = BeaversManagerState.LettingPlayerCutDowntree;
		m_timeStateStarted[1] = Time.time;
	}

 	void removeBeaver(Beaver beav){
		beav.destroyListener -= removeBeaver;
		m_listBeavers.Remove (beav);
		m_beaverKilledTotal++;
		if (onBeaverKilledListener != null) {
			onBeaverKilledListener(m_beaverKilledTotal);
		}
	}

	public void Restart(){
		m_currentState = BeaversManagerState.GameStarting;
		
		m_timeStateStarted[(int)m_currentState] = Time.time;
		m_beaverCreated = 0;
		m_beaverKilledTotal = 0;

	}

	public int getBeaverOnScreen(){
		return m_beaverCreated - m_beaverKilledTotal;

	}

	public int getBeaverKilledTotal() {
		return m_beaverKilledTotal;
	}
}
