/* using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridBoard : MonoBehaviour
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
    private string word; //the loaded answer, hardcoded for now

    //sets the final correct word

    //to keep track of which row we're on for the Grid Board
    private Tile[][] rows;

    [SerializeField] private GridLayoutGroup grid;  // Parent UI
    [SerializeField] private GameObject tilePrefab; // Prefab for Tile
    [SerializeField] private int rowsCount = 6;

    private int cols; // determined by word length

    //defining an index for the Tiles
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
    [Header("UI")]
    public Button tryAgainButton;
    public Button exitButton;



    private void Awake()
    {
        // assigns the in-game rows to the rows array
        //rows = GetComponentsInChildren<Row>();
        audioClip = GetComponent<AudioClip>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        cols = DataContainer.Word.Length;   // or another dynamic array length
        if (cols <= 0 || tilePrefab == null || grid == null)
        {
            Debug.LogError("Missing grid or prefab or invalid column count.");
            return;
        }
        BuildBoard(rowsCount, cols);
        ClearBoard();
        LoadData();
        SetData();
    }

    private void BuildBoard(int rowsCount, int cols)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;

        rows = new Tile[rowsCount][];
        for (int r = 0; r < rowsCount; r++)
        {
            rows[r] = new Tile[cols];
            for (int c = 0; c < cols; c++)
            {
                var go = Instantiate(tilePrefab, grid.transform, false);
                var tile = go.GetComponent<Tile>();

                if (tile == null)
                {
                    Debug.LogError("tilePrefab is missing the Tile component!", tilePrefab);
                }

                rows[r][c] = tile;
            }
        }
    }

    private void ClearBoard() // this resets the board
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].Length; col++)
            {
                rows[row][col].SetLetter('\0');
                rows[row][col].SetState(emptyState);
            }
        }

        rowIndex = 0;
        columnIndex = 0;
    }

    public void TryAgain()
    {
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
        //word = DataContainer.Word;  // "horse";
        word = "rock";
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
        if (rows == null) { Debug.LogError("rows is null"); return; }
        if (rowIndex >= rows.Length) { Debug.Log("rowIndex out of range"); return; }
        if (rows[rowIndex] == null) { Debug.LogError($"rows[{rowIndex}] is null"); return; }

        if (rows == null || rowIndex >= rows.Length) return;

        Tile[] currentRow = rows[rowIndex];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //allow for backspacing
            //columnIndex--; doesnt work as it can go out of bounds, we need to 'clamp', and it must be done first to reset index back by one
            columnIndex = Mathf.Max(columnIndex - 1, 0); //this gets the greatest value (the maximum) between the index-1 and 0 to reset the value, thus clamping it by a lower bound only
            //'\0' is a null value and counts as a single character, apparently
            currentRow[columnIndex].SetLetter('\0');
            currentRow[columnIndex].SetState(emptyState);
        }

        else if (columnIndex >= currentRow.Length)
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
                    currentRow[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                    //advances to next tile
                    columnIndex++;
                    currentRow[columnIndex].SetState(occupiedState);
                    //clamps the Col row to prevent out of index
                    columnIndex = Mathf.Min(columnIndex + 1, currentRow.Length);
                    //breaks out of for loop; prevents spamming 
                    break;
                }
            }
        }
    }

    private void SubmitRow(Tile[] row)
    {
        for (int i = 0; i < row.Length; i++)
        {
            Tile tile = row[i];
            if (tile.letter == word[i])
            {
                tile.SetState(correctState);
            }
            else if (word.Contains(tile.letter))
            {
                tile.SetState(wrongSpotState);
            }
            else
            {
                tile.SetState(incorrectState);
            }
        }

        //moves to next row and resets word back to zero
        rowIndex++;
        columnIndex = 0;

        if (HasWon(row))
        {
            winButton();
            enabled = false;
        }

        if (rowIndex >= rows.Length)
        {
            loseButton();
            enabled = false; //disables the script, will allow you to try again
        }

    }

    private bool HasWon(Tile[] row)
    {
        for (int i = 0; i < row.Length; i++)
        {
            if (row[i].state != correctState)
            {
                return false;
            }
        }

        return true;
    }

    private void OnEnable()
    {
        tryAgainButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    private void winButton()
    {
        exitButton.gameObject.SetActive(true);
    }

    private void loseButton()
    {
        tryAgainButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {

    } 

} */

//Line break; the following is AI-Generated trial code. If it works, delete and go through line by line to understand the issues


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridBoard : MonoBehaviour
{
    [Header("Dynamic Grid Settings")]
    public GameObject rowPrefab;
    public GameObject tilePrefab;
    public Transform boardParent;  // Container where rows will be instantiated
    public int totalRows = 6;

    [Header("Word Info")]
    private string word; // Target word
    private int wordLength;

    private List<Row> rows = new List<Row>();
    private int currentRow = 0;
    private int currentCol = 0;

    [Header("Tile States")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G,
        KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P,
        KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V,
        KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
    };

    private void Start()
    {
        Debug.Log("Script active!");
        //word = DataContainer.Word.ToLower().Trim();  // You can hardcode for testing
        word = "rock";
        wordLength = word.Length;

        GenerateRows();
    }

    private void GenerateRows()
    {
        for (int i = 0; i < totalRows; i++)
        {
            GameObject rowObj = Instantiate(rowPrefab, boardParent);
            for (int j = 0; j < wordLength; j++)
            {
                Instantiate(tilePrefab, rowObj.transform);
            }

            Row row = rowObj.GetComponent<Row>();
            rows.Add(row);

            foreach (var tile in row.Tiles)
                tile.SetState(emptyState);
        }
    }

    private void Update()
    {
        if (currentRow >= rows.Count) return;

        Row activeRow = rows[currentRow];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentCol > 0)
            {
                currentCol--;
                activeRow.Tiles[currentCol].SetLetter('\0');
                activeRow.Tiles[currentCol].SetState(emptyState);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentCol >= wordLength)
        {
            SubmitRow(activeRow);
        }
        else if (currentCol < wordLength)
        {
            foreach (var key in SUPPORTED_KEYS)
            {
                if (Input.GetKeyDown(key))
                {
                    char letter = (char)key;
                    activeRow.Tiles[currentCol].SetLetter(letter);
                    activeRow.Tiles[currentCol].SetState(occupiedState);
                    currentCol++;
                    break;
                }
            }
        }
    }

    private void SubmitRow(Row row)
    {
        for (int i = 0; i < wordLength; i++)
        {
            char guessedChar = row.Tiles[i].letter;

            if (guessedChar == word[i])
            {
                row.Tiles[i].SetState(correctState);
            }
            else if (word.Contains(guessedChar.ToString()))
            {
                row.Tiles[i].SetState(wrongSpotState);
            }
            else
            {
                row.Tiles[i].SetState(incorrectState);
            }
        }

        if (HasWon(row))
        {
            Debug.Log("You Win!");
            enabled = false;
            return;
        }

        currentRow++;
        currentCol = 0;

        if (currentRow >= totalRows)
        {
            Debug.Log("You Lose!");
            enabled = false;
        }
    }

    private bool HasWon(Row row)
    {
        for (int i = 0; i < wordLength; i++)
        {
            if (row.Tiles[i].state != correctState)
                return false;
        }
        return true;
    }
}