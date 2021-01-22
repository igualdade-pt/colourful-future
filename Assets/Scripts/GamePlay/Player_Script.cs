using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    [Header("Test Properties")]
    [Space]
    [SerializeField]
    private bool gameplayTest = false;

    [SerializeField]
    private bool mobile = false;

    [Header("Player Properties")]
    [Space]
    [SerializeField]
    private int indexGameSelected = -1;

    private GameInstanceScript gameInstance;

    private int indexLanguage = 0;

    private int indexDifficulty = 0;


    // **** Match Color ****

    private Color colorSelected;

    // **** Drag & Connect ****
    [Header("Drag & Connect  Properties, Only")]
    [Space]
    [SerializeField]
    private GameObject brush;

    [SerializeField]
    private Gradient colorOfLine;

    private LineRenderer currentLineRenderer;

    private Vector2 lastPos;



    private void Start()
    {
        if (!gameplayTest)
        {
            gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();

            // Attribute Language      
            indexLanguage = gameInstance.LanguageIndex;
            Debug.Log("Player Language Selected: " + indexLanguage);
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

                    break;
            }

            // Attribute Difficulty      
            indexDifficulty = gameInstance.DifficultyLevelIndex;
            Debug.Log("Difficulty Selected " + indexDifficulty);

            // Attribute Adventure      
            indexGameSelected = gameInstance.GameIndex;
            Debug.Log("Game Selected " + indexGameSelected);


            switch (indexGameSelected)
            {
                case 0:
                    colorSelected = new Color(1, 1, 1, 1);
                    break;

                case 1:

                    break;

                case 2:

                    break;

                default:
                    Debug.Log("Game Selected Error, index Adventure" + indexGameSelected);
                    break;
            }
        }

    }


    private void Update()
    {
        switch (indexGameSelected)
        {
            case 0:
                MatchColorGameUpdate();
                break;

            case 1:
                DragConnectUpdate();
                break;

            case 2:
                MemoryGameUpdate();
                break;

            default:
                Debug.Log("Game Selected Error, index Game" + indexGameSelected);
                break;
        }
    }


    // ************ Match Color
    private void MatchColorGameUpdate()
    {
        switch (mobile)
        {
            case true:
                // MOBILE
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(touch.position);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);

                        if (hit.collider != null)
                        {
                            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                            Debug.Log(hit.collider);
                        }
                    }
                }

                break;

            case false:
                // PC
                if (Input.GetMouseButtonDown(0))
                 {
                     var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                     RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                     Debug.DrawLine(ray, ray, Color.red);

                     if (hit.collider != null)
                     {
                         hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                         Debug.Log(hit.collider);
                     }
                 }

                break;
        }      
    }

    public void _ColorClicked(Button button)
    {
        colorSelected = button.image.color;
        Debug.Log(colorSelected);
    }


    // ************* Drag & Connect
    private void DragConnectUpdate()
    {
        switch (mobile)
        {
            case true:
                // MOBILE
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        CreateBrush();
                    }
                    else if (touch.phase == TouchPhase.Moved && currentLineRenderer != null)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        PointToMousePos();
                    }
                    else if (touch.phase == TouchPhase.Ended && currentLineRenderer != null)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        currentLineRenderer.gameObject.SetActive(false);
                        Destroy(currentLineRenderer.gameObject);

                        currentLineRenderer = null;
                    }
                }

                break;

            case false:
                // PC
                if (Input.GetMouseButtonDown(0))
                {
                    var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                    Debug.DrawLine(ray, ray, Color.red);

                    //if (hit.collider != null)
                    //{
                    //if (hit.collider.tag == "MazeBeginning")
                        //{
                        CreateBrush();
                    //}
                    //}
                }
                else if (Input.GetMouseButton(0) && currentLineRenderer != null)
                {
                    var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                    Debug.DrawLine(ray, ray, Color.red);

                    //if (hit.collider != null)
                    //{
                    //if (hit.collider.tag == "Maze" || hit.collider.tag == "MazeBeginning")
                        //{
                        PointToMousePos();
                    //}
                    //else if (hit.collider.tag == "MazeEnd")
                    //{
                    // WIN
                    //}
                    //}
                    //else
                    //{
                    //LOST
                    //currentLineRenderer.gameObject.SetActive(false);
                    //currentLineRenderer = null;
                    //}
                }
                else if (Input.GetMouseButtonUp(0) && currentLineRenderer != null)
                {
                    var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                    Debug.DrawLine(ray, ray, Color.red);

                    //if (hit.collider != null)
                    //{
                    //if (hit.collider.tag != "MazeEnd")
                        //{
                        currentLineRenderer.gameObject.SetActive(false);
                    Destroy(currentLineRenderer.gameObject);
                    //}
                    //}
                    currentLineRenderer = null;
                }

                break;
        }
    }

    void CreateBrush()
    {

        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.colorGradient = colorOfLine;

        //2 points to start a line renderer 
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        //currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }



    // ******************** Memory Game
    private void MemoryGameUpdate()
    {
        switch (mobile)
        {
            case true:
                // MOBILE
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            hit.collider.gameObject.GetComponent<Cards_Script>().CardClicked();
                        }
                    }
                }

                break;

            case false:
                // PC
                if (Input.GetMouseButtonDown(0))
                {
                    var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                    Debug.DrawLine(ray, ray, Color.red);

                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.GetComponent<Cards_Script>().CardClicked();
                    }
                }

                break;
        }
    }

}
