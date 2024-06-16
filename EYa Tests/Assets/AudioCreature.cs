using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCreature : MonoBehaviour
{

    [SerializeField] public AudioClip noise1;
    [SerializeField] public AudioClip Greeting;

    AudioSource mySource;
    private void Awake()
    {
        mySource = GetComponent<AudioSource>();
    }

    public void PlayTheAudio1()
    {
        mySource.PlayOneShot(noise1);
    }
    public void PlayTheAudio2()
    {
        mySource.PlayOneShot(Greeting);

    }
    public void PlayTheAudio3()
    {

    }
}
