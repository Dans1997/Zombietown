using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCreator : MonoBehaviour
{
    [Header("Rocket Prefab")]
    [SerializeField] float launchPower = 60f;

    [Header("Rocket Lauch Velocity")]
    [SerializeField] Rocket rocketPrefab;

    [Header("Rocket VFXs")]
    [SerializeField] ParticleSystem muzzleFlashVFX;

    [Header("Rocket SFXs")]
    [SerializeField] AudioClip fireSFX;
    [SerializeField] AudioClip reloadSFX;

    Rocket newRocket = null;
    bool hasFired = false;
    float destroyDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (newRocket == null) CreateRocket();
        if (hasFired) Destroy(newRocket, destroyDelay);
    }

    private void CreateRocket()
    {
        if (newRocket != null) return;

        newRocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
        newRocket.transform.SetParent(transform);

        newRocket.GetComponent<BoxCollider>().enabled = false;
        newRocket.GetComponent<Rigidbody>().isKinematic = true;

        AudioSource.PlayClipAtPoint(reloadSFX, transform.position, 0.3f);

        hasFired = false;
    }

    private void FireRocket()
    {
        if (newRocket == null && !hasFired) return;
      
        newRocket.transform.SetParent(null);

        newRocket.GetComponent<BoxCollider>().enabled = true;
        newRocket.GetComponent<Rigidbody>().isKinematic = false;
        newRocket.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * launchPower);

        muzzleFlashVFX.Play();
        AudioSource.PlayClipAtPoint(fireSFX, transform.position, 0.4f);

        hasFired = true;
    }
}
