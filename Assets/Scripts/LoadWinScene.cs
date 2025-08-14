using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScene : MonoBehaviour
{
    [SerializeField] public GameObject Player;

    private void ClearData()
    {
        Player.transform.position = Vector3.zero;
        Player.transform.rotation = Quaternion.identity;
        //ScoreManager.ResetScore();
    }

    // Update is called once per frame
    void Update()
    { 

        if (ScoreManager.Score >= 22000)
        {
            ClearData();
            SceneManager.LoadScene("End Scene");
        }
    }
}
