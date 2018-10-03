using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
public class BaseManageButtons : MonoBehaviour, IPointerClickHandler {
	public int meInt; 
	bool activeButton; 
	// Use this for initialization
	void Start () {
		this.gameObject.name = "Button_"+ meInt.ToString(); 
	}
	public void OnPointerClick (PointerEventData eventData){	
		if(activeButton){
		this.gameObject.GetComponent<Image>().color = new Color32 (255,255,255,255) ;
		activeButton =false;  
		}else{
		this.gameObject.GetComponent<Image>().color = new Color32 (150,150,150,255) ;
		activeButton =true; 
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
