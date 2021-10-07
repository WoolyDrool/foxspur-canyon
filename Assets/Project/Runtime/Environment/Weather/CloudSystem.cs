using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSystem : MonoBehaviour
{
    public static CloudSystem instance;
    [Header("Spawn Controls")] 
    public GameObject[] clouds;
    public float spawnInterval;
    public int maxCloudCount = 70;

    #region Internal Variables
    
    //Collider
    private BoxCollider _boundingBox;
    
    //Variables
    public int currentCloudCount;
    
    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 _boxSize;
    private Vector3 _boxCenter;

    #endregion
    void Awake()
    {
        instance = this;
        
        _boundingBox = GetComponent<BoxCollider>();

        endPos = _boundingBox.size;

        _boxSize = _boundingBox.size;
        _boxCenter = _boundingBox.center;

        StartCoroutine(InitialSpawn());

    }

    IEnumerator InitialSpawn()
    {
        for(int i = 0; i < maxCloudCount; i++)
        {
            SpawnCloud();
        }
        
        InvokeRepeating("AttemptSpawn", 1, spawnInterval);
        yield break;

    }

    void SpawnCloud()
    {
        currentCloudCount++;
        GameObject cloud = Instantiate(clouds[UnityEngine.Random.Range(0, clouds.Length)], this.transform);
        cloud.transform.localScale = GetRandomScale(cloud.transform);
        cloud.transform.position = GetRandomPosition();
       
    }

    void AttemptSpawn()
    {
        if (currentCloudCount < maxCloudCount)
        {
            SpawnCloud();
        }
        else
        {
            return;
        }
        
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-_boxSize.x, _boxSize.x), Random.Range(transform.position.y, transform.position.y + 1000), Random.Range(-_boxSize.z, _boxSize.z));

        return _boxCenter + randomPosition;
    }

    private Vector3 GetRandomScale(Transform cloud)
    {
        Vector3 randomScale = new Vector3(Random.Range(cloud.localScale.x - 200, cloud.localScale.x + 600),
            Random.Range(cloud.localScale.y - 300, cloud.localScale.y),
            Random.Range(cloud.localScale.z, cloud.localScale.z + 550));
        return randomScale;
    }

}
