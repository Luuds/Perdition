using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;
 public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{	public GameObject itemSlot; // gameobject this is intantiated under and will return to when on drag ends unless ondrop in slotbehaviour is called
    public int itemAmount;
    public Item itemData;
	Gamecontroller controll; 
	//InventoryDatabase inv; 
    void Start()
    {	//inv = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryDatabase>(); 
        gameObject.transform.name = itemData.Title;
		itemSlot = this.transform.parent.gameObject;
		controll = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>();   
    }
	public void OnBeginDrag (PointerEventData eventData)
	{		this.transform.SetParent (this.transform.parent.parent.parent); 
			this.transform.position = eventData.position; 
			controll.itemDraggedbool= true; 
			controll.itemDraggedData= this;
            gameObject.layer = 2;
        controll.menuOpen = true; 
            GetComponent<CanvasGroup> ().blocksRaycasts = false;
        Debug.Log("Drag Begin");
    }
	public void OnDrag (PointerEventData eventData)

	{
        this.transform.position = eventData.position;
        Debug.Log("Drag");
    }
	public void OnEndDrag (PointerEventData eventData)
    {
        controll.menuOpen = false;
        Debug.Log("End Drag"); 
        this.transform.SetParent(itemSlot.transform);
        this.transform.position = itemSlot.transform.position;
        controll.itemDraggedbool = false;
        controll.itemDraggedData = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.layer = 5;
    }



}
