using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
		//Test The GitHub Sync Function
    private new Rigidbody rigidbody;
    private AudioSource []audioSource;
    private Quaternion calibrationQuaternion;

    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton areaButton;
    public float tilt;
    public float speed;
    public Boundary boundary;
    public GameObject shot;
    public Transform []shotSpawns;

    public float fireRate;
    public float nextFire;

    //private int count = 0;
    //private float time;



    void Update()
    {
        if (areaButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            foreach(var shotSpawn in shotSpawns)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            
            audioSource[0].Play();
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
         );

        //Debug.Log("Update:rb.position.x:" + rigidbody.position.x);
        //Debug.Log("Update:transform.position.x:" + transform.position.x);
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //time=Time.realtimeSinceStartup;
        audioSource = GetComponents<AudioSource>();
        CalibrateAccellerometer();
    }
    void CalibrateAccellerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        //Debug.Log(accelerationSnapshot.x+" "+accelerationSnapshot.y+" "+accelerationSnapshot.z);
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }
    Vector3 FixAccelleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }
    void FixedUpdate()
    {
        Vector3 accelerationRaw = Input.acceleration;
        //Debug.Log(accelerationRaw.x + " " + accelerationRaw.y + " " + accelerationRaw.z);
        Vector3 acceleration = FixAccelleration(accelerationRaw);

        //Vector3 movement=new Vector3(acceleration.x,0.0f,acceleration.y);
        //Debug.Log(" "+movement.x + " " + movement.y + " " + movement.z);

        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        rigidbody.velocity = movement * speed;

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //rigidbody.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
        //if (Time.realtimeSinceStartup - time <= 1f)
        //{
        //    rigidbody.velocity = (new Vector3(1 / Time.deltaTime, 0, 0));
        //    count++;
        //}
        //else
        //{
        //    rigidbody.velocity = (new Vector3(0, 0, 0));
        //}

        //rigidbody.position = new Vector3
        //(
        //    0,//Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
        //    0,
        //    0//Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        //);



        //rb.AddativeForce(new Vector3(-moveHorizontal, 0.0f, -moveVertical) * speed);
        //if (rb.position.x <= boundary.xMin) 
        //{
        //	rb.velocity=new Vector3(0.0f,0.0f,0.0f);
        //}

        //Debug.Log("Time.realtimesSinceStartup:" + Time.realtimeSinceStartup);
        //Debug.Log("Time.deltaTime:"+Time.deltaTime);
        //Debug.Log("velocity(Time.deltaTime):" + 1 / Time.deltaTime);
        //Debug.Log("velocity:"+rigidbody.velocity.x);

        //Debug.Log("x:" + Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax));
        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);


    }

    void Test()
    {
        Debug.Log("First:fixedUpdate:rb.position.x:" + rigidbody.position.x);
        Debug.Log("First:fixedUpdate:transform.position.x:" + transform.position.x);

        rigidbody.position = new Vector3(2f, 0, 0);
        Debug.Log("Mid:fixedUpdate:rb.position.x:" + rigidbody.position.x);
        Debug.Log("Mid:fixedUpdate:transform.position.x:" + transform.position.x);
        transform.position = new Vector3(1f, 0, 0);

        Debug.Log("Last:fixedUpdate:rb.position.x:" + rigidbody.position.x);
        Debug.Log("Last:fixedUpdate:transform.position.x:" + transform.position.x);
    }
}

