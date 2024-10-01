using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Item //: MonoBehaviour
{

    public string itemName, itemDesc, itemImagePath;
    public int itemId, itemValue, itemMaxStack, itemDamage, itemDefence, itemConsume;
    public Sprite itemImage;
    //public GameObject itemModel;      //3D item drop model
    public ItemType itemType;

    private Sprite itemSprite;

    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Empty
    }

    public Item(string name, string desc, int id, int value, int maxStack, int damage, int defence, int consume, ItemType type, string imagePath)
    {
        itemName = name;
        itemDesc = desc;
        itemId = id;
        itemValue = value;
        itemMaxStack = maxStack;
        itemDamage = damage;
        itemDefence = defence;
        itemConsume = consume;
        itemType = type;
        itemImagePath = imagePath;

        itemSprite = Resources.Load<Sprite>(imagePath);

        //itemImage = Resources.Load<Sprite>(id.ToString());


        //itemModel = Resources.Load<GameObject>("a");      //3D item drop model
    }

    public Item()
    {

    }

    public Sprite GetItemImage()
    {
        if(itemSprite == null)
        {
            if (!string.IsNullOrEmpty(itemImagePath))
            {
                itemSprite = Resources.Load<Sprite>(itemImagePath);
            }
        }
        return itemSprite;
    }

}   

