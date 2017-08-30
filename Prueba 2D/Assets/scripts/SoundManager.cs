using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource voiceSound;
    public AudioSource musicSource;
    public static SoundManager instance = null;
    //public GameObject target;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    public float smoothTimeX = 0.1f;
    public float smoothTimeY = 1f;

    public float minY = -15f;

    private Vector2 velocity;

    void Awake () {
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}
    /*
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(
            transform.position.x,
            target.transform.position.x,
            ref velocity.x,
            smoothTimeX
            );

        float posY = Mathf.SmoothDamp(
            transform.position.y,
            target.transform.position.y,
            ref velocity.y,
            smoothTimeY
            );
        if (posY < minY)
            posY = minY;

        transform.position = new Vector3(
            posX,
            posY,
            0
            );
    }
    */
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
}
