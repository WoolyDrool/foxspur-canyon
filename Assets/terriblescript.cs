using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class terriblescript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!SceneManager.GetSceneByName("Scenes/Trails/WuliKupTrail").isLoaded)
            SceneManager.LoadScene("Scenes/Trails/WuliKupTrail", LoadSceneMode.Additive);
        
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
