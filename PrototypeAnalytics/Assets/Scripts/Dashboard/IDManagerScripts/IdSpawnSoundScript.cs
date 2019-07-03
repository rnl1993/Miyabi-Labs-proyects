using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IdSpawnSoundScript : MonoBehaviour {

    private AudioSource GetAudioSource;

    private void OnEnable(){

        GetAudioSource = GetComponent<AudioSource>();
        GetAudioSource.Play();
    }
}
