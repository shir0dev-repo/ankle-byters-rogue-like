using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        PlayMusic("BGM");
    }

    public void PlayMusic(string name)
    {
        Sound background = Array.Find(musicSounds, x => x.name == name);

        if (background == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = background.clip;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(sfxSounds, x => x.name == name);

        if (sfx == null)
        {
            Debug.Log("Sfx not found");
        }
        else
        {
            sfxSource.clip = sfx.clip;
            sfxSource.Play();
        }
    }

    public void MusicVoulume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
