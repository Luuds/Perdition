using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
public class PlayerTextController : MonoBehaviour {
	GameObject player ;
	Text text;
	int currentlyDisplayingText;
	List <string> playerSays = new List <string>(); 
	bool fade,ongoing; 
	float t; 
	Color a = new Color32(255,255,255,255);
	Color b = new Color32(255,255,255,0);
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player"); 
		text = GetComponent<Text>(); 
	
	}
	public void MakePlayerSay(List <string> descriptions, int curDesc){
	if(!ongoing){
	playerSays = descriptions; 
	currentlyDisplayingText = curDesc; 
	StartCoroutine(AnimateText());
	}
	}
 IEnumerator AnimateText(){ //you might havve to do something here or in the vicinity if you want the player to be able to interupt th text.
     text.color = a; 
	 float wait= 0.02f; 
	 fade= false;
	 ongoing= true; 
	 string stringEval=""; 
     for (int i = 0; i < (playerSays[currentlyDisplayingText].Length+1); i++){
		 wait= 0.02f;
	 		if(i!=playerSays[currentlyDisplayingText].Length){stringEval= playerSays[currentlyDisplayingText].Substring(i, 1); 
     	if(stringEval == " "|| stringEval=="!"||stringEval=="."||stringEval==","||stringEval=="?") {
		 wait=0.1f; 
	 }else{wait = 0.02f; }}
         text.text = playerSays[currentlyDisplayingText].Substring(0, i);
         yield return new WaitForSeconds(wait);
     }
	 yield return new WaitForSeconds(1.1f);
	 ongoing= false;   
	 fade= true; 
 }
 void FixedUpdate(){
	 transform.position = Camera.main.WorldToScreenPoint (player.transform.position) + new Vector3(0,350,0);
	 if(fade){

		t += 0.65f * Time.deltaTime;
		text.color = Color.Lerp(a,b,t); 
		if(text.color == b){
			fade=false;
			StopCoroutine(AnimateText());  
			text.text = ""; 
			t=0;
		}
	 }
 }
}
