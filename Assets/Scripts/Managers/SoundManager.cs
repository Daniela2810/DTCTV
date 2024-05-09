using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource loopedSoundSource;

    [SerializeField] private List<AudioClip> Music = new List<AudioClip>();
    [SerializeField] private List<AudioClip> soundClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> ObjectSound = new List<AudioClip>();
    [SerializeField] private List<AudioClip> loopedSoundClips = new List<AudioClip>();

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayMusic(0);
    }

    public void PlayMusic(int MusicIndex)
    {
        if (MusicIndex >= 0 && MusicIndex < Music.Count)
        {
            musicSource.clip = Music[MusicIndex];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySound(int soundIndex, bool isObjectSound)
    {
        List<AudioClip> selectedList = isObjectSound ? ObjectSound : soundClips;

        if (soundIndex >= 0 && soundIndex < selectedList.Count)
        {
            soundSource.PlayOneShot(selectedList[soundIndex]);
        }
    }

    public void StopSound()
    {
        soundSource.Stop();
    }

    public void PlayLoopedSound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < loopedSoundClips.Count)
        {
            loopedSoundSource.clip = loopedSoundClips[soundIndex];
            loopedSoundSource.loop = true;
            loopedSoundSource.Play();
        }
    }

    public void StopLoopedSound()
    {
        loopedSoundSource.Stop();
    }
}
