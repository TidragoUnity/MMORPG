using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public int maxInvSlots = 10;

    public List<InventorySlot> Container = new List<InventorySlot>();
    public ItemObject removeItem;
    public int removeAmount;

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
   
    }
    public void RemoveItem(ItemObject _item, int _amount)
    {
        int i = 0;
        foreach (var container in Container)
        {

            if(container.item == _item)
            {
                if(container.amount > _amount)
                {
                    container.RemoveAmount(_amount);
                    return;
                }
                else if(container.amount == _amount)
                {
                    Container.RemoveAt(i);
                    return;
                }
                return;
            }
            i++;
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
