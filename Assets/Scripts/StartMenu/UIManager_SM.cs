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

    [SerializeField]
    private string[] partnersLinks;

    [SerializeField]
    private string[] gamesLink;


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

    [Header("Texts")]
    [Space]
    [SerializeField]
    private GameObject[] textsInfo;

    [Header("Book")]
    [Space]
    [SerializeField]
    private GameObject page;

    [SerializeField]
    private GameObject handGuide;


    [SerializeField]
    [Header("IT")]
    private GameObject bookIT_EN;

    [SerializeField]
    private Sprite[] pagesBookIT_EN;

    private RectTransform[] tPagesBookIT_EN;


    [SerializeField]
    private GameObject bookIT_IT;

    [SerializeField]
    private Sprite[] pagesBookIT_IT;

    private RectTransform[] tPagesBookIT_IT;


    [SerializeField]
    private GameObject bookIT_PT;

    [SerializeField]
    private Sprite[] pagesBookIT_PT;

    private RectTransform[] tPagesBookIT_PT;


    [SerializeField]
    private GameObject bookIT_ES;

    [SerializeField]
    private Sprite[] pagesBookIT_ES;

    private RectTransform[] tPagesBookIT_ES;


    [SerializeField]
    private GameObject bookIT_SE;

    [SerializeField]
    private Sprite[] pagesBookIT_SE;

    private RectTransform[] tPagesBookIT_SE;


    // **************************************************
    [SerializeField]
    [Header("PT")]
    private GameObject bookPT_EN;

    [SerializeField]
    private Sprite[] pagesBookPT_EN;

    private RectTransform[] tPagesBookPT_EN;


    [SerializeField]
    private GameObject bookPT_IT;

    [SerializeField]
    private Sprite[] pagesBookPT_IT;

    private RectTransform[] tPagesBookPT_IT;


    [SerializeField]
    private GameObject bookPT_PT;

    [SerializeField]
    private Sprite[] pagesBookPT_PT;

    private RectTransform[] tPagesBookPT_PT;


    [SerializeField]
    private GameObject bookPT_ES;

    [SerializeField]
    private Sprite[] pagesBookPT_ES;

    private RectTransform[] tPagesBookPT_ES;


    [SerializeField]
    private GameObject bookPT_SE;

    [SerializeField]
    private Sprite[] pagesBookPT_SE;

    private RectTransform[] tPagesBookPT_SE;

    // **************************************************
    [SerializeField]
    [Header("ES")]
    private GameObject bookES_EN;

    [SerializeField]
    private Sprite[] pagesBookES_EN;

    private RectTransform[] tPagesBookES_EN;


    [SerializeField]
    private GameObject bookES_IT;

    [SerializeField]
    private Sprite[] pagesBookES_IT;

    private RectTransform[] tPagesBookES_IT;


    [SerializeField]
    private GameObject bookES_PT;

    [SerializeField]
    private Sprite[] pagesBookES_PT;

    private RectTransform[] tPagesBookES_PT;


    [SerializeField]
    private GameObject bookES_ES;

    [SerializeField]
    private Sprite[] pagesBookES_ES;

    private RectTransform[] tPagesBookES_ES;


    [SerializeField]
    private GameObject bookES_SE;

    [SerializeField]
    private Sprite[] pagesBookES_SE;

    private RectTransform[] tPagesBookES_SE;

    // **************************************************

    [SerializeField]
    [Header("SE")]
    private GameObject bookSE_EN;

    [SerializeField]
    private Sprite[] pagesBookSE_EN;

    private RectTransform[] tPagesBookSE_EN;


    [SerializeField]
    private GameObject bookSE_IT;

    [SerializeField]
    private Sprite[] pagesBookSE_IT;

    private RectTransform[] tPagesBookSE_IT;


    [SerializeField]
    private GameObject bookSE_PT;

    [SerializeField]
    private Sprite[] pagesBookSE_PT;

    private RectTransform[] tPagesBookSE_PT;


    [SerializeField]
    private GameObject bookSE_ES;

    [SerializeField]
    private Sprite[] pagesBookSE_ES;

    private RectTransform[] tPagesBookSE_ES;


    [SerializeField]
    private GameObject bookSE_SE;

    [SerializeField]
    private Sprite[] pagesBookSE_SE;

    private RectTransform[] tPagesBookSE_SE;

    // **************************************************


    private RectTransform[][][] tPages = new RectTransform[4][][] { new RectTransform[5][], new RectTransform[5][] , new RectTransform[5][] , new RectTransform[5][] };

    [SerializeField]
    [Space]
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
    private int indexLanguage;

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

        handGuide.SetActive(true);
    }

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartMenuManager>().GetComponent<StartMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        isSoundActive = true;
        canChange = true;

        // Info
        for (int i = 0; i < textsInfo.Length; i++)
        {
            textsInfo[i].SetActive(false);
        }

        //Book IT
        tPagesBookIT_EN = new RectTransform[pagesBookIT_EN.Length];
        tPagesBookIT_IT = new RectTransform[pagesBookIT_IT.Length];
        tPagesBookIT_PT = new RectTransform[pagesBookIT_PT.Length];
        tPagesBookIT_ES = new RectTransform[pagesBookIT_ES.Length];
        tPagesBookIT_SE = new RectTransform[pagesBookIT_SE.Length];

        for (int i = 0; i < pagesBookIT_EN.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookIT_EN.transform);
            tPagesBookIT_EN[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookIT_EN[i];
        }

        for (int i = 0; i < pagesBookIT_IT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookIT_IT.transform);
            tPagesBookIT_IT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookIT_IT[i];
        }

        for (int i = 0; i < pagesBookIT_PT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookIT_PT.transform);
            tPagesBookIT_PT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookIT_PT[i];
        }

        for (int i = 0; i < pagesBookIT_ES.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookIT_ES.transform);
            tPagesBookIT_ES[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookIT_ES[i];
        }

        for (int i = 0; i < pagesBookIT_SE.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookIT_SE.transform);
            tPagesBookIT_SE[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookIT_SE[i];
        }


        //Book PT
        tPagesBookPT_EN = new RectTransform[pagesBookPT_EN.Length];
        tPagesBookPT_IT = new RectTransform[pagesBookPT_IT.Length];
        tPagesBookPT_PT = new RectTransform[pagesBookPT_PT.Length];
        tPagesBookPT_ES = new RectTransform[pagesBookPT_ES.Length];
        tPagesBookPT_SE = new RectTransform[pagesBookPT_SE.Length];

        for (int i = 0; i < pagesBookPT_EN.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookPT_EN.transform);
            tPagesBookPT_EN[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookPT_EN[i];
        }

        for (int i = 0; i < pagesBookPT_IT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookPT_IT.transform);
            tPagesBookPT_IT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookPT_IT[i];
        }

        for (int i = 0; i < pagesBookPT_PT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookPT_PT.transform);
            tPagesBookPT_PT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookPT_PT[i];
        }

        for (int i = 0; i < pagesBookPT_ES.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookPT_ES.transform);
            tPagesBookPT_ES[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookPT_ES[i];
        }

        for (int i = 0; i < pagesBookPT_SE.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookPT_SE.transform);
            tPagesBookPT_SE[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookPT_SE[i];
        }


        //Book ES
        tPagesBookES_EN = new RectTransform[pagesBookES_EN.Length];
        tPagesBookES_IT = new RectTransform[pagesBookES_IT.Length];
        tPagesBookES_PT = new RectTransform[pagesBookES_PT.Length];
        tPagesBookES_ES = new RectTransform[pagesBookES_ES.Length];
        tPagesBookES_SE = new RectTransform[pagesBookES_SE.Length];

        for (int i = 0; i < pagesBookES_EN.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookES_EN.transform);
            tPagesBookES_EN[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookES_EN[i];
        }

        for (int i = 0; i < pagesBookES_IT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookES_IT.transform);
            tPagesBookES_IT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookES_IT[i];
        }

        for (int i = 0; i < pagesBookES_PT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookES_PT.transform);
            tPagesBookES_PT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookES_PT[i];
        }

        for (int i = 0; i < pagesBookES_ES.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookES_ES.transform);
            tPagesBookES_ES[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookES_ES[i];
        }

        for (int i = 0; i < pagesBookES_SE.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookES_SE.transform);
            tPagesBookES_SE[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookES_SE[i];
        }


        //Book SE
        tPagesBookSE_EN = new RectTransform[pagesBookSE_EN.Length];
        tPagesBookSE_IT = new RectTransform[pagesBookSE_IT.Length];
        tPagesBookSE_PT = new RectTransform[pagesBookSE_PT.Length];
        tPagesBookSE_ES = new RectTransform[pagesBookSE_ES.Length];
        tPagesBookSE_SE = new RectTransform[pagesBookSE_SE.Length];

        for (int i = 0; i < pagesBookSE_EN.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookSE_EN.transform);
            tPagesBookSE_EN[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookSE_EN[i];
        }

        for (int i = 0; i < pagesBookSE_IT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookSE_IT.transform);
            tPagesBookSE_IT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookSE_IT[i];
        }

        for (int i = 0; i < pagesBookSE_PT.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookSE_PT.transform);
            tPagesBookSE_PT[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookSE_PT[i];
        }

        for (int i = 0; i < pagesBookSE_ES.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookSE_ES.transform);
            tPagesBookSE_ES[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookSE_ES[i];
        }

        for (int i = 0; i < pagesBookSE_SE.Length; i++)
        {
            GameObject tempPage = Instantiate(page, bookSE_SE.transform);
            tPagesBookSE_SE[i] = tempPage.GetComponent<RectTransform>();
            tempPage.GetComponent<Image>().sprite = pagesBookSE_SE[i];
        }


        // Books 
        tPages[0][0] = tPagesBookIT_EN;
        tPages[0][1] = tPagesBookIT_IT;
        tPages[0][2] = tPagesBookIT_PT;
        tPages[0][3] = tPagesBookIT_ES;
        tPages[0][4] = tPagesBookIT_SE;

        tPages[0][0][0].parent.gameObject.SetActive(false);
        tPages[0][1][0].parent.gameObject.SetActive(false);
        tPages[0][2][0].parent.gameObject.SetActive(false);
        tPages[0][3][0].parent.gameObject.SetActive(false);
        tPages[0][4][0].parent.gameObject.SetActive(false);

        tPages[1][0] = tPagesBookPT_EN;
        tPages[1][1] = tPagesBookPT_IT;
        tPages[1][2] = tPagesBookPT_PT;
        tPages[1][3] = tPagesBookPT_ES;
        tPages[1][4] = tPagesBookPT_SE;

        tPages[1][0][0].parent.gameObject.SetActive(false);
        tPages[1][1][0].parent.gameObject.SetActive(false);
        tPages[1][2][0].parent.gameObject.SetActive(false);
        tPages[1][3][0].parent.gameObject.SetActive(false);
        tPages[1][4][0].parent.gameObject.SetActive(false);

        tPages[2][0] = tPagesBookES_EN;
        tPages[2][1] = tPagesBookES_IT;
        tPages[2][2] = tPagesBookES_PT;
        tPages[2][3] = tPagesBookES_ES;
        tPages[2][4] = tPagesBookES_SE;

        tPages[2][0][0].parent.gameObject.SetActive(false);
        tPages[2][1][0].parent.gameObject.SetActive(false);
        tPages[2][2][0].parent.gameObject.SetActive(false);
        tPages[2][3][0].parent.gameObject.SetActive(false);
        tPages[2][4][0].parent.gameObject.SetActive(false);

        tPages[3][0] = tPagesBookSE_EN;
        tPages[3][1] = tPagesBookSE_IT;
        tPages[3][2] = tPagesBookSE_PT;
        tPages[3][3] = tPagesBookSE_ES;
        tPages[3][4] = tPagesBookSE_SE;

        tPages[3][0][0].parent.gameObject.SetActive(false);
        tPages[3][1][0].parent.gameObject.SetActive(false);
        tPages[3][2][0].parent.gameObject.SetActive(false);
        tPages[3][3][0].parent.gameObject.SetActive(false);
        tPages[3][4][0].parent.gameObject.SetActive(false);
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

    /*
        Colourful Children - https://colourfulchildren.eu/

        Erasmus+ - https://ec.europa.eu/programmes/erasmus-plus/about_en 

        Associação igualdade.pt - https://igualdade.pt

        CIEG-ISCSP-ULisboa - http://cieg.iscsp.ulisboa.pt/ 

         Härryda - https://www.harryda.se/

        Murcia - https://www.murcia.es         

        Ravenna - https://www.comune.ra.it/ 

        Torres Vedras - http://cm-tvedras.pt/ 
     */

    public void _PartnersButtonClicked(int index)
    {
        if (informationPanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            Application.OpenURL(partnersLinks[index]);
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
            tPages[indexBookSelected][indexLanguage][0].parent.gameObject.SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            handGuide.SetActive(true);
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
            InitUpdatePages();
            buttonsBooksPanel.SetActive(false);
            //buttonCloseBooksPanel.SetActive(false);

            /*            for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
                        {
                            if (i == indexBook)
                            {*/
            buttonBookSelectedPanel[indexBook].SetActive(true);
            tPages[indexBook][indexLanguage][0].parent.gameObject.SetActive(true);
            allBooksPanel.SetActive(true);
            indexBookSelected = indexBook;
            /* }
         }*/
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
    public void _GameButtonClicked(int index)
    {
        if (gamePanel.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            //Application.OpenURL("market://details?id=" + gamesLink[index]);
            Application.OpenURL("https://colourfulchildren.eu/");
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

    public void UpdateLanguage(int index)
    {
        // Change Info Text
        textsInfo[index].SetActive(true);

        indexLanguage = index;
    }

    private void InitUpdatePages()
    {
        currentIndexPage = 0;
        // Change Paint
        int t = 0;

        for (int i = 0; i < tPages[bookSelected][indexLanguage].Length; i++)
        {
            if (0 == i)
            {
                tPages[bookSelected][indexLanguage][i].anchoredPosition = new Vector2(xPages[2], tPages[bookSelected][indexLanguage][i].anchoredPosition.y);
            }
            else if (1 == i)
            {
                tPages[bookSelected][indexLanguage][i].anchoredPosition = new Vector2(xPages[3], tPages[bookSelected][indexLanguage][i].anchoredPosition.y);
            }
            else
            {
                tPages[bookSelected][indexLanguage][i].anchoredPosition = new Vector2(xPages[4], tPages[bookSelected][indexLanguage][i].anchoredPosition.y);
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

        for (int i = 0; i < tPages[bookSelected][indexLanguage].Length; i++)
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
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[2], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[2], time).setEase(easeType).setOnComplete(CanChangePage);
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
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[3], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[3], time).setEase(easeType).setOnComplete(CanChangePage);
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
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[4], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[4], time).setEase(easeType).setOnComplete(CanChangePage);
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
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[1], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[1], time).setEase(easeType).setOnComplete(CanChangePage);
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
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[0], time).setEase(curve).setOnComplete(CanChangePage);
                }
                else
                {
                    LeanTween.moveX(tPages[bookSelected][indexLanguage][i], xPages[0], time).setEase(easeType).setOnComplete(CanChangePage);
                }
            }
            else
            {
                LeanTween.cancel(tPages[bookSelected][indexLanguage][i]);
                tPages[bookSelected][indexLanguage][i].anchoredPosition = new Vector2(xPages[4], tPages[bookSelected][indexLanguage][i].anchoredPosition.y);
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
            if (currentIndexPage < tPages[bookSelected][indexLanguage].Length - 1)
            {
                currentIndexPage++;
                UpdatePage(currentIndexPage);
                // Play Sound
                audioManager.PlayClip(0, 0.4f);
                // ****
                audioManager.PlayClip(10, 0.8f);
                // ****
            }
            else
            {
                currentIndexPage = tPages[bookSelected][indexLanguage].Length - 1;
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
                audioManager.PlayClip(0, 0.4f);
                // ****
                audioManager.PlayClip(10, 0.8f);
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
            handGuide.SetActive(false);
        }


        if (canDrag)
        {
            if (!positiveDrag)
            {
                if (canChange)
                {
                    canChange = false;
                    if (currentIndexPage < tPages[bookSelected][indexLanguage].Length - 1)
                    {
                        currentIndexPage++;
                        UpdatePage(currentIndexPage);
                        // Play Sound
                        audioManager.PlayClip(1, 0.4f);
                        // ****
                        audioManager.PlayClip(10, 0.8f);
                        // ****
                    }
                    else
                    {
                        currentIndexPage = tPages[bookSelected][indexLanguage].Length - 1;
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
                        audioManager.PlayClip(1, 0.4f);
                        // ****
                        audioManager.PlayClip(10, 0.8f);
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
