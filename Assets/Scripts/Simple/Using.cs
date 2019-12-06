using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Using : MonoBehaviour
{
    bool have;
    public ItemType toUse;
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
        have = Inventar.inventar.Use(toUse);
    }
}
