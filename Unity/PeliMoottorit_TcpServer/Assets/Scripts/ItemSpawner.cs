using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GetComponent <GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        if(Input.GetKey("s"))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject newObject = Instantiate(gameObject);
        newObject.transform.position = new Vector3(0, -1 + transform.localScale.y / 2, 0);
    }
}
