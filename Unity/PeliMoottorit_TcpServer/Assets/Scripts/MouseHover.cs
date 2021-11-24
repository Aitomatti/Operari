using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update

    //  todennäköisesti turhaa
    void Start()
    {

        //GameObject.Find("Start").GetComponent<TextMesh>().color = Color.yellow;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
