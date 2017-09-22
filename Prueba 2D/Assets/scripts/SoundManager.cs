using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource voiceSound;
    public AudioSource musicSource;
    public float fadeVelocity = 0.01f;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    public float smoothTimeX = 0.1f;
    public float smoothTimeY = 1f;

    public float minY = -15f;

    public AudioClip[] songList;

    private Vector2 velocity;
    private bool fadingOut = false;
    private bool fadingIn = false;
    private float musicStandarVolume;
   

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        musicStandarVolume = musicSource.volume;
        DontDestroyOnLoad(gameObject);
    }

    public void FixedUpdate()
    {
        if (fadingIn)
        {
            musicSource.volume += fadeVelocity;
            if (musicSource.volume >= musicStandarVolume)
            {
                musicSource.volume = Mathf.Clamp(musicSource.volume, 0, musicStandarVolume);
                fadingIn = false;
            }
        }
        else if (fadingOut)
        {
            musicSource.volume -= fadeVelocity;
            if (musicSource.volume <= 0)
            {
                musicSource.volume = 0;
                fadingOut = false;
            }
        }
    }
        public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.pitch = 1;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void RandomizeVoice(params AudioClip[] clips)
    {
        if (voiceSound.isPlaying)
            return;
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        voiceSound.pitch = randomPitch;
        voiceSound.clip = clips[randomIndex];
        voiceSound.Play();
    }

    public void stopVoice()
    {
        voiceSound.Stop();
    }

    public void playMusicByIndex(int index)
    {
        musicSource.clip = songList[index];
        musicSource.Play();
    }

    public void pauseMusic()
    {
        musicSource.Pause();
    }

    public void playCurrentMusic()
    {
        musicSource.UnPause();
    }

    public void fadeOutMusic()
    {
        fadingOut = true;
        fadingIn = false;
    }

    public void fadeInMusic()
    {
        musicSource.volume = 0;
        fadingIn = true;
        fadingOut = false;
    }

    public void setMusicVolumeToStandar()
    {
        musicSource.volume = musicStandarVolume;
    }
}
