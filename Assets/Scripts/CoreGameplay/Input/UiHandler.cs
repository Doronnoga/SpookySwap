using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Collections;
using uiInputScript;
using UnityEngine;

public class UiHandler : MonoBehaviour
{

    [SerializeField]
    private bool isOpen = false;
    [SerializeField]
    private UiInput ui;
    [SerializeField]
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        ui.OnEsc += openUI;
    }

    private void openUI() 
    {
        Debug.Log("Esc, open ui");
    }
    // Update is called once per frame
    void Update()
    {
        if (isOpen) 
        {
            
        }
    }
}
