using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{

    public static MusicPlayer instance { get; private set; }

    private void Awake()
    {


        if (instance == null )
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
