using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is the controller that creates the gameobject pool
/// it preemptively instantiates multiple gameobjects before the game actually starts
/// 
/// ***to add a gameobject to the pool, drag and drop the gameobject into the "objectpool" gameobject in the unity editor
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    /// <summary>
    /// The object prefabs which the pool can handle.
    /// </summary>
    public  GameObject[] objectPrefabs;

    /// <summary>
    /// The pooled objects currently available.
    /// </summary>
    public List<GameObject>[] pooledObjects;

    /// <summary>
    /// The amount of objects of each type to buffer.
    /// </summary>
    public int[] amountToBuffer;

    public int defaultBufferAmount = 3;

    /// <summary>
    /// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
    /// </summary>
    protected GameObject containerObject;

    /// <summary>
    /// The lists to place the finished objects
    /// </summary>
    private List<Door> finishedDoors;
    private List<Window> finishedWindows;

    private List<Door> queuedDoors;
    private List<Window> queuedWindows;

    int doorIdNum = 0;
    int windowIdNum = 0;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        containerObject = new GameObject("ObjectPool");

        finishedDoors = new List<Door>();
        finishedWindows = new List<Window>();

        queuedDoors = new List<Door>();
        queuedWindows = new List<Window>();

        //Loop through the object prefabs and make a new list for each one.
        //We do this because the pool can only support prefabs set to it in the editor,
        //so we can assume the lists of pooled objects are in the same order as object prefabs in the array
        pooledObjects = new List<GameObject>[objectPrefabs.Length];

        int i = 0;
        foreach (GameObject objectPrefab in objectPrefabs)
        {
            pooledObjects[i] = new List<GameObject>();

            int bufferAmount;

            if (i < amountToBuffer.Length)
                bufferAmount = amountToBuffer[i];
            else
                bufferAmount = defaultBufferAmount;

            for (int n = 0; n < bufferAmount; n++)
            {
                GameObject newObj = Instantiate(objectPrefab) as GameObject;
                newObj.name = objectPrefab.name;
                PoolObject(newObj);
            }

            i++;
        }
    }

    /// <summary>
    /// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
    /// then null will be returned.
    /// </summary>
    /// <returns>
    /// The object for type.
    /// </returns>
    /// <param name='objectType'>
    /// Object type.
    /// </param>
    /// <param name='onlyPooled'>
    /// If true, it will only return an object if there is one currently pooled.
    /// </param>
    public GameObject GetObjectForType(string objectType, bool onlyPooled)
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            GameObject prefab = objectPrefabs[i];
            if (prefab.name == objectType)
            {

                if (pooledObjects[i].Count > 0)
                {
                    GameObject pooledObject = pooledObjects[i][0];
                    pooledObjects[i].RemoveAt(0);
                    pooledObject.transform.parent = null;
                    pooledObject.SetActiveRecursively(true);

                    return pooledObject;

                }
                else if (!onlyPooled)
                {
                    return Instantiate(objectPrefabs[i]) as GameObject;
                }

                break;

            }
        }

        //If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
        return null;
    }

    /// <summary>
    /// Pools the object specified.  Will not be pooled if there is no prefab of that type.
    /// </summary>
    /// <param name='obj'>
    /// Object to be pooled.
    /// </param>
    public void PoolObject(GameObject obj)
    {
        
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            //if statement to set the door and window ids
            if (objectPrefabs[i].name == obj.name)
            {
                if (obj.name == "Door")
                {
                    //set the id of the door (methods from Product class)
                    //increment the variable for the next door to take
                    Door doorObject = obj.GetComponent<Door>();
                    doorObject.setId("Door# " + doorIdNum);
                    doorObject.setIdNum(doorIdNum);
                    //print("works door" + doorIdNum);

                    doorIdNum++;

                }
                else if (obj.name == "Window")
                {
                    //set the id of the window (methods from Product class)
                    //increment the variable for the next window to take
                    Window windowObject = obj.GetComponent<Window>();
                    windowObject.setId("Window# " + windowIdNum);
                    windowObject.setIdNum(windowIdNum);
                    //print("works window" + windowIdNum);

                    windowIdNum++;


                }
                else
                {
                    print("did not work");
                }

                //print (obj.name);
                obj.SetActiveRecursively(false);
                obj.transform.parent = containerObject.transform;
                pooledObjects[i].Add(obj);

                return;
            }
        }
    }

    //this method places the finished door or window in separate Lists
    public void poolFinishedObject(GameObject obj)
    {
        if (obj.tag == "Door")
        {
            obj.SetActiveRecursively(false);
            obj.transform.parent = containerObject.transform;

            //gets the "Door" component of the object
            //this is to be able to access the methods in the "Door" class
            Door doorClass = obj.GetComponent<Door>();
            finishedDoors.Add(doorClass);
            print("Stored: " + doorClass.getId() + "in finished Doors");

        }else if(obj.tag == "Window")
        {
            obj.SetActiveRecursively(false);
            obj.transform.parent = containerObject.transform;

            //gets the "Window" component of the object
            //this is to be able to access the methods in the "Window" class
            Window windowClass = obj.GetComponent<Window>();
            finishedWindows.Add(windowClass);
            print("Stored: " + windowClass.getId() + "in finished Windows");
        }
        else
        {
            print("No finished gameobject stored.");
        }
    }

    //gets all the Door components of all Door objects in the objectPool and stored in the queued doors
    public List<Door> getAllDoors()
    {
        queuedDoors.Clear();

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            for (int j = 0; j < pooledObjects[i].Count; j++)
            {
                //if statement to set the door and window ids
                if (pooledObjects[i][j].name.Equals("Door"))
                {
                    //gets the Door component from each object
                    Door doorObject = pooledObjects[i][j].GetComponent<Door>();
                    queuedDoors.Add(doorObject);

                }
                else
                {
                    print("NO gameobject by that name");
                }
            }            
            
        }

        return queuedDoors;
    }

    //gets all the Window components of all Window objects in the objectPool and stored in the queued doors
    public List<Window> getAllWindows()
    {
        queuedWindows.Clear();

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            for (int j = 0; j < pooledObjects[i].Count; j++)
            {
                //if statement to set the door and window ids
                if (pooledObjects[i][j].name.Equals("Window"))
                {
                    //gets the Window component from each object
                    Window windowObject = pooledObjects[i][j].GetComponent<Window>();
                    queuedWindows.Add(windowObject);

                }
                else
                {
                    print("NO gameobject by that name");
                }
            }

        }

        return queuedWindows;
    }

    ///GETTERS/////
    public List<Door> getAllFinishedDoors()
    {
        return finishedDoors;
    }

    public List<Window> getAllFinishedWindows()
    {
        return finishedWindows;
    }

    public int getBufferAmount()
    {
        return amountToBuffer[0];
    }

    public int getLengthOfPooledObjects()
    {
        return pooledObjects.Length;
    }
}
