using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickRefer : MonoBehaviour
{
    public HandLSlot handLSlot;
    public HandRSlot handRSlot;

    public void Initialize()
    {
        handLSlot = GetComponentInChildren<HandLSlot>();
        handRSlot = GetComponentInChildren<HandRSlot>();
    }
}
