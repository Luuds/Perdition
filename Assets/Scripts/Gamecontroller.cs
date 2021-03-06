﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using System.IO; 
using UnityEngine.SceneManagement; 
using LitJson; 

public class Gamecontroller : MonoBehaviour {
public static Gamecontroller control; 
public bool itemHeldbool = false;
public bool itemDraggedbool = false;
public ItemData itemDraggedData = null; 
public GameObject itemHeldObj = null; 
public GameObject slotSelect = null;
int minutes= 00, hours=6, hiddenSeconds; 
public Text counterText; 
public bool menuOpen=false; 
public bool pause = false; 
InventoryDatabase inv;
JsonData inventoryDataJson;
public List<Inventory> invDatabase = new List<Inventory>();
    // Use this for initialization
    void Awake () {
		SceneManager.sceneLoaded += OnSceneLoaded;
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this; 
		} else if (control != this) {
			Destroy (gameObject); 
		}
	}
	 	 void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene oldScene = scene; 
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    }

	void Start (){
			inv = gameObject.GetComponent<InventoryDatabase>();
			StartCoroutine ("timeMinutes");
			StartCoroutine ("timeHiddenSeconds"); 
			menuOpen=false; 
	}
   
    public void Save1()
    {
        inventoryDataJson = JsonMapper.ToJson(inv.database);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/SaveFiles/InventorySave1.json", inventoryDataJson.ToString());
    }
    public void Load1() {
        inventoryDataJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/SaveFiles/InventorySave1.json"));
        invDatabase.Clear(); 
        ConstructInventoryDatabase();
        if (invDatabase.Count > 0)
        {
            inv.database = invDatabase;
            foreach (Inventory inv in GetComponent<InventoryDatabase>().database)
            {
                Debug.Log(inv.ItemsAndSize.Count);
            }
        }
        else { invDatabase.Clear();
            Debug.Log("Can't Load"); 
                }
    }
    public void Save2()
    {
        inventoryDataJson = JsonMapper.ToJson(inv.database);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/SaveFiles/InventorySave2.json", inventoryDataJson.ToString());
    }
    public void Load2()
    {
        inventoryDataJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/SaveFiles/InventorySave2.json"));
        invDatabase.Clear();
        ConstructInventoryDatabase();
        if (invDatabase.Count > 0)
        {
            inv.database = invDatabase;
            foreach (Inventory inv in GetComponent<InventoryDatabase>().database)
            {
                Debug.Log(inv.ItemsAndSize.Count);
            }
        }
        else
        {
            invDatabase.Clear();
            Debug.Log("Can't Load");
        }
    }
    public void Save3()
    {
        inventoryDataJson = JsonMapper.ToJson(inv.database);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/SaveFiles/InventorySave3.json", inventoryDataJson.ToString());
    }
    public void Load3()
    {
        inventoryDataJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/SaveFiles/InventorySave3.json"));
        invDatabase.Clear();
        ConstructInventoryDatabase();
        if (invDatabase.Count > 0)
        {
            inv.database = invDatabase;
            foreach (Inventory inv in GetComponent<InventoryDatabase>().database)
            {
                Debug.Log(inv.ItemsAndSize.Count);
            }
        }
        else
        {
            invDatabase.Clear();
            Debug.Log("Can't Load");
        }
    }
    void ConstructInventoryDatabase()
    {
        for (int i = 0; i < inventoryDataJson.Count; i++)
        {
            List<int> itemsAndSize = new List<int>();
            List<int> itemsAmount = new List<int>();
            for (int k = 0; k < inventoryDataJson[i]["ItemsAndSize"].Count; k++)
            {
                itemsAndSize.Add((int)inventoryDataJson[i]["ItemsAndSize"][k]);
                itemsAmount.Add((int)inventoryDataJson[i]["ItemsAmount"][k]);
            }
            invDatabase.Add(new Inventory((int)inventoryDataJson[i]["ID"], inventoryDataJson[i]["Title"].ToString(), itemsAndSize, itemsAmount, inventoryDataJson[i]["Slug"].ToString()));
        }
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			
			Debug.Log(inv.database[0].ItemsAndSize[0].ToString() +inv.database[0].ItemsAndSize[1].ToString()+ 
			inv.database[0].ItemsAndSize[2].ToString() +inv.database[0].ItemsAndSize[3].ToString() + 
			inv.database[0].ItemsAndSize[4].ToString() +inv.database[0].ItemsAndSize[5].ToString() +
			inv.database[0].ItemsAndSize[6].ToString() +inv.database[0].ItemsAndSize[7].ToString()+ 
			inv.database[0].ItemsAndSize[8].ToString() +inv.database[0].ItemsAndSize[9].ToString() ); 
		
	
		Debug.Log(inv.database[0].ItemsAmount[0].ToString() +inv.database[0].ItemsAmount[1].ToString()+ 
			inv.database[0].ItemsAmount[2].ToString() +inv.database[0].ItemsAmount[3].ToString() + 
			inv.database[0].ItemsAmount[4].ToString() +inv.database[0].ItemsAmount[5].ToString() +
			inv.database[0].ItemsAmount[6].ToString() +inv.database[0].ItemsAmount[7].ToString()+ 
			inv.database[0].ItemsAmount[8].ToString() +inv.database[0].ItemsAmount[9].ToString() ); 
		}
		if(Input.GetKeyDown(KeyCode.B)){
			inv.AddItemtoInventoryByTitle("main_inv","Energy Bar",inv.invOpen_main_inv); 
		}
		counterText.text = hours.ToString ("00") + ":" + minutes.ToString ("00");
		if (minutes >=60){
			StopCoroutine ("timeMinutes"); 
			hours += 1; 
			gameObject.SendMessage("CheckingHour",null);
			minutes = 0; 
			StartCoroutine ("timeMinutes"); 
		}
		if (hours >= 24) {
			hours = 0; 
		}
	
	}
 void LateUpdate(){
	 if(itemHeldbool){
		 if(Input.GetMouseButtonDown(0)) {
			 StartCoroutine(Deselect());
		 }
		
		
	 }
 }
 IEnumerator Deselect(){
	
	 yield return new WaitForSeconds(0.1f);
	 if(slotSelect != null){
		slotSelect.GetComponent<Image>().sprite= Resources.Load<Sprite> ("UI/SlotInactive");
            Destroy(GameObject.FindGameObjectWithTag("Description Menu"));
            itemHeldObj =null;
		slotSelect=null;
		itemHeldbool=false;
	 }
	 
 }
	IEnumerator timeMinutes()
	{
		while (true) {
			yield return new WaitForSeconds (2); 
			minutes +=1; 
			gameObject.SendMessage("CheckingTime",null); 
		}
	}
	IEnumerator timeHiddenSeconds()
	{
		while (true ) {
			yield return new WaitForSeconds (1); 
			hiddenSeconds +=1;
		}
	}

}
