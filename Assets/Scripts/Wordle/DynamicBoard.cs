using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DynamicBoard : MonoBehaviour
{
    //this code provides an array to run through all the keys being used in this particular scene, so as to 
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
   {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G,
        KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P,
        KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V,
        KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
   };

    //stores all the solutions; for now, it is hardcoded
    private string[] solutions; //the dictionary for all the words in the game
    private string word; //the loaded answer

    //sets the final correct word

    //to keep track of which row we're on for the board
    private Row[] rows;

    //defining an index for the tiles
    private int rowIndex;
    private int columnIndex;
    //first column first row is 0,0, think of it like a graph or Matrix\

    //gets the audio to play
    public AudioSource audioSource;
    public AudioClip audioClip;

    //assign states
    [Header("States")] //allows title header to be shown in Unity Editor
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    //this adds and links the buttons
    public Button tryAgainButton;
    public Button exitButton;

    private bool isInitialized = false; //keeps track of if the script is started or not; for debugging purposes

    private BoardGenerator boardGenerator; //variable for the boardGenerator script



    private void Awake()
    {
        audioClip = GetComponent<AudioClip>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(Button tryAgain, Button exit)
    {
        tryAgainButton = tryAgain;
        exitButton = exit;
    }

    private void Start()
    {
        //gets the BoardGenerator.cs script
        boardGenerator = FindFirstObjectByType<BoardGenerator>();
        // assigns the in-game rows to the rows array
        rows = GetComponentsInChildren<Row>();
        LoadData();
        SetData();

        //this wires the buttons
        if (tryAgainButton != null)
            tryAgainButton.onClick.AddListener(TryAgain);

        if (exitButton != null)
            exitButton.onClick.AddListener(returnToScene);

        isInitialized = true;
    }

    private void ClearBoard() // this resets the board
    {

        rows = GetComponentsInChildren<Row>();

        if (rows == null || rows.Length == 0)
        {
            Debug.LogWarning("ClearBoard: No rows found!");
            return;
        }

        for (int row = 0; row < rows.Length; row++)
        {
            if (rows[row] == null)
            {
                Debug.LogWarning($"ClearBoard: Row {row} is null");
                continue;
            }

            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                if (rows[row].tiles[col] == null)
                {
                    Debug.LogWarning($"ClearBoard: Tile at row {row}, col {col} is null");
                    continue;
                }

                rows[row].tiles[col].SetLetter('\0');
                rows[row].tiles[col].SetState(emptyState);
            }
        }

        rowIndex = 0;
        columnIndex = 0;
    }

    public void TryAgain()
    {
        if (!isInitialized)
        {
            Debug.LogWarning("TryAgain called before board initialized!");
            return;
        }

        ClearBoard();
        enabled = true;

    }
    public void returnToScene()
    {
        ClearBoard();
        ScoreManager.AddPoints(1000);
        SceneManager.LoadScene("RPG-grassland");
    }

    private void SetData()
    {
        word = DataContainer.Word;  // "horse";
        audioSource.clip = DataContainer.AudioClip;
        Debug.Log(word);
        word = word.ToLower().Trim();
    }

    private void LoadData()
    {
        //to load in the final dictionary
        Debug.Log("dictionary loaded!");
    }

    private void Update()
    {
        Row currentRow = rows[rowIndex];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //allow for backspacing
            //columnIndex--; doesnt work as it can go out of bounds, we need to 'clamp', and it must be done first to reset index back by one
            columnIndex = Mathf.Max(columnIndex - 1, 0); //this gets the greatest value (the maximum) between the index-1 and 0 to reset the value, thus clamping it by a lower bound only
            //'\0' is a null value and counts as a single character, apparently
            currentRow.tiles[columnIndex].SetLetter('\0');
            currentRow.tiles[columnIndex].SetState(emptyState);
        }

        else if (columnIndex >= currentRow.tiles.Length)
        {
            //submits a row
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitRow(currentRow);
            }
        }

        else
        {
            //use an array because typing out keycodes for the entire alphabet is tiring and inefficient
            for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
            {
                //set letter for the tile to keep track of which tile out of 5 we are on
                if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                {
                    currentRow.tiles[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                    //advances to next tile
                    columnIndex++;
                    currentRow.tiles[columnIndex].SetState(occupiedState);
                    //breaks out of for loop; prevents spamming 
                    break;
                }
            }
        }
    }

    private void SubmitRow(Row row) //this function determines the correct place for words and submits a row
    {
        //submit a row
        Debug.Log("Row submitted!");

        //runs through entire row to check if letters are correct
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];

            //accessing the index of the tile in our word to compare characters
            if (tile.letter == word[i])
            {
                //correct letter in correct space
                tile.SetState(correctState);
            }
            //contains checks if the char is in the word
            else if (word.Contains(tile.letter))
            {
                //correct letter in wrong space
                tile.SetState(wrongSpotState);
            }
            else
            {
                //incorrect entirely
                tile.SetState(incorrectState);
            }
        }

        //moves to next row and resets word back to zero
        rowIndex++;
        columnIndex = 0;

        if (HasWon(row))
        {
            boardGenerator.winButton();
            enabled = false;
        }

        if (rowIndex >= rows.Length)
        {
            boardGenerator.loseButton();
            enabled = false; //disables the script, will allow you to try again
        }

    }

    private bool HasWon(Row row)
    {
        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (row.tiles[i].state != correctState)
            {
                return false;
            }
        }

        return true;
    }

    private void OnEnable()
    {
        //tryAgainButton.gameObject.SetActive(false);
        //exitButton.gameObject.SetActive(false);
    }

    /*private void winButton()
    {
        exitButton.gameObject.SetActive(true);
    }

    private void loseButton()
    {
        tryAgainButton.gameObject.SetActive(true);
    } */

    private void OnDisable()
    {

    }

}
