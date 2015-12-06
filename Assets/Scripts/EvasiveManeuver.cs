using UnityEngine;
using System.Collections;

public class EvasiveManeuver: MonoBehaviour 
{
   
    public float dodge;
    public float smoothing;    
    public float tilt; 
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private Rigidbody playerRigidbody;
    private float currentSpeed;
    private float targetManeuver;
    private new Rigidbody rigidbody;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = rigidbody.velocity.z;
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        StartCoroutine(Evade());
	}
	
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            if(playerRigidbody!=null)
                targetManeuver = Random.Range(1,dodge)*Mathf.Sign(playerRigidbody.position.x-transform.position.x);
            //targetManeuver = Random.Range(1,dodge);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));
            targetManeuver= 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x,maneuverWait.y));
        }
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
            );
    }
	// Update is called once per frame
	void FixedUpdate () 
    {
        float newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, targetManeuver, Time.deltaTime*smoothing);
        rigidbody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        if (playerRigidbody != null)
        {
            if (Mathf.Abs(rigidbody.position.x - playerRigidbody.position.x) <= 0.5)
                newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, 0, Time.deltaTime * smoothing * 5);//和飞船同一条线时 减低速度 现在值为5
        }
        rigidbody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        ////Debug.Log("Time.deltaTime*smoothing:" + Time.deltaTime * smoothing);
        //Debug.Log("rigidbody.velocity.x:"+rigidbody.velocity.x);
        rigidbody.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rigidbody.velocity.x * -tilt));
	}
}
