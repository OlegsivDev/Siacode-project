using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class CharacterController : MonoBehaviour
{
    public bool useBuiltInObjectPooling;
    public float speed;
    private Rigidbody2D _rb;
    public float rateOfFire;
    private bool _isShooting;
    public GameObject projectile;
    private Vector2 _direction;
    public float lastZMovingAngle;
    public float projectileSpawnDistance;
    public Animator animator;
    public Camera mainCamera;
    private Vector3 _toMouseVector;
    private float _toMouseZRotation;
    public GameObject bow;
    private Vector3 _bowPosition;
    public ObjectPool<GameObject> Pool;
    public int builtInPoolingCapacity;
    public int builtInPoolingMaxCapacity;
        
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isShooting = false;
        _bowPosition = bow.transform.position;

        if (useBuiltInObjectPooling)
        {
            Pool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(projectile, transform.position + _toMouseVector.normalized * projectileSpawnDistance, Quaternion.Euler(0,0, _toMouseZRotation));
            }, obj =>
            {
                GetMouseWorldPosition();
                obj.transform.position = transform.position + _toMouseVector.normalized * projectileSpawnDistance;
                obj.transform.rotation = Quaternion.Euler(0, 0, _toMouseZRotation);
                obj.gameObject.SetActive(true);
            }, obj =>
            {
                obj.gameObject.SetActive(false);
            }, obj =>
            {
                Destroy(obj.gameObject);
            }, false, builtInPoolingCapacity, builtInPoolingMaxCapacity);
        }
    }

    void Update()
    {

       
    }

    private void FixedUpdate()
    {
        Move();
        Shoot();
        // TODO: FIX ENTER IN MAIN MENU
        // CheckExitToMenu();
    }

    private void Move()
    {

        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.y = Input.GetAxisRaw("Vertical");
            _rb.velocity = (_direction * speed * Time.fixedDeltaTime);
            lastZMovingAngle = Mathf.Atan2(_direction.x, -1 * _direction.y) * Mathf.Rad2Deg;

            animator.SetFloat("Horizontal", _direction.x);
            animator.SetFloat("Vertical", _direction.y);
            animator.SetFloat("Speed", _direction.sqrMagnitude);

        }
        else
        {
            _rb.velocity = Vector2.zero;
        }


    }

    private void Shoot()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && !_isShooting)
        {
            StartCoroutine(Shooting());
            //if (_toMouseZRotation > 0)
            //{
            //    bow.transform.rotation = Quaternion.Euler(0, 0, 90);
            //    Vector3 newPosition = _bowPosition;
            //    _bowPosition.x = 0.1f;
            //    bow.transform.position = _bowPosition;
            //} else
            //{
            //    bow.transform.rotation = Quaternion.Euler(0, 0, -90);
            //    Vector3 newPosition = _bowPosition;
            //    _bowPosition.x = -0.1f;
            //    bow.transform.position = _bowPosition;
            //}
            //bow.gameObject.SetActive(true);
        } else
        {
            //bow.gameObject.SetActive(false);
        }
    }

    private IEnumerator Shooting()
    {
        _isShooting = true;
        
        // Uncomment to make shooting in moving direction 
        // Instantiate(projectile, transform.position + new Vector3(_direction.x, _direction.y, 0).normalized * projectileSpawnDistance, Quaternion.Euler(0,0, lastZMovingAngle));
        
        // Shooting to mouse direction
        GetMouseWorldPosition();
        if (useBuiltInObjectPooling)
        {
            Pool.Get();
        }
        else
        {
            Instantiate(projectile, transform.position + _toMouseVector.normalized * projectileSpawnDistance, Quaternion.Euler(0,0, _toMouseZRotation));
        }
        yield return new WaitForSeconds(60 / rateOfFire);
        _isShooting = false;
    }

    private void GetMouseWorldPosition()
    {
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _toMouseVector = worldMousePosition - transform.position;
        _toMouseVector.z = 0;
        _toMouseZRotation = Mathf.Atan2(_toMouseVector.x, -1 * _toMouseVector.y) * Mathf.Rad2Deg;
    }

    private void CheckExitToMenu()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        };
    }
}