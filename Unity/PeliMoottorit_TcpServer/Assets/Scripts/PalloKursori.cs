using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalloKursori : MonoBehaviour
{
    public static int menuSwitch;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (TcpServer.xRot <= -0.1)
        {
            // Quit
            transform.localPosition = new Vector3(-8f, 0.75f, -10.0f);
            menuSwitch = 2;

            GameObject.Find("Start").GetComponent<TextMesh>().color = Color.black;
            GameObject.Find("Quit").GetComponent<TextMesh>().color = Color.red;
        }
        else if (TcpServer.xRot >= 0.1)
        {
            // Start
            transform.localPosition = new Vector3(-8f, 0.75f, -5.0f);
            menuSwitch = 1;

            GameObject.Find("Start").GetComponent<TextMesh>().color = Color.red;
            GameObject.Find("Quit").GetComponent<TextMesh>().color = Color.black;
        }
        else
        {
            transform.localPosition = new Vector3(-8f, 0.75f, -7.58f);
            menuSwitch = 0;

            GameObject.Find("Start").GetComponent<TextMesh>().color = Color.black;
            GameObject.Find("Quit").GetComponent<TextMesh>().color = Color.black;
        }
    }
}
