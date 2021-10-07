using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLog : MonoBehaviour
{
    public float logHealth = 150;
    public GameObject logSpawn;
    public AudioClip impactSound;
    public AudioClip destroySound;
    private AudioSource _source;
    public GameObject model;
    public BoxCollider hitBox;
    public Transform logSpawn1;
    public Transform logSpawn2;
    public Transform logSpawn3;

    private float _randPitch;
    
    void Start()
    {
        _source = GetComponent<AudioSource>();
        hitBox = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _randPitch = UnityEngine.Random.Range(0.85f, 1f);
        logHealth -= damage;
        _source.PlayOneShot(impactSound);
        _source.pitch = _randPitch;
        
        if(damage > logHealth)
            DestroyLog();
    }

    public void DestroyLog()
    {
        _source.pitch = 1;
        _source.PlayOneShot(destroySound);
        model.SetActive(false);
        hitBox.enabled = false;
        GameObject clone;
        clone = Instantiate(logSpawn, new Vector3(logSpawn1.position.x, logSpawn1.position.y, logSpawn1.position.z), transform.rotation);
        clone = Instantiate(logSpawn, new Vector3(logSpawn2.position.x, logSpawn2.position.y, logSpawn2.position.z),
            transform.rotation);
        clone = Instantiate(logSpawn, new Vector3(logSpawn3.position.x, logSpawn3.position.y, logSpawn3.position.z),
            transform.rotation);
        Destroy(gameObject, 4f);
    }
}
