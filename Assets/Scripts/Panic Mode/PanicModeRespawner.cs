using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicModeRespawner : MonoBehaviour
{
    bool canRespawn = false;

    // Update is called once per frame
    void Update()
    {
        if (canRespawn) SetActiveAllChildren();   
    }

    public void RespawnAllZombies() => canRespawn = true;

    private void SetActiveAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        BroadcastMessage("EnterPanicMode");
    }
}
