using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LayoutScript : MonoBehaviour
{

    public VerticalLayoutGroup vertLayoutGroup;
    
    

    private void Start()
    {
        

        //Debug.Log(exampleChild.anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        vertLayoutGroup.CalculateLayoutInputHorizontal();
        vertLayoutGroup.CalculateLayoutInputVertical();
        vertLayoutGroup.SetLayoutHorizontal();
        vertLayoutGroup.SetLayoutVertical();
        
    }
}
