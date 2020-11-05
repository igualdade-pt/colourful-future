using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Paint_Script : MonoBehaviour
{

    private Color colorSelected;

    private bool doOnce = true;

    private void Start()
    {
        colorSelected = new Color(1, 1, 1, 1);
    }



    private void Update()
    {
        // MOBILE
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                Debug.Log(hit.collider);
            }
        }*/

        // PC
        if (Input.GetButton("Fire1"))
        {
            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray, ray);
            Debug.DrawLine(ray, ray, Color.red);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                Debug.Log(hit.collider);
            }
        }

        if (Input.GetButton("Fire2") && doOnce)
        {
            doOnce = false;
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/A" + ".png", 2);

            Invoke("SaveAt", 3.0f);
        }
    }

    public void _ColorClicked(Button button)
    {
        colorSelected = button.image.color;
        Debug.Log(colorSelected);
    }

    private void SaveAt()
    {
        FileUtil.MoveFileOrDirectory(Application.dataPath + "/A" + ".png", Application.dataPath + "/ScreenShots/A" + ".png");
    }
}
