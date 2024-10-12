using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Text_Display : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    private Text uiText;
    [Header("Colors")]
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color endColor;
    [Header("Time")]
    [SerializeField]
    private float fadeTime = 0.1f;
    [SerializeField]
    private float duration = 0.5f;

    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        uiText.color = startColor;
    }

    private void Update()
    {
        if (isColliding)
        {
            fadeTime += Time.deltaTime / duration;
            uiText.color = Color.Lerp(startColor, endColor, fadeTime); //changes color over time
            if (fadeTime >= 1f)
            {
                fadeTime = 1f;
                uiText.color = endColor;
            }
        }

        else
        {
            fadeTime -= Time.deltaTime / duration;
            uiText.color = Color.Lerp(startColor, endColor, fadeTime); //changes color over time
            if (fadeTime <= 0f)
            {
                fadeTime = 0;
                uiText.color = startColor;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D ghost) //player colliding with trigger
    {
        if (ghost.gameObject.tag == "Ghost")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D ghost) //player exit from trigger
    {
        if (ghost.gameObject.tag == "Ghost")
        {
            isColliding = false;
        }
    }

}
