using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePreload : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Preload());
        
        IEnumerator Preload()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadSceneAsync(1);
        }
    }

    void Update()
    {
        
    }
}
