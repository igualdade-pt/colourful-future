﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    private AudioManager audioManager;

    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject colourPanel;

    [SerializeField]
    private GameObject loadingPanel;

    [Header("Colour Menu")]
    [Space]
    [SerializeField]
    private GameObject handGuide;

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
        colourPanel.SetActive(false);
        mainPanel.SetActive(true);
        loadingPanel.SetActive(false);
        handGuide.SetActive(true);
    }

    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        canChange = true;

        tPaints[0] = tPaintsBasic;
        tPaints[1] = tPaintsExpert;
    }


    public void _ColourButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        mainPanel.SetActive(false);
        colourPanel.SetActive(true);
        InitUpdatePaint();
    }

    public void OpenMenuPaints()
    {
        mainPanel.SetActive(false);
        colourPanel.SetActive(true);
        InitUpdatePaint();
    }

    public void _ReturnColourButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        colourPanel.SetActive(false);
        mainPanel.SetActive(true);
        handGuide.SetActive(true);
    }

    public void _GameButtonClicked(int indexGame)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        mainMenuManager.LoadAsyncGamePlay(indexGame);
    }

    public void _SettingsButtonClicked(int indexScene)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        mainMenuManager.LoadScene(indexScene);
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
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
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
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
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
            handGuide.SetActive(false);
        }
        

        if (canDrag)
        {
            if (positiveDrag)
            {
                if (canChange)
                {
                    // Play Sound
                    audioManager.PlayClip(1, 0.6f);
                    // ****
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
                    // Play Sound
                    audioManager.PlayClip(1, 0.6f);
                    // ****
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
