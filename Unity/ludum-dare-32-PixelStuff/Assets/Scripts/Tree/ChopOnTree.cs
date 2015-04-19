using UnityEngine;
using System.Collections;

public class ChopOnTree : MonoBehaviour {
		public Sprite[] spr;
		public int nbChopForchangeState = 5;
		
		public int totalChop;
		
		// Use this for initialization
		void Start () {
			totalChop = 0;
		}
		
		public void triggerChop(){
			totalChop++;
			if (!isCompletlyChop ()) {
			this.GetComponent<SpriteRenderer> ().sprite = spr [totalChop / nbChopForchangeState];
			this.GetComponentInChildren<ParticleSystem> ().Play ();
			GameObject.FindGameObjectWithTag("TreeAnimation").GetComponent<ParticleSystem>().Play();
		}
		}

		public bool isCompletlyChop(){

		return totalChop >= nbChopForchangeState * spr.Length; 
		}
	}
