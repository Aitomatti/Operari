using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //   !!    EI KAYTOSSA     !!
    /*
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
        //Piilota menu itemit
        for (int i = 0; i < GameObject.Find("Text").transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        HiScore.Instance.Show();

        //Lopeta ainesten spawnaus
        Destroy(GameObject.FindGameObjectWithTag("Spawner"));

        //Pauseta peli
        Time.timeScale = 0;
    }
    */
}
