using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FloorTrigger.lostPoints >= 20)
        {
            Debug.Log("GAMEOVER - " + FloorTrigger.lostPoints);
            GameStop();
        }
    }

    void GameStop()
    {
                //NULLreference!!!!
        //GameObject.Find("Text").SetActive(true);

        HiScore.Instance.Show();

        Destroy(GameObject.FindGameObjectWithTag("Spawner"));

    }
}
