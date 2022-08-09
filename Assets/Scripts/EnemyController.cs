using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyController : MonoBehaviour
{
    	PhotonView view;
	public float vel;

    	public float MissileVelocity;
	public float MissileLifetime;
	public float aimSpread;
	public float missileSize;
	public float reloadTime;
	public float moveTime;
	
	public GameObject Missile;
	public GameObject MissileLauncherTop;
	public GameObject MissileLauncherBottom;
	public GameObject MissileLauncherLeft;
	public GameObject MissileLauncherRight;

	private GameObject TempMissile;
	
void Start()
    {
        view = GetComponent<PhotonView>();
		if(view.IsMine) InvokeRepeating("ChangeDirection", 0.5f * moveTime, moveTime);
		if(view.IsMine) InvokeRepeating("ShootAround", 0.5f * reloadTime, reloadTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    	{
		if(view.IsMine)
		{
			GetComponent<Rigidbody2D>().velocity *= -1;
		}
    }

 void ChangeDirection()
 {
	GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f) * vel, Random.Range(-1f, 1f) * vel);     			
 }
void ShootAround()
{
	if(view.IsMine)
	{
		switch(Random.Range(0, 4))
		{
			case 0:
				TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherTop.transform.position, Quaternion.identity);
    				TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1*aimSpread, aimSpread)*MissileVelocity,MissileVelocity);
				break;
			case 1:
				TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherBottom.transform.position, Quaternion.identity);
    				TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1*aimSpread, aimSpread)*MissileVelocity,-1 * MissileVelocity);
				break;
			case 2:
				TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherRight.transform.position, Quaternion.identity);
    				TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(MissileVelocity,Random.Range(-1*aimSpread, aimSpread)*MissileVelocity);
				break;
			case 3:
				TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherLeft.transform.position, Quaternion.identity);
    				TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(MissileVelocity * -1, Random.Range(-1*aimSpread, aimSpread)*MissileVelocity);
				break;			
		}
		TempMissile.GetComponent<MissileController>().Init(MissileLifetime, 1f);
		TempMissile.transform.localScale = new Vector3(missileSize,missileSize,1f);
	}
}
[PunRPC]
public void TakeDamage()
	{
		if(view.IsMine)
		{	
			PhotonNetwork.Destroy(gameObject);
		}
	}
}

