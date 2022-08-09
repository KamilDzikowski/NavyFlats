using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviour, Photon.Pun.IPunObservable
{
	public PhotonView photonView;

    private TMP_InputField chatInput;
	public TMP_Text SpeechText;
	public GameObject SpeechBubble;
private bool speaking = false;

void Awake()
{
	chatInput =  GameObject.Find("ChatInput").GetComponent<TMP_InputField>();
}

 void Update()
 {
	if(photonView.IsMine)
	{
		if(/*chatInput.isFocused &&*/ speaking == false)
		{
			if(Input.GetKeyDown(KeyCode.Return) && chatInput.text.Length > 0)
			{	
				speaking = true;
				SpeechBubble.SetActive(true);
				photonView.RPC("Speak", Photon.Pun.RpcTarget.AllBuffered, chatInput.text);
				chatInput.text = "";
			}
        	}
    	}
 }
[PunRPC]
private void Speak(string message)
{
	SpeechText.text = message;
	StartCoroutine("RemoveSpeechBubble");			
}

 IEnumerator RemoveSpeechBubble()
 {
	yield return new WaitForSeconds(4f);
	speaking = false;
	SpeechBubble.SetActive(false);     			
 }
 public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
{
	if(stream.IsWriting)
	{	
		stream.SendNext(SpeechBubble.active);
	}else if(stream.IsReading)
	{
		SpeechBubble.SetActive((bool)stream.ReceiveNext());
	}
}
}