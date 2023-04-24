using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    private int sortingOrderBase = 0;
    private Renderer renderer;
    private float offset = -0.4f;
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
