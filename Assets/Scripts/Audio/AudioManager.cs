using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    

    private void Awake()
    {
        DontDestroyOnLoad(this);

        foreach (Sound sound in sounds)
        {
            sound.source=gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;  
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
