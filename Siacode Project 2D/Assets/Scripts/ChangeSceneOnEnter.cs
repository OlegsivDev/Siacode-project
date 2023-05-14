using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TeleportToNextLevel"))
        {
            StartCoroutine(WaitToTeleport());
        }
    }

    IEnumerator WaitToTeleport()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Level_02");
    }
}
