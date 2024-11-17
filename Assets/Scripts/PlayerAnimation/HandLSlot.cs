# nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLSlot : MonoBehaviour
{
    GameObject playerModel;

    private void Awake()
    {
        playerModel = transform.GetComponentInParent<QuickRefer>().gameObject.transform.GetChild(0).gameObject;
    }

    public bool AddItem(string name)
    {
        DropCurrentItem();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(transform.GetChild(i).name == name);
        }

        return true;
    }

    public bool AddWeaponLBarbarian(string name)
    {
        DropCurrentItem();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(transform.GetChild(i).name == name);
        }

        return true;
    }

    public void DropCurrentItem()
    {
        GameObject? currentItem = GetCurrentItemObj();
        if (currentItem == null) return;

        currentItem.SetActive(false);
        string path = $"Prefabs/Items/{currentItem.name}";
        GameObject prefanObj = Resources.Load<GameObject>(path);
        Vector3 position = playerModel.transform.position + playerModel.transform.forward * 1 + new Vector3(0, 1.5f, 0);
        Quaternion rotation = Quaternion.Euler(5, playerModel.transform.rotation.eulerAngles.y + 90, 90);

        Instantiate(prefanObj, position, rotation);
    }

    public GameObject? GetCurrentItemObj()
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

    public IItem? GetCurrentIItem()
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
