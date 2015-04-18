using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float shakeSpeedX = 0.01f;
	public float shakeAmplitudeX = 0.1f;
	public float shakeSpeedY = 0.01f;
	public float shakeAmplitudeY = 0.1f;

	public bool m_isShakingX = false;
	public bool m_isShakingY = false;
	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		Camera.main.orthographicSize = 2.3f;
	}
	
	private void shakeX (bool brute) {
		if (brute) {
			float x = Random.Range (-shakeAmplitudeX, shakeAmplitudeX);
			Camera.main.transform.position = new Vector3 (x, 0.0f, 0.0f);
		} else {
			if(Camera.main.transform.position.x<-shakeAmplitudeX) {
				shakeSpeedX = Mathf.Abs(shakeSpeedX);
			}else if(Camera.main.transform.position.x>shakeAmplitudeX) {
				shakeSpeedX = -Mathf.Abs(shakeSpeedX);
			}
			Camera.main.transform.Translate(new Vector3 (shakeSpeedX, 0.0f, 0.0f));
		}
	}

	private void shakeY (bool brute) {
		if (brute) {
			float y = Random.Range (-shakeAmplitudeY, shakeAmplitudeY);
			Camera.main.transform.position = new Vector3 (0.0f, y, 0.0f);
		} else {
			if(Camera.main.transform.position.x<-shakeAmplitudeY) {
				shakeSpeedY = Mathf.Abs(shakeSpeedY);
			}else if(Camera.main.transform.position.x>shakeAmplitudeY) {
				shakeSpeedY = -Mathf.Abs(shakeSpeedY);
			}
			Camera.main.transform.Translate(new Vector3 (0.0f, shakeSpeedY, 0.0f));
		}
	}

	public void setShakeSpeedX(float sspeed) {
		shakeSpeedX = sspeed;
	}
	
	public void setShakeAmplitudeX(float samp) {
		shakeSpeedX = samp;
	}
	
	public void setShakeSpeedY(float sspeed) {
		shakeSpeedY = sspeed;
	}
	
	public void setShakeAmplitudeY(float samp) {
		shakeSpeedY = samp;
	}

	public void setShaking(bool x, bool y) {
		m_isShakingX = x;
		m_isShakingY = y;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isShakingX) {
			shakeX (false);
		}
		if (m_isShakingY) {
			shakeY (false);
		}
	}
}
