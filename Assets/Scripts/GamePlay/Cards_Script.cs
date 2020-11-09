using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards_Script : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] cardsFaces;

    [SerializeField]
    private Sprite cardBack;

    private int cardIndex;

    private bool canFlip = true;

    private GameplayManager gameplayManager;

    private bool remainVisible = false;

    [Header("Animation Card")]
    [SerializeField]
    private Animator myAnimator;


    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
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
        if (spriteRenderer.sprite != cardBack)
        {
            gameplayManager.AddCardRevealed(this);
        }
        else 
        {
            canFlip = true;
        }
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
}
