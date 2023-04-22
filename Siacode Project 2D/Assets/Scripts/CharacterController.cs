using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float rateOfFire;
    private bool _isShooting;
    public GameObject projectile;
    public Vector2 _transition;
    public float lastZMovingAngle;
    public float projectileSpawnDistance;

    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _isShooting = false;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        shoot();
    }

    private void Move()
    {
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _transition.x = Input.GetAxis("Horizontal");
            _transition.y = Input.GetAxis("Vertical");
            rb.velocity = (_transition * speed * Time.fixedDeltaTime);
            lastZMovingAngle = Mathf.Atan2(_transition.x, -1 * Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void shoot()
    {
        if (Input.GetKey(KeyCode.Space) && !_isShooting)
        {
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        _isShooting = true;
        Instantiate(projectile, transform.position + new Vector3(_transition.x, _transition.y, 0).normalized * projectileSpawnDistance, Quaternion.Euler(0,0, lastZMovingAngle));
        yield return new WaitForSeconds(60 / rateOfFire);
        _isShooting = false;
    }
}