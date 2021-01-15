using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TrackMoving : MonoBehaviour
{
    //public GameObject playerPrefab;
    //private GameObject player;
    public GameObject track;
    public GameObject bifurcation;
    public GameObject cross;
    public GameObject[] verticalHorizontal;
    public GameObject jump;
    public GameObject invisible;
    public bool gameOn;
    public int points;
    [NonSerialized]
    public bool canMove = true;

    public bool coll = false; // Y a-t-il eu collision ?
    private int multiplier = 1;
    private bool jumping = false;
    public float speed;
    public List<int> lastSpawn;
    private Vector3 spawnPosition;
    private Vector3 initialPosition;
    private List<GameObject> spawnedObjects;
    private float xOffset;
    float xOffsetInvisible;
    public string direction;
    private float t0;
    private float side;
    private int nbVies = 15;
    public int nbObjSpawned = 0;
    // Start is called before the first frame update

    public PartyManager myPartyManager;

    void Start()
    {
        points = 0;

        //Direction à prendre à la pochaine bifurcation
        direction = "right";

        //Dernier objet instancié
        lastSpawn.Add(0);// 0 : Track; 1 : Bifurcation; 2 : VerticalHorizontal; 3 : Jump
        lastSpawn.Add(0);
        lastSpawn.Add(0);

        //Vitesse d'avancement
        speed = 80f;

        //Position normale de spawn des plateformes
        spawnPosition = new Vector3(0, -10f, 465f);

        //Position de spawn de la plateforme d'origine
        initialPosition = new Vector3(0, -10f, 165f);

        //Instanciation du joueur
        //player = Instantiate(playerPrefab, new Vector3(0, 0f, 70), Quaternion.identity);

        //Liste des objets instanciés
        spawnedObjects = new List<GameObject>();

        //Instanciation de la plateforme d'origine
        GameObject newTrack = Instantiate(track, initialPosition, Quaternion.identity);
        //newTrack.transform.Rotate(new Vector3(-90f, 0 - 0));
        nbObjSpawned += 1;

        //Ajout de la plateforme à la liste des objets
        spawnedObjects.Add(newTrack);
    }

    // Update is called once per frame
    void Update()
    {
        //Vérification de l'état du jeu (On/Off)
        if (gameOn)
        {
            if (myPartyManager.health > 0)
            {
                //Spawn une plateforme si elle est assez proche
                if (spawnedObjects[spawnedObjects.Count-1].transform.position.z <= 165f)
				{
                    //Ajuster la position de la prochaine piste si bifurcation
                    if (lastSpawn[2] == 1 && xOffset > 0)
                    {
                        spawnPosition = new Vector3(-50, -10f, 465f);
                    }
                    else if (lastSpawn[2] == 1 && xOffset < 0)
                    {
                        spawnPosition = new Vector3(50, -10f, 465f);
                    }
                    else
                    {
                        spawnPosition = new Vector3(0, -10f, 465f);
                    }
                    //Incrémentation des points si pas de collision détectée
                    IncrementPoints();

                    GameObject nextTrack = NextTrack();
                    if(nextTrack == null) // Route sans obstacle
					{
                        GameObject newTrack = Instantiate(track, spawnPosition, Quaternion.identity);
                        spawnedObjects.Add(newTrack);
                        //newTrack.transform.Rotate(new Vector3(-90f, 0 - 0));
                        lastSpawn.Add(0);
                        lastSpawn.RemoveAt(0);
                    }
					else if(nextTrack == bifurcation) //Bifurcation
					{
                        //float random .Random();
                        side = UnityEngine.Random.Range(0,2); //Donne toujours le même chiffre
                        switch (side)
                        {
                            case (0):
                                xOffset = -50f;
                                break;

                            case (1):
                                xOffset = 50f;
                                break;
                        }

                        Vector3 spawnLeftOrRight = spawnPosition + new Vector3(xOffset, 0, 100.0f);
                        GameObject barrier = Instantiate(cross, spawnLeftOrRight, Quaternion.identity);
                        GameObject newTrack = Instantiate(bifurcation, spawnPosition, Quaternion.identity);
                        //newTrack.transform.Rotate(new Vector3(-90f, 0 - 0));
                        spawnedObjects.Add(barrier);
                        spawnedObjects.Add(newTrack);
                        lastSpawn.Add(1);
                        lastSpawn.RemoveAt(0);
                    }
					else if (verticalHorizontal.Contains(nextTrack))  //ObstacleVerticalHorizontal
                    {
                        float verOrHor = UnityEngine.Random.Range(0, 2);
                        GameObject newTrack = Instantiate(track, spawnPosition, Quaternion.identity);
                        GameObject obstacle = Instantiate(nextTrack, spawnPosition, Quaternion.identity);
                        //newTrack.transform.Rotate(new Vector3(-90f, 0 - 0));
                        spawnedObjects.Add(newTrack);
                        spawnedObjects.Add(obstacle);
                        float xOffset_VertHor;
                        switch (verOrHor)
						{
                            
                            case (0): //Vertical
                                xOffset_VertHor = UnityEngine.Random.Range(-15f, 15f);
                                //Vector3 spawnLeftOrRight = spawnPosition + new Vector3(xOffset_VertHor, 0, 0);
                                spawnedObjects[spawnedObjects.Count-1].transform.position += new Vector3(xOffset_VertHor, 0, 0);
                                lastSpawn.Add(2);
                                lastSpawn.RemoveAt(0);

                                break;

                            case (1): //Horizontal
                                float yOffset_Hor = 20f;
                                xOffset_VertHor = UnityEngine.Random.Range(0f, 25f); ;
                                spawnedObjects[spawnedObjects.Count - 1].transform.position += new Vector3(15f, yOffset_Hor, 0);
                                spawnedObjects[spawnedObjects.Count - 1].transform.Rotate(0, 0, 90);
                                lastSpawn.Add(4);
                                lastSpawn.RemoveAt(0);
                                break;

                        }
                    }
                    else if (nextTrack == invisible)  //Invisible Obstacle
                    {
                        
                        side = UnityEngine.Random.Range(0, 3); //Donne toujours le même chiffre
                        switch (side)
                        {
                            case (0):
                                xOffsetInvisible = -16f;
                                break;
                            case (1):
                                xOffsetInvisible = 0f;
                                break;
                            case (2):
                                xOffsetInvisible = 16f;
                                break;
                        }

                        Vector3 spawnLeftOrRight = spawnPosition + new Vector3(xOffsetInvisible, 0, 0f);
                        GameObject invisibleObstacle = Instantiate(invisible, spawnLeftOrRight, Quaternion.identity);
                        invisibleObstacle.GetComponent<invisible>().ID = side;
                        GameObject newTrack = Instantiate(track, spawnPosition, Quaternion.identity);
                        //Déplacer uniquement l'obstacle, pas le canvas ici;
                        //newTrack.transform.Rotate(new Vector3(-90f, 0 - 0));
                        spawnedObjects.Add(invisibleObstacle);
                        spawnedObjects.Add(newTrack);
                        lastSpawn.Add(5);
                        lastSpawn.RemoveAt(0);
                    }
                    else //Jump
					{
                        Vector3 spawnHigher = spawnPosition +  new Vector3(0, -2f, 0);
                        GameObject newTrack = Instantiate(track, spawnPosition, Quaternion.identity);
                        GameObject obstacle = Instantiate(nextTrack, spawnHigher, Quaternion.identity);
                        //newTrack.transform.Rotate(new Vector3(-90f, 0 - 0));
                        spawnedObjects.Add(newTrack);
                        spawnedObjects.Add(obstacle);
                        lastSpawn.Add(3);
                        lastSpawn.RemoveAt(0);
                    }
                    nbObjSpawned += 1;
                }

                //Détruire objets et les supprimer de la liste s'ils sortent du champs de vision
                for(int i = 0; i<spawnedObjects.Count; i++)
				{
                    spawnedObjects[i].transform.Translate(Vector3.back * Time.deltaTime*speed);
                    if (spawnedObjects[i].transform.position.z <= -250f)
					{
                        if (spawnedObjects[i].CompareTag("Cross"))
                        {
                            canMove = true;
                        }
                        Destroy(spawnedObjects[i]);
                        spawnedObjects.RemoveAt(i);
                    }
                }
			}
			else
			{
                gameOn = false;
                myPartyManager.GameOver();
                //Spawn ligne d'arrivée puis une fois passée,
                //Ajouter UI de fin de jeu
			}
		}

        SelectDirection();
        Turn();
        //Tilt();
        //Jump();
    }
    //Sélection aléatoire de la prochaine partie de piste
    GameObject NextTrack()
    {
        System.Random random = new System.Random();
        var nextTrackProba = random.NextDouble();
        var colorProba = random.NextDouble();
        GameObject NextTrack;
        if (nextTrackProba < 0.1f)//Track only 20%
        {
            NextTrack = null;
        }
        else if (nextTrackProba >= 0.18f && nextTrackProba < 0.36f)//Jump 20%
        {
            NextTrack = jump;
            /*if (colorProba < 0.25f)
			{
                NextTrack = jump[0];
			}
            else if(colorProba >= 0.25f && colorProba < 0.5f)
			{
                NextTrack = jump[1];
            }
            else if (colorProba >= 0.5f && colorProba < 0.75f)
			{
                NextTrack = jump[2];
            }
			else
			{
                NextTrack = jump[3];
            }*/
        }
        else if (nextTrackProba >= 0.36f && nextTrackProba < 0.72f)//Vertical/Horizontal 40%
        {
            if (colorProba < 0.25f)
            {
                NextTrack = verticalHorizontal[0];
            }
            else if (colorProba >= 0.25f && colorProba < 0.5f)
            {
                NextTrack = verticalHorizontal[1];
            }
            else if (colorProba >= 0.5f && colorProba < 0.75f)
            {
                NextTrack = verticalHorizontal[2];
            }
            else
            {
                NextTrack = verticalHorizontal[3];
            }
        }
        else if (nextTrackProba >= 0.72f && nextTrackProba < 0.82f)//Invisible obstacle
        {
            NextTrack = invisible;
        }
		else//Bifurcation 20%
		{
            NextTrack = bifurcation;
		}
        return NextTrack;
    }

    //Direction à prendre à la prochaine bifurcation
    private void SelectDirection()
    {
        if (Input.GetKeyDown("left") || Input.GetKeyDown(KeyCode.PageUp)) {

            direction = "left";
        }
        else if (Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.PageDown))
        {
            direction = "right";
        }
    }

    /*private void Tilt()
    {
        if (Input.GetKey("left"))
        {
            if(player.transform.position.x >= -10f)
			{
                player.transform.position += new Vector3(-1f, 0, 0);
            }
        }
        else if (Input.GetKey("right"))
        {
            if (player.transform.position.x <= 10f)
            {
                player.transform.position += new Vector3(1f, 0, 0);
            }
        }
        if (Input.GetKeyUp("left"))
        {
            player.transform.position = new Vector3(0, 1, 70);
        }
        else if (Input.GetKeyUp("right"))
        {
            player.transform.position = new Vector3(0, 1, 70);
        }
    }*/

    private void Turn()
	{
        if(lastSpawn[1] == 1)
		{
            int k = 2;
            if(lastSpawn[2] != 0)
			{
                k = 3;
			}

            if (direction == "left") {
                if (spawnedObjects[spawnedObjects.Count - k].transform.position.x > -50f && spawnedObjects[spawnedObjects.Count - k].transform.position.z < 70f && spawnedObjects[spawnedObjects.Count - k].transform.position.z > 20f)
                {
                    for (int i = 0; i < spawnedObjects.Count; i++)
                    {
                        spawnedObjects[i].transform.Translate(Vector3.right * Time.deltaTime * speed);
                    }
                }
            }
            else
            {
                if (spawnedObjects[spawnedObjects.Count - k].transform.position.x < 50f && spawnedObjects[spawnedObjects.Count - k].transform.position.z <70f && spawnedObjects[spawnedObjects.Count - k].transform.position.z > 20f)
                {
                    for (int i = 0; i < spawnedObjects.Count; i++)
                    {
                        spawnedObjects[i].transform.Translate(Vector3.left * Time.deltaTime * speed);
                    }
                }
            }



            /*if (spawnedObjects[spawnedObjects.Count - k].transform.position.z < 95f && spawnedObjects[spawnedObjects.Count - k].transform.position.z > 28f)
			{
                if (direction == "left")
                {
                    for (int i = 0; i < spawnedObjects.Count; i++)
                    {
                        spawnedObjects[i].transform.Translate(Vector3.right * Time.deltaTime * speed);
                    }
                }
                else
                {
                    for (int i = 0; i < spawnedObjects.Count; i++)
                    {
                        spawnedObjects[i].transform.Translate(Vector3.left * Time.deltaTime * speed);
                    }
                }
            }*/
        }
	}

    /*private void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
            jumping = true;
            t0 = Time.time;
            player.transform.position = new Vector3(0, 0, 70);
        }
        if(jumping)
		{
            float multip = Mathf.Sin((Time.time - t0) * speed/2);
            if(multip >= 0)
			{
                player.transform.Translate(Vector3.up * Time.deltaTime * speed/10);
            }
			else
			{
                player.transform.Translate(Vector3.down * Time.deltaTime * speed / 10);
                if(player.transform.position.y <= 10)
				{
                    player.transform.position = new Vector3(0, 1, 70);
                    jumping = false;
                }
            }
        }
	}*/

    private void IncrementPoints()
	{
        myPartyManager.ResetPlayerUI();
        //Incrémentation des points
        if (!coll)
        {
            points += multiplier;
            myPartyManager.AddPoint(multiplier);
            multiplier += 1;
            speed += 10;
        }
        else
        {
            coll = false;
        }
    }


    public void Hit(Obstacle obstacle)
    {
        if (!obstacle.hit)
        {
            myPartyManager.DelLife();
        }
        coll = true;
        speed = 80;
        multiplier = 1;

        if(obstacle.gameObject.CompareTag("Cross"))
		{
            if (canMove)
            {
                foreach (GameObject obj in spawnedObjects)
                {
                    switch(side)
                    {
                        case(0):
                            obj.transform.position += new Vector3(-100, 0, 0);
                            break;
                        case (1):
                            obj.transform.position += new Vector3(100, 0, 0);
                            break;
                        }

                }
                canMove = false;
            }
        }
        //Destroy(obstacle);
        //lastSpawn.RemoveAt(0);
    }
}
