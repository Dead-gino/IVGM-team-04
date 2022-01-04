using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(doorInteraction))]
[RequireComponent(typeof(AudioSource))]
public class Door_SFX_Controller : MonoBehaviour
{

    bool play = false;
    private bool triggered = false;
    public AudioClip SFX;

    AudioSource audio;
    doorInteraction door;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //audio.clip = SFX;

        door = GetComponent<doorInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door.activated && !triggered)
        {
            play = true;
            triggered = true;
        }
        if (play)
        {
            audio.Play();
            play = false;
        }
    }
}
