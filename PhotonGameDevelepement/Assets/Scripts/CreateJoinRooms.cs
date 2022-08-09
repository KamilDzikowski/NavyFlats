using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateJoinRooms : MonoBehaviourPunCallbacks
{
    	public TMP_InputField createInput;
	public TMP_InputField joinInput;
	public TMP_InputField userName;
	public TMP_Dropdown characterType;

	[SerializeField] private GameObject UsernameMenu;
	[SerializeField] private GameObject RoomsMenu;

  	ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

	void Awake()
	{
		RoomsMenu.SetActive(false);
	}

	public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(joinInput.text);
	}

	public void EnterTheGame()
	{
		SetUserName();
		SetCharacterType();
		UsernameMenu.SetActive(false);
		RoomsMenu.SetActive(true);
	}
	private void SetUserName()
	{
		if(userName.text.Length > 0) PhotonNetwork.NickName = userName.text;
	}
	private void SetCharacterType()
	{
		playerProperties["CharacterType"] = characterType.value;
		PhotonNetwork.SetPlayerCustomProperties(playerProperties);
	}
	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel("Game");
	}

	public void Quit()
        {
            Application.Quit();
        }
}
