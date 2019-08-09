using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using LitJson; 
using System.IO;
using UnityEngine.Networking;

public class InventoryDatabase : MonoBehaviour {
    public List<Inventory> database = new List<Inventory>();
    private JsonData inventoryData;
    private GameObject inventoryPanelRef;
    private GameObject inventorySlot;
    private GameObject inventoryItem;
    public bool invOpen_main_inv = false;
    ItemDatabase itemDatabase;
    Gamecontroller control;
    public List<GameObject> slots = new List<GameObject>();
    private string path;
    void Start()
    {
       //path = "Inventorys"; 
        inventoryData = JsonMapper.ToObject(Resources.Load<TextAsset>("Databases/Inventorys").ToString());


        control = GetComponent<Gamecontroller>();
        inventoryPanelRef = Resources.Load<GameObject>("Prefab/main_inv");
        inventorySlot = Resources.Load<GameObject>("Prefab/Slot");
        inventoryItem = Resources.Load<GameObject>("Prefab/Item");
        itemDatabase = GetComponent<ItemDatabase>();
       
        ConstructInventoryDatabase();



    }
    
	public bool CheckIfItemInInventory(int Inv, Item item){
	

		for (int i = 0; i <database[Inv].ItemsAmount.Count; i++) 
			if (database[Inv].ItemsAndSize[i] == item.ID) 
				return true;
		return false; 
			

	}
 	public void AddItemtoInventoryByTitle (string InvSlug, string title, bool invOpen){ //not sure if this will be sued at all just letting you know
	 int Inv = FetchInventoryBySlug(InvSlug).ID;
	 Item itemToadd = itemDatabase.FetchItemByTitle (title);
		if(invOpen == true)
		// now we instantiate/add if inventory is open. 
		{if (itemToadd.Stackable && CheckIfItemInInventory (Inv,itemToadd)) {
				for (int i = 0; i < database[Inv].ItemsAmount.Count; i++) {
					if (itemDatabase.FetchItemByID(database[Inv].ItemsAndSize[i]).Title == title && database[Inv].ItemsAmount[i]<itemToadd.StackLimit ) {
						ItemData data = slots [i].transform.GetChild (0).GetComponent <ItemData> (); 
							database[Inv].ItemsAmount[i]++;
							data.itemAmount++; 
							data.transform.GetChild (0).GetComponent<Text> ().text = (data.itemAmount).ToString (); 
							break;
						} else { if (database[Inv].ItemsAndSize[i] == -1) { 
							database[Inv].ItemsAndSize[i] = itemToadd.ID;
							database[Inv].ItemsAmount[i] = 1; 
							GameObject itemObj = Instantiate (inventoryItem);
							itemObj.GetComponent<ItemData> ().itemData = itemToadd;
							itemObj.GetComponent<ItemData> ().itemAmount = 1;  
							itemObj.transform.SetParent (slots [i].transform); 
							itemObj.GetComponent<Image> ().sprite = itemToadd.Sprite; 
							itemObj.transform.position = Vector2.zero;
							itemObj.transform.localScale = Vector3.one; 
							break; 
					 
						}
					}
					}
					

			} else {
				for (int i = 0; i <  database[Inv].ItemsAmount.Count; i++) {
					if (database[Inv].ItemsAndSize[i] == -1) {
						database[Inv].ItemsAndSize [i] = itemToadd.ID;
						database[Inv].ItemsAmount[i] = 1; 
						GameObject itemObj = Instantiate (inventoryItem);						
						itemObj.GetComponent<ItemData> ().itemData = itemToadd;
						itemObj.GetComponent<ItemData> ().itemAmount = 1; 
						itemObj.transform.SetParent (slots [i].transform); 
						itemObj.GetComponent<Image> ().sprite = itemToadd.Sprite; 
						itemObj.transform.position = Vector2.zero;
						itemObj.transform.localScale = Vector3.one;
						break; 
				} 
				}
			}
			
		}else{
			// If there is a stack of items that can take one more item and the inventory is closed it just adds to the pile 
			if (itemToadd.Stackable && CheckIfItemInInventory (Inv,itemToadd)) {
				for (int i = 0; i < database[Inv].ItemsAmount.Count; i++) {
					if (itemDatabase.FetchItemByID(database[Inv].ItemsAndSize[i]).Title == title && database[Inv].ItemsAmount[i]<itemToadd.StackLimit ) {
							database[Inv].ItemsAmount[i]++; 
							break;
						
			 
					}else { if (database[Inv].ItemsAndSize[i] == -1) {
							database[Inv].ItemsAndSize[i] = itemToadd.ID; 
							database[Inv].ItemsAmount[i] = 1; 
							break;
						
						}
				} 
			}
			}else{ // new item or item is not stackable
				for (int i = 0; i <  database[Inv].ItemsAmount.Count; i++) {
						if (database[Inv].ItemsAndSize[i] == -1) {
							database[Inv].ItemsAndSize[i] = itemToadd.ID; 
							database[Inv].ItemsAmount[i] = 1; 
							break;
						}
				}
			}
		}

		}
		
		
		

	public void OpenCloseInventory()
	{	
		if (invOpen_main_inv == false){
			GameObject inventoryPanel = Instantiate(inventoryPanelRef);
			RectTransform invPanelRect = inventoryPanel.GetComponent<RectTransform>(); 
			inventoryPanel.name = "main_inv"; 
			inventoryPanel.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform,false);
			//invPanelRect.localPosition = GameObject.FindGameObjectWithTag("Inventory Button").GetComponent<RectTransform>().localPosition + new Vector3 (-450, 50, 0); 
			inventoryPanel.transform.localScale = Vector3.one; 
			// instantiate inv panel here
		for (int i = 0; i < database[0].ItemsAndSize.Count; i++){
			slots.Add(Instantiate(inventorySlot));
			slots [i].transform.SetParent (inventoryPanel.transform); 
			slots [i].transform.localScale = Vector3.one; 
			slots [i].GetComponent<SlotBehaviour>().invID = 0; 
			slots [i].GetComponent<SlotBehaviour>().slotID = i;
			if (database[0].ItemsAndSize[i] != -1 ){
			GameObject itemObj = Instantiate (inventoryItem);						
			itemObj.GetComponent<ItemData> ().itemData =  itemDatabase.FetchItemByID(database[0].ItemsAndSize[i]);
			itemObj.GetComponent<ItemData> ().itemAmount = database[0].ItemsAmount[i];
			itemObj.transform.SetParent (slots [i].transform); 
			itemObj.GetComponent<Image> ().sprite = itemObj.GetComponent<ItemData> ().itemData.Sprite; 
			itemObj.transform.position = Vector2.zero;
			itemObj.transform.localScale = Vector3.one;
			if(itemObj.GetComponent<ItemData> ().itemAmount > 1){
			itemObj.transform.GetChild (0).GetComponent<Text> ().text = (itemObj.GetComponent<ItemData>().itemAmount).ToString ();}
						}
			}
			invOpen_main_inv = true; 
		}else{
		
			Destroy(GameObject.FindWithTag("Main Inventory"));
            Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
            slots.Clear();
			invOpen_main_inv=false;
			control.itemHeldbool=false;
			control.itemHeldObj=null;
			control.slotSelect = null; 
		 
		}
	}

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    private void Update()
    {/*
        if (invOpen_main_inv) {
            if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
            {
                Destroy(GameObject.FindWithTag("Main Inventory"));
                Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
                slots.Clear();
                invOpen_main_inv = false;
                control.itemHeldbool = false;
                control.itemHeldObj = null;
                control.slotSelect = null;
            }


        }

        */
    }
    // add item instantiation save and load here ????
    public Inventory FetchInventoryByTitle(string title){
	
		for (int i = 0; i < database.Count; i++) 
			if (database[i].Title == title)
				return database [i];
				
			
			return null;

	}
	public Inventory FetchInventoryBySlug(string slug){
	
		for (int i = 0; i < database.Count; i++) 
			if (database[i].Slug == slug)
				return database [i];
				
			
			return null;

	}
	public Inventory FetchInventoryByID(int id){

		for (int i = 0; i < database.Count; i++) 
			if (database[i].ID == id)
				return database [i];


		return null;

	}

	void ConstructInventoryDatabase(){
		for (int i = 0; i < inventoryData.Count; i++) {	
			List<int> itemsAndSize = new List <int> ();
			List<int> itemsAmount = new List <int> ();
			for (int k = 0; k < inventoryData [i] ["ItemsAndSize"].Count; k++) {
				itemsAndSize.Add ((int)inventoryData [i] ["ItemsAndSize"] [k]); 
				itemsAmount.Add ((int)inventoryData [i] ["ItemsAmount"] [k]); 
			}
			database.Add (new Inventory ((int)inventoryData [i] ["ID"], inventoryData [i] ["Title"].ToString (), itemsAndSize, itemsAmount, inventoryData [i] ["Slug"].ToString ())); 
		}
	}
}

public class Inventory{

	public int ID { get; set; }
	public string Title { get; set; }
	public List <int> ItemsAndSize{ get; set;}
	public List <int> ItemsAmount{ get; set;}
	public string Slug{ get; set;}

	public Inventory (int id, string title,List <int> itemsAndSize, List <int> itemsAmount, string slug){
	
		this.ID = id;
		this.Title = title; 
		this.ItemsAndSize = itemsAndSize; 
		this.ItemsAmount = itemsAmount;
		this.Slug = slug; 
			}
	public Inventory(){
	
		this.ID = -1; 
		//this.Title = "no Inventory"; 
	}
	
}
