using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public int helpnum;
    public Sprite os;
    public ItemType itUse;
    // Start is called before the first frame update
    void Start()
    {
        os = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Inventar.inventar.GetItem(os, itUse, helpnum);
        Destroy(gameObject);

    }
}
