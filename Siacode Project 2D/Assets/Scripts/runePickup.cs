using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePickup : MonoBehaviour
{
    public GameObject teleport;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            teleport.GetComponent<ChangeSceneOnEnter>().isTeleportActive = true;
            this.gameObject.SetActive(false);
        }
    }
}