using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingTrigger : MonoBehaviour
{
    [SerializeField] static public int AddedPoints = 0;

    private float timer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            AddedPoints++;
            timer = 0.1f;
        } 
    }

}
