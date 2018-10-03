using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems; 
public class HotspotData : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
	public string hotspotName; 
	public Hotspot hotspot; 
	HotspotDatabase hotSpotDatabase;
	ItemDatabase itemDatabase;  
	InventoryDatabase inv; 
	Gamecontroller controll;
	public bool menuOpen = false; 
	GameObject inventoryPanelRef; 
	GameObject inventorySlot; 
	GameObject inventoryItem; 
	GameObject createdMenu;
	public List <GameObject> slots = new List <GameObject>(); 
	public bool pressed = false; 
	// Use this for initialization
	void Start () {
		hotSpotDatabase = GameObject.FindGameObjectWithTag ("GameController").GetComponent<HotspotDatabase> ();
		itemDatabase = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ItemDatabase> ();
		inv = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InventoryDatabase> ();
		controll = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Gamecontroller>(); 
		hotspot=hotSpotDatabase.FetchHotspotBySlug(this.gameObject.name);
		hotspotName = hotspot.Title; 
		inventorySlot = Resources.Load<GameObject> ("Prefab/Slot") ; 
		inventoryItem = Resources.Load<GameObject> ("Prefab/Item") ;
	}
		void OnMouseOver ()
	{	
		if (Input.GetMouseButtonUp (0) && controll.itemDraggedbool) { 
			if (hotspot.AcceptItem == true) {
				//Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
				Ray ray =Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				Physics.Raycast (ray, out hit); 
				if (hit.collider != null && controll.itemDraggedData != null) {
					
					ItemData droppedItem = controll.itemDraggedData; 
					GameObject itemRef =controll.itemDraggedData.gameObject; 
					controll.itemDraggedData = null; 
					for (int i = 0; i < hotspot.ItemsRecieve.Count; i++) {
						if (droppedItem.itemData.ID == hotspot.ItemsRecieve [i] && hotSpotDatabase.database [hotspot.ID].ItemsLimit [i] > 0) {
							hotSpotDatabase.database [hotspot.ID].ItemsLimit [i] = -1;
							if (droppedItem.itemAmount == 1) {
								inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAndSize[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] = -1;
								inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAmount[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] = 0;
								Destroy (itemRef); 

							} else {
								droppedItem.itemAmount -= 1; 
								inv.database[droppedItem.itemSlot.GetComponent<SlotBehaviour>().invID].ItemsAmount[droppedItem.itemSlot.GetComponent<SlotBehaviour>().slotID] --;
								if (droppedItem.itemAmount == 0) {
									itemRef.transform.GetChild (0).GetComponent<Text> ().text = "";
								} else {
									itemRef.transform.GetChild (0).GetComponent<Text> ().text = (droppedItem.itemAmount ).ToString ();
								}
							}
						}}}}
						//Debug.Log(hotspot.ItemsLimit[1].ToString());
						}}

	
	void OnMouseExit (){
		
	}
	IEnumerator CreateMenu(){
		yield return new WaitForSeconds(0.3f); 
		if (hotspot.MenuInterface != "" && menuOpen== false&&pressed){
			menuOpen = true; 
			controll.menuOpen = true; 
			GameObject menuRef = Resources.Load<GameObject> ("Prefab/"+"Menu_"+hotspot.MenuInterface) ; 
			GameObject menu = Instantiate(menuRef,this.transform.position,Quaternion.identity); 
			createdMenu = menu; 
			menu.GetComponent<UIStayInPlace>().pos =Input.mousePosition;
			menu.GetComponent<UIStayInPlace>().mouse = true; 
			menu.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform,false);
			menu.transform.position = Input.mousePosition ;//Camera.main.WorldToScreenPoint (Input.mousePosition);// + new Vector3(0,50,0);
			menu.transform.localScale = Vector3.one; 
			if(hotspot.MenuInterface == "Basic"){
				GameObject buttonRef = Resources.Load<GameObject> ("Prefab/InteractionButton") ;
				for(int i =0; i<hotspot.MenuCommands.Count;i++){
					GameObject button = Instantiate(buttonRef,Vector2.zero,Quaternion.identity);
					button.transform.SetParent(menu.transform.GetChild(i),false);
					button.transform.position = menu.transform.GetChild(i).transform.position; 
					menu.transform.localScale = Vector3.one; 
					button.GetComponent<Image>().sprite =Resources.Load<Sprite> ("UI/"+hotspot.MenuCommands[i]+"ButtonInactive");   // change this to icon change or something just letting you know
					button.GetComponent<InteractionButtonScript>().parentHotspot = hotspot; 
					button.GetComponent<InteractionButtonScript>().parentHotspotData = this; 
					button.GetComponent<InteractionButtonScript>().buttonNumber = i; 
				}
			}
			yield return null; 
		}else if(menuOpen&&!pressed&&createdMenu!=null) {
			Destroy(createdMenu,0.08f);
			menuOpen =false; 
			controll.menuOpen = false;
			yield return null;  
			}
	}
	public void OnPointerDown(PointerEventData eventData){
		pressed = true; 
		StartCoroutine(CreateMenu()); 
		
	}
	public void OnPointerUp(PointerEventData eventData){
		StopCoroutine(CreateMenu()); 
		pressed = false; 
		StartCoroutine(CreateMenu()); 
		
	}
	void OnTriggerEnter(Collider other){
		//Debug.Log("Hey there stranger");
	//this is the open command
	if(other.gameObject.tag == "Player"&& GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName == "Open"){
		//Debug.Log("Hey there stranger you open me up!");
		OpenInventory(); 
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName = ""; 
		}
	//put other commands here 
	}
	void OnTriggerStay(Collider other){
		//Debug.Log("Hey there stranger");
	//this is the open command
	if(other.gameObject.tag == "Player"&& GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName == "Open"){
		//Debug.Log("Hey there stranger you open me up!");
		OpenInventory(); 
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName = ""; 
		}
	//put other commands here 
	}
	public void OpenInventory()
	{	int id = inv.FetchInventoryBySlug(hotspot.Slug).ID; 
		if (menuOpen == false){
			inventoryPanelRef =Resources.Load<GameObject> ("Prefab/InvMenu_"+(inv.database[id].ItemsAndSize.Count).ToString()); 
			GameObject inventoryPanel = Instantiate(inventoryPanelRef);
			//RectTransform invPanelRect = inventoryPanel.GetComponent<RectTransform>(); 
			inventoryPanel.GetComponent<UIStayInPlace>().pos = gameObject.transform.position;
			inventoryPanel.GetComponent<UIStayInPlace>().offset = new Vector3 (0,300,0); //this might change if you change the camera y offset --
			inventoryPanel.name = hotspot.Slug +"_inv"; 
			inventoryPanel.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform,false);
			inventoryPanel.transform.position = Camera.main.WorldToScreenPoint (gameObject.transform.position) + new Vector3(0,300,0);
			inventoryPanel.transform.localScale = Vector3.one; 
			Button button = inventoryPanel.transform.GetChild(0).GetComponent<Button>(); 
			button.onClick.AddListener(CloseInventory); 
			// instantiate inv panel here
		for (int i = 0; i < inv.database[id].ItemsAndSize.Count; i++){
			slots.Add(Instantiate(inventorySlot));
			slots [i].transform.SetParent (inventoryPanel.transform); 
			slots [i].transform.localScale = Vector3.one; 
			slots [i].GetComponent<SlotBehaviour>().invID = id; 
			slots [i].GetComponent<SlotBehaviour>().slotID = i;
			if (inv.database[id].ItemsAndSize[i] != -1 ){
			GameObject itemObj = Instantiate (inventoryItem);						
			itemObj.GetComponent<ItemData> ().itemData =  itemDatabase.FetchItemByID(inv.database[id].ItemsAndSize[i]);
			itemObj.GetComponent<ItemData> ().itemAmount = inv.database[id].ItemsAmount[i];
			itemObj.transform.SetParent (slots [i].transform); 
			itemObj.GetComponent<Image> ().sprite = itemObj.GetComponent<ItemData> ().itemData.Sprite; 
			itemObj.transform.position = Vector2.zero;
			itemObj.transform.localScale = Vector3.one;
			if(itemObj.GetComponent<ItemData> ().itemAmount > 1){
			itemObj.transform.GetChild (0).GetComponent<Text> ().text = (itemObj.GetComponent<ItemData>().itemAmount).ToString ();}
						}
			}
			button.transform.SetAsLastSibling(); 
			menuOpen = true;
			controll.menuOpen =true; 
		}
	}
	public void CloseInventory(){
			Destroy(GameObject.Find(hotspot.Slug +"_inv")); 
			slots.Clear();
			menuOpen=false; 
			controll.menuOpen =false;
		
	}
	void Update () {
		
	}
}
