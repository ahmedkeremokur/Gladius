using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, 
    IPointerClickHandler
{
    public Envanter envanter;
    public Item item;

    void Start()
    {
        envanter = GameObject.FindGameObjectWithTag("Envanter").GetComponent<Envanter>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (envanter.dragItemBool)
        {
            Debug.Log(envanter.dragItem.itemName + " dropped in trash can");

            envanter.HideDragItem();

            envanter.DeleteItem(envanter.dragItem);

        }
    }

}