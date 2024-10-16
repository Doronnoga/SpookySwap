using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Collections;
using uiInputScript;
using UnityEngine;

public class UiHandler : MonoBehaviour
{

    [SerializeField]
    private bool isOpen = true;
    [SerializeField]
    private UiInput ui;
    [SerializeField]
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        ui.OnEsc += ToggleUI;
    }
    private void ToggleUI() 
    {
        if (isOpen) 
        { 
            closeUI();
        }
        else 
        { 
            openUI();
        }
    }

    private void openUI()
    {
        Debug.Log("Esc, open ui");
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isOpen = true;
    }
    private void closeUI()
    {
        Debug.Log("Esc, close ui");
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        isOpen = false;
    }
}
    
