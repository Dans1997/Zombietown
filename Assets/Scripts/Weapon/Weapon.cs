using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 1f;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    [Header("VFXs")]
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject bulletHitVFX;

    [Header("SFXs")]
    [SerializeField] AudioClip fireSFX;
    [SerializeField] AudioClip noAmmoSFX;

    [Header("Ammo Display Canvas")]
    [SerializeField] Canvas ammoCanvas;

    bool isShootEnabled = true;
    [SerializeField] float timeBetweenShots = 0f;

    private void OnEnable()
    {
        isShootEnabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Display Ammo
        ammoCanvas.GetComponentInChildren<Text>().text = ammoSlot.GetCurrentAmmo(ammoType).ToString();

        if (!isShootEnabled) return;

        if (timeBetweenShots <= 0)
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(Shoot());
        }
        else
        {
            if(Input.GetMouseButton(0)) StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isShootEnabled = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) <= 0) 
        {
            /* NOT ENOUGH AMMO */
            AudioSource.PlayClipAtPoint(noAmmoSFX, transform.position, 1f);
            yield return new WaitForSeconds(timeBetweenShots);
            isShootEnabled = true;
        } 
        else
        {
            RocketCreator rocketCreator = GetComponentInChildren<RocketCreator>();
            if (rocketCreator) rocketCreator.SendMessage("FireRocket");
            else 
            {
                PlayMuzzleFlash();
                ProcessRaycast();
            }
          
            ammoSlot.ReduceCurrentAmmo(ammoType);
            yield return new WaitForSeconds(timeBetweenShots);
            isShootEnabled = true;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlashVFX.Play();
        AudioSource.PlayClipAtPoint(fireSFX, transform.position, 0.2f);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            GameObject gameObject = Instantiate(bulletHitVFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(gameObject, bulletHitVFX.GetComponentInChildren<ParticleSystem>().main.duration);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.ProcessHit(damage);
            }
        }
    }
}
