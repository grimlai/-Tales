using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Using : MonoBehaviour
{
    public GameObject newSprite;
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
        if (toUse == ItemType.None)
        {
            newSprite.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            have = Inventar.inventar.Use(toUse);
            if (have)
                gameObject.SetActive(false);
        }
    }
}
