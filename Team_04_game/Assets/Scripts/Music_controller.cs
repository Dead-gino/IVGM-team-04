using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music_controller : MonoBehaviour
{

    public AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        music.LoadAudioData();
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = music;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
