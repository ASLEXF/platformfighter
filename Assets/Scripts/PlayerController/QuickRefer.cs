using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickRefer : MonoBehaviour
{
    public HandLSlot handLSlot;
    public HandRSlot handRSlot;

    private void Awake()
    {
        handLSlot = GetComponentInChildren<HandLSlot>();
        handRSlot = GetComponentInChildren<HandRSlot>();
    }
}
