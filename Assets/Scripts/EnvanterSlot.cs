using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EnvanterSlot : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler
{
    public Item item;
    public int slotNumber;

    public Envanter env;

    Image itemImage;
    TextMeshProUGUI itemValue;

    void Start()
    {
        env = GameObject.FindGameObjectWithTag("Envanter").GetComponent<Envanter>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemValue = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        /*------Control

        Debug.Log("Slot Number: " + slotNumber);
        Debug.Log("Envanter Items Count: " + env.items.Count);
        */
    }

    void Update()
    {
        if (slotNumber >= 0 && slotNumber < env.items.Count)
        {
            item = env.items[slotNumber];
            if (item.itemName != null)
            {
                if (item.itemId < 10)
                {
                    itemImage.enabled = true;
                    itemValue.enabled = false;
                    itemImage.sprite = item.GetItemImage();
                    itemValue.text = item.itemValue.ToString();
                }

                else if (item.itemId >= 10)
                {
                    itemImage.enabled = true;
                    itemValue.enabled = true;
                    itemImage.sprite = item.GetItemImage();
                    itemValue.text = item.itemValue.ToString();
                }
            }
            else
            {
                itemImage.enabled = false;
                itemValue.text = "";
            }
        }

        else
        {
            Debug.LogWarning(" Slot Number is out of range: " + slotNumber);
        }
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(item.itemName != null && env.dragItemBool ==false)
        {
            env.ShowToolTip(item);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        env.HideToolTip();
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log(item.itemId + item.itemName + item.itemValue);
        if(item.itemName == null)
        {
            Debug.Log("Slot is Empty");
            
        }
        if(data.button.ToString() == "Left")
        {
            if (!env.dragItemBool)
            {
                env.ShowDragItem(item);
                env.items[slotNumber] = new Item();
            }
            else if (env.dragItemBool)
            {
                if(item.itemName == null)
                {
                    env.items[slotNumber] = env.dragItem;
                    env.HideDragItem();
                }
                else if (item.itemName != null)
                {
                    
                    if(item.itemName == env.dragItem.itemName)
                        {
                        if (item.itemType == Item.ItemType.Consumable)
                        {
                            int value = env.items[slotNumber].itemValue += env.dragItem.itemValue;
                            if(value <= env.items[slotNumber].itemMaxStack)
                            {
                                env.items[slotNumber].itemValue = value;
                                env.HideDragItem();
                            }
                            else
                            {
                                env.items[slotNumber].itemValue = env.items[slotNumber].itemMaxStack;
                                Item copyItem = new Item(env.items[slotNumber].itemName, env.items[slotNumber].itemDesc, env.items[slotNumber].itemId, 
                                    value - env.items[slotNumber].itemMaxStack, env.items[slotNumber].itemMaxStack, env.items[slotNumber].itemDamage,
                                    env.items[slotNumber].itemDefence, env.items[slotNumber].itemConsume, env.items[slotNumber].itemType, env.items[slotNumber].itemImagePath);
                                env.ShowDragItem(copyItem);
                            }
                        }      
                    }
                    else
                    {
                        Item newItem = env.items[slotNumber];
                        env.items[slotNumber] = env.dragItem;
                        env.dragItem = newItem;
                    }  
                }      
            }
        }
        if (data.button.ToString() == "Right")
        {
            if (!env.dragItemBool)
            {
                if(item.itemType == Item.ItemType.Consumable && item.itemValue > 1)
                {
                    int stackValue1 = item.itemValue / 2;
                    Item newItem = new Item(item.itemName, item.itemDesc, item.itemId, stackValue1, item.itemMaxStack, item.itemDamage, item.itemDefence, item.itemConsume, item.itemType, item.itemImagePath);
                    env.ShowDragItem(newItem);
                    int stackValue2 = item.itemValue - stackValue1;
                    env.items[slotNumber].itemValue = stackValue2;
                }
            }
            else if (env.dragItemBool)
            {
                
            }
        }
    }

}
