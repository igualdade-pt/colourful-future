using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [Header("Test Properties")]
    [Space]
    [SerializeField]
    private bool gameplayTest = false;

    [Header("--- Game Properties ---")]
    [Space]

    [SerializeField]
    private int indexGameSelected = -1;

    private UIManager_GM uiManager_GM;

    private GameInstanceScript gameInstance;

    private int indexLanguage = 0;

    private int indexDifficulty = 0;


    private int rows;
    private int cols;

    private int randomIndex = 0;


    // Match the Colour
    [Header("--- Match the Colour Game --- (0)")]
    [Space]

    [SerializeField]
    private GameObject matchColourGamePool;

    // Drag and Connect
    [Header("--- Drag and Connect Game --- (1)")]
    [Space]

    [SerializeField]
    private GameObject dragConnectGamePool;

    // Memory
    [Header("--- Memory Game --- (2)")]
    [Space]

    [SerializeField]
    private GameObject memoryGamePool;

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private GameObject cardPool;

    [SerializeField]
    private int numberOfCards;

    [SerializeField]
    private float multiplierPos = 1.8f;

    private int numberOfCardsRevealed = 0;

    private List<int> cardIndexes;

    private List<Cards_Script> cardsRevealed = new List<Cards_Script>(2);


    private void Start()
    {
        if (!gameplayTest)
        {
            gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();

            // Attribute Language      
            indexLanguage = gameInstance.LanguageIndex;
            switch (indexLanguage)
            {
                case 0:
                    Debug.Log("Main Menu, System language English: " + indexLanguage);

                    break;
                case 1:
                    Debug.Log("Main Menu, System language Italian: " + indexLanguage);

                    break;
                case 2:
                    Debug.Log("Main Menu, System language Portuguese: " + indexLanguage);

                    break;
                case 3:
                    Debug.Log("Main Menu, System language Spanish: " + indexLanguage);

                    break;
                case 4:
                    Debug.Log("Main Menu, System language Swedish: " + indexLanguage);
                    break;

                default:
                    Debug.Log("Main Menu, Unavailable language, English Selected: " + indexLanguage);

                    break;
            }

            // Attribute Difficulty      
            indexDifficulty = gameInstance.DifficultyLevelIndex;
            Debug.Log("Difficulty Selected " + indexDifficulty);

            // Attribute Adventure      
            indexGameSelected = gameInstance.GameIndex;
            Debug.Log("Game Selected " + indexGameSelected);

            //Set All Games SetActive False
            matchColourGamePool.SetActive(false);
            dragConnectGamePool.SetActive(false);
            memoryGamePool.SetActive(false);            
        }

        // Set Only the Game Selected
        switch (indexGameSelected)
        {
            case 0:
                MatchColourStart();
                matchColourGamePool.SetActive(true);
                break;

            case 1:
                DragConnectStart();
                dragConnectGamePool.SetActive(true);
                break;

            case 2:
                MemoryStart();
                memoryGamePool.SetActive(true);
                break;

            default:
                break;
        }
    }


    //MATCH THE COLOUR GAME
    private void MatchColourStart()
    {

    }

    // ******************************************************

    //Connect GAME
    private void DragConnectStart()
    {

    }

    // ******************************************************

    // MEMORY GAME
    private void MemoryStart()
    {
        cardIndexes = new List<int>(numberOfCards);
        for (int i = 0; i < numberOfCards / 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                cardIndexes.Add(i);
            }
        }

        rows = Mathf.CeilToInt(numberOfCards / 3f);
        cols = 3;

        int numbersOfCardsCreated = 0;
        float correctX = 0;
        float increaseY = 0;
        if (numberOfCards > 9 && numberOfCards <= 12)
        {
            increaseY = -0.5f;
        }


        for (int y = 0; y < rows; y++)
        {
            if (y == rows - 1)
            {
                cols = numberOfCards - numbersOfCardsCreated;
                if (cols % 2 == 0)
                {
                    correctX = 0.5f;
                }
            }

            for (int x = 0; x < cols; x++)
            {
                randomIndex = Random.Range(0, cardIndexes.Count);
                Vector3 position = new Vector3((cols / 2 - x - correctX) * multiplierPos, (rows / 2 - y + increaseY) * multiplierPos, 0);
                var temp = Instantiate(card, position, Quaternion.Euler(-5, 0, 0), cardPool.transform); // FIX ROTATION
                numbersOfCardsCreated++;
                temp.GetComponent<Cards_Script>().CardIndex = cardIndexes[randomIndex];
                temp.GetComponent<Cards_Script>().SetGameplayManager(this);
                cardIndexes.Remove(cardIndexes[randomIndex]);
            }
        }
    }

    public void AddCardRevealed(Cards_Script card)
    {
        cardsRevealed.Add(card);

        if (cardsRevealed.Count == cardsRevealed.Capacity)
        {
            numberOfCardsRevealed = 0;
            if (CheckMatch())
            {
                for (int i = 0; i < cardsRevealed.Count; i++)
                {
                    cardsRevealed[i].SetRemainVisible(true);
                    cardsRevealed[i].SetCanFlip(false);
                }
            }
            else
            {                
                for (int j = 0; j < cardsRevealed.Count; j++)
                {
                    cardsRevealed[j].SetCanFlip(true);
                    cardsRevealed[j].CardClicked();
                }
            }

            // Clear List
            cardsRevealed.Clear();
        }
    }

    private bool CheckMatch ()
    {
        bool success = false;
        if (cardsRevealed[0].CardIndex == cardsRevealed[1].CardIndex)
        {
            //Check if the indexes are the same
            success = true;
        }
        return success;
    }

    public bool TwoCardsRevealed()
    {
        bool success = false;
        if (numberOfCardsRevealed >= 2)
        {
            success = true;
        }
        return success;
    }

    public void IncreaseNumberOfCardsRevealed()
    {
        numberOfCardsRevealed++;
    }

    // ******************************************************



    public void GameEnded()
    {
        uiManager_GM.SetGameEndedPanel(true);
    }

    public void LoadSelectedScene(int indexSelected)
    {
        StartCoroutine(StartLoadAsyncScene(indexSelected));
    }

    private IEnumerator StartLoadAsyncScene(int indexLevel)
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexLevel);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}
