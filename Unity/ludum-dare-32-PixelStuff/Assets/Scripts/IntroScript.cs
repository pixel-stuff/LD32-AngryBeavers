﻿using UnityEngine;
using System.Collections;
using System;

public class IntroScript : MonoBehaviour {

	public Action onKeyDown;
	
	bool isPush = false;
	// Update is called once per frame
	void Update () {
		if (Input.anyKey && !isPush) {
			isPush = true;
			if(onKeyDown != null){
				onKeyDown();
			}
		}
	}
}
