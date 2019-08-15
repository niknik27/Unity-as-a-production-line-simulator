using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOutput : MonoBehaviour
{
    Window windowClass;

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
        if (col.gameObject.tag == "Window")
        {
            objectPool.poolFinishedObject(col.gameObject);
            windowClass = col.gameObject.GetComponent<Window>();
            //print("WINDOW " + windowClass.getId() + " FINISHED AND STORED");

        }
        else
        {

        }
    }
}
