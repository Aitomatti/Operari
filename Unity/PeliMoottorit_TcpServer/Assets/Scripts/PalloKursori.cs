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
        //Debug.Log("Yrot = " + TcpServer.yRot);
        //Debug.Log("asdasd");

        if(TcpServer.xRot >= 0.1)
        {
            // Quit
            transform.localPosition = new Vector3(-7.62f, 1, -2.46f);
            menuSwitch = 2;
        }
        else if (TcpServer.xRot <= -0.1)
        {
            // Start
            transform.localPosition = new Vector3(-7.62f, 1, -0.46f);
            menuSwitch = 1;
        }
        else
        {
            transform.localPosition = new Vector3(-7.62f, 1, -1.46f);
            menuSwitch = 0;
        }
    }
}
