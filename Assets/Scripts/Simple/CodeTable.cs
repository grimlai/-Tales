using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeTable : MonoBehaviour
{
    public Text codetext;
    bool[] codetr = new bool[20];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int codn = 0;
        int i = 0;
        while (i < 20)
        {
            if (i < 12)
            {
                if (codetr[i])
                    codn++;
            }
            else
            {
                if (!codetr[i])
                    codn++;
            }
            i++;
        }
        if (codn > 19)
            codetext.text = "2315";
    }

    public void Cod(int index)
    {
        if (codetr[index])
        {
            codetr[index] = false;
        }
        else codetr[index] = true;

    }

    public void CodeTrue()
    {
        int codn = 0;
        int i = 0;
        while (i < 20)
        {
            if (i < 12)
            {
                if (codetr[i])
                    codn++;
            }
            else
            {
                if (!codetr[i])
                    codn++;
            }
            i++;
        }
        if (codn > 19)
            codetext.text = "2315";
    }
}
