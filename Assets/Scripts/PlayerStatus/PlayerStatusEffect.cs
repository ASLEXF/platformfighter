using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerStatusEffect : MonoBehaviour
{
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

    public bool Poisoned
    {
        get
        {
            return HasDebuff(DebuffEnum.Poisoned);
        }
        set
        {
            if (value)
            {
                debuffEnums.Add(DebuffEnum.Poisoned);
            }
            else
            {
                debuffEnums.Remove(DebuffEnum.Poisoned);
            }

        }
    }

    private void Start()
    {
        lifeStatus = LifeStatusEnum.Alive;
        debuffEnums = new List<DebuffEnum>();
        buffEnums = new List<BuffEnum>();
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
