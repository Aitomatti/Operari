using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameLogic
/// </summary>
public class GameLogic : MonoBehaviour
{
    public static int gamePoints;
    public static bool runOnce = true;
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

        // Space pressed - Game Ended       Lisatty peli loppuun kun 10 ainesta tippuu
        if (Input.GetKeyUp(KeyCode.Space) || (FloorTrigger.lostPoints >= 10 && runOnce == true)) {

            runOnce = false;
            gamePoints = ToppingTrigger.AddedPoints;

            Debug.Log("score " + ToppingTrigger.AddedPoints);
            Debug.Log("lost " + FloorTrigger.lostPoints);
            Debug.Log("yht " + gamePoints);

            HiScore.Instance.ShowInputQuery(gamePoints);

            ToppingTrigger.AddedPoints = 0;
        }
    }
}
