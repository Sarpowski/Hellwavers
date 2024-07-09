using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    public AudioSource musicSource;
    public AudioSource sfxSource;


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
    }

    public void PlayMusic(string name)
    {
        
        Sound s = Array.Find(musicSounds, x => x.songName.Equals(name));
       
        
            musicSource.clip = s.audioClip;
            musicSource.Play();
        
    }

    public void PlaySFX(string name)
    {
        Debug.Log("Attempting to play SFX: " + name);
        Sound s = Array.Find(sfxSounds, x => x.songName == name);
        if (s == null)
        {
            Debug.LogError("SFX sound not found: " + name);
        }
        else
        {
            Debug.Log("SFX sound found: " + name);
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
}
