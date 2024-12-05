using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasGroup : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void DisableRaycasts() => canvasGroup.blocksRaycasts = false;

    public void EnableRaycasts() => canvasGroup.blocksRaycasts = true;
}
