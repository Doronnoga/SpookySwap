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
    [SerializeField]
    private float lerpDuration = 0.5f;

    void Start()
    {
        ui.OnEsc += ToggleUI;
    }

    private void ToggleUI()
    {
        if (isOpen)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, lerpDuration)); // Fade out
            closeUI();
        }
        else
        {
            openUI();
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, lerpDuration)); // Fade in
        }
    }

    private void openUI()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isOpen = true;
    }

    private void closeUI()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        isOpen = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvas, float start, float end, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            canvas.alpha = Mathf.Lerp(start, end, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = end;
    }
}
