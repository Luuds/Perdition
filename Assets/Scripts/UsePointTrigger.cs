using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.AI;

public class UsePointTrigger : MonoBehaviour {
    public bool onTheLeft;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerStay(Collider other){
	//this is the talk command
	
         
        if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName == "Use" + transform.parent.GetComponent<HotspotData>().hotspot.Slug)
        {
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName = "";
            transform.parent.gameObject.SendMessage("InUsePosition");
        }
        
        //put other commands here 
    }
    private void FixedUpdate()
    {
        if (onTheLeft == true)
        {
            GameObject.FindGameObjectWithTag("Player Image").GetComponent<Citizenanim>().turnLeft = false;
        }
        else if (onTheLeft != true)
        {
            GameObject.FindGameObjectWithTag("Player Image").GetComponent<Citizenanim>().turnLeft = true;
        }
    }

}
