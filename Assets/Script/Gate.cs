using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public float speed = 50f;
    public Transform[] gatePosition = new Transform[3];
    public List<GameObject> gateListClassic = new List<GameObject>();
    public List<GameObject> gateListSourd = new List<GameObject>();
    public Moule[] mouleList = new Moule[3];
    public Material mouleMat;
    public PartyManager myManager;
    public GameObject myQTE;
    private bool qteIsOver = false;
    private bool soundPlayed = false;

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject myGameObject;
            if (i < 2)
            {
                int id = Random.Range(0, gateListClassic.Count);
                myGameObject = Instantiate(gateListClassic[id], gatePosition[i].position, Quaternion.identity, transform);
            }
            else
            {
                int id = Random.Range(0, gateListSourd.Count);
                myGameObject = Instantiate(gateListSourd[id], gatePosition[i].position, Quaternion.identity, transform);
                //myGameObject.GetComponentInChildren<Image>()
            }
            mouleList[i] = myGameObject.GetComponent<Moule>();
        }
        myManager = FindObjectOfType<PartyManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.z < Camera.main.transform.position.z + 10) Destroy(gameObject);

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(!mouleList[2].gameObject.GetComponent<AudioSource>().isPlaying) mouleList[2].gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject myParent = other.transform.parent.gameObject; //je récupère le GameObject body qui contient tous les joints
        if (other.CompareTag("Player")) //si c'est bien un joueur il a un tag player
        {
            if (myParent.GetComponentInChildren<Player>().GetRole() != 4) //si j'ai pas déjà une croix sur la tete
            {
                if (myParent.GetComponentInChildren<Player>().Mute == true) //si je suis muet
                {
                    if (qteIsOver)
                    {
                        myParent.GetComponentInChildren<Player>().SetRole(5); //si QTE a été réussi je mets un tick vert
                        myManager.AddPoint(1);
                    }
                    else //sinon si j'ai pas réussi le QTE
                    {
                        if (!soundPlayed)
                        {
                            myParent.GetComponentInChildren<Player>().SetRole(4); //je mets une croix
                            soundPlayed = true;
                        } //je mets une croix
                    }
                }
                else //sinon si je suis pas muet
                { 
                    if (!soundPlayed) 
                    { 
                        myParent.GetComponentInChildren<Player>().SetRole(5); //je mets un tick vert
                        myManager.AddPoint(1);
                    }
                }
            }
        }

    }

    public void QTE_Completed()
    {
        qteIsOver = true;
    }
}
