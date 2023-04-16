using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCameraToPlayer : MonoBehaviour
{
    public Vector3 defaultPositionOffset;
    public Object player;
    private Transform _playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = player.GetComponent<Transform>();
        defaultPositionOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _playerTransform.position - defaultPositionOffset;
    }
}
