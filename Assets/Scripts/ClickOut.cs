using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickOut : MonoBehaviour,IPointerClickHandler {

	// Use this for initialization
	void Start () {
		transform.SetAsFirstSibling(); 
	}
	public void OnPointerClick(PointerEventData eventData){
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>().menuOpen = false; 
		Destroy(transform.parent.gameObject); 
	}
	// Update is called once per frame
	void Update () {
		
	}
}
