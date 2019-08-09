using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


public class EventandTalkDatabase : MonoBehaviour {

	public List<EventTalk> database = new List<EventTalk> (); 
	private JsonData eventTalkData;

    // Use this for initialization
    void Start ()
    {

        eventTalkData = JsonMapper.ToObject(Resources.Load<TextAsset>("Databases/EventTalk").ToString());



        ConstructEventTalkDatabase (); 
	}

	public void ConstructEventTalkDatabase(){
		for (int i = 0; i < eventTalkData.Count; i++) {
			List<int> itemsLimit = new List <int> (); 
			List<int> itemsRecieve = new List <int> ();
			List <string> menuCommands = new List <string>(); 
			List <string> messages = new List <string>(); 
			for (int k = 0; k < eventTalkData [i] ["ItemsRecieve"].Count; k++) {
				itemsRecieve.Add ((int)eventTalkData [i] ["ItemsRecieve"] [k]); 
				itemsLimit.Add ((int)eventTalkData [i] ["ItemsLimit"] [k]); 
				
			}
			for (int k = 0; k < eventTalkData [i] ["MenuCommands"].Count; k++) {
				menuCommands.Add (eventTalkData[i]["MenuCommands"][k].ToString()); 
			}
			for (int k = 0; k < eventTalkData [i] ["Messages"].Count; k++) {
				messages.Add (eventTalkData[i]["Messages"][k].ToString()); 
			}
			database.Add (new EventTalk ((int)eventTalkData[i]["ID"],eventTalkData[i]["Flowchart"].ToString(),messages,(int)eventTalkData[i]["Counter"],eventTalkData[i]["Slug"].ToString(),
				(bool)eventTalkData[i]["AcceptItem"],eventTalkData[i]["MenuInterface"].ToString(),menuCommands,itemsRecieve,itemsLimit ,eventTalkData[i]["Type"].ToString() )); 
		}
	
	}public EventTalk FetchEventTalkBySlug(string slug){

		for (int i = 0; i < database.Count; i++) 
			if (database[i].Slug == slug)
				return database [i];
		


		return null;

	}
	public EventTalk FetchEventTalkByID(int id){

		for (int i = 0; i < database.Count; i++) 
			if (database[i].ID == id)
				return database [i];


		return null;

	}
}

public class EventTalk{
	public int ID { get; set; }
	public string Flowchart { get; set; }
	public List <string> Messages{ get; set;}
	public int Counter{get;set;}
	public string Slug{ get; set;}
	public bool AcceptItem{ get; set; }
	public string MenuInterface{ get; set;}
	public List <string> MenuCommands{ get; set;}
	public List <int> ItemsRecieve{ get; set;}
	public List <int> ItemsLimit{ get; set;}
	public string Type{ get; set;}
	


	public EventTalk(int id, string flowchart, List <string> messages,int counter, string slug, bool acceptItem, string menuInterface,List <string> menuCommands, 
		List <int> itemsRecieve,List <int> itemsLimit, string type)
	{
		this.ID = id;
		this.Flowchart = flowchart; 
		this.Messages = messages;
		this.Counter=counter; 
		this.Slug = slug;
		this.AcceptItem = acceptItem;
		this.MenuInterface = menuInterface; 
		this.MenuCommands = menuCommands;  
		this.ItemsRecieve = itemsRecieve;
		this.ItemsLimit = itemsLimit; 
		this.Type = type;
	
	}
	public EventTalk(){
		this.ID = -1; 

	}


}
