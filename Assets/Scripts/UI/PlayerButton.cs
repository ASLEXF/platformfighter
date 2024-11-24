using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform ChosenIcon;
    Animator animator;

    private void Awake()
    {
        ChosenIcon = transform.Find("ChosenIcon");
        animator = GetComponent<Animator>();
    }

    public void SetSelected()
    {
        ChosenIcon.gameObject.SetActive(true);
    }

    public void SetUnselected()
    {
        ChosenIcon.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("unfold");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("fold");
    }
}
