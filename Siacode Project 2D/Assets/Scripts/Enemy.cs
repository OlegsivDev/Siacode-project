using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movingSpeed;
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector3.MoveTowards(transform.position,  GameObject.Find("Girl").transform.position, movingSpeed * Time.deltaTime));
    }

    public void getHit()
    {
        hp -= 1;
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Projectile"))
        {
            getHit();
            Destroy(col.gameObject);
        }
    }
}
