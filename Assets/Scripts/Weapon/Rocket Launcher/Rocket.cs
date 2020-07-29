using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Rocket Specs")]
    [SerializeField] float explosionRadius = 5f;

    [Header("Zombies Layer Mask")]
    [SerializeField] LayerMask zombieMask;

    [Header("Explosion Hit VFX Prefab")]
    [SerializeField] ParticleSystem explosionVFX;

    [Header("Explosion Hit SFX Prefab")]
    [SerializeField] GameObject explosionSFX;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 origin = transform.position;
        Collider[] zombiesHit = Physics.OverlapSphere(origin, explosionRadius, zombieMask);

        foreach (var zombie in zombiesHit)
        {
            zombie.gameObject.BroadcastMessage("OnDamageTaken", 10f);
        }

        PlayExplosionEffect();
        Destroy(gameObject);
    }

    private void PlayExplosionEffect()
    {
        // SFX
        GameObject explosionSound = Instantiate(explosionSFX, transform.position, transform.rotation);
        Destroy(explosionSound, 6f);

        // VFX
        ParticleSystem explosionEffect = Instantiate(explosionVFX, transform.position, transform.rotation) as ParticleSystem;
        explosionEffect.Play();
        Destroy(explosionEffect.gameObject, 5f);
    }
}
