using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
public class BaseManageButtons : MonoBehaviour, IPointerClickHandler {
	public int meInt; 
	bool activeButton = true; 
	BaseManagementMain mainScript; 
	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>().menuOpen = true; 
		this.gameObject.name = "Button_"+ meInt.ToString(); 
		mainScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseManagementMain>(); 
		activeButton=mainScript.activeButtons[meInt];
		if(!activeButton){
		this.gameObject.GetComponent<Image>().color = new Color32 (150,150,150,255) ;	
		}else{
		this.gameObject.GetComponent<Image>().color = new Color32 (255,255,255,255) ;
		}
			StartCoroutine(CheckIfActive()); 
	}
	IEnumerator CheckIfActive(){
	while (true){
		yield return new WaitForSeconds(2); 
		activeButton=mainScript.activeButtons[meInt];
		if(!activeButton){
		this.gameObject.GetComponent<Image>().color = new Color32 (150,150,150,255) ;	
		}else{
		this.gameObject.GetComponent<Image>().color = new Color32 (255,255,255,255) ;
		}
			
		}
	}
	public void OnPointerClick (PointerEventData eventData){	
		if(activeButton){
		this.gameObject.GetComponent<Image>().color = new Color32 (150,150,150,255) ;
		mainScript.activeButtons[meInt] =false; 
		activeButton =false;  
		}else{
		this.gameObject.GetComponent<Image>().color = new Color32 (255,255,255,255) ;
		
		mainScript.activeButtons[meInt] =true;
		activeButton =true; 
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
