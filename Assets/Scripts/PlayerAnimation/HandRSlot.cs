using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRSlot : MonoBehaviour
{

    public bool AddWeapon(string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(transform.GetChild(i).name == name);
        }

        return true;
    }

    public GameObject GetCurrentWeaponObj()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return transform.GetChild(i).gameObject;
            }
        }
        //Debug.LogWarning("weapon not found");
        return null;
    }

    public IWeapon GetCurrentIWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return transform.GetChild(i).gameObject.GetComponent<IWeapon>();
            }
        }
        //Debug.LogWarning("weapon not found");
        return null;
    }
}
