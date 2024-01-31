using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance {  get { return instance; } }

    [SerializeField] GameObject criminalCardPrefab;
    [SerializeField] GameObject cardDisplayForNeighborhoodPrefab;

    private List<GameObject> objectsInPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            objectsInPool = new List<GameObject>();
        }
        else
        {
            Debug.Log("Multiple Object Pools");
            gameObject.IsDestroyed();
        }
    }

    public GameObject GetObjectFromPool(string objectTag)
    {
        GameObject requestedObject = null;
        requestedObject = CheckPoolForObject(requestedObject, objectTag);

        if(requestedObject == null)
        {
            requestedObject = CreateNewObjectForPool(requestedObject, objectTag);
        }

        if(requestedObject == null)
        {
            Debug.Log("Invalid Object Pool Request");
        }
        return requestedObject;
    }

    private GameObject CheckPoolForObject(GameObject requestedObject, string obejctTag)
    {
        foreach (GameObject obj in objectsInPool)
        {
            if (!obj.activeInHierarchy)
            {
                if(obejctTag == obj.tag)
                {
                    requestedObject = obj;
                }
            }
        }
        return requestedObject;
    }

    private GameObject CreateNewObjectForPool(GameObject requstedObject, string objectTag)
    {
        requstedObject = SelectObjectToCreate(objectTag);
        requstedObject.transform.SetParent(transform, false);
        requstedObject.gameObject.SetActive(false);
        objectsInPool.Add(requstedObject);
        return requstedObject;
    }

    private GameObject SelectObjectToCreate(string objectTag)
    {
        switch (objectTag)
        {
            case "Card":
                return Instantiate(criminalCardPrefab);
            case "Neighborhood Display":
                return Instantiate(cardDisplayForNeighborhoodPrefab);
        }

        Debug.Log("Invalid Object Pool Request");
        return null;
    }
}
