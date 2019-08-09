using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[ExecuteAlways]
public class HotspotInspector : MonoBehaviour
{
    public List<Hotspot> database = new List<Hotspot>();
    public JsonData hotspotData;
    public JsonData hotspotDataJson; 
    public Hotspot meHotspot;
    public string slug;
    public int descCount;
    public int interactionCount;
    public int itemCount;
    // Start is called before the first frame update
    void Start()
    {

        //path = Application.dataPath + "/StreamingAssets/Inventorys.json";
       // hotspotData = JsonMapper.ToObject(Resources.Load<TextAsset>("Databases/Hotspots").ToString());
        //hotspotData = JsonMapper.ToObject (File.ReadAllText(path +"/Hotspots.json")); 
        // ConstructHotspotDatabase();
       meHotspot = FetchHotspotBySlug(this.gameObject.name);
        // slug = meHotspot.Slug; 
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
