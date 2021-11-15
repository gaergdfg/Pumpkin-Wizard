using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotallyNotAnEasterEgg : MonoBehaviour
{
    public GameObject voicesContainer;
    private AudioSource[] sounds;

    private int soundIndex = 0;

    private void Start() {
        sounds = voicesContainer.GetComponents<AudioSource>();
        shuffleSounds();
        StartCoroutine(SoundCoroutine());
    }

    private IEnumerator SoundCoroutine() {
        sounds[soundIndex].Play();
        soundIndex = (soundIndex + 1) % sounds.Length;

        if (soundIndex == 0) {
            shuffleSounds();
        }

        yield return new WaitForSeconds(0.4f);

        StartCoroutine(SoundCoroutine());
    }

    private void shuffleSounds() {
        for (int i = 0; i < sounds.Length; i++) {
            var temp = sounds[i];
            int randomIndex = Random.Range(i, sounds.Length);
            sounds[i] = sounds[randomIndex];
            sounds[randomIndex] = temp;
        }
    }

}
