using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFade : MonoBehaviour
{
    public AudioSource source;
    public Type fadeType;
    public enum Type {FadeIn, FadeOut}

    public float fadeDuration;
    private float startingVolume;

    public GeneralFade(Type type, float _duration, AudioSource _source)
    {
        fadeDuration = _duration;
        source = _source;

        switch (fadeType)
        {
            case Type.FadeIn:
            {
                break;
            }

            case Type.FadeOut:
            {
                break;
            }
        }
    }
}
