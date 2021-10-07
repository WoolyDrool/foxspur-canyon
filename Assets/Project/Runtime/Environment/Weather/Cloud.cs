using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cloud : MonoBehaviour
{
    private float spawnSpeed;
    public Vector3 endPosition;
    void Start()
    {
        spawnSpeed = Random.Range(25, 100);
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * spawnSpeed);

        if (transform.position.x < endPosition.x)
        {
            Destroy(this);
            CloudSystem.instance.currentCloudCount--;
        }
        
    }
}
