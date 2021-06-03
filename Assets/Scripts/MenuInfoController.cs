using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuInfoController : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI locuriDisp;
    public TextMeshProUGUI locuriTotale;
    public TextMeshProUGUI adresa;

    public void closeMenu()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.25f);
    }

    public void setInfo(Dictionary<string,object> info)
    {
        if (info.ContainsKey("name"))
        {
            name.text = "Nume: "+ info["name"].ToString();
        }
        if (info.ContainsKey("vicinity"))
        {
           adresa.text = "Locatie: "+ info["vicinity"].ToString();
        }
        if (info.ContainsKey("locuriDisp"))
        {
            locuriDisp.text = "Locuri disponibile: "+info["locuriDisp"].ToString();
        }
        if (info.ContainsKey("locuriTotale"))
        {
            locuriTotale.text = "Locuri totale: "+info["locuriTotale"].ToString();
        }
    }
}
