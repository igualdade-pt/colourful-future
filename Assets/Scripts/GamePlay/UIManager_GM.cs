using PaintCraft.Tools;
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
    private GameObject colourPanel;

    [SerializeField]
    private GameObject screenShotPanel;

    [SerializeField]
    private Text attemptsRemainText;

    [SerializeField]
    private Text correctRemainText;

    [SerializeField]
    private Text timerRemainText;

    [SerializeField]
    private RectTransform rectPaint;

    [SerializeField]
    private LineConfig lineConfig;

    private int totalCorrectMoves;
    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        gameEndedPanel.SetActive(false);
        colourPanel.SetActive(false);
        screenShotPanel.SetActive(false);
        timerRemainText.gameObject.SetActive(false);
        correctRemainText.gameObject.SetActive(false);
        attemptsRemainText.gameObject.SetActive(false);
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
        gameplayManager.LoadSelectedScene(3);
    }

    public void UpdateAttempts(int value)
    {
        attemptsRemainText.gameObject.SetActive(true);
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
}
