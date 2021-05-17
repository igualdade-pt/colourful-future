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

    private Player_Script playerScript;

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

    // ***************************************************************************************** //

    // Drag and Connect
    [Header("--- Drag and Connect Game --- (1)")]
    [Space]

    [SerializeField]
    private GameObject dragConnectGamePool;

    [SerializeField]
    private GameObject connectCard;

    [SerializeField]
    private float[] connectCardPositionX = new float[2] { -2, 2 };

    [SerializeField]
    private float connectCardPositionY = 2;

    [SerializeField]
    private GameObject characterCardConnect;

    [SerializeField]
    private Transform cardCharacterConnectPosition;

    [SerializeField]
    private GameObject connectCardPool;

    [SerializeField]
    private GameObject linePool;

    [SerializeField]
    private float multiplierYForConnectGame = 1.8f;

    [SerializeField]
    private int numberOfConnectCardsFaces = 7;

    [SerializeField]
    private float timeBetweenCards = 2;

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [Space]

    [Header("Difficulty: ")]
    [SerializeField]
    private int[] numberOfConnectCardsByDifficulty = new int[2] { 6, 8 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfRightConnectsByDifficulty = new int[2] { 6, 8 };

    [Header("Element 0 , 1 - Basic (min , sec), Element 2 ,3 - Expert (min , sec)")]
    [SerializeField]
    private int[] timerByDifficulty = new int[4] { 1, 30,
                                                    0, 59 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfAttemptsToConnectByDifficulty = new int[2] { 4, 4 };

    private int numberOfConnectCards;

    private int[] selectedCharacterIndexes;

    private List<int> selectedCharacterFailedIndexes = new List<int>();

    private int numberOfMoves = 0;

    private int numberOfRightConnects;

    private int numberOfAttemptsToConnect;

    private int sec;

    private int min;

    private List<int> connectCardIndexes;


    // ***************************************************************************************** //

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
    private float multiplierXYForMemoryGame = 1.8f;

    [SerializeField]
    private int numberOfCardsFaces = 7;

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [Space]

    [Header("Difficulty: ")]
    [SerializeField]
    private int[] numberOfCardsByDifficulty = new int[2] { 8, 12 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private float[] timeToFlipCardsByDifficulty = new float[2] { 4, 2 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfAttemptsByDifficulty = new int[2] { 4, 4 };

    private int numberOfCards;

    private int numberOfRightPairs;

    private int numberOfAttempts;

    private int numberOfCardsRevealed = 0;

    private List<int> cardIndexes;

    private List<Cards_Script> cardsRevealed = new List<Cards_Script>(2);


    // ***************************************************************************************** //

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

            // Attribute Game      
            indexGameSelected = gameInstance.GameIndex;
            Debug.Log("Game Selected " + indexGameSelected);

            //Set All Games SetActive False
            matchColourGamePool.SetActive(false);
            dragConnectGamePool.SetActive(false);
            memoryGamePool.SetActive(false);
        }

        uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();

        // Set Only the Game Selected
        switch (indexGameSelected)
        {
            case 0:
                MatchColourStart();
                matchColourGamePool.SetActive(true);
                break;

            case 1:
                numberOfConnectCards = numberOfConnectCardsByDifficulty[0];
                numberOfAttemptsToConnect = numberOfAttemptsToConnectByDifficulty[0];
                numberOfRightConnects = numberOfRightConnectsByDifficulty[0];
                min = timerByDifficulty[0];
                sec = timerByDifficulty[0 + 1];
                uiManager_GM.UpdateAttempts(numberOfAttemptsToConnect);
                uiManager_GM.SetTimerActive(true);
                uiManager_GM.UpdateTimer(min, sec);
                dragConnectGamePool.SetActive(true);
                DragConnectStart();
                StartCoroutine(CountdownSec());
                break;

            case 2:
                numberOfCards = numberOfCardsByDifficulty[0];
                numberOfAttempts = numberOfAttemptsByDifficulty[0];
                uiManager_GM.UpdateAttempts(numberOfAttempts);
                memoryGamePool.SetActive(true);
                MemoryStart();
                StartCoroutine(WaitToFlipCards(timeToFlipCardsByDifficulty[0]));
                numberOfRightPairs = 0;
                break;

            default:
                break;
        }

        playerScript = FindObjectOfType<Player_Script>().GetComponent<Player_Script>();
        playerScript.GameStarted(true);
    }


    //MATCH THE COLOUR GAME
    private void MatchColourStart()
    {

    }

    // ******************************************************

    //Connect GAME
    private void DragConnectStart()
    {

        var cardsFace = new List<int>(numberOfConnectCardsFaces);
        for (int i = 0; i < numberOfConnectCardsFaces; i++)
        {
            cardsFace.Add(i);
        }

        connectCardIndexes = new List<int>(numberOfConnectCards);
        for (int i = 0; i < numberOfConnectCards; i++)
        {

            int x = cardsFace[Random.Range(0, cardsFace.Count)];

            connectCardIndexes.Add(x);

            cardsFace.Remove(x);

        }

        rows = Mathf.CeilToInt(numberOfConnectCards / 2f);
        cols = 2;

        int numbersOfConnectCardsCreated = 0;


        if (numberOfConnectCards < 8)
        {
            connectCardPositionY = (multiplierYForConnectGame / 2);
            connectCardPositionY += ((multiplierYForConnectGame / 2) / 2);
        }

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                randomIndex = Random.Range(0, connectCardIndexes.Count);
                Vector3 position = new Vector3(connectCardPositionX[x], (connectCardPositionY - y) * multiplierYForConnectGame, 0);
                var temp = Instantiate(connectCard, position, Quaternion.identity, connectCardPool.transform); // FIX ROTATION
                numbersOfConnectCardsCreated++;
                temp.GetComponent<ConnectCards_Script>().CardIndex = connectCardIndexes[randomIndex];
                temp.GetComponent<ConnectCards_Script>().CardGroupIndex = Random.Range(0, 3);

                temp.GetComponent<ConnectCards_Script>().FlipCard();
                connectCardIndexes.Remove(connectCardIndexes[randomIndex]);
            }
        }

        int numberOfCardsCreated = connectCardPool.transform.childCount;

        var cards = new ConnectCards_Script[numberOfCardsCreated];
        for (int i = 0; i < numberOfCardsCreated; i++)
        {
            var card = connectCardPool.transform.GetChild(i).gameObject.GetComponent<ConnectCards_Script>();
            cards[i] = card;
        }

        int randomCard = Random.Range(0, cards.Length);

        int indexSelected = cards[randomCard].CardIndex;
        int indexGoupSelected = cards[randomCard].CardGroupIndex;
        while (indexGoupSelected == cards[randomCard].CardGroupIndex)
        {
            indexGoupSelected = Random.Range(0, 2);
        }

        Debug.Log("INDEX CARD: " + indexSelected + " , " + "GROUP INDEX: " + indexGoupSelected);

        var specialCard = Instantiate(characterCardConnect, cardCharacterConnectPosition.position, Quaternion.identity, dragConnectGamePool.transform);
        specialCard.GetComponent<ConnectCards_Script>().CardIndex = indexSelected;
        specialCard.GetComponent<ConnectCards_Script>().CardGroupIndex = indexGoupSelected;

        specialCard.GetComponent<ConnectCards_Script>().FlipCard();


        selectedCharacterIndexes = new int[numberOfRightConnects + numberOfAttemptsToConnect];

        selectedCharacterIndexes[numberOfMoves] = indexSelected;
    }

    public void CheckingCard(GameObject cardChosen)
    {
        StopAllCoroutines();
        var card = cardChosen.GetComponent<ConnectCards_Script>();
        if (selectedCharacterIndexes[numberOfMoves] == card.CardIndex)
        {
            numberOfRightConnects--;
            if (numberOfRightConnects <= 0)
            {
                uiManager_GM.SetGameEndedPanel(true);
            }
            else
            {
                StartCoroutine(NextCard());
            }
            playerScript.GameStarted(false);
        }
        else
        {
            selectedCharacterFailedIndexes.Add(selectedCharacterIndexes[numberOfMoves]);
            numberOfAttemptsToConnect--;
            uiManager_GM.UpdateAttempts(numberOfAttemptsToConnect);
            if (numberOfAttemptsToConnect <= 0)
            {
                uiManager_GM.UpdateAttempts(0);
                uiManager_GM.SetGameEndedPanel(true);
                if (linePool.transform.childCount > 0)
                {
                    for (int i = 0; i < linePool.transform.childCount; i++)
                    {
                        Destroy(linePool.transform.GetChild(i).gameObject);
                    }
                }

                Debug.Log("perdeu");
            }
            else
            {
                StartCoroutine(NextCard());
            }
            playerScript.GameStarted(false);
        }
    }

    private IEnumerator NextCard()
    {
        numberOfMoves++;
        int numberOfCardsCreated = connectCardPool.transform.childCount;

        for (int i = 0; i < numberOfCardsCreated; i++)
        {
            connectCardPool.transform.GetChild(i).gameObject.GetComponent<ConnectCards_Script>().FlipCard();
        }

        GameObject.FindGameObjectWithTag("CharacterCard").GetComponent<ConnectCards_Script>().FlipCard();

        yield return new WaitForSeconds(1);

        min = timerByDifficulty[0];
        sec = timerByDifficulty[0 + 1];
        uiManager_GM.UpdateTimer(min, sec);

        yield return new WaitForSeconds(timeBetweenCards - 1);
        NextDragConnect();

        StartCoroutine(CountdownSec());

        yield return new WaitForSeconds(0.5f);
        playerScript.GameStarted(true);
    }

    private IEnumerator CountdownSec()
    {
        int counter = sec;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            uiManager_GM.UpdateTimer(min, counter);
        }
        CountdownMin();
    }

    private void CountdownMin()
    {
        if (min > 0)
        {
            min--;
            sec = 59;
            uiManager_GM.UpdateTimer(min, sec);
            StartCoroutine(CountdownSec());
        }
        else
        {
            StopAllCoroutines();
            selectedCharacterFailedIndexes.Add(selectedCharacterIndexes[numberOfMoves]);
            numberOfAttemptsToConnect--;
            uiManager_GM.UpdateAttempts(numberOfAttemptsToConnect);
            if (numberOfAttemptsToConnect <= 0)
            {
                uiManager_GM.UpdateAttempts(0);
                uiManager_GM.SetGameEndedPanel(true);
                Debug.Log("perdeu");
            }
            else
            {
                StartCoroutine(NextCard());
            }
            if (linePool.transform.childCount > 0)
            {
                for (int i = 0; i < linePool.transform.childCount; i++)
                {
                    Destroy(linePool.transform.GetChild(i).gameObject);
                }
            }
            playerScript.GameStarted(false);
        }
    }


    private void NextDragConnect()
    {
        var cardsFace = new List<int>(numberOfConnectCardsFaces);
        for (int i = 0; i < numberOfConnectCardsFaces; i++)
        {
            cardsFace.Add(i);
        }

        connectCardIndexes = new List<int>(numberOfConnectCards);
        for (int i = 0; i < numberOfConnectCards; i++)
        {

            int x = cardsFace[Random.Range(0, cardsFace.Count)];

            connectCardIndexes.Add(x);

            cardsFace.Remove(x);

        }

        rows = Mathf.CeilToInt(numberOfConnectCards / 2f);
        cols = 2;

        int numbersOfConnectCardsCreated = 0;


        if (numberOfConnectCards < 8)
        {
            connectCardPositionY = (multiplierYForConnectGame / 2);
            connectCardPositionY += ((multiplierYForConnectGame / 2) / 2);
        }

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                randomIndex = Random.Range(0, connectCardIndexes.Count);

                var temp = connectCardPool.transform.GetChild(numbersOfConnectCardsCreated).gameObject;

                numbersOfConnectCardsCreated++;
                temp.GetComponent<ConnectCards_Script>().CardIndex = connectCardIndexes[randomIndex];
                temp.GetComponent<ConnectCards_Script>().CardGroupIndex = Random.Range(0, 3);

                temp.GetComponent<ConnectCards_Script>().FlipCard();
                connectCardIndexes.Remove(connectCardIndexes[randomIndex]);
            }
        }

        int numberOfCardsCreated = connectCardPool.transform.childCount;

        var cards = new ConnectCards_Script[numberOfCardsCreated];
        for (int i = 0; i < numberOfCardsCreated; i++)
        {
            var card = connectCardPool.transform.GetChild(i).gameObject.GetComponent<ConnectCards_Script>();
            cards[i] = card;
        }

        int randomCard = Random.Range(0, cards.Length);
        int randomCardIndex;
        bool findIndex = true;

        int numberOfSearchFail = 0;
        while (findIndex)
        {
            randomCard = Random.Range(0, cards.Length);
            randomCardIndex = cards[randomCard].CardIndex;
            for (int i = 0; i < numberOfMoves; i++)
            {
                if (randomCardIndex != selectedCharacterIndexes[i])
                {
                    findIndex = false;
                }
                else
                {
                    findIndex = true;
                    numberOfSearchFail++;
                    if (numberOfSearchFail >= 10)
                    {
                        bool findOther = true;
                        while (findOther)
                        {
                            for (int j = 0; j < selectedCharacterFailedIndexes.Count; j++)
                            {
                                for (int l = 0; l < cards.Length; l++)
                                {
                                    randomCardIndex = cards[l].CardIndex;
                                    Debug.Log(selectedCharacterFailedIndexes[j]);
                                    if (randomCardIndex == selectedCharacterFailedIndexes[j])
                                    {
                                        randomCard = l;                                        
                                        findOther = false;
                                        selectedCharacterFailedIndexes.RemoveAt(j);
                                        break;
                                    }
                                    else if (j >= selectedCharacterFailedIndexes.Count - 1)
                                    {

                                        break;
                                    }
                                }
                                if (!findOther)
                                {
                                    break;
                                }
                            }                            
                        }
                        findIndex = false;
                    }
                    break;
                }
            }
        }

        int indexSelected = cards[randomCard].CardIndex;
        int indexGoupSelected = cards[randomCard].CardGroupIndex;
        while (indexGoupSelected == cards[randomCard].CardGroupIndex)
        {
            indexGoupSelected = Random.Range(0, 2);
        }

        Debug.Log("INDEX CARD: " + indexSelected + " , " + "GROUP INDEX: " + indexGoupSelected);


        var specialCard = GameObject.FindGameObjectWithTag("CharacterCard");
        specialCard.GetComponent<ConnectCards_Script>().CardIndex = indexSelected;
        specialCard.GetComponent<ConnectCards_Script>().CardGroupIndex = indexGoupSelected;
        specialCard.GetComponent<ConnectCards_Script>().FlipCard();

        selectedCharacterIndexes[numberOfMoves] = indexSelected;
    }


    // ******************************************************

    // MEMORY GAME
    private void MemoryStart()
    {
        var cardsFace = new List<int>(numberOfCardsFaces);
        for (int i = 0; i < numberOfCardsFaces; i++)
        {
            cardsFace.Add(i);
        }


        cardIndexes = new List<int>(numberOfCards);
        for (int i = 0; i < numberOfCards / 2; i++)
        {
            int x = cardsFace[Random.Range(0, cardsFace.Count)];
            for (int j = 0; j < 2; j++)
            {
                cardIndexes.Add(x);
            }
            cardsFace.Remove(x);
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
                Vector3 position = new Vector3((cols / 2 - x - correctX) * multiplierXYForMemoryGame, (rows / 2 - y + increaseY) * multiplierXYForMemoryGame, 0);
                var temp = Instantiate(card, position, Quaternion.Euler(-5, 0, 0), cardPool.transform); // FIX ROTATION
                numbersOfCardsCreated++;
                temp.GetComponent<Cards_Script>().CardIndex = cardIndexes[randomIndex];
                temp.GetComponent<Cards_Script>().SetGameplayManager(this);
                temp.GetComponent<Cards_Script>().FlipCards(false);
                cardIndexes.Remove(cardIndexes[randomIndex]);
            }
        }
    }


    private IEnumerator WaitToFlipCards(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var cards = GameObject.FindGameObjectsWithTag("Cards");

        foreach (GameObject card in cards)
        {
            card.GetComponent<Cards_Script>().FlipCards(true);
        }

        playerScript.GameStarted(true);
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

    private bool CheckMatch()
    {
        bool success = false;
        if (cardsRevealed[0].CardIndex == cardsRevealed[1].CardIndex)
        {
            //Check if the indexes are the same
            success = true;
            numberOfRightPairs++;
            if (numberOfCards == numberOfRightPairs * 2)
            {
                uiManager_GM.SetGameEndedPanel(true);
                playerScript.GameStarted(false);
            }
        }
        else
        {
            numberOfAttempts--;
            uiManager_GM.UpdateAttempts(numberOfAttempts);
            if (numberOfAttempts <= 0)
            {
                uiManager_GM.UpdateAttempts(0);
                uiManager_GM.SetGameEndedPanel(true);
                Debug.Log("perdeu");
            }
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
