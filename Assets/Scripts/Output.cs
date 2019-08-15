using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output : MonoBehaviour
{
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

    public void storeFinishedProducts(GameObject finishedProduct)
    {
        if (finishedProduct.tag == "Door")
        {
            objectPool.poolFinishedObject(finishedProduct);

        }
        else if (finishedProduct.tag == "Window")
        {
            objectPool.poolFinishedObject(finishedProduct);
        }
        else
        {

        }
    }

}
