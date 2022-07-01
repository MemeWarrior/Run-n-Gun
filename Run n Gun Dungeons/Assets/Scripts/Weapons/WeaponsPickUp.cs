using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPickUp : MonoBehaviour
{
    public GameObject weapon;
    public string weaponName;
    public Collider2D target;
    public bool OnPlayer = false;
    private GameObject canvas;


    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
    }

    void Update(){
        if(OnPlayer && Input.GetKeyDown(KeyCode.E)){
            target.gameObject.transform.GetChild(0).GetComponent<WeaponShooting>().currentweapon = weaponName;
            target.gameObject.transform.GetChild(0).GetComponent<WeaponShooting>().isPick = true;
            target.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            target = other;
            OnPlayer = true;
            canvas.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnPlayer = false;
        canvas.transform.GetChild(2).gameObject.SetActive(false);
    }
}
