using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    GameObject charactorObj;
    [SerializeField] HandLSlot handLSlot;
    [SerializeField] HandRSlot handRSlot;

    [SerializeField] List<DropItem> dropItems;
    [SerializeField] DropItem currentItem;

    private void Awake()
    {
        charactorObj = transform.parent.gameObject;
    }

    private void Start()
    {
        //Physics.IgnoreCollision()
    }

    private void Update()
    {
        // sort

        currentItem = findClosestItem();

        setDropItemsOutline();
    }

    void sortDropItems()
    {

    }

    DropItem findClosestItem()
    {
        if (dropItems.Count == 0) return null;
        if (dropItems.Count == 1) return dropItems[0];

        DropItem current = dropItems[0];
        for (int i = 1; i < dropItems.Count; i++)
        {
            if (
                Vector3.Distance(gameObject.transform.position, dropItems[i].gameObject.transform.position) 
                < Vector3.Distance(gameObject.transform.position, current.gameObject.transform.position)
                )
            {
                current = dropItems[i];
            }
        }

        return current;
    }

    void setDropItemsOutline()
    {
        foreach (DropItem dropItem in dropItems)
        {
            if (dropItem != currentItem)
            {
                dropItem.SetOutLineOff();
            }
            else
            {
                dropItem.SetOutLineOn();
            }
        }
    }

    public void Interact()
    {
        if (currentItem != null)
        {
            switch(currentItem.itemType)
            {
                case ItemType.comsumable:
                {
                    break;
                }
                case ItemType.knightWeapon:
                {
                    if (charactorObj.name == "Knight")
                    {
                        if(handRSlot.AddWeapon(currentItem.gameObject.name))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                }
                case ItemType.knightItem:
                {
                    if (charactorObj.name == "Knight")
                    {
                        if(handLSlot.AddItem(currentItem.gameObject.name))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                }
                case ItemType.barbarianWeapon:
                {
                    if (charactorObj.name == "Barbarian")
                    {
                        if (handRSlot.AddWeapon(currentItem.gameObject.name))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                }
                case ItemType.barbarianItem:
                {
                    if (charactorObj.name == "Barbarian")
                    {
                        if (handLSlot.AddItem(currentItem.gameObject.name))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"{collider.transform.root.gameObject.name}");
        if (collider.gameObject.CompareTag("Item"))
        {
            DropItem dropItem = collider.gameObject.GetComponent<DropItem>();
            dropItems.Add(dropItem);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            DropItem dropItem = collider.gameObject.GetComponent<DropItem>();
            dropItem.SetOutLineOff();
            dropItems.Remove(dropItem);
        }
    }
}
