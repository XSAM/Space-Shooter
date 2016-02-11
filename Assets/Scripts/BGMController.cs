using UnityEngine;
using System.Collections;

public class BGMController: MonoBehaviour 
{
    public AudioClip audioClipA;
    public AudioClip audioClipB;
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
    
    void Update()
    {
        if(audioSource.isPlaying==false)
        {
            if(audioSource.clip==audioClipA)
                audioSource.clip = audioClipB;
            else
            {
                audioSource.clip = audioClipA;
            }
            audioSource.Play();
        }
    }

}

