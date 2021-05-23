using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    private AudioManager audioManager;

    [Header("Buttons")]
    [Space]
    [SerializeField]
    private Button soundButton;

    [SerializeField]
    private Sprite[] spriteOffOnSound;

    [SerializeField]
    private GameObject buttonCloseBooksPanel;

    [SerializeField]
    private GameObject[] buttonBookSelectedPanel;

    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject colourPanel;

    [SerializeField]
    private GameObject informationPanel;

    [SerializeField]
    private GameObject booksPanel;

    [SerializeField]
    private GameObject buttonsBooksPanel;

    [SerializeField]
    private GameObject allBooksPanel;


    private int indexBookSelected;

    private bool isSoundActive = true;

    [Header("Colour Menu")]
    [Space]
    [SerializeField]
    private GameObject paintBasicPanel;

    [SerializeField]
    private GameObject paintExpertPanel;

    [SerializeField]
    private RectTransform[] tPaintsBasic;

    [SerializeField]
    private RectTransform[] tPaintsExpert;

    private RectTransform[][] tPaints = new RectTransform [2][];

    [SerializeField]
    private float[] yPaints;

    [SerializeField]
    private LeanTweenType easeType;

    [SerializeField]
    private AnimationCurve curve;

    private float previousTime = 0;

    private int currentIndexPaint;
    private bool canChange;
    private Vector2 lastDragPosition;
    private bool positiveDrag;
    private bool canDrag;
    private int indexDificulty;

    private void Awake()
    {
        informationPanel.SetActive(false);
        booksPanel.SetActive(false);
        colourPanel.SetActive(false);
        mainPanel.SetActive(true);

        for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
        {
            buttonBookSelectedPanel[i].SetActive(false);
        }

    }

    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
        //audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        isSoundActive = true;
        canChange = true;

        tPaints[0] = tPaintsBasic;
        tPaints[1] = tPaintsExpert;
    }


    public void _InformationButtonClicked()
    {
        if (!informationPanel.activeSelf)
        {
            informationPanel.SetActive(true);
        }
    }

    public void _CloseInformationButtonClicked()
    {
        if (informationPanel.activeSelf)
        {
            informationPanel.SetActive(false);
        }
    }

    public void _LanguageButtonClicked(int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);

        mainMenuManager.LoadScene(indexScene);
    }

    public void _AgeButtonClicked(int indexScene)
    {
        Debug.Log("Age Clicked, Index Scene: " + indexScene);

        mainMenuManager.LoadScene(indexScene);
    }

    public void _BooksButtonClicked()
    {
        if (!booksPanel.activeSelf)
        {
            allBooksPanel.SetActive(false);
            booksPanel.SetActive(true);
            buttonsBooksPanel.SetActive(true);
        }
    }

    public void _CloseBooksButtonClicked()
    {
        if (booksPanel.activeSelf)
        {
            booksPanel.SetActive(false);
        }
    }

    public void _BookButtonSelectedClicked(int indexBook)
    {
        if (booksPanel.activeSelf)
        {
            buttonsBooksPanel.SetActive(false);
            buttonCloseBooksPanel.SetActive(false);

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
            allBooksPanel.SetActive(false);
            buttonBookSelectedPanel[indexBookSelected].SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            buttonCloseBooksPanel.SetActive(true);
        }
    }

    public void _SoundButtonClicked()
    {
        if (isSoundActive)
        {
            //soundButton.image.sprite = spriteOffOnSound[0];
            Debug.Log("sound is OFF, value:" + isSoundActive);
            //audioManager.SetVolume(isSoundActive);
            isSoundActive = false;
        }
        else
        {
            //soundButton.image.sprite = spriteOffOnSound[1];
            Debug.Log("sound is ON, value:" + isSoundActive);
            //audioManager.SetVolume(isSoundActive);
            isSoundActive = true;
        }
    }

    public void _ColourButtonClicked()
    {
        mainPanel.SetActive(false);
        colourPanel.SetActive(true);
        InitUpdatePaint();
    }

    public void _ReturnColourButtonClicked()
    {
        colourPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void _GameButtonClicked(int indexGame)
    {
        mainMenuManager.LoadAsyncGamePlay(indexGame);
    }

    public void UpdateLanguage(int indexLanguage)
    {

    }

    public void UpdateDificulty(int value)
    {
        indexDificulty = value;

        Debug.Log(value);

        switch (value)
        {
            case 0:
                paintExpertPanel.SetActive(false);
                paintBasicPanel.SetActive(true);
                break;

            case 1:
                paintBasicPanel.SetActive(false);
                paintExpertPanel.SetActive(true);
                break;

            default:
                paintExpertPanel.SetActive(false);
                paintBasicPanel.SetActive(true);
                break;
        }
    }


    private void InitUpdatePaint()
    {
        currentIndexPaint = 0;
        // Change Paint
        int t = 0 - Mathf.FloorToInt(yPaints.Length / 2);
        if (t < 0)
        {
            t += yPaints.Length;
        }

        for (int i = 0; i < tPaints[indexDificulty].Length; i++)
        {
            if (t > 0)
            {
                t--;
            }
            else
            {
                t = yPaints.Length - 1;
            }

            tPaints[indexDificulty][i].anchoredPosition = new Vector2(tPaints[indexDificulty][i].anchoredPosition.x, yPaints[t]);

        }

        previousTime = Time.time;
    }


    public void UpdatePaint(int indexPaint)
    {
        // Change Flag
        int t = indexPaint - Mathf.FloorToInt(yPaints.Length / 2);
        if (t < 0)
        {
            t += yPaints.Length;
        }

        for (int i = 0; i < tPaints[indexDificulty].Length; i++)
        {
            if (t > 0)
            {
                t--;
            }
            else
            {
                t = yPaints.Length - 1;
            }

            if (t == 1 || t == 2 || t == 3 || t == 4 || t == 5)
            {
                float time = Time.time - previousTime - 0.1f;
                time = Mathf.Clamp(time, 0f, 0.5f);
                if (time < 0.18)
                {
                    time = 0;
                }
                if (easeType == LeanTweenType.animationCurve)
                {
                    LeanTween.moveY(tPaints[indexDificulty][i], yPaints[t], time).setEase(curve).setOnComplete(CanChangePaint);
                }
                else
                {
                    LeanTween.moveY(tPaints[indexDificulty][i], yPaints[t], time).setEase(easeType).setOnComplete(CanChangePaint);
                }
            }
            else
            {
                LeanTween.cancel(tPaints[indexDificulty][i]);
                tPaints[indexDificulty][i].anchoredPosition = new Vector2(tPaints[indexDificulty][i].anchoredPosition.x, yPaints[t]);
            }
        }



    }

    private void CanChangePaint()
    {
        canChange = true;
    }

    public void _DownButtonClick()
    {
        if (canChange)
        {
            canChange = false;
            if (currentIndexPaint < tPaints[indexDificulty].Length - 1)
            {
                currentIndexPaint++;
            }
            else
            {
                currentIndexPaint = 0;
            }

            UpdatePaint(currentIndexPaint);
        }

    }

    public void _UpButtonClick()
    {
        if (canChange)
        {
            canChange = false;
            if (currentIndexPaint > 0)
            {
                currentIndexPaint--;
            }
            else
            {
                currentIndexPaint = tPaints[indexDificulty].Length - 1;
            }

            UpdatePaint(currentIndexPaint);
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

        if (Input.mousePosition.y != lastDragPosition.y)
        {
            canDrag = true;
            positiveDrag = Input.mousePosition.y > lastDragPosition.y;
        }
        

        if (canDrag)
        {
            if (positiveDrag)
            {
                if (canChange)
                {
                    canChange = false;
                    if (currentIndexPaint > 0)
                    {
                        currentIndexPaint--;
                    }
                    else
                    {
                        currentIndexPaint = tPaints[indexDificulty].Length - 1;
                    }
                    UpdatePaint(currentIndexPaint);
                }
            }
            else
            {
                if (canChange)
                {
                    canChange = false;
                    if (currentIndexPaint < tPaints[indexDificulty].Length - 1)
                    {
                        currentIndexPaint++;
                    }
                    else
                    {
                        currentIndexPaint = 0;
                    }
                    UpdatePaint(currentIndexPaint);
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
