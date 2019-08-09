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
    bool pointerOverAnd;
    public string interaction; 
	// Use this for initialization
	void Start () {
	
	controll = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Gamecontroller>();
	//button = GetComponent<Button>(); 
	//button.onClick.AddListener(TaskOnClick); 
	agent= GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
	
	}
	public void OnPointerEnter (PointerEventData eventData)
	{	GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/"+parentHotspot.MenuCommands[buttonNumber]+"ButtonActive");
        StopCoroutine(PointerOver());
        pointerOverAnd = true; 
        pointerOver = true; 

	}
	public void OnPointerExit (PointerEventData eventData)
	{	GetComponent<Image>().sprite = Resources.Load<Sprite> ("UI/"+parentHotspot.MenuCommands[buttonNumber]+"ButtonInactive");
		pointerOver =false;
        StartCoroutine (PointerOver());
	}


    IEnumerator PointerOver() {
        yield return new WaitForSeconds(0.1f);
        pointerOverAnd = false;
        
    }
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_STANDALONE

        if (pointerOver){
            
           // Debug.Log("Interaction Start");
            if (Input.GetMouseButtonUp(0)){
                Debug.Log("Interaction");
                if (GameObject.Find(parentHotspot.Slug).transform.position.x < agent.transform.position.x)
                {
                    GameObject.FindGameObjectWithTag("Player Image").GetComponent<Citizenanim>().turnLeft = true; 

                }
                else {
                    GameObject.FindGameObjectWithTag("Player Image").GetComponent<Citizenanim>().turnLeft = false; 
                        
                }

            if (parentHotspot.MenuCommands[buttonNumber] == "Examine"){
                    Debug.Log("Interaction Examine");
                    PlayerTextController playerText = GameObject.FindGameObjectWithTag("Player Text").GetComponent<PlayerTextController>(); 
			playerText.MakePlayerSay(parentHotspot.Description,parentHotspot.DescriptionCounter,parentHotspot);
			
		
			parentHotspotData.menuOpen = false;
			controll.menuOpen =false;
			Destroy(gameObject.transform.parent.parent.gameObject);
		}else if(parentHotspot.MenuCommands[buttonNumber] == "Open"){
                    Debug.Log("Interaction Open");
                    NavMeshHit hit; 
			NavMesh.SamplePosition(parentHotspotData.transform.position,out hit,1f,-1); 
			agent.SetDestination(hit.position); 
			agent.gameObject.GetComponent<PlayerController>().interactionName = "Open"+parentHotspot.Slug;
			controll.menuOpen =false;
			parentHotspotData.menuOpen = false;
			Destroy(gameObject.transform.parent.parent.gameObject);
		}else if(parentHotspot.MenuCommands[buttonNumber] == "Use"){//Trigger event
                    Debug.Log("Interaction Use");
                    parentHotspotData.gameObject.SendMessage("Use");
			parentHotspotData.menuOpen = false;
			controll.menuOpen =false;
			Destroy(gameObject.transform.parent.parent.gameObject);
			//add more commands here and in hotspots
		}else if(parentHotspot.MenuCommands[buttonNumber] == "Talk"){//Trigger event
                    Debug.Log("Interaction Talk");
                    parentHotspotData.gameObject.SendMessage("Talk");
			parentHotspotData.menuOpen = false;
			controll.menuOpen =false;
			Destroy(gameObject.transform.parent.parent.gameObject);
			//add more commands here and in hotspots
		}
		}
		
	}
#endif

#if UNITY_ANDROID

        if (Input.touchCount>0 && pointerOverAnd)
        {
            Debug.Log("Interaction Start");
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended)


                {
                    Debug.Log("Interaction");
                    if (GameObject.Find(parentHotspot.Slug).transform.position.x < agent.transform.position.x)
                    {
                        GameObject.FindGameObjectWithTag("Player Image").GetComponent<Citizenanim>().turnLeft = true;

                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("Player Image").GetComponent<Citizenanim>().turnLeft = false;

                    }

                    if (parentHotspot.MenuCommands[buttonNumber] == "Examine" && interaction == "Examine")
                    {
                        Debug.Log("Interaction Examine");
                        PlayerTextController playerText = GameObject.FindGameObjectWithTag("Player Text").GetComponent<PlayerTextController>();
                        playerText.MakePlayerSay(parentHotspot.Description, parentHotspot.DescriptionCounter, parentHotspot);
                       
                        parentHotspotData.menuOpen = false;
                        controll.menuOpen = false;
                        Destroy(gameObject.transform.parent.parent.gameObject);
                    }
                    else if (parentHotspot.MenuCommands[buttonNumber] == "Open" && interaction == "Open")
                    {
                        Debug.Log("Interaction Open");
                        NavMeshHit hit;
                        NavMesh.SamplePosition(parentHotspotData.transform.position, out hit, 1f, -1);
                        agent.SetDestination(hit.position);
                        agent.gameObject.GetComponent<PlayerController>().interactionName = "Open" + parentHotspot.Slug;
                        controll.menuOpen = false;
                        parentHotspotData.menuOpen = false;
                        Destroy(gameObject.transform.parent.parent.gameObject);
                    }
                    else if (parentHotspot.MenuCommands[buttonNumber] == "Use" && interaction == "Use")
                    {//Trigger event
                        Debug.Log("Interaction Use");
                        parentHotspotData.gameObject.SendMessage("Use");
                        parentHotspotData.menuOpen = false;
                        controll.menuOpen = false;
                        Destroy(gameObject.transform.parent.parent.gameObject);
                        //add more commands here and in hotspots
                    }
                    else if (parentHotspot.MenuCommands[buttonNumber] == "Talk" && interaction == "Talk")
                    {//Trigger event
                        Debug.Log("Interaction Talk");
                        parentHotspotData.gameObject.SendMessage("Talk");
                        parentHotspotData.menuOpen = false;
                        controll.menuOpen = false;
                        Destroy(gameObject.transform.parent.parent.gameObject);
                        //add more commands here and in hotspots
                    }
                    else if (parentHotspot.MenuCommands[buttonNumber] == null)
                    {//Trigger event
                        Debug.Log("Nulll");
                        parentHotspotData.menuOpen = false;
                        controll.menuOpen = false;
                        Destroy(gameObject.transform.parent.parent.gameObject);
                        //add more commands here and in hotspots
                    }
                }
            }
        }
#endif

    }

}
/*	while (agent.velocity.x > 0f){
				Debug.Log("Open3");
				gameObject.transform.parent.parent.transform.position = Camera.main.WorldToScreenPoint (parentHotspotData.transform.position) + new Vector3(0,50,0);
			}*/
