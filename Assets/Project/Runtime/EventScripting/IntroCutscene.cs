using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    public float delay = 20.3f;
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    void Start()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
        StartCoroutine(Wait());
    }

    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }

        yield break;
    }
}
