using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator spawnEnemy()
    {
        while (true)
        {
            Instantiate(enemy, new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(5);
        }
    }
}
