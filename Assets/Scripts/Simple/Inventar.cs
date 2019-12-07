using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None, Kay, Card, Ball, Backet, Bast, Apple, Booc, Egg, Wrench, BirdFood
}

public class Inventar : MonoBehaviour
{
    public static Inventar inventar;

    public bool[] helpt = new bool[40];
    public Image selectidIm;
    public Item selectidIt;
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
        helpt[0] = true;
    }

    public void GetItem(Sprite its, ItemType itUse, int h)
    {
        helpt[h] = true;
        index = 0;
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
    public bool Use(ItemType toUse, int h)
    {
        if (toUse == selectidIt.itUse)
        {
            selectidIm.sprite = null;
            selectidIm.color = new Color(1, 1, 1, 0);
            selectidIm = null;
            selectidIt = null;
            return true;
        }
        else return false;
    }

    public void ItNum(int itnum)
    {
        selectidIm = it[itnum];
        selectidIt = items[itnum];
        Debug.Log(selectidIt.itUse);
    }

    public int HelpNeed()
    {
        int i = 0;
        while (i < helpt.Length)
        {
            if (helpt[i] != false)
                i++;
            else return i;
        }
        return i;
    }
}

