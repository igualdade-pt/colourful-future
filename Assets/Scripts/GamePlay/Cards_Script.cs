using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards_Script : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] cardsFaces;

    [SerializeField]
    private Sprite[] firstSetOfCards;

    [SerializeField]
    private Sprite[] secondSetOfCards;

    [SerializeField]
    private Sprite[] thirdSetOfCards;

    private Sprite[] [] cardsFacesByType = new Sprite[3][];

    [SerializeField]
    private Sprite cardBack;

    private int cardIndex;

    private int cardGroupIndex;

    private bool canFlip = true;

    private GameplayManager gameplayManager;

    private bool remainVisible = false;

    private bool gameStarted = false;

    [Header("Animation Card")]
    [SerializeField]
    private Animator myAnimator;


    private void Start()
    {
        spriteRenderer.sprite = cardBack;
        canFlip = true;

        // Initialize the elements.
        cardsFacesByType[0] = firstSetOfCards;
        cardsFacesByType[1] = secondSetOfCards;
        cardsFacesByType[2] = thirdSetOfCards;
    }

    public void CardClicked()
    {
        
        if (canFlip && !remainVisible && !gameplayManager.TwoCardsRevealed())
        {
            canFlip = false;
            // Rotation And Flip Sprite
            if (spriteRenderer.sprite == cardBack) // Turn Face
            {
                gameplayManager.IncreaseNumberOfCardsRevealed();
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
        if (gameStarted)
        {
            if (spriteRenderer.sprite != cardBack)
            {
                gameplayManager.AddCardRevealed(this);
            }
            else
            {
                canFlip = true;
            }
        }
    }

    private void SwitchSprite()
    {
        if (spriteRenderer.sprite == cardBack) // Turn Face
        {
            spriteRenderer.sprite = cardsFacesByType[cardGroupIndex][cardIndex];
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

    public void SetGameplayManager(GameplayManager gm)
    {
        gameplayManager = gm;
    }

    public void SetRemainVisible(bool temp)
    {
        remainVisible = temp;
    }

    public void SetCanFlip(bool flip)
    {
        canFlip = flip;
    }


    /// <summary>
    /// After this flip the game will start?
    /// </summary>
    public void FlipCards(bool value)
    {
        if (canFlip && !remainVisible)
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
        gameStarted = value;
    }
}
