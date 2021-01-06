using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    public GameObject DownArrow;
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject CircleDown;
    public GameObject CircleLeft;
    public GameObject CircleRight;
    public float fillAmount = 0.05f;
    private float a;

    // Start is called before the first frame update
    void Start()
    {
        DownArrow.SetActive(false);
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
        CircleDown.SetActive(false);
        CircleLeft.SetActive(false);
        CircleRight.SetActive(false);
        a = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {

        switch (a)
        {
            case 0:
                DownArrow.SetActive(true);
                CircleDown.SetActive(true);
                CircleDown.GetComponent<Image>().fillAmount = fillAmount;
                if (Input.GetKeyDown(KeyCode.B))
                {
                    fillAmount += .05f;
                    if (fillAmount > 1.1)
                    {
                        DownArrow.SetActive(false);
                        CircleDown.SetActive(false);
                        fillAmount = 0.05f;
                    }
                }
                break;

            case 1:
                LeftArrow.SetActive(true);
                CircleLeft.SetActive(true);
                CircleLeft.GetComponent<Image>().fillAmount = fillAmount;
                if (Input.GetKeyDown(KeyCode.PageUp))
                {
                    fillAmount += .05f;
                    if (fillAmount > 1.1)
                    {
                        LeftArrow.SetActive(false);
                        CircleLeft.SetActive(false);
                        fillAmount = 0.05f;
                    }
                }
                break;

            case 2:
                RightArrow.SetActive(true);
                CircleRight.SetActive(true);
                CircleRight.GetComponent<Image>().fillAmount = fillAmount;
                if (Input.GetKeyDown(KeyCode.PageDown))
                {
                    fillAmount += .05f;
                    if (fillAmount > 1.1)
                    {
                        RightArrow.SetActive(false);
                        CircleRight.SetActive(false);
                        fillAmount = 0.05f;
                    }
                }
                break;
        }
                /*if (Input.GetKeyDown(KeyCode.B))
                {
                    CircleDown.GetComponent<Image>().fillAmount = fillAmount;
                    fillAmount += .05f;
                    Debug.Log("Down");
                    if (fillAmount > 1.1)
                    {
                        qte1 = true;
                        DownArrow.SetActive(false);
                        CircleDown.SetActive(false);
                        fillAmount = 0.05f;
                    }
                }
                if (qte1 == true)
                {
                    RightArrow.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.PageDown))
                    {
                        CircleRight.GetComponent<Image>().fillAmount = fillAmount;
                        fillAmount += .05f;
                        Debug.Log("Right");
                    }
                    if (fillAmount > 1.1)
                    {
                        qte2 = true;
                        qte1 = false;
                        RightArrow.SetActive(false);
                        CircleRight.SetActive(false);
                        fillAmount = 0.05f;
                    }
                }
                if (qte2 == true)
                {
                    LeftArrow.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.PageUp))
                    {
                        CircleLeft.GetComponent<Image>().fillAmount = fillAmount;
                        fillAmount += .05f;
                        Debug.Log("Left");
                        if (fillAmount > 1.1)
                        {
                            qte2 = false;
                            LeftArrow.SetActive(false);
                            CircleLeft.SetActive(false);
                        }
                    }
                }*/
    }
}

