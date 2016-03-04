using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class MusicVolumeModifier : MonoBehaviour {

	AudioSource myAudioSource;

	// Use this for initialization
	void Awake () {
		myAudioSource = GetComponent<AudioSource>();

		if( myAudioSource == null ) {
			Debug.LogWarning( gameObject.name + "'s AudioModifier is missing it's AudioSource" );
		}
		OptionMenu.ModifyMusicVolume += UpdateMusicVolume;
	}

	void OnEnable() {
		
	}

	void OnDisable() {
//		OptionMenu.ModifyMusicVolume -= UpdateMusicVolume;
	}

	void UpdateMusicVolume() {
		if( myAudioSource != null ) {
			myAudioSource.volume = GameManager.musicVolume / 20f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
