using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_SM : MonoBehaviour
{

    private StartMenuManager startMenuManager;

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartMenuManager>().GetComponent<StartMenuManager>();
    }

    public void _StartButtonClicked (int indexScene)
    {
        Debug.Log("Start Clicked, Index Scene: " + indexScene);

        startMenuManager.LoadAsyncScene(indexScene);
    }

    public void _InformationButtonClicked ()
    {

    }

    public void _LanguageButtonClicked (int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);

        startMenuManager.LoadScene(indexScene);
    }

    public void _AgeButtonClicked(int indexScene)
    {
        Debug.Log("Age Clicked, Index Scene: " + indexScene);

        startMenuManager.LoadScene(indexScene);
    }

    public void _BooksButtonClicked ()
    {

    }

    public void _SoundButtonClicked()
    {

    }

    public void UpdateLanguage(int indexLanguage)
    {
        
    }

}
