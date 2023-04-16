using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;

    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardTranslation = Input.GetAxis("Vertical");
        float horizontalTranslation = Input.GetAxis("Horizontal");
        Vector3 translation = new Vector3(horizontalTranslation, 0, forwardTranslation);
        transform.Translate(translation * speed * Time.deltaTime,Space.World);
        Vector3 rotationVector = GetToMouseVector();
        rotationVector.y = transform.position.y;
        transform.LookAt(rotationVector);
     
        // To move towards mouse
        // Vector3 translationTwo = GetToMouseVector() - transform.position;
        // transform.Translate(translationTwo * speed * Time.deltaTime);
    }

    private Vector3 GetToMouseVector()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            return target;
        }

        return new Vector3(0, 0, 0);
    }
}
