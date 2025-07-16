using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    private bool isNearEnemy = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isNearEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isNearEnemy = false;
        }
    }

    private void Start()
    {
        Debug.Log(isNearEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearEnemy && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("load new scene");
            SceneManager.LoadScene("Wordle");
        }
    }
}
