using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Gender Choice")]
public class GenderChoice : ScriptableObject
{
    [Header("Sounds")] 
    public AudioClip[] jumpSounds;
    public AudioClip[] yawnSounds;
    public AudioClip[] damageSounds;
    public AudioClip[] breathingSounds;
    public AudioClip deathSound;

    [Header("Prefixes")] 
    public string formalPrefix;
    public string possessivePronoun;
    public string identityPronoun;
}
