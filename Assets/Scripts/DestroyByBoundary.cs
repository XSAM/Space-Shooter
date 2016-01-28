using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("In:"+this.name + " Destroy:" + other.name);
        Destroy(other.gameObject);
    }
}
