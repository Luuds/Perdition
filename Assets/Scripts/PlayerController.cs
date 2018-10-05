using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public enum OffMeshLinkMoveMethod {
   Teleport,
   Parabola,
}
public class PlayerController : MonoBehaviour {
	//private float doubleClickTimeLimit = 0.25f;
	public Camera cam;
	public float jumpHeight; 
	public float jumpDuration;
	InventoryDatabase inv; 
	NavMeshAgent agent; 
	public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
	Gamecontroller controll; 
	GameObject playerTextRef,playerText; 
	public string interactionName = ""; 
	// Use this for initialization
	IEnumerator Start () {
		inv = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryDatabase>(); 
		controll = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>(); 
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
		agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
		playerTextRef = Resources.Load<GameObject>("Prefab/Player_Text");
		playerText = Instantiate(playerTextRef);
		playerText.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform, false);
		playerText.transform.localScale = Vector3.one; 

		//StartCoroutine(InputListener());
		agent.autoTraverseOffMeshLink = false;
     while (true) {
       if (agent.isOnOffMeshLink) {
         if (method == OffMeshLinkMoveMethod.Parabola)
           yield return StartCoroutine (Parabola (agent, jumpHeight,jumpDuration));
         agent.CompleteOffMeshLink ();
       }
       yield return null;
     }
	 
	}  
	 IEnumerator Parabola (NavMeshAgent agent, float height, float duration) {
     OffMeshLinkData data = agent.currentOffMeshLinkData;
     Vector3 startPos = agent.transform.position;
     Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
     float normalizedTime = 0.0f;
     while (normalizedTime < 1.0f) {
		 if(startPos.y+1 < endPos.y){
			float yOffset = (height*0.9f) * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       		agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
 			normalizedTime += Time.deltaTime / (duration+0.1f);
	   } else if (startPos.y> endPos.y+1){
		    float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       		agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
		  	normalizedTime += Time.deltaTime / duration;
	   }else{
		float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       	agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
		normalizedTime += Time.deltaTime / duration;}
    
	   
     
       yield return null;
     }
   }
/*
	private IEnumerator InputListener() 
{
    while(enabled)
    { //Run as long as this is activ

        if(Input.GetMouseButtonDown(0))
            yield return ClickEvent();

        yield return null;
    }
}
	private IEnumerator ClickEvent()
{
    //pause a frame so you don't pick up the same mouse down event.
    yield return new WaitForEndOfFrame();

    float count = 0f;
    while(count < doubleClickTimeLimit)
    {
        if(Input.GetMouseButtonDown(0))
        {
            DoubleClick();
            yield break;
        }
        count += Time.deltaTime;// increment counter by change in time between frames
        yield return null; // wait for the next frame
    }
   SingleClick();
	}

	private void SingleClick()
	{}*/
	void Update(){
	 if( (Input.GetMouseButton(0))){	
		Ray ray =Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool hotspotHit= false; 
		if(Physics.Raycast (ray, out hit)){
		if(hit.collider.gameObject.layer == LayerMask.NameToLayer("HotspotLayer")) {
			hotspotHit=true;
		}
		else{
			hotspotHit = false; 	
			}
		}
		if(!hotspotHit){
		interactionName=""; 	
		if((!inv.invOpen_main_inv&&!controll.menuOpen)&&(!controll.menuOpen||!inv.invOpen_main_inv)){ //no menu should be open; 
			NavMeshHit myHit;
			NavMeshHit myHit1;
			NavMeshHit myHit2;
			Vector3 positionOnNavMesh =  new Vector3 (cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0f );
			Vector3 positionOnNavMeshPlus = new Vector3 (cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0.2f );  
			Vector3 positionOnNavMeshMinus = new Vector3 (cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, -0.2f );
			NavMesh.SamplePosition(positionOnNavMeshPlus,out myHit1, 6f, -1);
			NavMesh.SamplePosition(positionOnNavMeshMinus,out myHit2, 6f, -1);
			NavMesh.SamplePosition(positionOnNavMesh,out myHit, 6f, -1);
			if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus)<Vector3.Distance(myHit2.position,positionOnNavMeshMinus))
			{	
				Vector3 positionNew = new Vector3 (myHit1.position.x,myHit1.position.y, myHit1.position.z);
				agent.SetDestination( positionNew); 
			
			}
			else if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus)>Vector3.Distance(myHit2.position,positionOnNavMeshMinus))
			{	
				Vector3 positionNew = new Vector3 (myHit2.position.x,myHit2.position.y, myHit2.position.z);
				agent.SetDestination( positionNew); 
			
			} else{
				Vector3 positionNew = new Vector3 (myHit.position.x,myHit.position.y, myHit.position.z);
				agent.SetDestination( positionNew); 
				
			}
  		}
	}
	}
	}
	void OnDestroy(){
		Destroy(playerText); 
	}
	private void DoubleClick()
	
	{	
	}

}

