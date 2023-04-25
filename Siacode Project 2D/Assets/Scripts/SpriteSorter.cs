using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    
    private int sortingOrderBase = 0;
    private Renderer renderer;
    public float offset = 0;
    public bool isStatic = false;
    void Start()
    {

    }
    private void Awake()
    {
        renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        renderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);

        if (isStatic)
            Destroy(this);
    }
}

