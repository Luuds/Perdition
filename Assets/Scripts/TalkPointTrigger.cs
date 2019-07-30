﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TalkPointTrigger : MonoBehaviour {

    public bool onTheLeft; 
    // Use this for initialization
    void Start () {
      
	}
	void OnTriggerStay(Collider other){
        //this is the talk command


        if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName == "Talk" + transform.parent.GetComponent<HotspotData>().hotspot.Slug)
        {


            transform.parent.GetComponent<TalkBehaviour>().triggered = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName = "";
           
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
