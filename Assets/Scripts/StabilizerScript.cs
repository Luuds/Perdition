using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StabilizerScript : MonoBehaviour {
    public int postionInt = 54;
    List<int> twelveObj1 = new List<int>();
    List<int> twelveObj2 = new List<int>();
    List<int> twelveObj = new List<int>();
    List<int> twelveObjRND = new List<int>();
    List<int> twelveObjNewRND = new List<int>();
    public GameObject leftRect;
    public GameObject rightRect;
    RectTransform leftRectTrans;
    RectTransform rightRectTrans;
    GameObject point;
    List<GameObject> pointsLeft = new List<GameObject>();
    List<GameObject> pointsRight = new List<GameObject>();
    // Use this for initialization
    void Start () {
        point = Resources.Load<GameObject>("Prefab/CalibrationPoint");
        EasyCalibration(); 

    }
    void EasyCalibration() {
        leftRectTrans = leftRect.GetComponent<RectTransform>();
        rightRectTrans = rightRect.GetComponent<RectTransform>(); 

        twelveObj1.Add(7);
        twelveObj2.Add(5);

        twelveObj1.Add(4);
        twelveObj2.Add(8);

        twelveObj1.Add(8);
        twelveObj2.Add(4);

        twelveObj1.Add(5);
        twelveObj2.Add(7);

        twelveObj1.Add(5);
        twelveObj2.Add(9);

        twelveObj1.Add(9);
        twelveObj2.Add(5);

        twelveObj1.Add(9);
        twelveObj2.Add(4);

        twelveObj1.Add(4);
        twelveObj2.Add(9);

        twelveObj1.Add(8);
        twelveObj2.Add(5);

        twelveObj1.Add(5);
        twelveObj2.Add(8);

        twelveObj1.Add(6);
        twelveObj2.Add(6);

        for (int i = 0; i < 4; i++)
        {
            twelveObj.Add(Random.Range(0, 11));

        }
        twelveObj.Add(10);
        for (int k = 0; k < 5; k++)
        {
            int h = twelveObj[Random.Range(0, twelveObj.Count)]; 
            twelveObjRND.Add(h);
            twelveObj.Remove(h);
        }
        for (int j = 0; j < twelveObjRND.Count; j++)
        {



                Debug.Log(twelveObj1[twelveObjRND[j]] + "," + twelveObj2[twelveObjRND[j]]);
                if (twelveObj1[twelveObjRND[j]] == twelveObj1[10] && twelveObj2[twelveObjRND[j]] == twelveObj2[10])
            {
                
            }
            else {
               
            }
            
        }
        EasyCalibrationSpwan();


    }
    void EasyCalibrationSpwan() {
        for (int g = 0; g < twelveObj1[twelveObjRND[0]]; g++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));  //new Vector2(Random.Range(leftRectTrans.rect.xMin, leftRectTrans.rect.xMax), Random.Range(leftRectTrans.rect.yMin, leftRectTrans.rect.yMax)); 
            pointsLeft.Add(Instantiate(point));
            pointsLeft[g].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(0));
            pointsLeft[g].transform.localScale = Vector3.one;
            pointsLeft[g].transform.localPosition = position;
            // pointsLeft[g].transform.GetComponent<RectTransform>().anchoredPosition = position;


        }
        for (int i = 0; i < twelveObj2[twelveObjRND[0]]; i++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));  //new Vector2(Random.Range(leftRectTrans.rect.xMin, leftRectTrans.rect.xMax), Random.Range(leftRectTrans.rect.yMin, leftRectTrans.rect.yMax)); 
            pointsRight.Add(Instantiate(point));
            pointsRight[i].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(1));
            pointsRight[i].transform.localScale = Vector3.one;
            pointsRight[i].transform.localPosition = position;
            // pointsLeft[g].transform.GetComponent<RectTransform>().anchoredPosition = position;


        }
         
    }
	// Update is called once per frame
	void Update () {
		
	}
}
