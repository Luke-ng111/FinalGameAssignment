using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor; //maps the colours to the image and outline
    }

    private TextMeshProUGUI text; //calls textmesh pro element
    private Image fill;
    private Outline outline; //gets our colours from Unity

    public State state { get; private set; }
    public char letter { get; private set; }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    public void SetLetter(char letter) //sets the letter in the tile
    {
        this.letter = letter;
        text.text = letter.ToString();
    }

    //function to set the state
    public void SetState(State state)
    {
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor; //tutorial says its outline.effectcolor but that gives an error; im not sure why
    }
}
