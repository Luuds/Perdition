using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainScript : MonoBehaviour {
public Transform target;
public float smoothSpeed;
public Vector3 offset; 
Vector3 velocity; 
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	Vector3	desiredPosition = target.position + offset; 
	Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position,desiredPosition,ref velocity,smoothSpeed);
	transform.position= smoothedPosition; 
	}
}
