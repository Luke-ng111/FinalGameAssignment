using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Board : MonoBehaviour
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

    //to keep track of which row we're on for the board
    private Row[] rows;

    //defining an index for the tiles
    private int rowIndex;
    private int columnIndex;
    //first column first row is 0,0, think of it like a graph or Matrix\

    //assign states
    [Header("States")] //allows title header to be shown in Unity Editor
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;
    [Header("UI")]
    public Button tryAgainButton;

    private void Awake()
    {
        // assigns the in-game rows to the rows array
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        LoadData();
        setWord();
    }

    private void ClearBoard() // this resets the board
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row].tiles[col].SetLetter('\0');
                rows[row].tiles[col].SetState(emptyState);
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

    private void setWord()
    {
        word = "horse";
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
            if(Input.GetKeyDown(KeyCode.Return))
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
            enabled = false;
        }

        if (rowIndex >= rows.Length)
        {
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
        tryAgainButton.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        tryAgainButton.gameObject.SetActive(true);
    }

}
