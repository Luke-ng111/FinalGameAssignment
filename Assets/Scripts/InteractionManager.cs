using UnityEngine;

// This class follows the Singleton design pattern.
//
// A Singleton ensures that only one instance (or object) of a class exists at a time throughout the game. 
// It provides a global point of access to that instance, meaning you don’t need to create new instances 
// every time you want to use it.
//
// In this case, the InteractionManager class will manage game interactions and should only have one instance
// throughout the entire game. This prevents multiple copies of the InteractionManager from interfering with 
// each other. Using a Singleton pattern helps maintain consistent behavior across different parts of the game.
//
// With the Singleton in place, you can easily access the InteractionManager instance anywhere in the code 
// using `InteractionManager.Instance`. The pattern also ensures that the instance is not duplicated, 
// and its state is maintained throughout the game.
//
public class InteractionManager : MonoBehaviour
{
    // Static instance of the class
    public static InteractionManager Instance { get; private set; }

    [SerializeField] private GameObject NoInteractTextContainer;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy this object if a duplicate is found
        }
        else
        {
            Instance = this;  // Set the instance to this object
            DontDestroyOnLoad(gameObject);  // Optional: persist the singleton across scenes
        }
    }

    public void SetInteractionTextActive(bool active)
    {
        NoInteractTextContainer.SetActive(active);
    }
}
