// Idea from raybarrera http://gamedev.stackexchange.com/questions/63705/getting-and-playing-audioclips-by-string-name

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;

    public AudioClip[] clipsArray;

    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                //Tell Unity not to destory this object when loading a new scene.
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a singleton already exists nad you find antoher reference in scene, destroy it.
            if (this != _instance)
                Destroy(this.gameObject);
        }


        foreach (var clip in clipsArray)
        {
            soundDictionary.Add(clip.name, clip);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySound(string sound)
    {
        if (soundDictionary.ContainsKey(sound))
        {
            var soundFX = soundDictionary[sound];
            audio.PlayOneShot(soundFX);
        }
        else
            Debug.Log("Audio Clip not found.");
    }

    public void PlaySoundWithVolumeScale(string sound, float volumeScale)
    {
        if (soundDictionary.ContainsKey(sound))
        {
            var soundFX = soundDictionary[sound];
            audio.PlayOneShot(soundFX, volumeScale);
        }
        else
            Debug.Log("Audio Clip not found.");
    }

    public void PlaySoundWithRandomScale(string sound)
    {
        float scale = Random.Range(0.5f, 1f);

        if (soundDictionary.ContainsKey(sound))
        {
            var soundFX = soundDictionary[sound];
            audio.PlayOneShot(soundFX, scale);
        }
        else
            Debug.Log("Audio Clip not found.");
    }
}
