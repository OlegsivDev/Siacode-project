using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 mousePos;
    public float cameraDistance;
    public float cameraMinDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        // TODO: FIX THIS
        //MoveCameraToPlayer();
    }

    private void MoveCameraToPlayer()
    {
        GetMouseInputOnScreen();
        if ((mousePos - player.transform.position).magnitude > cameraMinDistance)
        {
            transform.position = player.transform.position +
                (mousePos - player.transform.position).normalized *
                Mathf.Clamp((mousePos - player.transform.position).magnitude, 0, cameraDistance);
        } else
        {
            transform.position = player.transform.position;
        }
    }

    private void GetMouseInputOnScreen()
    {
        mousePos = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -8);
    }
}
