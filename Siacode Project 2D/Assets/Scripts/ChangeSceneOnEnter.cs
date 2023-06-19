using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnter : MonoBehaviour
{
    [SerializeField] private float teleportTimeSeconds;
    [SerializeField] private TriggerAnimation trigger;

    public bool isTeleportActive;

    private void Start()
    {
        isTeleportActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isTeleportActive)
        {
            StartCoroutine(WaitToTeleport());
        }
    }

    IEnumerator WaitToTeleport()
    {
        trigger.Trigger();
        yield return new WaitForSeconds(teleportTimeSeconds);
        SceneManager.LoadScene("Level_02");
    }
}