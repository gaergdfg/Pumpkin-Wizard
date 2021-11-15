using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {
	public static AudioManager Instance;

	public GameObject soundEffectsContainer;
	public GameObject voicesContainer;

	public Sound[] sounds;

	[HideInInspector]
	public float generalVolume;
	[HideInInspector]
	public bool areSpecialEffectsMuted;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		foreach (Sound sound in sounds) {
			if (sound.name == "Theme") {
				sound.source = this.gameObject.AddComponent<AudioSource>();
			} else if (isVoiceSound(sound.name))  {
                sound.source = voicesContainer.AddComponent<AudioSource>();
            } else {
                sound.source = soundEffectsContainer.AddComponent<AudioSource>();
            }

            sound.source.clip = sound.clip;

			sound.source.volume = sound.volume;
			sound.source.loop = sound.loop;
		}

		generalVolume = 0.5f;
		areSpecialEffectsMuted = false;
	}

	void Start() {
		play("Theme");
	}

	Sound findSound(string name) {
		return Array.Find(sounds, sound => sound.name == name);
	}

	public void play(string name) {
		Sound sound = findSound(name);
		if (sound == null) {
            Debug.Log("sound: (" + name + ") not found");
            return;
        }
        sound.source.Play();
	}

	public void setVolume(float volume) {
		generalVolume = volume;
		foreach (Sound sound in sounds) {
			sound.source.volume = volume;
		}
	}

	public void toggleSpecialEffects() {
		areSpecialEffectsMuted = !areSpecialEffectsMuted;
		foreach (Sound sound in sounds) {
			if (sound.name != "Theme") {
				sound.source.mute = areSpecialEffectsMuted;
			}
		}
	}

	private bool isVoiceSound(string name) {
		return (name[0] == 'k' || name[0] == 'p') && name[1] == '_';
    }
}
