using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PCCode : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        //transform.position += new Vector3(eventData.delta.x, 0, 0);
    }

    public float[] time = new float[4];
    public bool[] up = new bool[4];
    public Text[] c = new Text[12];
    public Text codetext;
    public Animation[] animation = new Animation[4];
    int[] code = new int[12];

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        while (i < 3)
        {
            code[i] = i;
            code[i + 3] = i;
            code[i + 6] = i;
            code[i + 9] = i;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < code.Length)
        {
            c[i].text = code[i].ToString();
            i++;
        }
        time[0] += Time.deltaTime;
        time[1] += Time.deltaTime;
        time[2] += Time.deltaTime;
        time[3] += Time.deltaTime;
    }                  

    public void C1(int cn)
    {
        if (time[cn] > 1.2)
            {
                cn *= 3;
                int i = 0;
                while (i < 3)
                {
                    code[cn + i]++;
                    if (code[cn + i] > 9)
                        code[cn + i] = 0;
                    if (code[cn + i] < 0)
                        code[cn + i] = 9;
                    i++;
                }
            }
    }

    public void C2(int cn)
    {
        if (time[cn] > 1.2)
            {
                cn *= 3;
                int i = 0;
                while (i < 3)
                {
                    code[cn + i]--;
                    if (code[cn + i] > 9)
                        code[cn + i] = 0;
                    if (code[cn + i] < 0)
                        code[cn + i] = 9;
                    i++;
                }
            }
    }

    public void Int()
    {
        // if (code[0, 1] == 2 && code[1, 1] == 3 && code[2, 1] == 1 && code[3, 1] == 5)
        codetext.text = c[1].text + c[4].text + c[7].text + c[10].text;
        //if (codetext.text == "2315")
        //{
        //    animation.Play();
        //    Parallax.startA = true;
        //    PC.SetActive(false);
        //}
    }

    public void Animate(int an)
    {
        if (time[an] > 1.2)
        {
            animation[an].Play("PCcodeDown");
            time[an] = 0;
        }
    }

    public void AnimateDown(int an)
    {
        if (time[an] > 1.2)
        {
            animation[an].Play("PCCode");
            time[an] = 0;
        }
    }
}
