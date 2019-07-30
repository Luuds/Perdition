using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutInv : MonoBehaviour, IPointerClickHandler
{
    
    GameObject parentHospot;
    public string parentName;
    HotspotData data; 
    // Start is called before the first frame update
    void Start()
    {
        transform.SetAsFirstSibling();
        int nameLength = transform.parent.name.Length - 4;
        parentName = transform.parent.name.Remove(nameLength); 
        parentHospot = GameObject.Find(parentName);
        data = parentHospot.GetComponent<HotspotData>(); 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        data.CloseInventory();
        Debug.Log("ClickOut"); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
