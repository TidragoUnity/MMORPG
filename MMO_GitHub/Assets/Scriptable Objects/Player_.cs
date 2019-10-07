using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    public InventoryObject inventory;
    public int playerInvSlots;
    private void Start()
    {
        inventory.maxInvSlots = playerInvSlots;
    }
    public ItemObject removeItem;
    public int removeInt;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("RemoveItem");
            inventory.RemoveItem(removeItem, removeInt);
            return;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        var item = other.GetComponent<Item_>();
        if (item)
        {
            if(inventory.Container.Count < inventory.maxInvSlots)
            {
                inventory.AddItem(item.item, 1);
                Destroy(other.gameObject);
            }

        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


}
