using UnityEngine;
using System.Collections;

public class BGScroller: MonoBehaviour 
{
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

	// Use this for initialization
	void Start () 
    {
        startPosition = transform.position;
		//Debug.Log ("BGpostion"+transform.position);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        //Debug.Log(Time.time);
       float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
       //float newPosition = tileSizeZ-Mathf.Abs((Time.time * scrollSpeed) % tileSizeZ);
		//Debug.Log ("Time.time" + Time.time);

        transform.position = startPosition + Vector3.forward * newPosition;
	}

    void Test()
    {
        Debug.Log("Repeat:" + Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ));
        Debug.Log("%:" + (tileSizeZ - Mathf.Abs((Time.time * scrollSpeed) % tileSizeZ)));
    }
}
