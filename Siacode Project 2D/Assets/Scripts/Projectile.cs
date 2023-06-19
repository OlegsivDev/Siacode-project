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
    public AudioSource impactSound;
    private bool _impacted;
    public static event Action OnArrowImpact;

    public GameObject girl;
    // Start is called before the first frame update
    void Start()
    {
        _impacted = false;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnArrowImpact.Invoke();
        _impacted = true;
        Invoke("DestroyObject", 0.5f);
    }

    private void FixedUpdate()
    {
        if (!_impacted)
        {
            var arrowTransform = transform;
            arrowTransform.position -= arrowTransform.up * speed * Time.deltaTime;
        }
    }
}
