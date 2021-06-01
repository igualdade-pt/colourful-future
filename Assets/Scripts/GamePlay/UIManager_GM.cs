using PaintCraft.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_GM : MonoBehaviour
{
    private GameplayManager gameplayManager;
    private AudioManager audioManager;

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject gameEndedPanel;

    [SerializeField]
    private GameObject colourPanel;

    [SerializeField]
    private GameObject screenShotPanel;

    [SerializeField]
    private GameObject returnButton;

    [SerializeField]
    private Text attemptsRemainText;

    [SerializeField]
    private Text correctRemainText;

    [SerializeField]
    private Transform correctRemainPool;

    [SerializeField]
    private Image correctRemainImage;

    [SerializeField]
    private Color correctRemainColor;

    private Image[] correctRemainImages;

    [SerializeField]
    private Text timerRemainText;

    [SerializeField]
    private RectTransform rectPaint;

    [SerializeField]
    private LineConfig lineConfig;


    private int totalCorrectMoves;

    private int startGame = 0;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        loadingPanel.SetActive(false);
        gameEndedPanel.SetActive(false);
        colourPanel.SetActive(false);
        screenShotPanel.SetActive(false);
        returnButton.SetActive(false);
        timerRemainText.gameObject.SetActive(false);
        correctRemainText.gameObject.SetActive(false);
        attemptsRemainText.transform.parent.gameObject.SetActive(false);

        StartCoroutine(x());
    }

    private IEnumerator x()
    {
        yield return new WaitForEndOfFrame();

        rectPaint.offsetMin = new Vector2(0, -130);
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

    public void _RestartButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        var x = SceneManager.GetActiveScene();
        gameplayManager.LoadSelectedScene(x.buildIndex);
        //SetGameEndedPanel(false);
    }

    public void SetGameEndedPanel(bool value)
    {
        gameEndedPanel.SetActive(value);
    }

    public void _Return()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        gameplayManager.LoadSelectedScene(3);
    }

    public void UpdateAttempts(int value)
    {
        attemptsRemainText.transform.parent.gameObject.SetActive(true);
        attemptsRemainText.text = value.ToString();
    }

    public void UpdateCorrectsMoves(int value)
    {
        correctRemainText.text = value.ToString() + "/" + totalCorrectMoves.ToString();

        int lenght = correctRemainPool.childCount;

        for (int i = 0; i < lenght; i++)
        {
            if (value-1 == i)
            {
                correctRemainPool.GetChild(i).GetComponent<Image>().color = correctRemainColor;
            }
        }

    }

    public void UpdateTimer(int min, int sec)
    {
        timerRemainText.text = min.ToString() + ":" + sec.ToString("00");
    }

    public void SetTimerActive(bool value)
    {
        timerRemainText.gameObject.SetActive(value);
    }

    public void SetCorrectMovesTextActive(bool value, int numberOfRemains)
    {
        correctRemainText.gameObject.SetActive(value);

        Vector3 position = new Vector3(-45, 0, 0);

        Quaternion rotation = Quaternion.Euler(0, 0, 90);

        for (int i = 0; i < numberOfRemains; i++)
        {
            if (i == 0 || i == numberOfRemains / 2)
            {
                rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                rotation = Quaternion.Euler(0, 0, 90);
            }
    
            if (i == 1 || i == (numberOfRemains / 2) + 1)
            {
                position.y -= 65;
            }
            else if (i > 1 || i > (numberOfRemains / 2) +1)
            {
                position.y -= 95;
            } 

            if (numberOfRemains/2 == i)
            {
                position.y = 0;
                position.x *= -1;
            }

            var g = Instantiate(correctRemainImage, correctRemainPool);

            g.transform.localPosition = position;
            g.transform.localRotation = rotation;
        }
    }

    public void SetTotalCorrectMovesTextActive(int value)
    {
        totalCorrectMoves = value;
    }

    public void SetReturnButton(bool value)
    {
        returnButton.SetActive(value);
    }


    public void _BrushSizeClicked(float value)
    {
        lineConfig.scale = value;
    }

    public void SetColourPanel(bool value)
    {
        colourPanel.SetActive(value);
    }

    public void _ScreenShotButton()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        audioManager.PlayClip(2, 0.6f);
        // ****
        StartCoroutine(CaptureScreen());
    }
    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        screenShotPanel.SetActive(true);
        colourPanel.SetActive(false);

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        string day = System.DateTime.Now.ToString("dd-MM-yy");
        string hour = System.DateTime.Now.ToString("HH-mm-ss");
        ScreenCapture.CaptureScreenshot("screenshot_" + day + "_" + hour + ".png");

        // Show UI after we're done
        colourPanel.SetActive(true);
        screenShotPanel.SetActive(false);
    }


    public void _PlayButtonSound()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
    }
}
