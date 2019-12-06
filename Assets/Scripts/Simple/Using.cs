using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Doing
{
    Hide, Show, Animation, Popap, Move, News
}

public class Using : MonoBehaviour
{
    public float stopposy;
    public float stopposx;
    public Doing doing;
    public GameObject newSprite;
    bool have;
    public ItemType toUse;

    // Start is called before the first frame update
    void Start()
    {
        if (stopposx == 0)
            stopposx = gameObject.transform.localPosition.x;
        if (stopposy == 0)
            stopposy = gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (toUse == ItemType.None)
        {
            //newSprite.SetActive(true);
            //gameObject.SetActive(false);
            Use();
        }
        else
        {
            have = Inventar.inventar.Use(toUse);
            if (have)
                Use();
            //gameObject.SetActive(false);
        }
    }

    void Use()
    {
        switch (doing)
        {
            case Doing.Hide:
                gameObject.SetActive(false);
                break;
            case Doing.Show:
                gameObject.SetActive(true);
                break;
            case Doing.Animation:
                break;
            case Doing.Popap:
                break;
            case Doing.Move:
                StartCoroutine(Move());
                break;
            case Doing.News:
                newSprite.SetActive(true);
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    IEnumerator Move()
    {
        while (gameObject.transform.localPosition.y < stopposy)
        {
            gameObject.transform.position += new Vector3(0, 4.5f, 0) * Time.deltaTime;
            yield return null;
        }
        while (gameObject.transform.localPosition.x < stopposx)
        {
            gameObject.transform.position += new Vector3(4.5f, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }
}
