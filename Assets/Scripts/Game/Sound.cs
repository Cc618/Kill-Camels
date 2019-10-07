using UnityEngine;

// To use it in the Audio Manager

[System.Serializable]
public class Sound
{
    // Id
    public string name;

    // Data
    public AudioClip clip;

    // To play it
    [HideInInspector]
    public AudioSource source;
}