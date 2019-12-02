using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeCultureInfo : MonoBehaviour
{

    void Awake()
    {
        transform.SetAsFirstSibling();
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    void Update()
    {
        if (CultureInfo.CurrentCulture.Name == "ru-RU")
            CultureInfo.CurrentCulture = new CultureInfo("en");
    }
}
