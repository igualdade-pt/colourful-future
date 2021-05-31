using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_SM : MonoBehaviour
{

    private StartMenuManager startMenuManager;

    private AudioManager audioManager;

    [Header("Buttons")]
    [Space]
    [SerializeField]
    private Image soundImage;

    [SerializeField]
    private Sprite[] spritesOnOffSound;

    [SerializeField]
    private GameObject buttonCloseBooksPanel;

    [SerializeField]
    private GameObject[] buttonBookSelectedPanel;


    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject informationPanel;

    [SerializeField]
    private GameObject booksPanel;

    [SerializeField]
    private GameObject allBooksPanel;

    [SerializeField]
    private GameObject buttonsBooksPanel;

    [SerializeField]
    private GameObject gamePanel;


    private int indexBookSelected;

    private bool isSoundActive = true;


    [Header("Book")]
    [Space]
    [SerializeField]
    private RectTransform[] tPagesBookTV;

    [SerializeField]
    private RectTransform[] tPaintsExpert;

    private RectTransform[][] tPages = new RectTransform[4][];

    [SerializeField]
    private float[] xPages;

    [SerializeField]
    private LeanTweenType easeType;

    [SerializeField]
    private AnimationCurve curve;

    private float previousTime = 0;

    private int currentIndexPage;
    private bool canChange;
    private Vector2 lastDragPosition;
    private bool positiveDrag;
    private bool canDrag;

    private int bookSelected;

    private void Awake()
    {
        informationPanel.SetActive(false);
        booksPanel.SetActive(false);
        gamePanel.SetActive(false);
        soundImage.sprite = spritesOnOffSound[0];

        for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
        {
            buttonBookSelectedPanel[i].SetActive(false);
        }
    }

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartMenuManager>().GetComponent<StartMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        isSoundActive = true;
        canChange = true;
        tPages[0] = tPagesBookTV;
        tPages[1] = tPaintsExpert;
    }

    public void _StartButtonClicked(int indexScene)
    {
        Debug.Log("Start Clicked, Index Scene: " + indexScene);
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        startMenuManager.LoadAsyncScene(indexScene);
    }

    public void _InformationButtonClicked()
    {
        if (!informationPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            informationPanel.SetActive(true);
        }
    }

    public void _CloseInformationButtonClicked()
    {
        if (informationPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            informationPanel.SetActive(false);
        }
    }

    public void _LanguageButtonClicked(int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        startMenuManager.LoadScene(indexScene);
    }

    public void _AgeButtonClicked(int indexScene)
    {
        Debug.Log("Age Clicked, Index Scene: " + indexScene);
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        startMenuManager.LoadScene(indexScene);
    }

    public void _BooksButtonClicked()
    {
        if (!booksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            allBooksPanel.SetActive(false);
            booksPanel.SetActive(true);
            buttonsBooksPanel.SetActive(true);
        }
    }

    public void _CloseBooksButtonClicked()
    {
        if (booksPanel.activeSelf && !allBooksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            booksPanel.SetActive(false);
        }
        else if (allBooksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            allBooksPanel.SetActive(false);
            buttonBookSelectedPanel[indexBookSelected].SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            //buttonCloseBooksPanel.SetActive(true);
        }

    }

    public void _BookButtonSelectedClicked(int indexBook)
    {
        if (booksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            bookSelected = indexBook;
            InitUpdatePaint();
            buttonsBooksPanel.SetActive(false);
            //buttonCloseBooksPanel.SetActive(false);

            for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
            {
                if (i == indexBook)
                {
                    buttonBookSelectedPanel[i].SetActive(true);
                    allBooksPanel.SetActive(true);
                    indexBookSelected = indexBook;
                }
            }
        }
    }

    public void _CloseBookButtonSelectedClicked()
    {
        if (booksPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            allBooksPanel.SetActive(false);
            buttonBookSelectedPanel[indexBookSelected].SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            buttonCloseBooksPanel.SetActive(true);
        }
    }


    public void _GamesButtonClicked()
    {
        if (!gamePanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            gamePanel.SetActive(true);
        }
    }

    public void _CloseGamesButtonClicked()
    {
        if (gamePanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            gamePanel.SetActive(false);
        }
    }

    public void _SoundButtonClicked()
    {
        if (isSoundActive)
        {
            //soundButton.image.sprite = spriteOffOnSound[0];
            Debug.Log("sound is OFF, value:" + isSoundActive);
            audioManager.SetMasterVolume(isSoundActive);
            isSoundActive = false;
            soundImage.sprite = spritesOnOffSound[1];
        }
        else
        {
            //soundButton.image.sprite = spriteOffOnSound[1];
            Debug.Log("sound is ON, value:" + isSoundActive);
            audioManager.SetMasterVolume(isSoundActive);
            isSoundActive = true;
            soundImage.sprite = spritesOnOffSound[0];
        }
    }

    public void UpdateLanguage(int indexLanguage)
    {

    }

    private void InitUpdatePaint()
    {
        currentIndexPage = 0;
        // Change Paint
        int t = 0;

        for (int i = 0; i < tPages[bookSelected].Length; i++)
        {
            if (0 == i)
            {
                tPages[bookSelected][i].anchoredPosition = new Vector2(xPages[2], tPages[bookSelected][i].anchoredPosition.y);
            }
            else if (1 == i)
            {
                tPages[bookSelected][i].anchoredPosition = new Vector2(xPages[3], tPages[bookSelected][i].anchoredPosition.y);
            }
            else
            {
                tPages[bookSelected][i].anchoredPosition = new Vector2(xPages[4], tPages[bookSelected][i].anchoredPosition.y);
            }

        }

        previousTime = Time.time;
    }


    public void UpdatePage(int indexPage)
    {
        int t = indexPage;

        int first = t + 1;
        int second = t + 2;
        int third = t - 1;
        int fourth = t - 2;

        for (int i = 0; i < tPages[bookSelected].Length; i++)
        {
            if (t == i)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[2], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[2], time).setEase(easeType).setOnComplete(CanChangePage);
                }
            }
            else if (first == i)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[3], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[3], time).setEase(easeType).setOnComplete(CanChangePage);
                }
            }
            else if (second == i)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[4], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[4], time).setEase(easeType).setOnComplete(CanChangePage);
                }
            }
            else if (third == i)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[1], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[1], time).setEase(easeType).setOnComplete(CanChangePage);
                }
            }
            else if (fourth == i)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[0], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][i], xPages[0], time).setEase(easeType).setOnComplete(CanChangePage);
                }
            }
            else
            {
                LeanTween.cancel(tPages[bookSelected][i]);
                tPages[bookSelected][i].anchoredPosition = new Vector2(xPages[4], tPages[bookSelected][i].anchoredPosition.y);
            }
        }



    }

    private void CanChangePage()
    {
        canChange = true;
    }

    public void _RightButtonClick()
    {
        if (canChange)
        {
            canChange = false;
            if (currentIndexPage < tPages[bookSelected].Length - 1)
            {
                currentIndexPage++;
                UpdatePage(currentIndexPage);
                // Play Sound
                audioManager.PlayClip(0, 0.6f);
                // ****
            }
            else
            {
                currentIndexPage = tPages[bookSelected].Length - 1;
                canChange = true;
            }

        }

    }

    public void _LeftButtonClick()
    {
        if (canChange)
        {
            canChange = false;
            if (currentIndexPage > 0)
            {
                currentIndexPage--;
                UpdatePage(currentIndexPage);
                // Play Sound
                audioManager.PlayClip(0, 0.6f);
                // ****
            }
            else
            {
                currentIndexPage = 0;
                canChange = true;
            }

        }
    }


    public void _BeginDrag()
    {
        lastDragPosition = Input.mousePosition;
        //lastDragPosition = Input.GetTouch(0).position;
    }

    public void _Drag()
    {
        canDrag = false;

        if (Input.mousePosition.x != lastDragPosition.x)
        {
            canDrag = true;
            positiveDrag = Input.mousePosition.x > lastDragPosition.x;
        }


        if (canDrag)
        {
            if (!positiveDrag)
            {
                if (canChange)
                {
                    canChange = false;
                    if (currentIndexPage < tPages[bookSelected].Length - 1)
                    {
                        currentIndexPage++;
                        UpdatePage(currentIndexPage);
                        // Play Sound
                        audioManager.PlayClip(1, 0.6f);
                        // ****
                    }
                    else
                    {
                        currentIndexPage = tPages[bookSelected].Length - 1;
                        canChange = true;
                    }
                }
            }
            else
            {
                if (canChange)
                {
                    canChange = false;
                    if (currentIndexPage > 0)
                    {
                        currentIndexPage--;
                        UpdatePage(currentIndexPage);
                        // Play Sound
                        audioManager.PlayClip(1, 0.6f);
                        // ****
                    }
                    else
                    {
                        currentIndexPage = 0;
                        canChange = true;
                    }

                }
            }
        }


        lastDragPosition = Input.mousePosition;
        //lastDragPosition = Input.GetTouch(0).position;
    }

    public void _EndDrag()
    {
        canDrag = true;
    }

}
