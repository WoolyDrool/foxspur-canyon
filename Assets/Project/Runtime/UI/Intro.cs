using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Global;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject[] textElements;
    public Animator fadeAnimator;
    [SerializeField] private int index;
    public GameObject continueButton;
    void Start()
    {
        fadeAnimator.Play("anim_Main_FadeIn");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && index < textElements.Length-1)
        {
            index++;
            Mathf.Clamp(index, 0, 11);
        }

        textElements[index].SetActive(true);

        if (index == 7)
        {
            textElements[0].SetActive(false);
            textElements[7].SetActive(true);
        }
    }

    public void Continue()
    {
        StartCoroutine(Fade());
        continueButton.SetActive(false);
    }

    IEnumerator Fade()
    {
        Debug.Log("yep");
        yield return new WaitForSeconds(0);
        fadeAnimator.gameObject.SetActive(true);
        fadeAnimator.Play("anim_Main_FadeOut");
        yield return new WaitForSeconds(7);
        FindObjectOfType<SceneLoadingManager>().LoadScene("MainGameWorld");
    }
}
