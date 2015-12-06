using UnityEngine;
using System.Collections;

public class BGMController: MonoBehaviour 
{
    private AudioSource audioSource;
    static BGMController instance;

	// Use this for initialization
	void Awake () 
    {
        if(instance!=null&&instance!=this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(this.gameObject);
	}

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
    }

}

