using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Spine.Unity; 
public class UseBaseConsole : MonoBehaviour {

	// Use this for initialization
	HotspotData parentHotspotData; 
	GameObject menuInst; 
	GameObject menu; 
	NavMeshAgent agent; 
	Gamecontroller controll;
    void Start () {
			agent= GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
			parentHotspotData = GetComponent<HotspotData>(); 
			controll = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Gamecontroller>();
	}
	public void Use(){
			NavMeshHit hit; 
			NavMesh.SamplePosition(parentHotspotData.transform.GetChild(0).transform.position,out hit,1f,-1); 
			agent.SetDestination(hit.position); 
			agent.gameObject.GetComponent<PlayerController>().interactionName = "Use"+parentHotspotData.hotspot.Slug;
			controll.menuOpen =false;
	
	}
     public void InUsePosition() {
            if (parentHotspotData.hotspot.Slug == "button_console") {
                menuInst = Resources.Load<GameObject>("Prefab/BaseButtonsPanel");
                menu = Instantiate(menuInst, Vector3.zero, Quaternion.identity);
                menu.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform);
                menu.transform.localScale = Vector3.one;
                menu.transform.localPosition = Vector3.zero;
                controll.menuOpen = true;
               // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().interactionName = "";
                
            }
        
			
			
	
		//put other commands here 
	}
	// Update is called once per frame
	void Update () {
		
	}
}
