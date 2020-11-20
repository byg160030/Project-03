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
    }

    public GameObject GetBullet()
    {
        Debug.Log("Hello!");
        return null;
    }
}
