using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types
{
    In,
    Out
}
public class ObjectFader : MonoBehaviour
{
    public GameObject objectToAnimate;
    
    public Types animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public Material material;
    
    public Vector2 from;
    public Vector2 to;
    
    private LTDescr _tweenObject;

    void Start()
    {
        HandleTween();
    }

    void Update()
    {
        
    }

    void HandleTween()
    {
        if (objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }

        switch (animationType)
        {
            case Types.In:
            {
                FadeIn();
                break;
            }
            case Types.Out:
            {
                FadeOut();
                break;
            }
        }
    }

    void FadeIn()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        //material.color = new Color(material.color.r, material.color.g, material.color.b, from.x);
        
        LeanTween.alpha(objectToAnimate, to.x, duration);
    }

    void FadeOut()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }


        objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;

        _tweenObject = LeanTween.alphaCanvas(objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);
        //StartCoroutine(DisableAfterDelay(delay + duration));
    }
}
