using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip winMusic;

    //Cached Components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeClipTo(levelMusic, true);
    }

    public void ChangeClipTo(AudioClip newClip, bool willLoop)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.loop = willLoop;
        audioSource.clip = newClip;
        audioSource.Play();
    }

}
