using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rb;

    Vector2 movement;
    
    void Update(){
        MovementInput();

    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    void MovementInput()
    {
        float mx = Input.GetAxisRaw.("Horizontal");
        float my = Input.GetAxisRaw.("Vertical");

        movement = new Vector2(mx.my).normalized;
    }

    
