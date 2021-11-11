using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject demoKuutioPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject gameObject = GetComponent <GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        if(Input.GetKeyDown("s"))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject newObject = Instantiate(demoKuutioPrefab);
        newObject.transform.position = new Vector3(0, transform.localPosition.y + 50 , 0);
    }
}
