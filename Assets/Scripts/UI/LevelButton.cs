using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform ChosenIcon;
    Animator animator;

    private void Start()
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
        animator.Play("title_show_up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("title_hide_up");
    }
}
