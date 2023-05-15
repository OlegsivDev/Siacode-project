using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public bool useBuiltInObjectPooling;
    public ObjectPool<GameObject> Pool;
    public float speed;

    public GameObject girl;
    // Start is called before the first frame update
    void Start()
    {
        girl = GameObject.Find("Girl");
        Pool = girl.GetComponent<CharacterController>().Pool;
        useBuiltInObjectPooling = girl.GetComponent<CharacterController>().useBuiltInObjectPooling;
        
        Invoke("DestroyObject", 1f);
    }

    private void OnEnable()
    {
        Invoke("DestroyObject", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject()
    {
        if (useBuiltInObjectPooling)
        {
            Pool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }
}
