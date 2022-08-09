using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootingController : MonoBehaviour
{
    PhotonView view;

	public float MissileVelocity;
	public float MissileLifetime;
	public float aimSpread;
	public float missileSize;
	public float reloadTime;
	
	public GameObject Missile;
	public GameObject MissileLauncherTop;
	public GameObject MissileLauncherBottom;
	public GameObject MissileLauncherLeft;
	public GameObject MissileLauncherRight;

	private GameObject TempMissile;
	private bool ready = true;
	
 void Start()
    {
        view = GetComponent<PhotonView>();
		
    }
 
void Update()
    {
    if(view.IsMine)
		{
			if (Input.GetKeyDown(KeyCode.Space) && ready)
        		{
				ready = false;
				StartCoroutine("Reload");			
				switch(this.GetComponent<PlayerController>().GetDirection()) 
				{
  				case 0:
    					TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherTop.transform.position, Quaternion.identity);
    					TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1*aimSpread, aimSpread)*MissileVelocity,MissileVelocity);
					break;
  				case 2:
    					TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherBottom.transform.position, Quaternion.identity);
    					TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1*aimSpread, aimSpread)*MissileVelocity,-1*MissileVelocity);
					break;
  				case 3:
    					TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherLeft.transform.position, Quaternion.identity);
    					TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(-1*MissileVelocity,Random.Range(-1*aimSpread, aimSpread)*MissileVelocity);
					break;
  				case 1:
    					TempMissile = PhotonNetwork.Instantiate(Missile.name, MissileLauncherRight.transform.position, Quaternion.identity);
    					TempMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(MissileVelocity,Random.Range(-1*aimSpread, aimSpread)*MissileVelocity);
					break;
				}
				TempMissile.GetComponent<MissileController>().Init(MissileLifetime, 1f);
				TempMissile.transform.localScale = new Vector3(missileSize,missileSize,1f);
			}
		}
   }
 IEnumerator Reload()
 {
	yield return new WaitForSeconds(reloadTime);
	ready = true;
}
}

