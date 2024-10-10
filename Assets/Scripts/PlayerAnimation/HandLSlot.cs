using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLSlot : MonoBehaviour
{
    public bool AddItem(string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(transform.GetChild(i).name == name);
        }

        return true;
    }

    public GameObject GetCurrentItemObj()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return transform.GetChild(i).gameObject;
            }
        }
        
        return null;
    }

    public IItem GetCurrentIItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return transform.GetChild(i).gameObject.GetComponent<IItem>();
            }
        }
        
        return null;
    }
}
