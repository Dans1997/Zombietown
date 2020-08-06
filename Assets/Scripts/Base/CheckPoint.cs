using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private static CheckPoint instance;
    bool isActive = true;

    // Awake is called before Start
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    // Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
    void OnEnable() => SceneManager.sceneLoaded += OnLevelFinishedLoading;

    // Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. 
    // Remember to always have an unsubscription for every delegate you subscribe to!
    void OnDisable() => SceneManager.sceneLoaded -= OnLevelFinishedLoading;

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (!isActive) return;
        foreach (Transform zombie in transform)
        {
            Destroy(zombie.gameObject);
        }
    }

    public bool IsCheckPointActive() => isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerHealth>()) return;
        isActive = true;
    }
}
