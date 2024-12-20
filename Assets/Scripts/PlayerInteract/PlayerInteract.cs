# nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    Animator animator = null!;
    GameObject charactorObj = null!;
    HandLSlot handLSlot = null!;
    HandRSlot handRSlot = null!;

    [SerializeField] List<DropItem> dropItems = null!;
    [SerializeField] DropItem? currentItem;

    bool isDropItemsUpdated = true;

    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        charactorObj = transform.parent.gameObject;
    }

    public void Initialize()
    {
        handLSlot = GetComponentInParent<QuickRefer>().handLSlot;
        handRSlot = GetComponentInParent<QuickRefer>().handRSlot;
    }

    private void Start()
    {
        GameEvents.Instance.OnItemDropped += UpdateDropItems;
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

    DropItem? findClosestItem()
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

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (currentItem != null)
        {
            // pick up item
            animator.SetTrigger("Interact");
            switch(currentItem.itemType)
            {
                case ItemType.comsumable:
                    break;
                case ItemType.knightWeapon:
                    if (charactorObj.name.StartsWith("Knight"))
                    {
                        if(handRSlot.AddWeaponKnight(currentItem.itemName))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }
                    else if (charactorObj.name.StartsWith("Barbarian"))
                    {
                        if(handLSlot.AddItem(currentItem.itemName))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                case ItemType.knightItem:
                    if (charactorObj.name.StartsWith("Knight"))
                    {
                        if(handLSlot.AddItem(currentItem.itemName))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }
                    else if (charactorObj.name.StartsWith("Barbarian"))
                    {
                        if (handLSlot.AddItem(currentItem.itemName))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                case ItemType.barbarianWeapon:
                    if (charactorObj.name.StartsWith("Barbarian"))
                    {
                        if (handRSlot.AddWeaponBarbarian(currentItem.itemName))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }
                    else if (charactorObj.name.StartsWith("Knight"))
                    {
                        if (handLSlot.AddItem(currentItem.itemName))
                        {
                            Destroy(currentItem.gameObject);
                            dropItems.Remove(currentItem);
                        }
                    }

                    break;
                default:
                    Debug.LogWarning($"unhandled item '{currentItem}'");
                    break;
            }
        }
        else if (handLSlot.GetCurrentItemObj()?.CompareTag("Item") ?? false)
        {
            // drop item
            handLSlot.DropCurrentItem();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log($"{collider.transform.root.gameObject.name}");
        if (collider.gameObject.CompareTag("Item"))
        {
            DropItem? dropItem = collider.gameObject.GetComponent<DropItem>();
            if (!(dropItem?.isThrown ?? true))
                dropItems.Add(dropItem!);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        // TODO: need test
        // handle drop items that are thrown into interact range
        if (isDropItemsUpdated) return;

        if (collider.gameObject.CompareTag("Item"))
        {
            DropItem? dropItem = collider.gameObject.GetComponent<DropItem>();
            if (!(dropItem?.isThrown ?? true) && !dropItems.Contains(dropItem))
                dropItems.Add(dropItem!);
        }

        isDropItemsUpdated = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            DropItem? dropItem = collider.gameObject.GetComponent<DropItem>();
            dropItem.SetOutLineOff();
            dropItems.Remove(dropItem);
        }
    }

    private void UpdateDropItems()
    {
        isDropItemsUpdated = false;
    }
}
