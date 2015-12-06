using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("cannot find 'GameController' script");
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Boundary"&&other.tag!="Enemy")
        {
            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            if(explosion!=null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            
            gameController.AddScore(scoreValue);
            Debug.Log("In:" + this.name + " Destroy:" + other.name+" this.name");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


}
