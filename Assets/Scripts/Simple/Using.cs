using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum Doing
{
    Hide, Show, Animation, Popap, News, Water, Item, End
}

public class Using : MonoBehaviour
{
    public Sprite os;
    public int helpnum;
    public Animation animation;
    public Text codetext;
    public bool cod;
    public Doing doing;
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

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (toUse == ItemType.None)
        {
            //newSprite.SetActive(true);
            //gameObject.SetActive(false);
            if (cod)
            {
                newSprite = null;
                doing = Doing.Animation;
                Use();
            }
            else Use();
        }
        else
        {
            have = Inventar.inventar.Use(toUse, helpnum);
            if (have)
                Use();
            //gameObject.SetActive(false);
        }
    }

    public void Use()
    {
        switch (doing)
        {
            case Doing.Hide:
                gameObject.SetActive(false);
                break;
            case Doing.Show:
                newSprite.SetActive(true);
                break;
            case Doing.Animation:
                if (animation != null)
                {
                    animation.Play();
                    animation = null;
                }
                if (newSprite != null)
                    newSprite.SetActive(true);
                break;
            case Doing.Popap:
                newSprite.SetActive(true);
                Parallax.startA = false;
                break;
            case Doing.News:
                newSprite.SetActive(true);
                gameObject.SetActive(false);
                break;
            case Doing.Water:
                break;
            case Doing.Item:
                os = GetComponent<SpriteRenderer>().sprite;
                Inventar.inventar.GetItem(os, ItemType.fish, helpnum);
                Destroy(gameObject);
                break;
            case Doing.End:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
                break;
            default:
                break;
        }
        Inventar.inventar.helpt[helpnum] = true;
    }

    //IEnumerator Move()
    //{
    //    while (gameObject.transform.localPosition.y < stopposy)
    //    {
    //        gameObject.transform.position += new Vector3(0, 4.5f, 0) * Time.deltaTime;
    //        yield return null;
    //    }
    //    while (gameObject.transform.localPosition.x < stopposx)
    //    {
    //        gameObject.transform.position += new Vector3(4.5f, 0, 0) * Time.deltaTime;
    //        yield return null;
    //    }
    //}

    public void CodInput()
    {
        if (codetext.text == "2315")
            cod = true;
        //else codetext.text = null;
    }
}
