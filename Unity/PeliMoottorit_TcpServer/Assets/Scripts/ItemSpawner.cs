using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject demoKuutioPrefab;
    public GameObject demoSpherePrefab;
    public GameObject demoCapsulePrefab;
    GameObject newObject;

    [SerializeField]
    private float spawnRate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject gameObject = GetComponent <GameObject>();
        Invoke("Spawn", spawnRate);
    }
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        if(Input.GetKeyDown("s") )
        {
            Debug.Log("Spawned by keybutton s");
            Spawn();
        }
    }

    void Spawn()
    {
        Debug.Log("Spawned object");
        int rndNum = Random.Range(0,3);
        float xPosition = Random.Range(-4, 5);
        float zPosition = Random.Range(-4, 5);

        switch (rndNum)
        {
            case 0:
                newObject = (GameObject)Instantiate(demoKuutioPrefab);
                break;
            case 1:
                newObject = (GameObject)Instantiate(demoSpherePrefab);
                break;
            case 2:
                newObject = (GameObject)Instantiate(demoCapsulePrefab);
                break;

        }
        newObject.transform.position = new Vector3(xPosition, transform.localPosition.y + 50 , zPosition);

        //vahenna spawnaus ajanvalia
        
        Debug.Log("Spawned rate= "+ spawnRate);

        if(spawnRate > 0.3)
        {
            spawnRate /= 1.1f;
        }
        Invoke("Spawn", spawnRate);
    }
}
