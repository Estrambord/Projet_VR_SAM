using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject CircleLeft;
    public GameObject CircleRight;
    public float fillAmount = 0.05f;
    private float a;
    private bool didFinished = false;

    void Awake()
    {
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
        CircleLeft.SetActive(false);
        CircleRight.SetActive(false);
        a = Random.Range(0, 2);
    }

    void Update()
    {
        if (didFinished == false)
        {
            switch (a)
            {
                case 0:
                    LeftArrow.SetActive(true);
                    CircleLeft.SetActive(true);
                    CircleLeft.GetComponent<Image>().fillAmount = fillAmount;
                    if (Input.GetKeyDown(KeyCode.PageUp))
                    {
                        fillAmount += .1f;
                    }
                    break;

                case 1:
                    RightArrow.SetActive(true);
                    CircleRight.SetActive(true);
                    CircleRight.GetComponent<Image>().fillAmount = fillAmount;
                    if (Input.GetKeyDown(KeyCode.PageDown))
                    {
                        fillAmount += .1f;
                    }
                    break;
            }

            if (fillAmount > 1f)
            {
                LeftArrow.SetActive(false);
                CircleLeft.SetActive(false);
                RightArrow.SetActive(false);
                CircleRight.SetActive(false);
                GetComponentInParent<Gate>().QTE_Completed();
                didFinished = true;
            }
        }

       
    }
}

/*
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
               */

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