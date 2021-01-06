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
    public bool SameArrow = false;
    public bool qte1 = false;
    public bool qte2 = false;

    // Start is called before the first frame update
    void Start()
    {
        CircleDown.GetComponent<Image>().fillAmount = fillAmount;
        CircleLeft.GetComponent<Image>().fillAmount = fillAmount;
        CircleRight.GetComponent<Image>().fillAmount = fillAmount;
        DownArrow.SetActive(true);
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
        CircleDown.SetActive(true);
        CircleLeft.SetActive(true);
        CircleRight.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
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
        }
    }
}

