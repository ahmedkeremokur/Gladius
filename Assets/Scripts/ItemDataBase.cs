using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDataBase : MonoBehaviour
{

    public List<Item> items;

    void Awake()
    {

        //-----------------name, desc, id, value, maxStack, damage, defence, consume, type, imagePath
        


        //--        Weapons         --//


        items.Add(new Item("", "", 0, 0, 0, 0, 0, 0, Item.ItemType.Empty, ""));
        items.Add(new Item("Tiny Sword", "Weapon", 1, 1, 1, 1, 2, 0, Item.ItemType.Weapon, "1"));
        items.Add(new Item("Tiny Axe", "Weapon", 2, 1, 1, 4, 1, 0, Item.ItemType.Weapon, "2"));
        items.Add(new Item("Big Sword", "Weapon", 3, 1, 1, 5, 2, 0, Item.ItemType.Weapon, "3"));
        items.Add(new Item("Hammer", "Weapon", 4, 1, 1, 12, 1, 0, Item.ItemType.Weapon , "4"));
        items.Add(new Item("Runic Axe", "Weapon", 5, 1, 1, 15, 3, 0, Item.ItemType.Weapon, "5"));
        //items.Add(new Item("Runic Axe", "Weapon", 6, 1, 1, 22, 5, Item.ItemType.Weapon, "6"));





        //--        Armors         --//


        items.Add(new Item("Copper Armor", "Defence: 10", 7, 1, 1, 0, 10, 0, Item.ItemType.Armor, "7"));
        items.Add(new Item("Steel Armor", "Defence: 28", 8, 1, 1, 0, 28, 0, Item.ItemType.Armor, "8"));
        items.Add(new Item("Golden Armor", "Defence: 92", 9, 1, 1, 0, 92, 0, Item.ItemType.Armor, "9"));






        //--        Potions         --//


        items.Add(new Item("Health Potion", "Consumable", 10, 1, 10, 0, 0, 20, Item.ItemType.Consumable, "10"));
        items.Add(new Item("Mana Potion", "Consumable", 11, 1, 10, 0, 0, 40, Item.ItemType.Consumable, "11"));

    }
}
