using UnityEngine;

// For sound fx
public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance;

    // Sounds list
    public Sound[] sounds;

    // Play a sound by name
    public static void Play(string name)
    {
        // Search sound
        Sound s = System.Array.Find(instance.sounds, sound => sound.name == name);

        s.source.Play();
    }

    // Position limit
    public static void Play(string name, float pos)
    {
        if (Mathf.Abs(PlayerMovements.instance.transform.position.x - pos) <= 20)
            Play(name);
    }

    void Awake()
    {
        // Singleton
        instance = this;

        // Sound init
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = false;
        }
    }
}

