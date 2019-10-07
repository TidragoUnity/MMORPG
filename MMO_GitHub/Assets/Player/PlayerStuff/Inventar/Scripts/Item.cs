using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName ="Inventory/Items/Default")]
public class Item : ScriptableObject
{
    [SerializeField]
    protected string Name;
    [SerializeField]
    protected string Type;
    [SerializeField]
    protected int Amount;
    [SerializeField]
    protected Sprite icon;



}
