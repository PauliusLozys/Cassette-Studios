using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;

            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.Log("Sound: " + name + "could not be found");
        }

        s.source.Play();
    }

    public void PlayDeathSound()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Theme");
        s.source.Stop();
        Sound d = Array.Find(sounds, sound => sound.name == "Death");
        d.source.Play();

        //d.source.Stop();
        s.source.PlayDelayed(3);
    }
}
