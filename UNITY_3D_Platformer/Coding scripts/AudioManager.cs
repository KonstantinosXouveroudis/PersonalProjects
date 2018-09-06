using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

	//Same as the Start method, except that it's played right before.
	void Awake () {
		
		//Process all sound available and save them.
		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
	}
	
    public void Play(string soundName)
    {
        //Find a sound in the sounds array whose name is the same as the soundName parameter.
        Sound s = Array.Find(sounds, sounds => sounds.name == soundName);
        s.source.Play();
    }
}
