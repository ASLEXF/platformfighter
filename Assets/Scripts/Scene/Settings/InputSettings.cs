using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSettings : MonoBehaviour
{
    CanvasGroup canvasGroup;
    GameObject arrowBox;

    private void Awake()
    {
        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        arrowBox = GameObject.Find("ArrowBox").gameObject;
    }

    private void Start()
    {
        RebindManager.Instance.canvasGroup = this.canvasGroup;
        RebindManager.Instance.arrowBox = this.arrowBox;

        arrowBox.SetActive(false);
    }
}
