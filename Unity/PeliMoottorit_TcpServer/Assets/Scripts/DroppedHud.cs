using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedHud : MonoBehaviour
{
    TextMesh droppedAmount;
    // Start is called before the first frame update
    void Start()
    {
        droppedAmount = GetComponent<TextMesh>();

        GameLogic.runOnce = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        droppedAmount.text = "Missed: " + FloorTrigger.lostPoints + "/10";

        switch (GameLogic.runOnce)
        {
            case false:
                transform.position = new Vector3(transform.position.x, 500f, transform.position.z);
                break;

            case true:
                transform.position = new Vector3(transform.position.x, 30f, transform.position.z);
                break;
        }
    }
}
