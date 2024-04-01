using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    protected override void Awake()
    {
        base.Awake();
    }

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

    public void SetVolumeMusic(float volume)
    {
        float clampedVolume = RemapFrom01(0, 0.5f, volume); // I clamped the music between 0 and 0.2 because its too loud lol
        musicSource.volume = clampedVolume;
    }

    public void SetVolumeSFX(float volume)
    {
        sfxSource.volume = volume;
    }

    private float RemapFrom01(float low, float high, float value)
    {
        return low + (high - low) * value;
    }
}
