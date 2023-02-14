using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }

    public TileCell cell { get; private set; }

    public int number { get; private set; }

    private Image background;
    private TextMeshProUGUI text;

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        background.color = state.BackgroundColor;
        text.color = state.TextColor;
        text.text = number.ToString();
    }


}
