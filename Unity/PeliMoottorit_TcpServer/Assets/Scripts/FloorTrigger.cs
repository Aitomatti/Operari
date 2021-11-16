using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] static public int lostPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        lostPoints++;
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
