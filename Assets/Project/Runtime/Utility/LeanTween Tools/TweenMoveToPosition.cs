using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class TweenMoveToPosition : MonoBehaviour
{
    public Transform positionToMoveTo;
    public LTBezierPath path;
    public float speed;
    private LTDescr _tweenObject;
    void Start()
    {
        _tweenObject = LeanTween.move(gameObject, positionToMoveTo.position, speed);
        _tweenObject.destroyOnComplete = true;
    }

    void Update()
    {
        
    }
}
