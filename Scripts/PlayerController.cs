using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour
{
	PhotonView view;

	public float movementSpeed;
	
	public GameObject PlayerCamera;
	public TMP_Text PlayerName;

	private int facing = 1;
	
void Start()
    {
        view = GetComponent<PhotonView>();
		gameObject.tag = "Player";
    }

	void Awake()
	{
		PlayerCamera.SetActive(false);
		view = GetComponent<PhotonView>();
		if(view.IsMine)
		{
			PlayerCamera.SetActive(true);
			PlayerName.text = PhotonNetwork.NickName;
			PlayerName.color =  new Color(0,1,0, 0.5f);;
		}
		else
		{
			PlayerName.text = view.Owner.NickName;
			PlayerName.color =  new Color(1,0,0, 0.5f);;
		}
	}
private void Update()
{
	if(view.IsMine)
	{
		if (Input.GetKeyDown(KeyCode.Escape))
        	{
          		PhotonNetwork.LeaveRoom();
  			PhotonNetwork.LoadLevel("Lobby");	
       	 }
	}
}

    private void FixedUpdate()
    {
	if(view.IsMine)
	{
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			facing = 0;
                transform.position = new Vector2( transform.position.x,  transform.position.y + movementSpeed * Time.deltaTime);
		}
      	if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			facing = 2;
                 transform.position = new Vector2( transform.position.x,  transform.position.y - movementSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			facing = 3;
                transform.position = new Vector2( transform.position.x - movementSpeed * Time.deltaTime,  transform.position.y);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			facing = 1;
                transform.position = new Vector2( transform.position.x + movementSpeed * Time.deltaTime,  transform.position.y);
		}
   	 }
	}
	[PunRPC]
	public void TakeDamage()
	{
		if(view.IsMine)
		{	
			PhotonNetwork.LeaveRoom();
  			PhotonNetwork.LoadLevel("Lobby");	
		}
	}
	public int GetDirection()
	{
		return facing;
	}
}