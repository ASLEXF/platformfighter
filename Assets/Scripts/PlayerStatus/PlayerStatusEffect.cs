using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.XR;

public class PlayerStatusEffect : MonoBehaviour
{
    PlayerController controller;
    Animator animator;

    public LifeStatusEnum lifeStatus;
    [SerializeField] List<DebuffEnum> debuffEnums;
    [SerializeField] List<BuffEnum> buffEnums;

    public bool Stunned
    {
        get
        {
            return HasDebuff(DebuffEnum.Stunned);
        }
        set
        {
            if (value)
            {
                debuffEnums.Add(DebuffEnum.Stunned);
            }
            else
            {
                debuffEnums.Remove(DebuffEnum.Stunned);
            }

        }
    }

    Coroutine frozenCotoutine;
    public bool Frozen
    {
        get
        {
            return HasDebuff(DebuffEnum.Frozen);
        }
        set
        {
            if (value)
            {
                frozenCotoutine = StartCoroutine(frozen());
            }
            else
            {
                StopCoroutine(frozenCotoutine);
                debuffEnums.Remove(DebuffEnum.Frozen);
            }

        }
    }

    private void Awake()
    {
        controller = transform.parent.Find("ControlPoint").GetComponent<PlayerController>();
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        lifeStatus = LifeStatusEnum.Alive;
        debuffEnums = new List<DebuffEnum>();
        buffEnums = new List<BuffEnum>();
    }

    private IEnumerator frozen()
    {
        debuffEnums.Add(DebuffEnum.Frozen);
        animator.speed = 0;
        yield return new WaitForSeconds(3.0f);  // TODO: optimize here
        debuffEnums.Remove(DebuffEnum.Frozen);
        animator.speed = 1;
    }

    private bool HasDebuff(DebuffEnum debuff)
    {
        foreach (DebuffEnum debuffEnum in debuffEnums)
        {
            if (debuffEnum == debuff) return true;
        }
        return false;
    }
}
