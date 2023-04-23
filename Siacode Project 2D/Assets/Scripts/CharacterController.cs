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
    private Rigidbody2D _rb;
    public float rateOfFire;
    private bool _isShooting;
    public GameObject projectile;
    private Vector2 _direction;
    public float lastZMovingAngle;
    public float projectileSpawnDistance;
    public Animator animator;
    public Camera camera;
    private Vector3 _toMouseVector;
    private float _toMouseZRotation;

    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isShooting = false;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.y = Input.GetAxis("Vertical");
            _rb.velocity = (_direction * speed * Time.fixedDeltaTime);
            lastZMovingAngle = Mathf.Atan2(_direction.x, -1 * _direction.y) * Mathf.Rad2Deg;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && !_isShooting)
        {
            StartCoroutine(Shooting());
        }
    }

    private IEnumerator Shooting()
    {
        _isShooting = true;
        
        // Uncomment to make shooting in moving direction 
        // Instantiate(projectile, transform.position + new Vector3(_direction.x, _direction.y, 0).normalized * projectileSpawnDistance, Quaternion.Euler(0,0, lastZMovingAngle));
        
        // Shooting to mouse direction
        GetMouseWorldPosition();
        Instantiate(projectile, transform.position + _toMouseVector.normalized * projectileSpawnDistance, Quaternion.Euler(0,0, _toMouseZRotation));
        yield return new WaitForSeconds(60 / rateOfFire);
        _isShooting = false;
    }

    private void GetMouseWorldPosition()
    {
        Vector3 worldMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        _toMouseVector = worldMousePosition - transform.position;
        _toMouseVector.z = 0;
        _toMouseZRotation = Mathf.Atan2(_toMouseVector.x, -1 * _toMouseVector.y) * Mathf.Rad2Deg;
    }
}