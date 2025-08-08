using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerAudioManager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip dash, tp, jump;
    public static PlatformerAudioManager instance;

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
