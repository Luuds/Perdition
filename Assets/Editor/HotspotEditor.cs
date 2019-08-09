using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;
[CustomEditor(typeof(HotspotInspector))]
public class HotspotEditor : Editor
{
    public List<Item> databaseItems = new List<Item>();
    private JsonData itemData;
    public List<Hotspot> database = new List<Hotspot>();
    private int descAmount;
    private int interAmount;
    private int itemAmount;
    private JsonData hotspotDataJson;
    private JsonData hotspotData;
    private Hotspot newHotspot; 
    private void Awake()
    {
        itemData = JsonMapper.ToObject(Resources.Load<TextAsset>("Databases/Items").ToString());
        hotspotData = JsonMapper.ToObject(Resources.Load<TextAsset>("Databases/Hotspots").ToString());
        ConstructHotspotDatabase();
        ConstructItemDatabase();
    }
    public override void OnInspectorGUI()
    {



        HotspotInspector myHotspoInspector = (HotspotInspector)target;
        myHotspoInspector.slug = EditorGUILayout.DelayedTextField("Slug", myHotspoInspector.gameObject.name);
        myHotspoInspector.gameObject.name = myHotspoInspector.slug;
        myHotspoInspector.meHotspot = FetchHotspotBySlug(myHotspoInspector.slug);
        //    
        if (myHotspoInspector.meHotspot != null && myHotspoInspector.meHotspot.ID !=-1)
        {
            EditorGUILayout.LabelField("ID", myHotspoInspector.meHotspot.ID.ToString());
            myHotspoInspector.meHotspot.Title = EditorGUILayout.DelayedTextField("Title", myHotspoInspector.meHotspot.Title);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Description Counter", myHotspoInspector.meHotspot.DescriptionCounter.ToString());
            descAmount = EditorGUILayout.DelayedIntField("Number of Descriptions", myHotspoInspector.meHotspot.Description.Count);
            myHotspoInspector.descCount = descAmount;

            if (descAmount < myHotspoInspector.meHotspot.Description.Count) {
                for (int r = 0; r < myHotspoInspector.meHotspot.Description.Count - descAmount; r++)
                {
                    myHotspoInspector.meHotspot.Description.RemoveAt(myHotspoInspector.meHotspot.Description.Count - 1);
                }
            }
            for (int i = 0; i < descAmount; i++)
            {
                if (descAmount <= myHotspoInspector.meHotspot.Description.Count)
                {
                    myHotspoInspector.meHotspot.Description[i] = EditorGUILayout.DelayedTextField("Description " + i.ToString(), myHotspoInspector.meHotspot.Description[i]);
                }
                else if (descAmount > myHotspoInspector.meHotspot.Description.Count)
                {
                    myHotspoInspector.meHotspot.Description.Add(EditorGUILayout.DelayedTextField("Description " + i.ToString(), ""));
                }



            }
            EditorGUILayout.Space();
          
            myHotspoInspector.meHotspot.MenuInterface = EditorGUILayout.TextField("Menu Interface Prefab", myHotspoInspector.meHotspot.MenuInterface);

            interAmount = EditorGUILayout.DelayedIntField("Number of Interactions", myHotspoInspector.meHotspot.MenuCommands.Count);
            myHotspoInspector.interactionCount = interAmount;
            if (interAmount < myHotspoInspector.meHotspot.MenuCommands.Count)
            {
                for (int r = 0; r < myHotspoInspector.meHotspot.MenuCommands.Count - interAmount; r++)
                {
                    myHotspoInspector.meHotspot.MenuCommands.RemoveAt(myHotspoInspector.meHotspot.MenuCommands.Count - 1);
                }
            }
            for (int k = 0; k < interAmount; k++)
            {
                if (interAmount <= myHotspoInspector.meHotspot.MenuCommands.Count)
                {
                    myHotspoInspector.meHotspot.MenuCommands[k] = EditorGUILayout.TextField( myHotspoInspector.meHotspot.MenuCommands[k]);
                }
                else if (interAmount > myHotspoInspector.meHotspot.MenuCommands.Count)
                {
                    myHotspoInspector.meHotspot.MenuCommands.Add(EditorGUILayout.TextField( ""));
                }


            }
            EditorGUILayout.Space();
            myHotspoInspector.meHotspot.AcceptItem = EditorGUILayout.Toggle("Accept Item", myHotspoInspector.meHotspot.AcceptItem);
            itemAmount = EditorGUILayout.DelayedIntField("Number of Items", myHotspoInspector.meHotspot.ItemsRecieve.Count);
            myHotspoInspector.itemCount = itemAmount;
           
          
            //EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(200)); 
            if (itemAmount < myHotspoInspector.meHotspot.ItemsRecieve.Count)
            {
                for (int r = 0; r < myHotspoInspector.meHotspot.ItemsRecieve.Count - itemAmount; r++)
                {
                    
                    myHotspoInspector.meHotspot.ItemsRecieve.RemoveAt(myHotspoInspector.meHotspot.ItemsRecieve.Count - 1);
                   
                }
            }
            for (int h = 0; h < itemAmount; h++)
            {
                if (itemAmount <= myHotspoInspector.meHotspot.ItemsRecieve.Count && FetchItemBySlug(FetchItemByID(myHotspoInspector.meHotspot.ItemsRecieve[h]).Slug) != null)
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(250));
               

                    myHotspoInspector.meHotspot.ItemsRecieve[h] = EditorGUILayout.DelayedIntField( myHotspoInspector.meHotspot.ItemsRecieve[h]);

                    

                    myHotspoInspector.meHotspot.ItemsRecieve[h] = FetchItemBySlug(EditorGUILayout.DelayedTextField( FetchItemByID(myHotspoInspector.meHotspot.ItemsRecieve[h]).Slug)).ID;
                   
                    myHotspoInspector.meHotspot.ItemsLimit[h] = EditorGUILayout.DelayedIntField(myHotspoInspector.meHotspot.ItemsLimit[h]);

                    EditorGUILayout.EndHorizontal();
                }
               
                else if (itemAmount > myHotspoInspector.meHotspot.ItemsRecieve.Count)
                {
                    myHotspoInspector.meHotspot.ItemsRecieve.Add(EditorGUILayout.IntField(-1));
                    EditorGUILayout.LabelField("Item Name",FetchItemByID(myHotspoInspector.meHotspot.ItemsRecieve[h]).Slug);
                    myHotspoInspector.meHotspot.ItemsLimit.Add (EditorGUILayout.DelayedIntField(-1));
                }
              

            }
            myHotspoInspector.meHotspot.ItemType = EditorGUILayout.DelayedTextField("Accept all Items of Type:", myHotspoInspector.meHotspot.ItemType);
            // EditorGUILayout.EndHorizontal(); 


            if (GUILayout.Button("Save To Hotspot Database"))
            {
                database[FetchHotspotByID(myHotspoInspector.meHotspot.ID).ID+1] = myHotspoInspector.meHotspot; 
                hotspotDataJson = JsonMapper.ToJson(database);
                File.WriteAllText(Application.dataPath + "/Resources/Databases/Hotspots.json", hotspotDataJson.ToString());
                AssetDatabase.Refresh();
            }


            }
           else 
            {
            newHotspot = FetchHotspotByID(-1); 
            EditorGUILayout.HelpBox("No such Hotspot Found", MessageType.Warning);
            EditorGUILayout.LabelField("ID", newHotspot.ID.ToString());
            newHotspot.Slug = myHotspoInspector.slug; 
            newHotspot.Title = EditorGUILayout.DelayedTextField("Title", newHotspot.Title);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Description Counter", newHotspot.DescriptionCounter.ToString());
            descAmount = EditorGUILayout.DelayedIntField("Number of Descriptions", newHotspot.Description.Count);
            myHotspoInspector.descCount = descAmount;

            if (descAmount < newHotspot.Description.Count)
            {
                for (int r = 0; r < newHotspot.Description.Count - descAmount; r++)
                {
                    newHotspot.Description.RemoveAt(newHotspot.Description.Count - 1);
                }
            }
            for (int i = 0; i < descAmount; i++)
            {
                if (descAmount <= newHotspot.Description.Count)
                {
                    newHotspot.Description[i] = EditorGUILayout.DelayedTextField("Description " + i.ToString(), newHotspot.Description[i]);
                }
                else if (descAmount > newHotspot.Description.Count)
                {
                    newHotspot.Description.Add(EditorGUILayout.DelayedTextField("Description " + i.ToString(), ""));
                }



            }
            EditorGUILayout.Space();

            newHotspot.MenuInterface = EditorGUILayout.TextField("Menu Interface Prefab", newHotspot.MenuInterface);

            interAmount = EditorGUILayout.DelayedIntField("Number of Interactions", newHotspot.MenuCommands.Count);
            myHotspoInspector.interactionCount = interAmount;
            if (interAmount < newHotspot.MenuCommands.Count)
            {
                for (int r = 0; r < newHotspot.MenuCommands.Count - interAmount; r++)
                {
                    newHotspot.MenuCommands.RemoveAt(newHotspot.MenuCommands.Count - 1);
                }
            }
            for (int k = 0; k < interAmount; k++)
            {
                if (interAmount <= newHotspot.MenuCommands.Count)
                {
                    newHotspot.MenuCommands[k] = EditorGUILayout.TextField(newHotspot.MenuCommands[k]);
                }
                else if (interAmount > newHotspot.MenuCommands.Count)
                {
                    newHotspot.MenuCommands.Add(EditorGUILayout.TextField(""));
                }


            }
            EditorGUILayout.Space();
            newHotspot.AcceptItem = EditorGUILayout.Toggle("Accept Item", newHotspot.AcceptItem);
            itemAmount = EditorGUILayout.DelayedIntField("Number of Items", newHotspot.ItemsRecieve.Count);
            myHotspoInspector.itemCount = itemAmount;


            //EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(200)); 
            if (itemAmount < newHotspot.ItemsRecieve.Count)
            {
                for (int r = 0; r < newHotspot.ItemsRecieve.Count - itemAmount; r++)
                {

                    newHotspot.ItemsRecieve.RemoveAt(newHotspot.ItemsRecieve.Count - 1);

                }
            }
            for (int h = 0; h < itemAmount; h++)
            {
                if (itemAmount <= newHotspot.ItemsRecieve.Count && FetchItemBySlug(FetchItemByID(newHotspot.ItemsRecieve[h]).Slug) != null)
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(250));


                    newHotspot.ItemsRecieve[h] = EditorGUILayout.DelayedIntField(newHotspot.ItemsRecieve[h]);



                    newHotspot.ItemsRecieve[h] = FetchItemBySlug(EditorGUILayout.DelayedTextField(FetchItemByID(newHotspot.ItemsRecieve[h]).Slug)).ID;

                    newHotspot.ItemsLimit[h] = EditorGUILayout.DelayedIntField(newHotspot.ItemsLimit[h]);

                    EditorGUILayout.EndHorizontal();
                }

                else if (itemAmount > newHotspot.ItemsRecieve.Count)
                {
                    newHotspot.ItemsRecieve.Add(EditorGUILayout.IntField(-1));
                    EditorGUILayout.LabelField("Item Name", FetchItemByID(newHotspot.ItemsRecieve[h]).Slug);
                    newHotspot.ItemsLimit.Add(EditorGUILayout.DelayedIntField(-1));
                }
                newHotspot.ItemType = EditorGUILayout.DelayedTextField("Accept all Items of Type:", newHotspot.ItemType);

            }

            if (GUILayout.Button("Add NEW To Hotspot Database"))
            {
                database.Clear(); 
                ConstructHotspotDatabase(); 
                database.Add(newHotspot);
                database[database.Count-1].ID = database.Count-2;
                hotspotDataJson = JsonMapper.ToJson(database);
                File.WriteAllText(Application.dataPath + "/Resources/Databases/Hotspots.json", hotspotDataJson.ToString());
                AssetDatabase.Refresh();
            }

            /// 

        }
        
        //   public List<int> ItemsRecieve { get; set; }
        // public List<int> ItemsLimit { get; set; }


    }
    public void ConstructHotspotDatabase()
    {
        for (int i = 0; i < hotspotData.Count; i++)
        {
            List<int> itemsLimit = new List<int>();
            List<int> itemsRecieve = new List<int>();
            List<string> menuCommands = new List<string>();
            List<string> description = new List<string>();
            for (int k = 0; k < hotspotData[i]["ItemsRecieve"].Count; k++)
            {
                itemsRecieve.Add((int)hotspotData[i]["ItemsRecieve"][k]);
                itemsLimit.Add((int)hotspotData[i]["ItemsLimit"][k]);

            }
            for (int k = 0; k < hotspotData[i]["MenuCommands"].Count; k++)
            {
                menuCommands.Add(hotspotData[i]["MenuCommands"][k].ToString());
            }
            for (int k = 0; k < hotspotData[i]["Description"].Count; k++)
            {
                description.Add(hotspotData[i]["Description"][k].ToString());
            }
            database.Add(new Hotspot((int)hotspotData[i]["ID"], hotspotData[i]["Title"].ToString(), description, (int)hotspotData[i]["DescriptionCounter"], hotspotData[i]["Slug"].ToString(),
                (bool)hotspotData[i]["AcceptItem"], hotspotData[i]["MenuInterface"].ToString(), menuCommands, itemsRecieve, itemsLimit, hotspotData[i]["ItemType"].ToString()));
        }
    }

    
    public Hotspot FetchHotspotBySlug(string slug)
    {

        for (int i = 0; i < database.Count; i++)
            if (database[i].Slug == slug)
                return database[i];



        return null;

    }
    public Hotspot FetchHotspotByID(int id)
    {

        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];


        return null;

    }
    public Item FetchItemBySlug(string slug)
    {

        for (int i = 0; i < databaseItems.Count; i++)
            if (databaseItems[i].Slug == slug)
                return databaseItems[i];


        return null;

    }

    public Item FetchItemByID(int id)
    {

        for (int i = 0; i < databaseItems.Count; i++)
            if (databaseItems[i].ID == id)
                return databaseItems[i];


        return null;

    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            databaseItems.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"],
                itemData[i]["description"].ToString(), itemData[i]["slug"].ToString(), (bool)itemData[i]["stackable"],
                (int)itemData[i]["stackLimit"], itemData[i]["type"].ToString(), (int)itemData[i]["energy"]));
        }
    }


}