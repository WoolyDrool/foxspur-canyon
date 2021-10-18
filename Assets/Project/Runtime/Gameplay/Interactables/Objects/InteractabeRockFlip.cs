using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractabeRockFlip : MonoBehaviour
{
    public Animator flipAnimation;
    public Interactable inter;
    public Collider col;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnFlip()
    {
        inter.enabled = false;
        col.enabled = false;
        flipAnimation.Play("anim_obj_rockFlip");
    }
}
