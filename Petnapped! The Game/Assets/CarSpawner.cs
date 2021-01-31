using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] int spawnTimer;
    [SerializeField] GameObject car1, car2;
    [SerializeField] Transform carSpawn1, carSpawn2;
    private GameObject carClone1, carClone2;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CarSpawnTimer());
    }

    IEnumerator CarSpawnTimer()
    {
        int timer = Random.Range(3, 9);
        carClone1 = Instantiate(car1, carSpawn1.position, carSpawn1.rotation);
        carClone2 = Instantiate(car2, carSpawn2.position, carSpawn2.rotation);
        yield return new WaitForSeconds(timer);

        StartCoroutine(CarSpawnTimer());

    }
}
