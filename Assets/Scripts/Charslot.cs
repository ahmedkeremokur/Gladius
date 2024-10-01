using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Charslot : MonoBehaviour ,     
    IPointerEnterHandler, 
    IPointerExitHandler,
    IPointerDownHandler
{

    public Item item;
    public Item.ItemType type;
    Image itemImage;
    
    public Character character;
    public Envanter env;

    void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        character = FindObjectOfType<Character>();

        if (item == null)
        {
            item = new Item();
        }
    }
    void Start()
    {       
        if (itemImage == null)
        {
            Debug.LogWarning("itemImage == null in charslot start");
        }

        if (env == null)
        {
            env = FindObjectOfType<Envanter>();
        }
        UpdateSlot();
    }

    void Update()
    {
        if (item != null && item.itemName != null && itemImage != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.GetItemImage();
        }
        else if (itemImage != null)
        {
            itemImage.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (env != null && !env.dragItemBool)
        {
            if (item != null && item.itemName != null)
            {
                env.ShowToolTip(item);
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        env.HideToolTip();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(data.button.ToString() == "Left")
        {
            if(!env.dragItemBool)       //Empty Hands
            {
                if (item != null && item.itemName != null)
                {
                    RemoveItem();
                    env.ShowDragItem(item);
                    item = new Item();
                }
            }
            else   // We are holding something
            {
                Debug.LogWarning("Dragging an item" + env.dragItem.itemName);
                Debug.LogWarning("Consumption is: " + env.dragItem.itemConsume.ToString());
                if(item != null && type == env.dragItem.itemType)
                {
                    Debug.LogError("4");
                    Debug.Log("Item types match");
                    if(item.itemName != null)
                    {
                        Debug.LogError("5");
                        Item tempItem = item;
                        item = env.dragItem;
                        env.dragItem = tempItem;                 
                    }
                                    
                    else
                    {
                        Debug.LogError("6");
                        Debug.LogWarning("Slot is empty, placing item");
                        if (env.dragItem.itemType == Item.ItemType.Consumable && character.hp <= (character.maxHp - 20))        //Item is consumable
                        {
                            Debug.LogError("7");
                            Debug.LogWarning("Item type is consumable");

                            Debug.Log($"Character HP before: {character.hp}");

                            character.hp += env.dragItem.itemConsume;

                            Debug.Log($"Character HP after: {character.hp}");

                            
                            if (env.dragItem.itemValue == 1)
                            {
                                Debug.LogError("8");
                                env.HideDragItem();
                                env.DeleteItem(env.dragItem);
                                character.UpdateStatUI();
                                Debug.LogWarning("Drink Potions");
                            }

                            else if (env.dragItem.itemValue > 1)
                            {
                                Debug.LogError("9");
                                env.dragItem.itemValue--;
                                character.UpdateStatUI();
                                Debug.LogWarning("Drink Potions");
                            }
                                                     
                        }
                        else if (env.dragItem.itemType != Item.ItemType.Consumable)
                        {
                            if(env.dragItem.itemType == Item.ItemType.Weapon)
                            {
                                Item newWeapon = env.dragItem;
                                EquipItem(newWeapon);
                                Debug.LogError("Weapon Enter");
                                item = env.dragItem;
                                env.HideDragItem();
                                
                            }
                            else if(env.dragItem != null && env.dragItem.itemType == Item.ItemType.Armor)
                            {
                                Item newArmor = env.dragItem;
                                EquipItem(newArmor);
                                Debug.LogError("Armor Enter");
                                item = env.dragItem;
                                env.HideDragItem();
                            }                         
                        }     
                    }                    
                }
                else
                {
                    Debug.Log("This item cannot be placed in here!");
                }
            }
        }
        UpdateSlot();
    }


    public void EquipItem(Item newItem)
    {
        item = newItem;
        UpdateSlot();

        if (item != null && item.itemType == Item.ItemType.Weapon)
        {
            character.SetWeaponDamage(item.itemDamage);
        }
        else if (item != null && item.itemType == Item.ItemType.Armor)
        {
            character.SetArmorDefence(item.itemDefence);
        }
    }

    public void RemoveItem()
    {
        if (item != null && item.itemType == Item.ItemType.Weapon)
        {
            character.RemoveWeaponDamage(item.itemDamage);
        }
        else if (item != null && item.itemType == Item.ItemType.Armor)
        {
            character.RemoveArmorDefence(item.itemDefence);
        }

        UpdateSlot();
    }


    public void UpdateSlot()
    {
        Debug.Log("Updating slot with item: " + (item != null ? item.itemName : "null"));
        if (item != null && !string.IsNullOrEmpty(item.itemName))
        {
            itemImage.enabled = true;
            itemImage.sprite = item.GetItemImage();
        }
        else
        {
            itemImage.enabled = false;
            itemImage.sprite = null;
        }
    }

   


    public int GetWeaponDamage()
    {
        if (item != null && item.itemType == Item.ItemType.Weapon)
        {
            character.weaponDamage = item.itemDamage;
            return item.itemDamage;
        }
        return 0;     
    }

    public int GetArmorDefence()
    {
        if (item != null && item.itemType == Item.ItemType.Armor)
        {
            character.armorArmor = item.itemDefence;
            return item.itemDefence;
        }
        return 0;
    }
}
