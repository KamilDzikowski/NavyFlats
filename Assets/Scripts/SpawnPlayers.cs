using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SpawnPlayers : MonoBehaviour
{
    	public GameObject Sniper;
	public GameObject Stormtrooper;
	public GameObject Tank;
	private GameObject player;

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	
	public TMP_Text pingText;

	private void Start()
	{
		Vector2 randomPosition = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
		switch(PhotonNetwork.LocalPlayer.CustomProperties["CharacterType"])
		{
			case 0:
				player = PhotonNetwork.Instantiate(Stormtrooper.name, randomPosition, Quaternion.identity);
				break;
			case 1:
				player = PhotonNetwork.Instantiate(Sniper.name, randomPosition, Quaternion.identity);
				break;
			case 2:
				player = PhotonNetwork.Instantiate(Tank.name, randomPosition, Quaternion.identity);
				break;
		}
	}
	void Update()
	{
		pingText.text = "Ping: " + PhotonNetwork.GetPing();
	}
}
