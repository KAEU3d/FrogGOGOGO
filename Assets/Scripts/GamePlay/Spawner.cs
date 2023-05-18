using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> spawnObjects;
    public int direction;
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, Random.Range(5f, 7f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn ()
    {
        var carIndex = Random.Range(0, spawnObjects.Count);
        var forwardObject = Instantiate(spawnObjects[carIndex], transform.position, Quaternion.identity, transform);
        forwardObject.GetComponent<MoveForward>().direction = direction;
    }
}
