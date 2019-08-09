using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
public class SlotBehaviour : MonoBehaviour,IDropHandler,IPointerClickHandler {
	public int invID; 
	public int slotID; 
	InventoryDatabase inv; 
	
    GameObject desc_Menu; 
	//ItemDatabase itemDatabase;
	Gamecontroller control; 
    void Start()
    {	inv = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryDatabase>();
		//itemDatabase = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ItemDatabase>(); 
		control = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>();
        desc_Menu = Resources.Load<GameObject>("Prefab/Menu_Description");
    }
	public void OnDrop (PointerEventData eventData)
	{	
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();
		Text text = droppedItem.transform.GetChild (0).GetComponent<Text> (); 
		if (droppedItem.itemSlot != this.gameObject){
			inv.database[invID].ItemsAndSize[slotID] = droppedItem.itemData.ID; 
			inv.database[invID].ItemsAmount[slotID] = droppedItem.itemAmount;
			if(this.transform.childCount >0){
				ItemData childItemData = this.transform.GetChild (0).GetComponent<ItemData>();
				 if(droppedItem.itemData.ID ==childItemData.itemData.ID 
				 		&&droppedItem.itemData.Stackable == true
						&&childItemData.itemAmount < childItemData.itemData.StackLimit){
					int amountDiff = childItemData.itemData.StackLimit - childItemData.itemAmount; 
					int subtractAmount = droppedItem.itemAmount - amountDiff; 
					if(subtractAmount <=0){
						
						childItemData.itemAmount += droppedItem.itemAmount;
						inv.database[invID].ItemsAmount[slotID] = childItemData.itemAmount;
						inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAndSize[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID]= -1;
						inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAmount[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID]=0; 
						this.transform.GetChild (0).transform.GetChild(0).GetComponent<Text>().text =  childItemData.itemAmount.ToString();  
						Destroy(droppedItem.gameObject);
						//destroy dropped item and update database
						
					}else{
						childItemData.itemAmount+= amountDiff;
						this.transform.GetChild (0).transform.GetChild(0).GetComponent<Text>().text =  childItemData.itemAmount.ToString(); 
						droppedItem.itemAmount-= amountDiff; 
						droppedItem.gameObject.transform.SetParent(droppedItem.itemSlot.transform); 
						inv.database[invID].ItemsAmount[slotID] = childItemData.itemAmount;
						inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAmount[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID]=droppedItem.itemAmount; 
						if(droppedItem.itemAmount==1){
							text.text= ""; 

						}else{
							text.text = droppedItem.itemAmount.ToString(); 
						}
					}


				}
				// if there is an item in the slot already, updating the database in ItemDatabase script
				else{
				inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAndSize[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] 
				= childItemData.itemData.ID; 
				inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAmount[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] 
				= childItemData.itemAmount;   
				childItemData.itemSlot = droppedItem.itemSlot; 
				this.transform.GetChild(0).transform.SetParent(droppedItem.itemSlot.transform); 
				droppedItem.itemSlot = this.gameObject; 
				eventData.pointerDrag.transform.SetParent (this.transform); 
				}
			}else{ // if there isn't one 
				inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAndSize[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] 
				= -1; 
				inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAmount[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] 
				= 0; 
				eventData.pointerDrag.transform.SetParent (this.transform); 
				droppedItem.itemSlot = this.gameObject; 
			}
			
			
		}
		 }
	public void OnPointerClick (PointerEventData eventData)
	{
        Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
	if(control.itemHeldbool==false){
			if(this.transform.childCount > 0){
			GameObject childItem = this.transform.GetChild(0).gameObject; 
			control.itemHeldObj = childItem; 
			control.itemHeldbool = true; 
			control.slotSelect = this.gameObject; 
			this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/SlotActive");

                GameObject menuObj = Instantiate(desc_Menu);
                control.menuOpen = true; 
                menuObj.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform);
                menuObj.transform.position = transform.position + new Vector3(0,100);
                menuObj.transform.localScale = Vector3.one;
                menuObj.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = childItem.GetComponent<ItemData>().itemData.Title;
                menuObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = childItem.GetComponent<ItemData>().itemData.Description;
                menuObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Item Type: "+ childItem.GetComponent<ItemData>().itemData.Type;
                menuObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "\nValue: " + childItem.GetComponent<ItemData>().itemData.Value.ToString();
                if (childItem.GetComponent<ItemData>().itemData.Type == "Consumable")
                {
                    menuObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += "\n<color=green>Energy: +" + childItem.GetComponent<ItemData>().itemData.Energy.ToString() +"</color>";
                    menuObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.yellow;
                }
                else if (childItem.GetComponent<ItemData>().itemData.Type == "Seed") {
                    menuObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.green;
                }
                else if (childItem.GetComponent<ItemData>().itemData.Type == "Artifact")
                {
                    menuObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().color = Color.cyan;
                }
            }
		}
	if(control.itemHeldbool==true && control.slotSelect!=this.gameObject){
			if(this.transform.childCount >0){
				GameObject childItem = this.transform.GetChild(0).gameObject;
				if(childItem.GetComponent<ItemData>().itemData.ID ==control.itemHeldObj.GetComponent<ItemData>().itemData.ID 
				 		&&childItem.GetComponent<ItemData>().itemData.Stackable == true
						&&childItem.GetComponent<ItemData>().itemAmount < childItem.GetComponent<ItemData>().itemData.StackLimit){
									
						childItem.GetComponent<ItemData>().itemAmount ++; 
						childItem.transform.GetChild(0).GetComponent<Text>().text =childItem.GetComponent<ItemData>().itemAmount.ToString();
						if (control.itemHeldObj.GetComponent<ItemData>().itemAmount==1){
							inv.database[invID].ItemsAndSize[slotID]= -1; 
							inv.database[invID].ItemsAmount[slotID]= 0;
							Destroy(control.itemHeldObj);
							control.itemHeldObj = null; 
							control.itemHeldbool=false;
                            Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
                            control.slotSelect.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/SlotInactive"); 
							control.slotSelect =null;
                            control.menuOpen = false;
                    } else{
						control.itemHeldObj.GetComponent<ItemData>().itemAmount--; 
						inv.database[invID].ItemsAmount[slotID]--; 
						if(control.itemHeldObj.GetComponent<ItemData>().itemAmount==1){
						control.itemHeldObj.transform.GetChild(0).GetComponent<Text>().text = ""; 
						}else{
						control.itemHeldObj.transform.GetChild(0).GetComponent<Text>().text =control.itemHeldObj.GetComponent<ItemData>().itemAmount.ToString();  
						}
						control.itemHeldObj = null; 
						control.itemHeldbool=false;
                        Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
                        control.slotSelect.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/SlotInactive"); 
						control.slotSelect =null;
                        control.menuOpen = false;
                    }
				 
				

				}else{
				control.itemHeldObj.transform.SetParent(this.gameObject.transform);
                control.itemHeldObj.GetComponent<ItemData>().itemSlot = this.gameObject;
                    childItem.GetComponent<ItemData>().itemSlot = control.slotSelect; 
                childItem.transform.SetParent(control.slotSelect.transform); 
				inv.database[invID].ItemsAndSize[slotID] = control.itemHeldObj.GetComponent<ItemData>().itemData.ID; 
				inv.database[control.slotSelect.GetComponent<SlotBehaviour>().invID].ItemsAndSize[control.slotSelect.GetComponent<SlotBehaviour>().slotID] = 
				childItem.GetComponent<ItemData>().itemData.ID; 
				inv.database[invID].ItemsAmount[slotID] = control.itemHeldObj.GetComponent<ItemData>().itemAmount; 
				inv.database[control.slotSelect.GetComponent<SlotBehaviour>().invID].ItemsAmount[control.slotSelect.GetComponent<SlotBehaviour>().slotID] = 
				childItem.GetComponent<ItemData>().itemAmount;
                Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
                control.slotSelect.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/SlotInactive"); 
				control.itemHeldObj = null; 
				control.itemHeldbool=false;
				control.slotSelect =null;
                control.menuOpen = false;
                }
			}else{if (control.itemHeldObj.GetComponent<ItemData>().itemAmount >1){
					inv.database[invID].ItemsAndSize[slotID] = control.itemHeldObj.GetComponent<ItemData>().itemData.ID;
							inv.database[invID].ItemsAmount[slotID] = 1; 
							GameObject itemObj = Instantiate (control.itemHeldObj);
							itemObj.GetComponent<ItemData> ().itemData = control.itemHeldObj.GetComponent<ItemData>().itemData;
							itemObj.GetComponent<ItemData> ().itemAmount = 1;  
							itemObj.transform.SetParent (this.transform); 
							itemObj.GetComponent<Image> ().sprite = control.itemHeldObj.GetComponent<ItemData>().itemData.Sprite; 
							itemObj.transform.position = Vector2.zero;
							itemObj.transform.localScale = Vector3.one; 
							itemObj.transform.GetChild(0).GetComponent<Text>().text = ""; 
							control.itemHeldObj.GetComponent<ItemData>().itemAmount --; 
							inv.database[control.slotSelect.GetComponent<SlotBehaviour>().invID].ItemsAmount[control.slotSelect.GetComponent<SlotBehaviour>().slotID] --; 
							if(control.itemHeldObj.GetComponent<ItemData>().itemAmount==1){
							control.itemHeldObj.transform.GetChild(0).GetComponent<Text>().text = ""; 
							}else{
							control.itemHeldObj.transform.GetChild(0).GetComponent<Text>().text =control.itemHeldObj.GetComponent<ItemData>().itemAmount.ToString();  
							}
                    Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
                    control.slotSelect.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/SlotInactive"); 
							control.itemHeldObj = null; 
							control.itemHeldbool=false;
							control.slotSelect =null;
                    control.menuOpen = false;
                }	else{	
			control.itemHeldObj.transform.SetParent(this.gameObject.transform); 
			inv.database[invID].ItemsAndSize[slotID] = control.itemHeldObj.GetComponent<ItemData>().itemData.ID; 
			inv.database[control.slotSelect.GetComponent<SlotBehaviour>().invID].ItemsAndSize[control.slotSelect.GetComponent<SlotBehaviour>().slotID] = -1; 
			inv.database[invID].ItemsAmount[slotID] = control.itemHeldObj.GetComponent<ItemData>().itemAmount; 
			inv.database[control.slotSelect.GetComponent<SlotBehaviour>().invID].ItemsAmount[control.slotSelect.GetComponent<SlotBehaviour>().slotID] = 0;
                    Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
                    control.slotSelect.GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/SlotInactive"); 
			control.itemHeldObj = null; 
			control.itemHeldbool=false;
			control.slotSelect =null;
                    control.menuOpen = false;
                }
			}
		}
	}
	void Update(){
		/*if (!control.itemHeldbool){
		if(Input.GetMouseButtonDown(0)){
			Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
			RaycastHit2D hit = Physics2D.Raycast (mouseWorldPosition, Vector2.zero); 
			if (hit.collider == null || hit.collider.gameObject.tag != "slot") {
			control.itemHeldObj = null; 
			control.itemHeldbool=false;
			control.slotSelect.GetComponent<Image>().color = this.gameObject.GetComponent<Image>().color;
			control.slotSelect =null; 
		}}}
	*/}	
}
