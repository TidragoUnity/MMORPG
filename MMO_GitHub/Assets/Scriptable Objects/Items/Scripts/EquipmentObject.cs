using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Item/Equipment")]
public class EquipmentObject : ItemObject
{
    public int attBonus;
    public int defenceBonus;
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
