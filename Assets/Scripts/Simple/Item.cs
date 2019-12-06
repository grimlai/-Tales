using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itUse;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Inventar.inventar.GetItem(GetComponent<SpriteRenderer>().sprite, itUse);
        Destroy(gameObject);

    }
}
