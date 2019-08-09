using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Spine.Unity;
using UnityEngine.EventSystems;

public enum OffMeshLinkMoveMethod {
   Teleport,
   Parabola,
   Curve,
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
    Animator anim;
    IEnumerator Start () {
		inv = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryDatabase>(); 
		controll = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>(); 
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
		agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
		playerTextRef = Resources.Load<GameObject>("Prefab/Player_Text");
		playerText = Instantiate(playerTextRef);
		playerText.transform.SetParent(GameObject.FindGameObjectWithTag("Main Canvas").transform, false);
        //playerText.transform.localPosition = new Vector3(0, 200,0);
        playerText.transform.localScale = Vector3.one;
        anim = GameObject.FindGameObjectWithTag("Player Image").GetComponent<Animator>();
        //StartCoroutine(InputListener());
        agent.autoTraverseOffMeshLink = false;
     while (true) {
       if (agent.isOnOffMeshLink) {
                Vector3 startPos = agent.transform.position;
                OffMeshLinkData data = agent.currentOffMeshLinkData;
                Vector3 endPos = data.endPos + Vector3.forward * agent.baseOffset;
                if (endPos.x > startPos.x)
                {
                    anim.transform.gameObject.GetComponent<SkeletonMecanim>().Skeleton.ScaleX = 1;
                }
                else { anim.transform.gameObject.GetComponent<SkeletonMecanim>().Skeleton.ScaleX = -1; }

                if (method == OffMeshLinkMoveMethod.Parabola)
                anim.SetBool("Stop",true);
                anim.SetBool("Jump",true);
               
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("Jump", false);
                yield return StartCoroutine (Parabola (agent, jumpHeight,jumpDuration));
                anim.SetBool("Land", true);
               // anim.SetBool("Stop", false);
                yield return new WaitForSeconds(0.1f);
                agent.CompleteOffMeshLink ();
                anim.SetBool("Land", false);


            }
       yield return null;
     }
	 
	}

    IEnumerator Parabola (NavMeshAgent agent, float height, float duration) {
     OffMeshLinkData data = agent.currentOffMeshLinkData;
     Vector3 startPos = agent.transform.position;
     Vector3 endPos = data.endPos + Vector3.forward*agent.baseOffset;
     float normalizedTime = 0.0f;
     
     while (normalizedTime < 1.0f) {
		 if(startPos.z+1 < endPos.z){
			float yOffset = (height*0.9f) * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       		agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.forward;
 			normalizedTime += Time.deltaTime / (duration+0.1f);
	   } else if (startPos.z> endPos.z+1){
		    float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       		agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.forward;
		  	normalizedTime += Time.deltaTime / duration;
              
            }
            else{
		float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       	agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.forward;
		normalizedTime += Time.deltaTime / duration;}
            


            yield return null;
            

        }
   }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    void FixedUpdate()
    {
#if UNITY_ANDROID
        if (!controll.menuOpen)
        {
          if   (Input.GetMouseButton(0) && !IsPointerOverUIObject()){
                interactionName = "";
               // Debug.Log("Homo");
                if ( !controll.menuOpen)
                { //no menu should be open; 
                    NavMeshHit myHit;
                    NavMeshHit myHit1;
                    NavMeshHit myHit2;
                    Vector3 positionOnNavMesh = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    Vector3 positionOnNavMeshPlus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    Vector3 positionOnNavMeshMinus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    NavMesh.SamplePosition(positionOnNavMeshPlus, out myHit1, 6f, -1);
                    NavMesh.SamplePosition(positionOnNavMeshMinus, out myHit2, 6f, -1);
                    NavMesh.SamplePosition(positionOnNavMesh, out myHit, 6f, -1);
                    if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) < Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                    {
                        Vector3 positionNew = new Vector3(myHit1.position.x, myHit1.position.y, myHit1.position.z);
                        agent.SetDestination(positionNew);

                    }
                    else if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) > Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                    {
                        Vector3 positionNew = new Vector3(myHit2.position.x, myHit2.position.y, myHit2.position.z);
                        agent.SetDestination(positionNew);

                    }
                    else
                    {
                        Vector3 positionNew = new Vector3(myHit.position.x, myHit.position.y, myHit.position.z);
                        agent.SetDestination(positionNew);

                    }
                }
            }
        
    }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
        if (!controll.menuOpen)
        {

            if (Input.GetMouseButtonUp(0) && Gamecontroller.IsPointerOverGameObject())
            {
               
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var go in raycastResults)
                    {
                        if (go.gameObject.layer == LayerMask.NameToLayer("UI"))
                        {

                            return;
                        }
                        else if (go.gameObject.layer == LayerMask.NameToLayer("HotspotLayer"))
                        {
                            interactionName = "";

                            if ((!inv.invOpen_main_inv && !controll.menuOpen) && (!controll.menuOpen || !inv.invOpen_main_inv))
                            { //no menu should be open; 
                                NavMeshHit myHit;
                                NavMeshHit myHit1;
                                NavMeshHit myHit2;
                                Vector3 positionOnNavMesh = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                                Vector3 positionOnNavMeshPlus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                                Vector3 positionOnNavMeshMinus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                                NavMesh.SamplePosition(positionOnNavMeshPlus, out myHit1, 6f, -1);
                                NavMesh.SamplePosition(positionOnNavMeshMinus, out myHit2, 6f, -1);
                                NavMesh.SamplePosition(positionOnNavMesh, out myHit, 6f, -1);
                                if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) < Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                                {
                                    Vector3 positionNew = new Vector3(myHit1.position.x, myHit1.position.y, myHit1.position.z);
                                    agent.SetDestination(positionNew);

                                }
                                else if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) > Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                                {
                                    Vector3 positionNew = new Vector3(myHit2.position.x, myHit2.position.y, myHit2.position.z);
                                    agent.SetDestination(positionNew);

                                }
                                else
                                {
                                    Vector3 positionNew = new Vector3(myHit.position.x, myHit.position.y, myHit.position.z);
                                    agent.SetDestination(positionNew);

                                }
                            }
                        }
                    }

                }


            }
            else if (Input.GetMouseButton(0) && !Gamecontroller.IsPointerOverGameObject())
            {
                interactionName = "";
              
                if (!controll.menuOpen)
                { //no menu should be open; 
                    NavMeshHit myHit;
                    NavMeshHit myHit1;
                    NavMeshHit myHit2;
                    Vector3 positionOnNavMesh = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    Vector3 positionOnNavMeshPlus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    Vector3 positionOnNavMeshMinus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    NavMesh.SamplePosition(positionOnNavMeshPlus, out myHit1, 6f, -1);
                    NavMesh.SamplePosition(positionOnNavMeshMinus, out myHit2, 6f, -1);
                    NavMesh.SamplePosition(positionOnNavMesh, out myHit, 6f, -1);
                    if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) < Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                    {
                        Vector3 positionNew = new Vector3(myHit1.position.x, myHit1.position.y, myHit1.position.z);
                        agent.SetDestination(positionNew);

                    }
                    else if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) > Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                    {
                        Vector3 positionNew = new Vector3(myHit2.position.x, myHit2.position.y, myHit2.position.z);
                        agent.SetDestination(positionNew);

                    }
                    else
                    {
                        Vector3 positionNew = new Vector3(myHit.position.x, myHit.position.y, myHit.position.z);
                        agent.SetDestination(positionNew);

                    }
                }
            }
        }
#endif
    }
    /* if (Gamecontroller.IsPointerOverGameObject())

                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hotspotHit = false;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("HotspotLayer"))
                {
                    hotspotHit = true;
                }

                else
                {
                    hotspotHit = false;
                }
            }
            if (!hotspotHit)
            {
                interactionName = "";

                if ((!inv.invOpen_main_inv && !controll.menuOpen) && (!controll.menuOpen || !inv.invOpen_main_inv))
                { //no menu should be open; 
                    NavMeshHit myHit;
                    NavMeshHit myHit1;
                    NavMeshHit myHit2;
                    Vector3 positionOnNavMesh = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    Vector3 positionOnNavMeshPlus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    Vector3 positionOnNavMeshMinus = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                    NavMesh.SamplePosition(positionOnNavMeshPlus, out myHit1, 6f, -1);
                    NavMesh.SamplePosition(positionOnNavMeshMinus, out myHit2, 6f, -1);
                    NavMesh.SamplePosition(positionOnNavMesh, out myHit, 6f, -1);
                    if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) < Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                    {
                        Vector3 positionNew = new Vector3(myHit1.position.x, myHit1.position.y, myHit1.position.z);
                        agent.SetDestination(positionNew);

                    }
                    else if (Vector3.Distance(myHit1.position, positionOnNavMeshPlus) > Vector3.Distance(myHit2.position, positionOnNavMeshMinus))
                    {
                        Vector3 positionNew = new Vector3(myHit2.position.x, myHit2.position.y, myHit2.position.z);
                        agent.SetDestination(positionNew);

                    }
                    else
                    {
                        Vector3 positionNew = new Vector3(myHit.position.x, myHit.position.y, myHit.position.z);
                        agent.SetDestination(positionNew);

                    }
                }
            }
        }
   */

    void OnDestroy(){
		Destroy(playerText); 
	}
    

}

