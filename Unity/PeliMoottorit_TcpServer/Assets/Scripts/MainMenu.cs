using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject toppingSpawner;
    // Start is called before the first frame update
    void Start()
    {
        HiScore.Instance.Show();
    }

    void OnMouseUp()
    {
        switch (PalloKursori.menuSwitch)
        {
            case 1:
                //RESET GAMESCENE
                GameObject.Find("Text").SetActive(false);

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

                break;

            case 2:
                Application.Quit();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //nappi painettu
        if (TcpServer.button == 1 || Input.GetKeyUp("p"))
        {
            Debug.Log("Button press");
            if (Input.GetKeyUp("p")) PalloKursori.menuSwitch = 1;
            OnMouseUp();
        }
        
    }
}
