using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOutput : MonoBehaviour
{
    Door doorClass;

    GameObject containerObjectPool;
    ObjectPool objectPool;

    // Start is called before the first frame update
    void Start()
    {
        containerObjectPool = GameObject.Find("ObjectPool");
        objectPool = containerObjectPool.GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Door")
        {
            objectPool.poolFinishedObject(col.gameObject);
            doorClass = col.gameObject.GetComponent<Door>();
            //print("DOOR " + doorClass.getId() +  " FINISHED AND STORED");

        }
        else
        {

        }
    }
}
