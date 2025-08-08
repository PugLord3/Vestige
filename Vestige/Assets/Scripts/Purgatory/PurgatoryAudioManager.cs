using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgatoryAudioManager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip key, dash, slash;
    public static PurgatoryAudioManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSFX(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
