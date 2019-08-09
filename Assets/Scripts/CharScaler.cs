using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class CharScaler : MonoBehaviour
{
    public float  sizeMin, sizeMax;
    public Transform min, max; 
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player Image").transform;
       // sizeMax = playerTransform.localScale.y; 
    }

    // Update is called once per frame
    void Update()
    {
        float size  = (((playerTransform.position.z - min.position.z)*(sizeMax-sizeMin))/(max.position.z - min.position.z)) +sizeMin;
        // float size = ((playerTransform.position.z) * (sizeMax - sizeMin)) + sizeMin;  
        //float size = (((playerTransform.position.z - min) * (sizeMax - sizeMin)) / max - min) + sizeMin;
        playerTransform.localScale = new Vector3(size, size, size);
       /* if (size < sizeMin) {
            playerTransform.localScale = new Vector3(sizeMin, sizeMin, sizeMin);
        }
        else if  (size > sizeMax)
        {
            playerTransform.localScale = new Vector3(sizeMax, sizeMax, sizeMax);
        }*/
    }
}
