using UnityEngine;
using System.Collections;

public class ChopOnTree : MonoBehaviour {
		public Sprite[] spr;
		public int nbChopForchangeState = 5;
		
		private int totalChop;
		
		// Use this for initialization
		void Start () {
			totalChop = 0;
		}
		
		public void triggerChop(){
			totalChop++;
			if(!isCompletlyChop())
				this.GetComponent<SpriteRenderer>().sprite = spr [totalChop / nbChopForchangeState];
		}

		public bool isCompletlyChop(){

		return totalChop >= nbChopForchangeState * spr.Length; 
		}
	}
