using System.Collections;
using System.Collections.Generic;
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
        if (item.itemName != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.GetItemImage();
        }
        else
        {
            itemImage.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (!env.dragItemBool)
        {
            if (item.itemName != null)
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
            if(!env.dragItemBool)
            {
                if(item.itemName != null)
                {
                    env.ShowDragItem(item);
                    item = new Item();
                }
            }
            else
            {
                Debug.LogWarning("Dragging an item" + env.dragItem.itemName);
                Debug.LogWarning("Consumption is: " + env.dragItem.itemConsume.ToString());
                if(type == env.dragItem.itemType)
                {
                    Debug.Log("Item types match");
                    if(item.itemName != null)
                    {
                        Item tempItem = item;
                        item = env.dragItem;
                        env.dragItem = tempItem;                 
                    }
                                    
                    else
                    {
                        Debug.LogWarning("Slot is empty, placing item");
                        if (env.dragItem.itemType == Item.ItemType.Consumable && character.hp <= (character.maxHp - 20))
                        {
                            Debug.LogWarning("Item type is consumable");

                            Debug.Log($"Character HP before: {character.hp}");

                            character.hp += env.dragItem.itemConsume;

                            Debug.Log($"Character HP after: {character.hp}");

                            
                            if (env.dragItem.itemValue == 1)
                            {
                                env.HideDragItem();
                                env.DeleteItem(env.dragItem);
                                character.UpdateStatUI();
                                Debug.LogWarning("Drink Potions");
                            }

                            else if (env.dragItem.itemValue > 1)
                            {
                                env.dragItem.itemValue--;
                                character.UpdateStatUI();
                                Debug.LogWarning("Drink Potions");
                            }
                                                     
                        }
                        else if (env.dragItem.itemType != Item.ItemType.Consumable)
                        {
                            item = env.dragItem;
                            env.HideDragItem();
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

    public void UpdateSlot()
    {
        Debug.Log("Updating slot with item: " + (item != null ? item.itemName : "null"));
        if (item.itemName != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.GetItemImage();
        }
        else
        {
            itemImage.enabled = false;
        }
    }

    public int GetWeaponDamage()
    {
        if (item != null && item.itemType == Item.ItemType.Weapon)
        {
            return item.itemDamage;
        }
        return 0;
    }

    public int GetArmorDefence()
    {
        if (item != null && item.itemType == Item.ItemType.Weapon){
            return item.itemDefence;
        }
        return 0;
    }

    /*public int GetPotionHp()
    {
        if (item != null && item.itemType == Item.ItemType.Consumable)
        {
            return item.itemConsume;
        }
        int healing = item.ite
        return 0;
    }
    */
}
