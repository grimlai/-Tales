using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PCCode : MonoBehaviour,IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x,0,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
