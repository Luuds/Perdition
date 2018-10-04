using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseManagementMain : MonoBehaviour {
	public List <bool> activeButtons = new List<bool>(); 
	public List <int> buttons = new List<int>();
	int buttonCalc, minuteAmount; 
	int buttonNumber=10;
	public Text effText; 
	float currentEff,dispEff,buttonEff; 
	// Use this for initialization
	void Start () {
		CreateButtons(buttonNumber); 
		
	}
	void CreateButtons(int buttonNumber){
		for(int i = 0; i<buttonNumber;i++){
			buttons.Add(i);
			activeButtons.Add(true);
			
		}
	}
	public void CheckingTime(){
	 
		for(int i = 0; i<buttonNumber;i++){
			if(activeButtons[i]){
			int rndNumber = Random.Range(0,100); 
			if(rndNumber==9 && activeButtons[i]){
				activeButtons[i] = false; 
		
			}
			//Debug.Log (activeButtons[i].ToString());
		}
		}
		//Debug.Log ("Gay");
		for (int i = 0; i < buttonNumber; i++){
			if(activeButtons[i]==true){
				buttonCalc++; 
				
				 
			}
			
		}
		minuteAmount++;
		buttonEff= ((((float)buttonCalc/(float)minuteAmount)*2f)+20f)/100f;
		
	}
	public void CheckingHour(){

	}
	// Update is called once per frame
	void Update () {
	effText.text = dispEff.ToString("0") + " %";
		for (int i = 0; i < buttonNumber; i++){
			if(activeButtons[i]==true){
		
				currentEff++;
				
			}
			
		}
		dispEff =  (20f + (currentEff*2));
		currentEff = 0; 
	
	}
}
