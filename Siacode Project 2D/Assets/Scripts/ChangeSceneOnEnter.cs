using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnter : MonoBehaviour
{
    [SerializeField] private float teleportTimeSeconds;
    [SerializeField] private TriggerAnimation trigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
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
