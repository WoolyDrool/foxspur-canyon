using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuildState
{
    DEV,
    DEMO,
    ALPHA,
    BETA,
    RELEASE,
    PATCH
}

[CreateAssetMenu(fileName = "GameData", menuName = "New GameData")]

public class GameData : ScriptableObject
{
    [Header("Game Version")] 
    public BuildState state;
    public int majorVersion;
    public int minorVersion;
    public int patch;
    public int buildVersion;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
