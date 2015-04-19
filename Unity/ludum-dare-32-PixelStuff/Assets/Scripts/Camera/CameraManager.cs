using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float shakeSpeedX = 0.01f;
	public float shakeAmplitudeX = 0.1f;
	public float shakeSpeedY = 0.01f;
	public float shakeAmplitudeY = 0.1f;

	public GameObject gameOverScreenSprite;

	public bool m_isShakingX = false;
	public bool m_isShakingY = false;
	public bool m_isGameOver = false;
	
	private float m_shakingDuration = 0.0f;
	// Use this for initialization
	void Start () {
		restartSettings();
		setShaking (false, true, 2.0f);
	}

	private void restartSettings() {
		Camera.main.transform.position = new Vector3 (0.0f, 0.0f, -2.0f);
		Camera.main.orthographicSize = 2.3f;
		gameOverScreenSprite.transform.position = new Vector3 (0.0f, 0.0f, -1.0f);
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
			if(Camera.main.transform.position.y<-shakeAmplitudeY) {
				shakeSpeedY = Mathf.Abs(shakeSpeedY);
			}else if(Camera.main.transform.position.y>shakeAmplitudeY) {
				shakeSpeedY = -Mathf.Abs(shakeSpeedY);
			}
			Camera.main.transform.Translate(new Vector3 (0.0f, shakeSpeedY, 0.0f));
		}
	}

	private void enableGameOverScreen() {
		gameOverScreenSprite.SetActive(true);
	}
	
	private void disableGameOverScreen() {
		gameOverScreenSprite.SetActive(false);
	}

	public void writeOnScreen(string text) {
		GUIText guiText = gameOverScreenSprite.AddComponent<GUIText> ();
		guiText.color = Color.red;
		guiText.transform.position = new Vector3(0.5f,0.5f,0f);
		guiText.font = Font.CreateDynamicFontFromOSFont("Arial", 11);
		guiText.text = text;
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

	public void setShaking(bool x, bool y, float duration) {
		m_isShakingX = x;
		m_isShakingY = y;
		m_shakingDuration = duration;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isShakingX && m_shakingDuration>0.0f) {
			shakeX (false);
		}
		if (m_isShakingY && m_shakingDuration>0.0f) {
			shakeY (false);
		}
		if (m_shakingDuration <= 0.0f) {
			m_isShakingX = false;
			m_isShakingY = false;
		}
		if (!m_isShakingX && !m_isShakingY) {
			restartSettings();
		}
		if (m_isGameOver) {
			enableGameOverScreen ();
		} else {
			disableGameOverScreen ();
		}
		m_shakingDuration -= Time.deltaTime;
	}
}
