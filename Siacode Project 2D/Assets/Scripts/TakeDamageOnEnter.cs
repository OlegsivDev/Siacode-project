using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageOnEnter : MonoBehaviour
{
    [SerializeField] private int damageAmountPerSecond;

    private IEnumerator _coroutine;
    private Collider2D playerCol;
    private bool corutineStarted;

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = null;
        corutineStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        playerCol = col;
        _coroutine = takingDamage();
        StartCoroutine(takingDamage());
        Debug.Log("Spike entered");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopAllCoroutines();
        corutineStarted = false;
        _coroutine = null;
    }

    private IEnumerator takingDamage()
    {
        if (!corutineStarted)
        {
            while (true)
            {
                corutineStarted = true;
                Debug.Log("Damage get");
                playerCol.gameObject.GetComponent<PlayerHealth>().takeDamage(damageAmountPerSecond);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            yield break;
        }
    }
}