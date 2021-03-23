using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_GM : MonoBehaviour
{
    private GameplayManager gameplayManager;

    [SerializeField]
    private GameObject gameEndedPanel;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        gameEndedPanel.SetActive(false);
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
}
