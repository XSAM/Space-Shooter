using UnityEngine;
using System.Collections;

public class DestroyByTime: MonoBehaviour 
{
    public float lifeTime;

    void Start()
    {
        Debug.Log("LifeTime Destroy");
        Destroy(gameObject, lifeTime);
    }
}
