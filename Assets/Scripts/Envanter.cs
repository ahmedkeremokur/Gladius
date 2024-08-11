using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Newtonsoft.Json;

public class Envanter : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public List<Item> charSlotItems = new List<Item>();
    public List<Charslot> charSlots = new List<Charslot>();

    public Controller controller;

    public GameObject slot, tooltip, draggingItem, noMoney;
    public ItemDataBase database;

    public bool tooltipBool, dragItemBool;
    public Item tooltipItem, dragItem;  

    void Start()
    {
        // Initialize inventory slots

        for (int i = 0; i < 10; i++)
        {
            GameObject slots = (GameObject)Instantiate(slot);
            slots.transform.SetParent(this.gameObject.transform);
            slots.GetComponent<EnvanterSlot>().slotNumber = i;
            slots.name = "Slot" + i;
            items.Add(new Item());
        }


        // Initialize char slots
        if (charSlots.Count == 0)
        {
            charSlots.AddRange(FindObjectsOfType<Charslot>());
            Debug.Log("CharSlots = 0, Initializing...");
            if(charSlots.Count != 0)
            {
                Debug.Log("CharSlots Intitialized.");
            }
        }
       
        foreach (var charSlot in charSlots)
        {
            charSlotItems.Add(new Item());
        }

        noMoney.SetActive(false);
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();


        //----------Load Inventory----------//
        LoadInventory();

        while (items.Count < 10)
        {
            items.Add(new Item());
        }

        if(dragItem != null)
        {
            ShowDragItem(dragItem);
            Debug.Log("Drag item restored");
        }
    }

    void Update()
    {

        if (tooltipBool)
        {
            tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tooltipItem.itemName;
            tooltip.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tooltipItem.itemDesc;
            tooltip.gameObject.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }

        if (dragItemBool)
        {
            draggingItem.transform.GetChild(0).GetComponent<Image>().sprite = dragItem.GetItemImage();
            draggingItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dragItem.itemValue.ToString();
            draggingItem.gameObject.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            
            if (dragItem.itemValue > 1 && dragItem.itemDesc != null)
            {
                draggingItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
                draggingItem.transform.GetChild(0).GetComponent<Image>().enabled = true;
                dragItemBool = true;
            }
            else if (dragItem.itemValue <= 1 && dragItem.itemDesc != null)
            {
                draggingItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
                draggingItem.transform.GetChild(0).GetComponent<Image>().enabled = true;
                dragItemBool = true;
            }
            else if (dragItem.itemName == null)
            {
                draggingItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
                draggingItem.transform.GetChild(0).GetComponent<Image>().enabled = false;
                dragItemBool = false;
            }

           
        }
       
        //-----------------------Keycodes for Testing-----------------------//


        //---------------Item Keycodes---------------//

        if (Input.GetKeyDown(KeyCode.Space))    //Check Slots
        {
            if (AreAllSlotsFull())
            {
                Debug.Log("Slotlar Full");
            }
            else
            {
                Debug.Log("Boþ Slot Mevcut");
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) //Potions
        {
            AddItem(9, 1);
            AddItem(10, 1);
        }
        if (Input.GetKeyDown(KeyCode.W)) // Upper Weapons
        {
            AddItem(4, 1);
        }    
    }

    //--------- While dragging item; show item and tooltip ----------//
    public void ShowDragItem(Item item)
    {
        dragItem = item;
        dragItemBool = true;
        draggingItem.SetActive(true);
    }
    public void HideDragItem()
    {
            dragItem = new Item();
            dragItemBool = false;
            draggingItem.SetActive(false);       
    }
    public void ShowToolTip(Item item)
    {
        tooltipItem = item;
        tooltipBool = true;
        tooltip.SetActive(true);
    }
    public void HideToolTip()
    {
        tooltipBool = false;
        tooltip.SetActive(false);
    }
    public void AddItem(int id, int value)
    {

        for(int i = 0; i<database.items.Count; i++)
        {
            if(database.items[i].itemId == id)
            {
                Item item = new Item(database.items[i].itemName, database.items[i].itemDesc, 
                    database.items[i].itemId, value, database.items[i].itemMaxStack, database.items[i].itemDamage, 
                    database.items[i].itemDefence, database.items[i].itemConsume, database.items[i].itemType, database.items[i].itemImagePath);
                if(item.itemType == Item.ItemType.Consumable)
                {
                    CombineSlot(item);
                    break;
                }

                else
                {
                    AddItemEmptySlot(item);
                    break;
                }

            }
        }

    }

    void CombineSlot(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemId == item.itemId)
            {
                items[i].itemValue += item.itemValue;
                break;
            }
            else if (i == items.Count - 1)
            {
                AddItemEmptySlot(item);
                break;
            }
        }
    }
    void AddItemEmptySlot(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == null)
            {
                items[i] = item;
                return;
                //break;
            }
        }
    }

    //Check if there is not any empty slot

    public bool AreAllSlotsFull()
    {
        foreach (var item in items)
        {
            if (string.IsNullOrEmpty(item.itemName))
            {
                return false;
            }
        }
        return true;
    }

    
    
    //-------------Buy Buttons------------------

    public void BuyItem(int itemId, int itemCost)
    {
        if (!AreAllSlotsFull())
        {
            if (controller.gold >= itemCost)
            {
                AddItem(itemId, 1);
                controller.gold -= itemCost;
            }
            else
            {
                NoMoney();
            }
        }
        else
        {
            Debug.Log("No Empty Slot");
        }
    }

    public void Buy1() => BuyItem(1, 150);
    public void Buy2() => BuyItem(2, 500);
    public void Buy3() => BuyItem(3, 1000);
    public void Buy4() => BuyItem(4, 2500);
    public void Buy5() => BuyItem(5, 6000);
    public void Buy6() => BuyItem(6, 12000);
    public void Buy7() => BuyItem(7, 3000);
    public void Buy8() => BuyItem(8, 15000);
    public void Buy9() => BuyItem(9, 50000);
    public void Buy10() => BuyItem(10, 150);
    public void Buy11() => BuyItem(11, 100);




    private void NoMoney()
    {
        noMoney.SetActive(true);
    }

    public void NoMoneyExit()
    {
        noMoney.SetActive(false);
    }

    //--Thrash--//
    public void DeleteItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemId == item.itemId)
            {
                items[i] = new Item();  // Empty the slot
                break;
            }
        }
    }

    //---------- Load & Save Inventory (JSON)----------//

    public void SaveInventory()
    {
        //Inventory
        string inventoryJson = JsonConvert.SerializeObject(items);
        PlayerPrefs.SetString("Inventory", inventoryJson);

        //Char Slots

        charSlotItems.Clear();
        foreach (var charSlot in charSlots)
        {
            charSlotItems.Add(charSlot.item);
        }

        string charSlotJson = JsonConvert.SerializeObject(charSlotItems);
        PlayerPrefs.SetString("CharSlots", charSlotJson);

        //Dragitem

        string dragItemJson = JsonConvert.SerializeObject(dragItem);
        PlayerPrefs.SetString("DragItem", dragItemJson);

        PlayerPrefs.Save();

    }

    public void LoadInventory()
    {
        //Load Inventory
        if (PlayerPrefs.HasKey("Inventory"))
        {
            string json = PlayerPrefs.GetString("Inventory");
            items = JsonConvert.DeserializeObject<List<Item>>(json);
        }


        //---- Reload item images ----//

        foreach (var item in items)
        {
            if (!string.IsNullOrEmpty(item.itemImagePath))
            {
                item.GetItemImage();
            }
        }

        //Load Character Slots
        if (PlayerPrefs.HasKey("CharSlots"))
        {
            string charSlotJson = PlayerPrefs.GetString("CharSlots");
            charSlotItems = JsonConvert.DeserializeObject<List<Item>>(charSlotJson);

            for (int i = 0; i < charSlots.Count; i++)
            {
                    charSlots[i].item = charSlotItems[i];
                    charSlots[i].UpdateSlot();  
            }
        }

        //Load Drag Item
        if (PlayerPrefs.HasKey("DragItem"))
        {
            string dragItemJson = PlayerPrefs.GetString("DragItem");
            dragItem = JsonConvert.DeserializeObject<Item>(dragItemJson);
        }

    }

    public void OnApplicationQuit()
    {
        SaveInventory();
    }
}
