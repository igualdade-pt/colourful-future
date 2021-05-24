using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDrag_Script : MonoBehaviour
{

    [SerializeField]
    private int index = 0;

    [Header("Note: Correct Ones First")]
    [Space]
    [SerializeField]
    private Sprite[] items;

    public void SetSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = items[index];

    }

    public int Index
    {
        get { return index; }
        set { index = value; }
    }
}
