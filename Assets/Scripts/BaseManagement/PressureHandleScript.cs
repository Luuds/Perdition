using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressureHandleScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    public bool dragging = false;
    Rigidbody2D rb2D;
    public float rotationSpeed = 4f; 
    Vector3 orgDragPos;
    Vector3 newDragPos;

    float torque;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

        // Update is called once per frame
      
    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

   

    public void OnBeginDrag(PointerEventData eventData)
        { 
        dragging = true; 
       
    }

        public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.transform.GetComponent<RectTransform>().rotation.eulerAngles.z > 0f) { torque = 45 - this.transform.GetComponent<RectTransform>().rotation.eulerAngles.z; }
        if (this.transform.GetComponent<RectTransform>().rotation.eulerAngles.z > 180f) { torque = (360 - this.transform.GetComponent<RectTransform>().rotation.eulerAngles.z) + 45; }
        Debug.Log(torque);
        if (torque >= 60) 
        {
          rb2D.AddTorque(60, ForceMode2D.Impulse);
        }
        else
        { 
            rb2D.AddTorque(torque +5f, ForceMode2D.Impulse);
        }

        //Debug.Log(this.transform.GetComponent<RectTransform>().rotation.eulerAngles.z);
        transform.parent.GetComponent<PressureRegulatorScript>().coolantTotal += torque; 
        dragging = false; 
            
        }

    void FixedUpdate()
    {
        
        if (dragging)
        {
            float YaxisRotation = Input.GetAxis("Mouse Y") * (rotationSpeed/2);
            float XaxisRotation = Input.GetAxis("Mouse X") * (rotationSpeed/2);
           
            // select the axis by which you want to rotate the GameObject
            transform.Rotate(Vector3.forward, YaxisRotation + XaxisRotation);

        }
        this.transform.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, ClampAngle(this.transform.GetComponent<RectTransform>().rotation.eulerAngles.z, -45, 45));

    }
}
