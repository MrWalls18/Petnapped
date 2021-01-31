using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float carSpeed = 0.11f;

    private void Start()
    {
        StartCoroutine(DestroyCar());
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.position = transform.position + new Vector3(0f, 0f, carSpeed * Time.deltaTime);
    }

    IEnumerator DestroyCar()
    {
        yield return new WaitForSeconds(20);

        Destroy(this.gameObject);
    }
}
