using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameLogic
/// </summary>
public class GameLogic : MonoBehaviour
{
    public static int gamePoints;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start() 
    {

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // 4. Escape painettu, kutsutaan HiScore Singletonin Show Metodia (HiScore.Instance.Show)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HiScore.Instance.Show();
        }


        // 5. Escape Nappain nostettu ylös, kutsutaan HiScore Singletonin Hide Metodia
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HiScore.Instance.Hide();
        }



        // Space pressed - Game Ended       Lisatty peli loppuun kun 20 ainesta tippuu
        if (Input.GetKeyUp(KeyCode.Space) || FloorTrigger.lostPoints == 20) {

            gamePoints = ToppingTrigger.AddedPoints - (FloorTrigger.lostPoints * 100);

            Debug.Log("score " + ToppingTrigger.AddedPoints);
            Debug.Log("lost " + FloorTrigger.lostPoints);
            Debug.Log("yht " + gamePoints);

            HiScore.Instance.ShowInputQuery(gamePoints);

            FloorTrigger.lostPoints = 0;
        }
    }
}
