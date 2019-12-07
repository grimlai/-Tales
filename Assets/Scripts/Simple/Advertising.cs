using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Advertising : MonoBehaviour
{
    public Text helpText;
    string[] help = new string[40];
    public float timer;
    public bool adv;
    public int num;
    public GameObject helpButton;
    // Start is called before the first frame update
    void Start()
    {
        help[0] = "Одеяло";
        help[1] = "Карта";
        help[2] = "Книга";
        help[3] = "Ведро";
        help[4] = "Дверца";
        help[5] = "Одеяло";
        help[6] = "Одеяло";
        help[7] = "Одеяло";
        help[8] = "Одеяло";
        help[9] = "Одеяло";
        help[10] = "Одеяло";
        help[11] = "Одеяло";
        help[12] = "Одеяло";
        help[13] = "Одеяло";
        help[14] = "Одеяло";
        help[15] = "Одеяло";
        help[16] = "Одеяло";
        help[17] = "Одеяло";
        help[18] = "Одеяло";
        help[19] = "Одеяло";
        help[20] = "Одеяло";

    }

    // Update is called once per frame
    void Update()
    {
        if (adv)
        {
            helpButton.SetActive(true);
            adv = false;
        }
    }

    public void Adv()
    {
        timer = 0;
        StartCoroutine(Advt());
    }

    IEnumerator Advt()
    {
        while (timer < 15)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        adv = true;
    }

    public void Help()
    {
        num = Inventar.inventar.HelpNeed();
        helpText.text = help[num];
    }
}
