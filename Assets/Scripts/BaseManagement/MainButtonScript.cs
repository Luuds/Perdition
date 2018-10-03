using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonScript : MonoBehaviour {

public int buttonAmount = 10; 
public int buttonsValue;
public List <GameObject> buttons= new List <GameObject>();  
GameObject panel; 
GameObject button; 
	// Use this for initialization
	void Start () {
		panel = transform.GetChild(1).gameObject;
		button = Resources.Load<GameObject> ("Prefab/BaseButton") ;
		for(int i = 0;i<buttonAmount;i++){
			buttons.Add(Instantiate(button)); 
			buttons [i].transform.SetParent (panel.transform); 
			buttons [i].transform.localScale = Vector3.one; 
			buttons [i].GetComponent<BaseManageButtons>().meInt = i; 
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
