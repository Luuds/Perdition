using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseManagementMain : MonoBehaviour {
	public List <bool> activeButtons = new List<bool>(); 
	public List <int> buttons = new List<int>();
    public bool fiveCheck ; 
	int buttonCalc, minuteAmount; 
	int buttonNumber=10;
	public Text effText;
    float currentEff, dispEff, efficiance;
    public float stabilizerEff = 15f;
    int fiveMin;
    // Use this for initialization
    void Start () {
		CreateButtons(buttonNumber);
        stabilizerEff = 15f;
        fiveCheck = true; 
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
		
		}
		}

		for (int i = 0; i < buttonNumber; i++){
			if(activeButtons[i]==true){
				buttonCalc++; 
				
				 
			}
			
		}
		minuteAmount++;
        fiveMin++;
        if (fiveMin >= 7&& fiveCheck) {
            stabilizerEff -= Random.Range(0, 1.5f);
            fiveMin = 0; 
        }

		efficiance= ((((float)buttonCalc/(float)minuteAmount)*2f)+10f + stabilizerEff)/100f;
		//Debug.Log(efficiance); 
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
		dispEff =  (10f + (currentEff*2) + stabilizerEff);
		currentEff = 0; 
	
	}
}
