using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject pistolBullet;
    public GameObject shotgunBullet;
    public GameObject sniperBullet;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject sniper; 
    public float offset;
    public Transform shotPoint;
    public float timeBtwShots = 10f;
    public bool isPick = false;
    public string currentweapon;
    public float shotgunAngle = 7f;

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        Debug.Log(timeBtwShots);
        if (timeBtwShots <= 0)
        {
            if (isPick == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(currentweapon == "Pistol")
                    {
                        SoundManager.PlayPistol();
                        Instantiate(pistolBullet, shotPoint.position, transform.rotation);
                        timeBtwShots = 0.2f;
                    }
                    else if(currentweapon == "Shotgun")
                    {
                        SoundManager.PlayShotgun();
                        //Vector3 rot = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z-45,transform.rotation.w);
                        for(int i = -2; i <= 2; i++)
                        {
                            var spawnBullet = Instantiate(shotgunBullet, shotPoint.position, transform.rotation);
                            spawnBullet.transform.Rotate(0,0,shotgunAngle*i);
                            timeBtwShots = 1f;
                        }
                    }
                    else if(currentweapon == "Sniper")
                    {
                        SoundManager.PlaySniper();
                        Instantiate(sniperBullet, shotPoint.position, transform.rotation);
                        timeBtwShots = 1.8f;
                    }
                }
            } 
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
}
