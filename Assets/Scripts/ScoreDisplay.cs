using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //scoreText = scoreGO.GetComponent<TMPro.TextMeshProUGUI>();
        //scoreText.text = "Score: " + ScoreManager.Score.ToString();
        scoreText.text = "ពិន្ទុ: " + ScoreManager.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
