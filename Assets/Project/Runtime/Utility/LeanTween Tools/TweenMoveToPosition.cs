using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TweenMoveToPosition : MonoBehaviour
{
    public Transform positionToMoveTo;
    public LTBezierPath path;
    public float speed;
    private LTDescr _tweenObject;
    public Vector3 rotation;
    public bool startImmediately;
    public bool destroyOnComplete = true;
    void Start()
    {
        if(startImmediately)
            DoTween();
    }

    void Update()
    {
        
    }

    public void DoTween()
    {
        _tweenObject = LeanTween.move(gameObject, positionToMoveTo.position, speed);
        _tweenObject.destroyOnComplete = destroyOnComplete;
    }
}
