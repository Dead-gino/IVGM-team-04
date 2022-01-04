using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Switch_Gravity))]
[RequireComponent(typeof(AudioSource))]
public class Gravity_SFX_Controller : MonoBehaviour
{

    bool play_jump;
    public AudioClip SFX;

    AudioSource audio;
    Switch_Gravity grav;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = SFX;

        grav = GetComponent<Switch_Gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        play_jump = grav.play_jump;
        if (play_jump)
        {
            audio.Play();
        }
    }
}
