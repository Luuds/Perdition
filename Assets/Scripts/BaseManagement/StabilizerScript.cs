using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StabilizerScript : MonoBehaviour {
    GameObject controll;
    BaseManagementMain baseMainScript; 
    public int postionInt = 54;
    List<int> twelveObj1 = new List<int>();
    List<int> twelveObj2 = new List<int>();
    List<int> twelveObj3 = new List<int>();
    List<int> twelveObj4 = new List<int>();
    List<int> twelveObj = new List<int>();
    List<int> twelveObjRND = new List<int>();
    List<int> twelveObjNewRND = new List<int>();
    GameObject point;
    GameObject evenButton;
    GameObject unevenButton;
    GameObject evenButtonRef;
    GameObject unevenButtonRef;
    GameObject square;
    GameObject squareRefL;
    GameObject squareRefR;
    List<GameObject> pointsLeft = new List<GameObject>();
    List<GameObject> pointsRight = new List<GameObject>();
    List<Vector2> pointsLeftTarget = new List<Vector2>();
    List<Vector2> pointsRightTarget = new List<Vector2>();
    GameObject squareRefLD;
    GameObject squareRefRD;
    GameObject countdownLight;
    List<GameObject> pointsLeftD = new List<GameObject>();
    List<GameObject> pointsRightD = new List<GameObject>();
    List<Vector2> pointsLeftTargetD = new List<Vector2>();
    List<Vector2> pointsRightTargetD = new List<Vector2>();
    List<GameObject> lights = new List<GameObject>();
    // Use this for initialization
    int counter = 0;
    int timeCounter;
    bool pressed = false;
    bool pressedComplex = false;
    bool correct = false;
    public int success;
    float speed = 0.4f;
    private Vector2 velocity = Vector2.zero;
    Color colorNew = new Color(255, 255, 255, 0);
    float t = 0;
    float duration = 3; 


    void Start () {
        controll = GameObject.FindGameObjectWithTag("GameController");
        baseMainScript = controll.GetComponent<BaseManagementMain>(); 
        point = Resources.Load<GameObject>("Prefab/StabilizerUI/CalibrationPoint");
        evenButtonRef = Resources.Load<GameObject>("Prefab/StabilizerUI/CalibButtonEven");
        unevenButtonRef = Resources.Load<GameObject>("Prefab/StabilizerUI/CalibButtonUneven");
        square= Resources.Load<GameObject>("Prefab/StabilizerUI/Square");
        countdownLight= Resources.Load<GameObject>("Prefab/StabilizerUI/CountDownLight");
        // EasyCalibration(); 

    }
    public void FullCalibration()
    {
        controll.GetComponent<BaseManagementMain>().fiveCheck = false;
        counter = 0; 
        StartCoroutine(FullCalibrationNumerator(3));
        transform.GetChild(3).GetComponent<Button>().interactable = false;
        transform.GetChild(4).GetComponent<Button>().interactable = false;
    }
   IEnumerator FullCalibrationNumerator( int delay)
    {
        timeCounter = 8;
        for (int l = 0; l < timeCounter; l++)
        {
            lights.Add(Instantiate(countdownLight));
            lights[l].transform.SetParent(transform.GetChild(5));
            lights[l].transform.localScale = Vector3.one;
            lights[l].GetComponent<Image>().color = new Color(255,255,255,0);
        }
        twelveObj1.Clear();
        twelveObj2.Clear();
        twelveObj3.Clear();
        twelveObj4.Clear();
        twelveObjRND.Clear();
        twelveObj.Clear();
        squareRefL = Instantiate(square);
        squareRefR = Instantiate(square);
        squareRefLD = Instantiate(square);
        squareRefRD = Instantiate(square);
        squareRefL.transform.SetParent(transform.GetChild(1).GetChild(0));
        squareRefR.transform.SetParent(transform.GetChild(1).GetChild(0));
        squareRefL.transform.localScale = Vector3.one;
        squareRefR.transform.localScale = Vector3.one;
        squareRefLD.transform.SetParent(transform.GetChild(1).GetChild(0));
        squareRefRD.transform.SetParent(transform.GetChild(1).GetChild(0));
        squareRefLD.transform.localScale = Vector3.one;
        squareRefRD.transform.localScale = Vector3.one;
        evenButton = Instantiate(evenButtonRef);
        unevenButton = Instantiate(unevenButtonRef);
        evenButton.transform.SetParent(transform.GetChild(2));
        unevenButton.transform.SetParent(transform.GetChild(2));
        unevenButton.transform.localScale = Vector3.one;
        evenButton.transform.localScale = Vector3.one;
 
        evenButton.GetComponent<Button>().onClick.AddListener(() => PressedEvenComplex());
        unevenButton.GetComponent<Button>().onClick.AddListener(() => PressedUnevenComplex());

        twelveObj1.Add(4);
        twelveObj2.Add(5);
        twelveObj3.Add(3);
        twelveObj4.Add(4); 

        twelveObj1.Add(4);
        twelveObj2.Add(3);
        twelveObj3.Add(3);
        twelveObj4.Add(2);

        twelveObj1.Add(8);
        twelveObj2.Add(3);
        twelveObj3.Add(9);
        twelveObj4.Add(8);

        twelveObj1.Add(6);
        twelveObj2.Add(7);
        twelveObj3.Add(5);
        twelveObj4.Add(8);
        
        twelveObj1.Add(4);
        twelveObj2.Add(5);
        twelveObj3.Add(7);
        twelveObj4.Add(6);

        twelveObj1.Add(6);
        twelveObj2.Add(4);
        twelveObj3.Add(3);
        twelveObj4.Add(6);

        twelveObj1.Add(4);
        twelveObj2.Add(3);
        twelveObj3.Add(5);
        twelveObj4.Add(5);

        twelveObj1.Add(5);
        twelveObj2.Add(5);
        twelveObj3.Add(6);
        twelveObj4.Add(4);

        twelveObj1.Add(4);
        twelveObj2.Add(4);
        twelveObj3.Add(4);
        twelveObj4.Add(4);

        twelveObj1.Add(6);
        twelveObj2.Add(6);
        twelveObj3.Add(6);
        twelveObj4.Add(6);

        twelveObj1.Add(5);
        twelveObj2.Add(5);
        twelveObj3.Add(5);
        twelveObj4.Add(5);
        for (int i = 0; i < 7; i++)
        {
            twelveObj.Add(Random.Range(0, 11));

        }
        twelveObj.Add(10);
        for (int k = 0; k < 8; k++)
        {
            int h = twelveObj[Random.Range(0, twelveObj.Count)];
            twelveObjRND.Add(h);
            twelveObj.Remove(h);
        }

        yield return new WaitForSeconds(delay);
        t = 0;
        StartCoroutine(FullCalibrationSpwan(counter));
        yield return null;

    }
    IEnumerator FullCalibrationSpwan(int number)
    {   if (timeCounter == 8)
        {
            for (int l = 0; l < lights.Count; l++)
            {
                lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }
        InvokeRepeating("CountDown", 1f, 1f);
        for (int g = 0; g < twelveObj1[twelveObjRND[number]]; g++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));
            pointsLeftTarget.Add(new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt)));
            pointsLeft.Add(Instantiate(point));
            pointsLeft[g].GetComponent<Image>().color = colorNew;

            pointsLeft[g].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(0));
            pointsLeft[g].transform.localScale = Vector3.one;
            pointsLeft[g].transform.localPosition = position;
            // pointsLeft[g].transform.GetComponent<RectTransform>().anchoredPosition = position;


        }
        for (int i = 0; i < twelveObj2[twelveObjRND[number]]; i++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));
            pointsRightTarget.Add(new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt)));
            pointsRight.Add(Instantiate(point));
            pointsRight[i].GetComponent<Image>().color = colorNew;
            pointsRight[i].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(1));
            pointsRight[i].transform.localScale = Vector3.one;
            pointsRight[i].transform.localPosition = position;



        }
        for (int g = 0; g < twelveObj3[twelveObjRND[number]]; g++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));
            pointsLeftTargetD.Add(new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt)));
            pointsLeftD.Add(Instantiate(point));
            pointsLeftD[g].GetComponent<Image>().color = colorNew;

            pointsLeftD[g].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(2));
            pointsLeftD[g].transform.localScale = Vector3.one;
            pointsLeftD[g].transform.localPosition = position;
            // pointsLeft[g].transform.GetComponent<RectTransform>().anchoredPosition = position;


        }
        for (int i = 0; i < twelveObj4[twelveObjRND[number]]; i++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));
            pointsRightTargetD.Add(new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt)));
            pointsRightD.Add(Instantiate(point));
            pointsRightD[i].GetComponent<Image>().color = colorNew;
            pointsRightD[i].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(3));
            pointsRightD[i].transform.localScale = Vector3.one;
            pointsRightD[i].transform.localPosition = position;



        }


        yield return new WaitWhile(waitingForComplexInput);

        if (correct && pressedComplex)
        {

            success++;
            Debug.Log(success);
        }
        else if (!correct && pressedComplex)
        {
           
            Debug.Log(success);

        }
        else
        {
            
            Debug.Log(success);
        }


        counter++;
        CancelInvoke("CountDown");
        timeCounter = 8;
        pressedComplex = false;
        correct = false;
        for (int k = 0; k < pointsRight.Count; k++)
        {
            Destroy(pointsRight[k]);
        }
        for (int f = 0; f < pointsLeft.Count; f++)
        {
            Destroy(pointsLeft[f]);
        }
        for (int k = 0; k < pointsRightD.Count; k++)
        {
            Destroy(pointsRightD[k]);
        }
        for (int f = 0; f < pointsLeftD.Count; f++)
        {
            Destroy(pointsLeftD[f]);
        }
        pointsLeft.Clear();
        pointsRight.Clear();
        pointsLeftD.Clear();
        pointsRightD.Clear();
        pointsLeftTarget.Clear();
        pointsRightTarget.Clear();
        pointsLeftTargetD.Clear();
        pointsRightTargetD.Clear();
        // lights[0].GetComponent<Image>().color = new Color(255,255,255,0); 

        if (counter <8)
        {
            controll.GetComponent<BaseManagementMain>().stabilizerEff += success * 5;
            if (controll.GetComponent<BaseManagementMain>().stabilizerEff >= 40f) { controll.GetComponent<BaseManagementMain>().stabilizerEff = 40f; }
            success = 0;
            evenButton.GetComponent<Button>().interactable = false;
            unevenButton.GetComponent<Button>().interactable = false;

            yield return new WaitForSeconds(2f);
            evenButton.GetComponent<Image>().color = Color.white;
            unevenButton.GetComponent<Image>().color = Color.white;
            evenButton.GetComponent<Button>().interactable = true;
            unevenButton.GetComponent<Button>().interactable = true;
            t = 0;
            StartCoroutine(FullCalibrationSpwan(counter));
           
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Done");
            evenButton.GetComponent<Button>().onClick.RemoveListener(PressedEvenComplex);
            unevenButton.GetComponent<Button>().onClick.RemoveListener(PressedUnevenComplex);
            Destroy(evenButton);
            Destroy(unevenButton);
            Destroy(squareRefL);
            Destroy(squareRefR);
            Destroy(squareRefLD);
            Destroy(squareRefRD);
            for (int f = 0; f < lights.Count; f++)
            {
                Destroy(lights[f]);
            }
            t = 0;
            lights.Clear(); 
            transform.GetChild(3).GetComponent<Button>().interactable = true;
            transform.GetChild(4).GetComponent<Button>().interactable = true;
            controll.GetComponent<BaseManagementMain>().stabilizerEff += success * 5;
            if (controll.GetComponent<BaseManagementMain>().stabilizerEff>= 40f) { controll.GetComponent<BaseManagementMain>().stabilizerEff = 40f;}
                success = 0;
            controll.GetComponent<BaseManagementMain>().fiveCheck =true;

        }
        if (controll.GetComponent<BaseManagementMain>().stabilizerEff >= 40f)
        {
            yield return new WaitForSeconds(0.4f);
            Debug.Log("Done");
            evenButton.GetComponent<Button>().onClick.RemoveListener(PressedEvenComplex);
            unevenButton.GetComponent<Button>().onClick.RemoveListener(PressedUnevenComplex);
            Destroy(evenButton);
            Destroy(unevenButton);
            Destroy(squareRefL);
            Destroy(squareRefR);
            Destroy(squareRefLD);
            Destroy(squareRefRD);
            for (int f = 0; f < lights.Count; f++)
            {
                Destroy(lights[f]);
            }
            t = 0;
            lights.Clear();
            transform.GetChild(3).GetComponent<Button>().interactable = true;
            transform.GetChild(4).GetComponent<Button>().interactable = true;
            controll.GetComponent<BaseManagementMain>().stabilizerEff += success * 5;
            if (controll.GetComponent<BaseManagementMain>().stabilizerEff >= 40f) { controll.GetComponent<BaseManagementMain>().stabilizerEff = 40f; }
            success = 0;
            controll.GetComponent<BaseManagementMain>().fiveCheck = true;
        }

    }

    public void EasyCalibration()

    {
        controll.GetComponent<BaseManagementMain>().fiveCheck = false;
        counter = 0;
        StartCoroutine(EasyCalibrationNumerator(3));
        transform.GetChild(3).GetComponent<Button>().interactable = false;
        transform.GetChild(4).GetComponent<Button>().interactable = false;
    }

    IEnumerator EasyCalibrationNumerator(int delay) {
        timeCounter = 6;
        for (int l = 0; l < timeCounter; l++)
        {
            lights.Add(Instantiate(countdownLight));
            lights[l].transform.SetParent(transform.GetChild(5));
            lights[l].transform.localScale = Vector3.one;
            lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        twelveObj1.Clear();
        twelveObj2.Clear();
        twelveObjRND.Clear();
        twelveObj.Clear();
        transform.GetChild(3).GetComponent<Button>().interactable = false;
        transform.GetChild(4).GetComponent<Button>().interactable = false;

        squareRefL = Instantiate(square);
        squareRefR = Instantiate(square);
        squareRefL.transform.SetParent(transform.GetChild(1).GetChild(0));
        squareRefR.transform.SetParent(transform.GetChild(1).GetChild(0));
        squareRefL.transform.localScale = Vector3.one;
        squareRefR.transform.localScale = Vector3.one;
        evenButton = Instantiate(evenButtonRef);
        unevenButton = Instantiate(unevenButtonRef);
        evenButton.transform.SetParent(transform.GetChild(2));
        unevenButton.transform.SetParent(transform.GetChild(2));
        unevenButton.transform.localScale = Vector3.one;
        evenButton.transform.localScale = Vector3.one;
        evenButton.GetComponent<Button>().onClick.AddListener(() =>PressedEven());
        unevenButton.GetComponent<Button>().onClick.AddListener(() => PressedUneven());

        twelveObj1.Add(7);
        twelveObj2.Add(3);

        twelveObj1.Add(4);
        twelveObj2.Add(8);

        twelveObj1.Add(8);
        twelveObj2.Add(4);

        twelveObj1.Add(5);
        twelveObj2.Add(7);

        twelveObj1.Add(5);
        twelveObj2.Add(8);

        twelveObj1.Add(9);
        twelveObj2.Add(6);

        twelveObj1.Add(5);
        twelveObj2.Add(4);

        twelveObj1.Add(3);
        twelveObj2.Add(4);

        twelveObj1.Add(5);
        twelveObj2.Add(5);

        twelveObj1.Add(8);
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
        yield return new WaitForSeconds(delay);
        t = 0; 
        StartCoroutine (EasyCalibrationSpwan(counter));
        yield return null;
        

    }

    IEnumerator EasyCalibrationSpwan(int number)
    {
        if (timeCounter == 6)
        {
            for (int l = 0; l < lights.Count; l++)
            {
                lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }

        InvokeRepeating("CountDown", 1f, 1f);
        for (int g = 0; g < twelveObj1[twelveObjRND[number]]; g++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));
            pointsLeftTarget.Add(new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt))); 
            pointsLeft.Add(Instantiate(point));
            pointsLeft[g].GetComponent<Image>().color = colorNew; 

            pointsLeft[g].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(0));
            pointsLeft[g].transform.localScale = Vector3.one;
            pointsLeft[g].transform.localPosition = position;
            


        }
        for (int i = 0; i < twelveObj2[twelveObjRND[number]]; i++)
        {
            Vector2 position = new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt));
            pointsRightTarget.Add(new Vector2(Random.Range(-postionInt, postionInt), Random.Range(-postionInt, postionInt)));
            pointsRight.Add(Instantiate(point));
            pointsRight[i].GetComponent<Image>().color = colorNew;
            pointsRight[i].transform.SetParent(transform.GetChild(1).GetChild(0).GetChild(1));
            pointsRight[i].transform.localScale = Vector3.one;
            pointsRight[i].transform.localPosition = position;



        }


        yield return new WaitWhile(waitingForInput);

        if (correct && pressed)
        {
            
            success++;
         
          
        }
        else if (!correct && pressed)
        {
           
        }
        else
        {
            
           
           
        }
        Debug.Log(twelveObj1[twelveObjRND[number]].ToString() + twelveObj2[twelveObjRND[number]].ToString());
        Debug.Log(success);

        counter++;
        CancelInvoke("CountDown");
        timeCounter = 6;
        pressed = false;
        correct = false;
        for (int k = 0; k < pointsRight.Count; k++)
        {
            Destroy(pointsRight[k]);
        }
        for (int f = 0; f < pointsLeft.Count; f++)
        {
            Destroy(pointsLeft[f]);
        }
        pointsLeft.Clear();
        pointsLeftTarget.Clear(); 
        pointsRight.Clear();
        pointsRightTarget.Clear(); 
        lights[0].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        if (counter < 5)
        {
            controll.GetComponent<BaseManagementMain>().stabilizerEff += success * 4;
            if (controll.GetComponent<BaseManagementMain>().stabilizerEff >= 40f) { controll.GetComponent<BaseManagementMain>().stabilizerEff = 40f; }
            success = 0;
           
            evenButton.GetComponent<Button>().interactable = false;
            unevenButton.GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(2f);
            evenButton.GetComponent<Image>().color = Color.white;
            unevenButton.GetComponent<Image>().color = Color.white;
            evenButton.GetComponent<Button>().interactable = true;
            unevenButton.GetComponent<Button>().interactable = true;
            t = 0;
            StartCoroutine(EasyCalibrationSpwan(counter));
         
        }
        else { Debug.Log("Done");
            yield return new WaitForSeconds(0.5f);
            evenButton.GetComponent<Button>().onClick.RemoveListener(PressedEven);
            unevenButton.GetComponent<Button>().onClick.RemoveListener(PressedUneven);
            Destroy(evenButton);
            Destroy(unevenButton);
            Destroy(squareRefL);
            Destroy(squareRefR);
            t = 0;
            for (int f = 0; f < lights.Count; f++)
            {
                Destroy(lights[f]);
            }
            lights.Clear();
            transform.GetChild(3).GetComponent<Button>().interactable = true;
            transform.GetChild(4).GetComponent<Button>().interactable = true;
            controll.GetComponent<BaseManagementMain>().stabilizerEff += success * 4;
            if (controll.GetComponent<BaseManagementMain>().stabilizerEff >= 40f) { controll.GetComponent<BaseManagementMain>().stabilizerEff = 40f; }
            success = 0;
            controll.GetComponent<BaseManagementMain>().fiveCheck = true;
        }
        if (controll.GetComponent<BaseManagementMain>().stabilizerEff >= 40f) { // move this to a new Ienumerator and call it in fixedUpdate
            yield return new WaitForSeconds(0.4f);
            CancelInvoke("CountDown");
            timeCounter = 6;
            pressed = false;
            correct = false;
            for (int k = 0; k < pointsRight.Count; k++)
            {
                Destroy(pointsRight[k]);
            }
            for (int f = 0; f < pointsLeft.Count; f++)
            {
                Destroy(pointsLeft[f]);
            }
            pointsLeft.Clear();
            pointsLeftTarget.Clear();
            pointsRight.Clear();
            pointsRightTarget.Clear();
            evenButton.GetComponent<Button>().onClick.RemoveListener(PressedEven);
            unevenButton.GetComponent<Button>().onClick.RemoveListener(PressedUneven);
            Destroy(evenButton);
            Destroy(unevenButton);
            Destroy(squareRefL);
            Destroy(squareRefR);
            t = 0;
            for (int f = 0; f < lights.Count; f++)
            {
                Destroy(lights[f]);
            }
            lights.Clear();
            transform.GetChild(3).GetComponent<Button>().interactable = true;
            transform.GetChild(4).GetComponent<Button>().interactable = true;
     
        }
        // StopCoroutine(EasyCalibrationSpwan(0)); 

    }

    void PressedEven() {
        for (int l = 0; l < lights.Count; l++)
        {
            lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        pressed = true;

        for (int j = 0; j < twelveObjRND.Count; j++)
        {
           
            if (twelveObj1[twelveObjRND[counter]] == twelveObj1[10] && twelveObj2[twelveObjRND[counter]] == twelveObj2[10])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = true; 
            }else if (twelveObj1[twelveObjRND[counter]] == twelveObj1[9] && twelveObj2[twelveObjRND[counter]] == twelveObj2[9])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = true;
            }
            else if (twelveObj1[twelveObjRND[counter]] == twelveObj1[8] && twelveObj2[twelveObjRND[counter]] == twelveObj2[8])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = true;
            }
            else
            {
                evenButton.GetComponent<Image>().color = Color.red;
                unevenButton.GetComponent<Image>().color = Color.green;
                correct = false; 
            }
        }
    }

    void PressedUneven()
    {
        for (int l = 0; l < lights.Count; l++)
        {
            lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }

        pressed = true;
        for (int j = 0; j < twelveObjRND.Count; j++)
        {

            if (twelveObj1[twelveObjRND[counter]] == twelveObj1[10] && twelveObj2[twelveObjRND[counter]] == twelveObj2[10])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = false;
            }
            else if (twelveObj1[twelveObjRND[counter]] == twelveObj1[9] && twelveObj2[twelveObjRND[counter]] == twelveObj2[9])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct =false;
            }
            else if (twelveObj1[twelveObjRND[counter]] == twelveObj1[8] && twelveObj2[twelveObjRND[counter]] == twelveObj2[8])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct =false;
            }
            else
            {
                evenButton.GetComponent<Image>().color = Color.red;
                unevenButton.GetComponent<Image>().color = Color.green;
                correct = true;
            }
        }
    }
    void PressedEvenComplex()
    {
        for (int l = 0; l < lights.Count; l++)
        {
            lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }

        pressedComplex = true;
        for (int j = 0; j < twelveObjRND.Count; j++)
        {
            if (twelveObj1[twelveObjRND[counter]] == twelveObj1[10] && twelveObj2[twelveObjRND[counter]] == twelveObj2[10] && twelveObj3[twelveObjRND[counter]] == twelveObj3[10] && twelveObj4[twelveObjRND[counter]] == twelveObj4[10])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = true;
            } else if (twelveObj1[twelveObjRND[counter]] == twelveObj1[8] && twelveObj2[twelveObjRND[counter]] == twelveObj2[8] && twelveObj3[twelveObjRND[counter]] == twelveObj3[8] && twelveObj4[twelveObjRND[counter]] == twelveObj4[8]) {

                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = true;
            }
            else if (twelveObj1[counter] == twelveObj1[9] && twelveObj2[twelveObjRND[counter]] == twelveObj2[9] && twelveObj3[twelveObjRND[counter]] == twelveObj3[9] && twelveObj4[twelveObjRND[counter]] == twelveObj4[9])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = true;
            }
            else
            {
                evenButton.GetComponent<Image>().color = Color.red;
                unevenButton.GetComponent<Image>().color = Color.green;
                correct = false;
            }
        }
    }

    void PressedUnevenComplex()
    {
        for (int l = 0; l < lights.Count; l++)
        {
            lights[l].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        pressedComplex = true;
        for (int j = 0; j < twelveObjRND.Count; j++)
        {
            if (twelveObj1[counter] == twelveObj1[10] && twelveObj2[counter] == twelveObj2[10] && twelveObj3[counter] == twelveObj3[10] && twelveObj4[counter] == twelveObj4[10])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = false;
            }
            else if (twelveObj1[counter] == twelveObj1[4] && twelveObj2[counter] == twelveObj2[4] && twelveObj3[counter] == twelveObj3[4] && twelveObj4[counter] == twelveObj4[4])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = false;
            }
            else if (twelveObj1[counter] == twelveObj1[9] && twelveObj2[counter] == twelveObj2[9] && twelveObj3[counter] == twelveObj3[9] && twelveObj4[counter] == twelveObj4[9])
            {
                evenButton.GetComponent<Image>().color = Color.green;
                unevenButton.GetComponent<Image>().color = Color.red;
                correct = false;
            }
            else
            {
                evenButton.GetComponent<Image>().color = Color.red;
                unevenButton.GetComponent<Image>().color = Color.green;
                correct = true;
            }
        }
    }
    bool waitingForInput() {

        if (timeCounter == 0) {
            return false;
        } else if(pressed){
            return false; 
        }
        else {

            return true; }
        
    }
    bool waitingForComplexInput()
    {

        if (timeCounter == 0)
        {
            return false;
        }
        else if (pressedComplex)
        {
            return false;
        }
        else
        {

            return true;
        }

    }
    void CountDown() {
        lights[timeCounter-1].GetComponent<Image>().color = new Color(255,255,255,0); 
        timeCounter--; 
    }
	// Update is called once per frame
	void FixedUpdate () {
        transform.GetChild(6).GetComponent<Text>().text = baseMainScript.stabilizerEff.ToString("0") + " /" + " 40%"; 
        t += Time.deltaTime / duration; 

       Color lerpedColor = Color.Lerp(colorNew, Color.white,t);
       // Color lerpedColor = Color.Lerp(colorNew, Color.white, Time.deltaTime * speed);
        if (pointsLeft.Count > 0 && pointsRight.Count > 0) {
            for (int c = 0; c < pointsLeft.Count; c++) {
                pointsLeft[c].transform.localPosition = Vector2.Lerp(pointsLeft[c].transform.localPosition, pointsLeftTarget[c], Time.deltaTime * speed * Random.Range(0f, 10f));
                pointsLeft[c].GetComponent<Image>().color = lerpedColor;
            }
            for (int u = 0; u < pointsRight.Count; u++)
            {
                pointsRight[u].transform.localPosition = Vector2.Lerp(pointsRight[u].transform.localPosition, pointsRightTarget[u], Time.deltaTime * speed * Random.Range(0f, 10f));
                pointsRight[u].GetComponent<Image>().color = lerpedColor;
            }
        }
        if (pointsLeftD.Count > 0 && pointsRightD.Count > 0)
        {
            for (int p = 0; p < pointsLeftD.Count; p++)
            {
                pointsLeftD[p].transform.localPosition = Vector2.Lerp(pointsLeftD[p].transform.localPosition, pointsLeftTargetD[p], Time.deltaTime * speed * Random.Range(0f, 10f));
                pointsLeftD[p].GetComponent<Image>().color = lerpedColor;
            }
            for (int q = 0; q < pointsRightD.Count;q++)
            {
                pointsRightD[q].transform.localPosition = Vector2.Lerp(pointsRightD[q].transform.localPosition, pointsRightTargetD[q], Time.deltaTime * speed * Random.Range(0f, 10f));
                pointsRightD[q].GetComponent<Image>().color = lerpedColor;
            }
        }
        //    Debug.Log(twelveObj1[twelveObjRND[j]] + "," + twelveObj2[twelveObjRND[j]]); Vector2.MoveTowards(pointsRight[k].transform.localPosition, pointsRightTarget[k], Time.deltaTime * speed * Random.Range(0f, 10f));
    }
    // Vector2.SmoothDamp(pointsRight[k].transform.localPosition, pointsRightTarget[k], ref velocity, speed); Color lerpedColor = Color.Lerp(colorNew, Color.white, Mathf.PingPong(Time.time, 0.7f));
}
