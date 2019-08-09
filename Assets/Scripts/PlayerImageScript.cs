using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class PlayerImageScript : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position; 
        
    }
}
