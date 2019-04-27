using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharScaler : MonoBehaviour
{
    public float max, min, sizeMin, sizeMax;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player Image").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        float size = (((playerTransform.position.z - min) * (sizeMax - sizeMin)) / max - min) + sizeMin;
        playerTransform.localScale = new Vector3(size, size, size); 
        
    }
}
