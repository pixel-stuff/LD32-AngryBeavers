using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float shakeSpeed = 0.01f;
	public float shakeAmplitude = 0.1f;
	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		Camera.main.orthographicSize = 2.3f;
	}

	void shakeY (bool brute) {
		if (brute) {
			float y = Random.Range (-0.1f, 0.1f);
			Camera.main.transform.position = new Vector3 (0.0f, y, 0.0f);
		} else {
			if(Camera.main.transform.position.x<-shakeAmplitude) {
				shakeSpeed = Mathf.Abs(shakeSpeed);
			}else if(Camera.main.transform.position.x>shakeAmplitude) {
				shakeSpeed = -Mathf.Abs(shakeSpeed);
			}
			Camera.main.transform.Translate(new Vector3 (0.0f, shakeSpeed, 0.0f));
		}
	}
	
	void shakeX (bool brute) {
		if (brute) {
			float x = Random.Range (-0.1f, 0.1f);
			Camera.main.transform.position = new Vector3 (x, 0.0f, 0.0f);
		} else {
			if(Camera.main.transform.position.x<-shakeAmplitude) {
				shakeSpeed = Mathf.Abs(shakeSpeed);
			}else if(Camera.main.transform.position.x>shakeAmplitude) {
				shakeSpeed = -Mathf.Abs(shakeSpeed);
			}
			Camera.main.transform.Translate(new Vector3 (shakeSpeed, 0.0f, 0.0f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		shakeX (false);
		shakeY (false);
	}
}
