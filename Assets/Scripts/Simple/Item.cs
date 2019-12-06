using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
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

    void OnMouseDown()
    {
        Inventar.inventar.GetItem(os, itUse);
        Destroy(gameObject);

    }
}
