using UnityEngine;
using System.Collections;

public class ObjectPooling : MonoBehaviour {

    public GameObject prefab;
    public GameObject bulletHolder;
    public int poolSize;

    private GameObject[] pool;

    void Start()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity,bulletHolder.transform);
            pool[i].SetActive(false);
        }
    }

    public GameObject RetrieveInstance()
    {
        foreach (GameObject go in pool)
        {
            if (!go.activeInHierarchy)
            {
                go.SetActive(true);
                return go;
            }
        }

        return null;
    }

    public void DevolveInstance(GameObject go)
    {
        go.SetActive(false);
    }
}
