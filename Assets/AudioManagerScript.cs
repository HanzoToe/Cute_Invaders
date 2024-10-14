using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    public AudioManagerScript Instance;
    
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
      
            PlayMusic("Theme1");

        if (Input.GetKey(KeyCode.Space))
        {
            PlaySFX("Shoot1");
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Ljud källa finns ej");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    
    }
    public void PlaySFX(String name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) 
        {
            Debug.Log("Ljud källa finns ej");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
    