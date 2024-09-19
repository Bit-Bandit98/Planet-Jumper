using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectPoolItem
{
    public int AmountToPool;
    public GameObject ObjectToPool;
    public bool ShouldExpand;

}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Singleton;
    public List<ObjectPoolItem> ItemsToPool;
    public List<GameObject> PooledObjects;
  

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        PooledObjects = new List<GameObject>();

        foreach(ObjectPoolItem Item in ItemsToPool) { 
        for(int i = 0; i < Item.AmountToPool; i++)
        {
            GameObject OBJ = (GameObject)Instantiate(Item.ObjectToPool);
            OBJ.SetActive(false);
            PooledObjects.Add(OBJ);
        }
        }
    }

    public GameObject GetPooledObject(string Tag)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == Tag)
            {
                return PooledObjects[i];
            }
        }
       
        foreach (ObjectPoolItem Item in ItemsToPool)
        {
            if (Item.ObjectToPool.tag == tag)
            {
                if (Item.ShouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(Item.ObjectToPool);
                    obj.SetActive(false);
                    PooledObjects.Add(obj);
                    return obj;
                }

            }
            }
        
        return null;
    }

}
