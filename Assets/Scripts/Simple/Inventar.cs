using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Kay, Card, Ball, Backet, Bast, Apple, Booc, Egg
}

public class Inventar : MonoBehaviour
{
    public static Inventar inventar;
    int index;
    public Image[] it = new Image[5];
    public Item[] items = new Item[5];

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
        items[index] = new Item();
        items[index].os = its;
        items[index].itUse = itUse;
        it[index].sprite = its;
        it[index].color = Color.white;
    }
    public bool Use(ItemType toUse)
    {
        if (toUse == items[index].itUse)
        {
            it[index].sprite = null;
            it[index].color = new Color(1, 1, 1, 0);
            return true;
        }
        else return false;
    }
}
