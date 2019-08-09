using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressureRegulatorScript : MonoBehaviour 
{
    GameObject handle;
    PressureHandleScript handleScript;
    GameObject bar;
    float fillamount =0f;
    public float coolantTotal;
    public float fillTime = 1f; 
    
    // Start is called before the first frame update
    void Start()
    {
        handle = transform.GetChild(3).gameObject;
        bar = transform.GetChild(4).GetChild(0).GetChild(0).gameObject;
        handleScript = handle.GetComponent<PressureHandleScript>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    { if (coolantTotal > 0) { fillamount = coolantTotal /400; }
        else { fillamount = 0; }
        if (bar.GetComponent<Image>().fillAmount < fillamount)
        {
            bar.GetComponent<Image>().fillAmount += fillTime * Time.deltaTime;
        }
    }
}
