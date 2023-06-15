using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public AudioSource runningSound;
    private PlayerInput playerInput;
    private PlayerInputActions _playerInputActions;
    private Coroutine _shootingCoroutine;

    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Shoot.performed += OnShootActionStarted;
        _playerInputActions.Player.Shoot.canceled += OnShootActionEnded;
        // playerInputActions.Player.Move.performed += Move;
        _playerInputActions.Player.Enable();

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
        // TODO: FIX ENTER IN MAIN MENU
    }

    private void Move()
    {
        _direction = _playerInputActions.Player.Move.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", _direction.x);
        animator.SetFloat("Vertical", _direction.y);
        animator.SetFloat("Speed", _direction.sqrMagnitude);
        if (_direction.x != 0 || _direction.y != 0)
        {
            _rb.velocity = (_direction * speed * Time.fixedDeltaTime);
            lastZMovingAngle = Mathf.Atan2(_direction.x, -1 * _direction.y) * Mathf.Rad2Deg;

            if (!runningSound.isPlaying)
            {
                runningSound.Play();
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
            if (runningSound.isPlaying)
            {
                runningSound.Stop();
            }
        }
    }

    private void OnShootActionStarted(InputAction.CallbackContext context)
    {
        // Start the shooting coroutine
        _shootingCoroutine = StartCoroutine(Shooting());
    }
    
    private void OnShootActionEnded(InputAction.CallbackContext context)
    {
        // Stop the shooting coroutine
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
        }
    }

    private IEnumerator Shooting()
    {
        {
            while (true)
            {
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
            }
        }
        // Uncomment to make shooting in moving direction 
        // Instantiate(projectile, transform.position + new Vector3(_direction.x, _direction.y, 0).normalized * projectileSpawnDistance, Quaternion.Euler(0,0, lastZMovingAngle));
        
        // Shooting to mouse direction
        
        // if (useBuiltInObjectPooling)
        // {
        //     Pool.Get();
        // }
        // else
        // {
        //     Instantiate(projectile, transform.position + _toMouseVector.normalized * projectileSpawnDistance, Quaternion.Euler(0,0, _toMouseZRotation));
        // }
        // yield return new WaitForSeconds(60 / rateOfFire);
        // _isShooting = false;
    }

    private void GetMouseWorldPosition()
    {
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _toMouseVector = worldMousePosition - transform.position;
        _toMouseVector.z = 0;
        _toMouseZRotation = Mathf.Atan2(_toMouseVector.x, -1 * _toMouseVector.y) * Mathf.Rad2Deg;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}