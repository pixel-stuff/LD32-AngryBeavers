using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	GameObject m_backgroundGameObject;
	AudioSource m_backgroundAudioSource;
	
	GameObject m_brothersRunGameObject;
	AudioSource m_brothersRunAudioSource;
	
	public float m_backgroundVolume = 0.01f;
	public float m_brothersRunVolume = 0.5f;
	public float m_globalVolume = 1.0f;

	public void Play(string clipname)
	{
		//Create an empty game object
		GameObject go = new GameObject ("Audio_" +  clipname);

		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load ("Sounds/"+clipname, typeof(AudioClip))) as AudioClip;
	
		//Add and bind an audio source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = newClip;
		source.volume = source.volume * m_globalVolume;
		//Play and destroy the component
		source.Play();
		Destroy (go, newClip.length);
		return;
	}

	public void startAudioBackground() {
		m_backgroundAudioSource.Play();
	}

	public void stopAudioBackground() {
		m_backgroundAudioSource.Stop();
	}
	
	public void startAudioBrothersRunning() {
		m_brothersRunAudioSource.Play();
	}
	
	public void stopAudioBrothersRunning() {
		m_brothersRunAudioSource.Stop();
	}

	// Use this for initialization
	void Start () {
		//Create an empty game object
		m_backgroundGameObject = new GameObject ("Audio_Background");
		//Load clip from ressources folder
		AudioClip bgClip =  Instantiate(Resources.Load ("Sounds/BackgroundMusic", typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		m_backgroundAudioSource = m_backgroundGameObject.AddComponent<AudioSource>();
		m_backgroundAudioSource.clip = bgClip;
		m_backgroundAudioSource.loop = true;
		m_backgroundAudioSource.volume = m_backgroundVolume * m_globalVolume;
		//Play and destroy the component
		//Create an empty game object
		m_brothersRunGameObject = new GameObject ("Audio_BrothersRunning");
		//Load clip from ressources folder
		AudioClip brotherClip =  Instantiate(Resources.Load ("Sounds/foot_step", typeof(AudioClip))) as AudioClip;
		
		//Add and bind an audio source
		m_brothersRunAudioSource = m_backgroundGameObject.AddComponent<AudioSource>();
		m_brothersRunAudioSource.clip = brotherClip;
		m_brothersRunAudioSource.loop = true;
		m_brothersRunAudioSource.volume = m_brothersRunVolume * m_globalVolume;
		//Play and destroy the component
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
	