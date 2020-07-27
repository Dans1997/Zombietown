using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] AudioClip[] footstepAudioClips;
    float timeBetweenSteps = 0.5f;

    // Cached Components
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayFootstepSounds());
    }

    IEnumerator PlayFootstepSounds()
    {
        while(true)
        {
            bool isWalking = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
            bool isGrounded = Mathf.Abs(rigidBody.velocity.y) < 0.1f;
            if (isWalking && isGrounded) audioSource.PlayOneShot(footstepAudioClips[0], 1f);
            yield return new WaitForSeconds(timeBetweenSteps);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
