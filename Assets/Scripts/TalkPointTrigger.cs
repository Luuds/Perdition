using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPointTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerStay(Collider other){
	//this is the open command
	if(other.gameObject.tag == "Player"&& GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName 
	== "Talk"+transform.parent.GetComponent<HotspotData>().hotspot.Slug){
			transform.parent.GetComponent<TalkBehaviour>().triggered = true; 
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName = ""; 
	}
	//put other commands here 
	}

}
