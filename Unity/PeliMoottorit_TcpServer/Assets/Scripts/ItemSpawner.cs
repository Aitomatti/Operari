using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject juusto;
    public GameObject pepperon;
    public GameObject oliivi;
    public GameObject banaani;
    GameObject newObject;

    [SerializeField]
    public static float spawnRate = 5f;
    // Start is called before the first frame update
    void Start()
    {
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
        int rndNum = Random.Range(0,4);
        float xPosition = Random.Range(-9, 10);
        float zPosition = Random.Range(-9, 10);

        switch (rndNum)
        {
            case 0:
                newObject = Instantiate(juusto);
                break;
            case 1:
                newObject = Instantiate(pepperon);
                break;
            case 2:
                newObject = Instantiate(oliivi);
                break;
            case 3:
                newObject = Instantiate(banaani);
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
