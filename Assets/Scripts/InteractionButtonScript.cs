using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Spine.Unity; 
public class InteractionButtonScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public Hotspot parentHotspot;
	Button button; 
	public int buttonNumber; 
	public HotspotData parentHotspotData; 
	NavMeshAgent agent; 
	Gamecontroller controll; 
	bool pointerOver; 
	// Use this for initialization
	void Start () {
	
	controll = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Gamecontroller>();
	//button = GetComponent<Button>(); 
	//button.onClick.AddListener(TaskOnClick); 
	agent= GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
	
	}
	public void OnPointerEnter (PointerEventData eventData)
	{	GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/"+parentHotspot.MenuCommands[buttonNumber]+"ButtonActive");
		pointerOver = true; 
	}
	public void OnPointerExit (PointerEventData eventData)
	{	GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/"+parentHotspot.MenuCommands[buttonNumber]+"ButtonInactive");
		pointerOver =false; 
	}

			
	
	// Update is called once per frame
	void Update () {
			
	if(pointerOver){
		if(Input.GetMouseButtonUp(0)){
                if (GameObject.Find(parentHotspot.Slug).transform.position.x < agent.transform.position.x)
                {
                    GameObject.FindGameObjectWithTag("Player Image").GetComponent<SkeletonMecanim>().skeleton.ScaleX = -1;

                }
                else {
                    GameObject.FindGameObjectWithTag("Player Image").GetComponent<SkeletonMecanim>().skeleton.ScaleX = 1;
                }

            if (parentHotspot.MenuCommands[buttonNumber] == "Examine"){
            
            PlayerTextController playerText = GameObject.FindGameObjectWithTag("Player Text").GetComponent<PlayerTextController>(); 
			playerText.MakePlayerSay(parentHotspot.Description,parentHotspot.DescriptionCounter);
			if(parentHotspot.DescriptionCounter<parentHotspot.Description.Count-1){
				parentHotspot.DescriptionCounter ++; 
				
			}  
		
			parentHotspotData.menuOpen = false;
			controll.menuOpen =false;
			Destroy(gameObject.transform.parent.parent.gameObject);
		}else if(parentHotspot.MenuCommands[buttonNumber] == "Open"){
			NavMeshHit hit; 
			NavMesh.SamplePosition(parentHotspotData.transform.position,out hit,1f,-1); 
			agent.SetDestination(hit.position); 
			agent.gameObject.GetComponent<PlayerController>().interactionName = "Open"+parentHotspot.Slug;
			controll.menuOpen =false;
			parentHotspotData.menuOpen = false;
			Destroy(gameObject.transform.parent.parent.gameObject);
		}else if(parentHotspot.MenuCommands[buttonNumber] == "Use"){//Trigger event
			parentHotspotData.gameObject.SendMessage("Use");
			parentHotspotData.menuOpen = false;
			controll.menuOpen =false;
			Destroy(gameObject.transform.parent.parent.gameObject);
			//add more commands here and in hotspots
		}else if(parentHotspot.MenuCommands[buttonNumber] == "Talk"){//Trigger event
			parentHotspotData.gameObject.SendMessage("Talk");
			parentHotspotData.menuOpen = false;
			controll.menuOpen =false;
			Destroy(gameObject.transform.parent.parent.gameObject);
			//add more commands here and in hotspots
		}
		}
		
	}
	}
}
/*	while (agent.velocity.x > 0f){
				Debug.Log("Open3");
				gameObject.transform.parent.parent.transform.position = Camera.main.WorldToScreenPoint (parentHotspotData.transform.position) + new Vector3(0,50,0);
			}*/