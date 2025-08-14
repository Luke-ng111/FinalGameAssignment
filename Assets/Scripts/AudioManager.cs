using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    SerializeField AudioSourceOne;

    private void Awake()
    {
        if (Instance == null)
            {
                Instance = this;
            }
        DontDestroyOnLoad(gameObject);    //persist the singleton across scenes     
    }


public void Start()
    {
        GetComponentInChildren<AudioSource>();
        if (gameObject.GetComponent<AudioSource>() != null )
        {
            //AudioSourceOne = gameObject;
        }
    }


}
