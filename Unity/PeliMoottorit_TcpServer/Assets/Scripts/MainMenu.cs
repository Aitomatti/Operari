using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject toppingSpawner;
    public Transform menuOption;

    private bool startDone = true;

    // Start is called before the first frame update
    void Start()
    {
        HiScore.Instance.Show();
    }

    // Update is called once per frame
    void Update()
    {
        //nappi painettu
        if (TcpServer.button == 1 && startDone == true || Input.GetKeyUp("p"))
        {
            Debug.Log("Button press");
            if (Input.GetKeyUp("p")) PalloKursori.menuSwitch = 1;
            OnMouseUp();
        }

        if (Input.GetKeyUp("o")) FloorTrigger.lostPoints = 20;

    }

    void OnMouseUp()
    {
        switch (PalloKursori.menuSwitch)
        {
            case 1:
                //Jatka/Aloita peli
                Time.timeScale = 1;

                //RESET GAMESCENE
                for (int i = 0; i < GameObject.Find("Text").transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);       
                }

                HiScore.Instance.Hide();

                var clones = GameObject.FindGameObjectsWithTag("CLONE");

                foreach (var clone in clones)
                {
                    Destroy(clone);
                }

                ToppingTrigger.AddedPoints = 0;
                FloorTrigger.lostPoints = 0;
                ItemSpawner.spawnRate = 5f;

                Instantiate(toppingSpawner);

                startDone = false; //Estetaan spawnauksen ajaminen useasti
                GameLogic.runOnce = true;
                break;

            case 2:
                //SAMMUTA PELI
                Application.Quit();
                break;
        }
    }

    public void GameStop()
    {
        //Nayta menu itemit
        for (int i = 0; i < GameObject.Find("Text").transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        HiScore.Instance.Show();

        //Lopeta ainesten spawnaus
        Destroy(GameObject.FindGameObjectWithTag("Spawner"));

        //Pauseta peli
        Time.timeScale = 0;

        startDone = true;
        
    }
}
