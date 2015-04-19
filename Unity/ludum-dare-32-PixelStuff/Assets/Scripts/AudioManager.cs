using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public void Play(string clipname)
	{
		//Create an empty game object
		GameObject go = new GameObject ("Audio_" +  clipname);

		//Load clip from ressources folder
		AudioClip newClip =  Instantiate(Resources.Load ("Sounds/"+clipname, typeof(AudioClip))) as AudioClip;
	
		//Add and bind an audio source
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = newClip;
	
		//Play and destroy the component
		source.Play();
		Destroy (go, newClip.length);
		return;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
	