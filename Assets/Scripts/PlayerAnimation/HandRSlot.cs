# nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRSlot : MonoBehaviour
{
    GameObject playerModel;

    private void Awake()
    {
        playerModel = transform.root.GetChild(0).gameObject;
    }

    public bool AddWeapon(string name)
    {
        DropCurrentWeapon();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(transform.GetChild(i).name == name);
        }

        return true;
    }

    public void DropCurrentWeapon()
    {
        GameObject? currentWeapon = GetCurrentWeaponObj();
        if (currentWeapon == null) return;

        string path = $"Prefabs/{currentWeapon.name}";
        GameObject prefanObj = Resources.Load<GameObject>(path);
        Vector3 position = playerModel.transform.position + playerModel.transform.forward * 1 + new Vector3(0, 1.5f, 0);
        Quaternion rotation = Quaternion.Euler(5, playerModel.transform.rotation.eulerAngles.y + 90, 90);

        Instantiate(prefanObj, position, rotation);
    }

    public GameObject? GetCurrentWeaponObj()
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

    public IWeapon? GetCurrentIWeapon()
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
