using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Kay,Card
}

public class Inventar : MonoBehaviour
{
    public static Inventar inventar;
    int index;
    public Image[] it = new Image[5];

    void Awake()
    {
        inventar = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetItem(Sprite its, ItemType itUse)
    {
        while (!(it[index].sprite == null))
        {
            index++;
        }
        it[index].sprite = its;
        it[index].color = Color.white;
    }
    public void Use(int toUse)
    {
        //if (toUse == itUse)
        //{
        //    it[index].sprite = null;
        //}
    }
}
