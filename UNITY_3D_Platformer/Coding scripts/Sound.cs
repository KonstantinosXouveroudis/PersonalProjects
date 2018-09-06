using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] //Adding slider for volume
    public float volume;

    [Range(0.1f, 3f)] //Adding slider for pitch
    public float pitch;

    [HideInInspector] 
    public AudioSource source; //We need this to be public in order to access it from the AudioManager script.
}
