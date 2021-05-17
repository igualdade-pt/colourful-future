using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCards_Script : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] cardsFaces;

    [SerializeField]
    private Sprite[] firstSetOfCards;

    [SerializeField]
    private Sprite[] secondSetOfCards;

    [SerializeField]
    private Sprite[] thirdSetOfCards;

    private Sprite[][] cardsFacesByType = new Sprite[3][];

    [SerializeField]
    private Sprite cardBack;

    private int cardIndex;

    private int cardGroupIndex;

    private bool canFlip = true;

/*    private GameplayManager gameplayManager;

    private bool remainVisible = false;

    private bool gameStarted = false;*/

    [Header("Animation Card")]
    [SerializeField]
    private Animator myAnimator;


    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        canFlip = true;

        // Initialize the elements.
        cardsFacesByType[0] = firstSetOfCards;
        cardsFacesByType[1] = secondSetOfCards;
        cardsFacesByType[2] = thirdSetOfCards;
    }

    public void FlipCard()
    {
        if (canFlip)
        {
            canFlip = false;
            // Rotation And Flip Sprite
            if (spriteRenderer.sprite == cardBack) // Turn Face
            {
                myAnimator.SetTrigger("FlipBack");
            }
            else // Turn Back
            {
                myAnimator.SetTrigger("FlipFront");
            }
        }
    }

    private void FinishAnimation()
    {

        canFlip = true;

    }

    private void SwitchSprite()
    {
        if (spriteRenderer.sprite == cardBack) // Turn Face
        {
            spriteRenderer.sprite = cardsFaces[cardIndex];
        }
        else // Turn Back
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    public int CardIndex
    {
        get { return cardIndex; }
        set { cardIndex = value; }
    }

    public int CardGroupIndex
    {
        get { return cardGroupIndex; }
        set { cardGroupIndex = value; }
    }

    /*    public void SetGameplayManager(GameplayManager gm)
        {
            gameplayManager = gm;
        }*/
}
