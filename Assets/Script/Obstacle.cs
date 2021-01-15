using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public TrackMoving manager;
	public Material M_transparent;
	public bool hit = false;
	// Start is called before the first frame update
	private void Start()
	{
		manager = FindObjectOfType<TrackMoving>();
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
        {
			manager.Hit(this);
			Player currentPlayer = other.GetComponent<Player>();
			if (currentPlayer != null)
			{
				currentPlayer.SetRole(4);
            }
			else
            {
				GameObject myParent = other.transform.parent.gameObject;
				myParent.GetComponentInChildren<Player>().SetRole(4);
			}
			this.gameObject.GetComponent<Renderer>().material = M_transparent;
			hit = true;
            //gameObject.SetActive(false);
            //manager.lastSpawn.RemoveAt(0);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		manager.canMove = true;
	}

}
