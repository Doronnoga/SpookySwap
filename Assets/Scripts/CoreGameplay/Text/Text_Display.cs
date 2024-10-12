using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace TextDisplayClass 
{ 
  public class Text_Display : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    private TMP_Text uiText;
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
    [SerializeField]
    private string playerTag;

    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<TMP_Text>();
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

    void OnTriggerEnter2D(Collider2D other) //player colliding with trigger
    {
        if (other.gameObject.tag == playerTag)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //player exit from trigger
    {
        if (other.gameObject.tag == playerTag)
        {
            isColliding = false;
        }
    }

}
}
