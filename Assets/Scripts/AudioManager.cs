using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /*  HOW TO USE:
     *  
     *  1. In the class the sound had to originate from, place before the starting method: [SerializeField] private AudioClip AUDIONAME;
     *  2. Drag the audio file into the new field of the object the script is located in.
     *  3. Decide where and how you want your sound to play. Your options for the playtype are PlaySFX, PlayMusic, PlayMusicWithFade, and PlayMusicWithCrossFade.
     *  4. Place down: AudioManager.Instance.PLAYTYPE(AUDIONAME); where in the script you want the sound to be played.
     * 
     *  This class was not recreated by Team B1, but originaly built by Epitome.
     */

    #region Static Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion

    private void Awake()
    {
        //Make sure we dont destroy this instance
        DontDestroyOnLoad(this.gameObject);

        //Create audio sources, and save them as references
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        // Loop the music tracks
        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        //Swap the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        //Set the field of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        float t = 0.0f;

        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));

            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        // Fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;

        for (t = 0; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }

        original.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlaySFXProximity(AudioClip clip, float volume = 1, float spatialBlend = 1.0f) // BROKEN! - does not work
    {
        sfxSource.spatialBlend = spatialBlend;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}