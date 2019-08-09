using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTrans;
    public float speedCoefficient;
    Vector3 lastpos;
    // Start is called before the first frame update
    void Start()
    {
        lastpos = transform.position + cameraTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= ((lastpos - cameraTrans.position) * speedCoefficient);
        lastpos = cameraTrans.position;

    }
}
