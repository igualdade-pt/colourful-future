using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_GM : MonoBehaviour
{
    private GameplayManager gameplayManager;

    [SerializeField]
    private GameObject gameEndedPanel;

    [SerializeField]
    private Text attemptsRemainText;

    [SerializeField]
    private Text correctRemainText;

    [SerializeField]
    private Text timerRemainText;

    private int totalCorrectMoves;
    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        gameEndedPanel.SetActive(false);
        timerRemainText.gameObject.SetActive(false);
        correctRemainText.gameObject.SetActive(false);
    }

    public void UpdateLanguage(int indexLanguage)
    {
        switch (indexLanguage)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:
                break;

            default:
                Debug.Log("UiManager_GM Menu, Unavailable language, English Selected: " + indexLanguage);

                break;
        }
    }

    public void _RestartButtonClicked(int indexLevel)
    {
        gameplayManager.LoadSelectedScene(indexLevel);
        SetGameEndedPanel(false);
    }

    public void SetGameEndedPanel(bool value)
    {
        gameEndedPanel.SetActive(value);
    }

    public void _Return()
    {
        SceneManager.LoadScene(3);
    }

    public void UpdateAttempts(int value)
    {
        attemptsRemainText.text = value.ToString();
    }

    public void UpdateCorrectsMoves(int value)
    {
        correctRemainText.text = value.ToString() + "/" + totalCorrectMoves.ToString();

    }

    public void UpdateTimer(int min, int sec)
    {        
        if (sec < 10)
        {
            timerRemainText.text = min.ToString() + ":0" + sec.ToString();
        }
        else
        {
            timerRemainText.text = min.ToString() + ":" + sec.ToString();
        }        
    }

    public void SetTimerActive(bool value)
    {
        timerRemainText.gameObject.SetActive(value);
    }

    public void SetCorrectMovesTextActive(bool value)
    {
        correctRemainText.gameObject.SetActive(value);
    }

    public void SetTotalCorrectMovesTextActive(int value)
    {
        totalCorrectMoves = value;
    }
}
