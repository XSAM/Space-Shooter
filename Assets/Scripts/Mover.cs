using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
    private new Rigidbody rigidbody;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.velocity = new Vector3(0, 0, 1.0f) * speed;
        rigidbody.velocity = transform.forward * speed;
        
	}
	
}
