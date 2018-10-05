using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
using Fungus; 
public class TalkBehaviour : MonoBehaviour {
	HotspotData parentHotspotData; 
	GameObject menuInst; 
	GameObject menu; 
	NavMeshAgent agent; 
	Gamecontroller controll; 
	EventandTalkDatabase database; 
	public bool triggered = false;  
	GameObject objFlowchart;
	Flowchart flowchart; 
	EventTalk eventTalk;
	// Use this for initialization
	void Start () {
	agent= GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
	parentHotspotData = GetComponent<HotspotData>(); 
	controll = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Gamecontroller>();
	database = GameObject.FindGameObjectWithTag ("GameController").GetComponent<EventandTalkDatabase>();
	eventTalk = database.FetchEventTalkBySlug(parentHotspotData.hotspot.Slug);
	GameObject objFlowchart = GameObject.Find(eventTalk.Flowchart);
	flowchart = objFlowchart.GetComponent<Flowchart>(); 
	}
	public void Talk(){
		Debug.Log("Talk to Shroomie6"); 
			NavMeshHit hit; 
			NavMesh.SamplePosition(transform.GetChild(0).transform.position,out hit,1f,-1); 
			agent.SetDestination(hit.position); 
			agent.gameObject.GetComponent<PlayerController>().interactionName = "Talk"+parentHotspotData.hotspot.Slug;
			controll.menuOpen =false;
			
	}

	void Update () {
		if(triggered){
		 Debug.Log("Talk to Shroomie"); 
		
			DialogueCounter();   
			
			//controll.menuOpen =true;
			triggered= false;
	
			}
		
	}
	void DialogueCounter (){
	flowchart.SendFungusMessage(eventTalk.Messages[eventTalk.Counter]); 
	if(eventTalk.Counter<eventTalk.Messages.Count-1){
				eventTalk.Counter ++; 
				
			}  	
			
	} 
}
