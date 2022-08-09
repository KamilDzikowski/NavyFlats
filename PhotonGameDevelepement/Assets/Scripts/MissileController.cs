using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MissileController : MonoBehaviour
{
	PhotonView view;

	private float lifetime = 100f;
	private float damage;

	public void Init(float a, float b)
	{
		lifetime = a;
		damage = b;
	}
	
	void Start()
	{
		view = GetComponent<PhotonView>();
		if(view.IsMine) StartCoroutine("DestroyInTime");
	}
	
	 IEnumerator DestroyInTime()
 	{
		yield return new WaitForSeconds(lifetime);
		PhotonNetwork.Destroy(gameObject);     			
 	}

	private void OnTriggerEnter2D(Collider2D other)
    	{
 		if(view.IsMine)
		{
			if(other.gameObject.tag == "Player")
			{
				other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",Photon.Pun.RpcTarget.AllBuffered);
			}
			else if(other.gameObject.tag == "Enemy")
			{
				other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",Photon.Pun.RpcTarget.AllBuffered);
			}
			PhotonNetwork.Destroy(gameObject);
		}
    }
	/*private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.GetComponent<PhotonView>().name);
		if(other.gameObject.tag == "Player")
		{
			Debug.Log(other.gameObject.GetComponent<PhotonView>().IsMine);
			if(other.gameObject.GetComponent<PhotonView>().IsMine) other.gameObject.GetComponent<PlayerController>().TakeDamage();
			//if(view.IsMine) PhotonNetwork.Destroy(gameObject);
		}
	}*/
}
