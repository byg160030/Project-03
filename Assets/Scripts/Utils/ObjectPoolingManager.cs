using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{

    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance {  get { return instance; } }

    public GameObject bulletPrefab;
    public int bulletAmount = 20;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        Debug.Log("Hello!");
        return null;
    }
}
