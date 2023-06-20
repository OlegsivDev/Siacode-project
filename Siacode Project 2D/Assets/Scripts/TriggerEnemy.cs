using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    public List<GameObject> objects;
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
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Enemy>().Trigger();
        }
        gameObject.SetActive(false);
    }
}
