using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameLogic
/// </summary>
public class GameLogic : MonoBehaviour
{
    /// <summary>
    /// time
    /// </summary>
    private float time;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start() 
    {

        time = Time.time;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // 4. Escape painettu, kutsutaan HiScore Singletonin Show Metodia (HiScore.Instance.Show)
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            HiScore.Instance.Show();
        }


        // 5. Escape Nappain nostettu ylös, kutsutaan HiScore Singletonin Hide Metodia
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HiScore.Instance.Hide();
        }*/



        // Space pressed - Game Ended
        if (Input.GetKeyUp(KeyCode.Space)) {
                float score = Time.time - time;
                HiScore.Instance.ShowInputQuery(score);
            }
    }
}
