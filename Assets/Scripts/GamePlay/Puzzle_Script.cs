using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Script : MonoBehaviour
{
    [SerializeField]
    private bool isPiece;

    [SerializeField]
    private int index = 0;

    private bool empty = true;

    [SerializeField]
    private Sprite [] pieces;

    private void Start()
    {
        empty = true;
    }

    public void SetSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = pieces[index];
    }

    public bool IsPiece
    {
        get { return isPiece; }
        set { isPiece = value; }
    }

    public bool Empty
    {
        get { return empty; }
        set { empty = value; }
    }

    public int Index
    {
        get { return index; }
        set { index = value; }
    }
}
