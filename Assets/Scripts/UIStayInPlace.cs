using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStayInPlace : MonoBehaviour {
public Vector3 pos;
public Vector3 offset; 
public bool mouse = false; 
	// Use this for initialization
	void Start () {
	//string myName = gameObject.name;
	//string originName = myName.Replace("_inv",""); 
	//pos = GameObject.Find(originName).transform.position; 	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(mouse){
		transform.position = pos; 
		}else{
		transform.position = Camera.main.WorldToScreenPoint (pos) + offset;
		}
	}
}
